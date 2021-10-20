using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using System.IO;
using ReadingForumServer.Classes;
using MySql.Data.MySqlClient;

namespace ReadingForumServer.Controllers
{
    public class ServerService
    {
        public User uer;
        public Form2 Form2;

        public ServerService(Form2 form2)
        {
            Form2 = form2;
            Form2.ServerService = this;
        }


        public Socket Listener { set; get; }
        public List<int> Forbidden = new List<int>();//禁言用户列表
        private Dictionary<string, Socket> SocketsConnected = new Dictionary<string, Socket>();//连接的客户端
        private Dictionary<string, User> UserOnline = new Dictionary<string, User>();//ip与用户的映射
        public string Msg = null;
        private Thread ListenThread;
        public int listener = 0;

        public bool Start(string limit, string ip)
        {
            string[] ss = ip.Split(new char[] { '\r', ':' }, StringSplitOptions.RemoveEmptyEntries);
            IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Parse(ss[0]), Int32.Parse(ss[1]));
            try
            {
                Listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                Listener.Bind(iPEndPoint);
                Listener.Listen(Int32.Parse(limit));
                ListenThread = new Thread(ListenConnection);
                ListenThread.IsBackground = true;
                ListenThread.Start();
                Msg += (DateTime.Now.ToString() + "\n服务器打开成功\n");
                return true;
            }
            catch (Exception)
            {
                Msg += ("端口被占用，无法打开服务器\n");
                Console.WriteLine("端口被占用，无法打开服务器");
                return false;
            }
        }
        public void ListenConnection()
        {
            Socket connect = null;
            while (true)
            {
                try
                {
                    connect = Listener.Accept();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    break;
                }
                string remote = connect.RemoteEndPoint.ToString();
                if (!SocketsConnected.ContainsKey(remote))
                    SocketsConnected.Add(remote, connect);
                Msg += (DateTime.Now.ToString() + "\n" + remote + "连接到服务器" + "\r\n");
                Thread Recv = new Thread(RecvMsg);
                Recv.IsBackground = true;
                Recv.Start(remote);
                Form2.Invoke(new EventHandler(delegate

                {
                    listener = listener + 1;
                    Form2.label3.Text = listener.ToString();

                }));
            }
        }

        private async void RecvMsg(object remote)
        {
            string Remote = remote as string;
            Socket connect = SocketsConnected[Remote];
            while (true)
            {
                byte[] Recv = new byte[1024 * 1024];
                try
                {
                    int len = connect.Receive(Recv);
                    if (len <= 0) throw new Exception();
                    string RecvStr = Encoding.UTF8.GetString(Recv, 0, len);
                    Console.WriteLine(RecvStr);
                    string[] ss = RecvStr.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                    if (RecvStr.StartsWith("[REG]"))//判断是否是客户端发送的注册消息
                    {
                        if (await Regist(ss[1].Trim(), ss[2].Trim(), ss[3].Trim()))
                            connect.Send(Encoding.UTF8.GetBytes("[REG]\nOK"));
                        else connect.Send(Encoding.UTF8.GetBytes("[REG]\nFail"));
                    }
                    else if (RecvStr.StartsWith("[LOG]"))
                    {
                        if (await Login(ss[1].Trim(), ss[2].Trim()))
                        {
                            connect.Send(Encoding.UTF8.GetBytes("[LOG]\nOK"));
                            User user = new User(GetUserId(ss[1].Trim()), getNickName(ss[1].Trim()), ss[1].Trim(), ss[2].Trim(), getIsForbid(ss[1].Trim()), Remote);
                            UserOnline.Add(Remote, user);
                        }
                        else connect.Send(Encoding.UTF8.GetBytes("[LOG]\nFail"));
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("连接中断");
                    Msg += (DateTime.Now.ToString() + "\n" + connect.RemoteEndPoint.ToString() + "断开连接\r\n");
                    if (SocketsConnected.ContainsKey(connect.RemoteEndPoint.ToString()))
                        SocketsConnected.Remove(connect.RemoteEndPoint.ToString());
                    if (UserOnline.ContainsKey(connect.RemoteEndPoint.ToString()))
                        UserOnline.Remove(connect.RemoteEndPoint.ToString());
                    connect.Close();
                    Form2.Invoke(new EventHandler(delegate

                    {
                        listener = listener - 1;
                        Form2.label3.Text = listener.ToString();

                    }));
                    break;
                }
            }
        }
        /// <summary>
        /// 连接数据库
        /// </summary>
        /// <returns></returns>
        /// 

        private static MySqlConnection GetConnection()
        {
            string ConnectString = "server=localhost;User Id=root;password=root;Database=reading";
            MySqlConnection conn = new MySqlConnection(ConnectString);
            return conn;
        }

        public async Task<bool> Regist(string sickname, string account, string password)
        {
            int flag = 0;
            return await Task.Run(() =>
            {
                using (MySqlConnection conn = GetConnection())
                {
                    conn.Open();
                    using (MySqlCommand UserInsert = new MySqlCommand($"insert into user(nickname,account,password,isforbid)" +
                        $"values('{sickname}','{account}','{password}',{false})", conn))
                    {
                        try
                        {
                            flag = UserInsert.ExecuteNonQuery();
                        }
                        catch (Exception)
                        { }
                    }
                }
                return flag > 0;
            });
        }

        public async Task<bool> Login(string account, string password)
        {
            bool flag = false;
            return await Task.Run(() =>
            {
                using (MySqlConnection conn = GetConnection())
                {
                    conn.Open();
                    using (MySqlCommand MyCmd = new MySqlCommand($"select password from user where account=" +
                            $"'{account}'", conn))
                    {
                        MySqlDataReader reader = MyCmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            reader.Read();
                            if (password == reader.GetString(0)) flag = true;
                        }
                        reader.Close();
                    }
                }
                return flag;
            });
        }
        public int GetUserId(string account)
        {
            int id = 0;
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                using (MySqlCommand MyCmd = new MySqlCommand($"select id from user where account='{account}'", conn))
                {
                    using (MySqlDataReader reader = MyCmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            id = reader.GetInt32(0);
                        }
                    }
                }
            }
            return id;
        }

        public bool getIsForbid(string account)
        {
            bool forbid = false;
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                using (MySqlCommand MyCmd = new MySqlCommand($"select isforbid from user where account='{account}'", conn))
                {
                    using (MySqlDataReader reader = MyCmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            forbid = reader.GetBoolean(0);
                        }
                    }
                }
            }
            return forbid;
        }

        public string getNickName(string account)
        {
            string nickname = "";
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                using (MySqlCommand MyCmd = new MySqlCommand($"select nickname from user where account='{account}'", conn))
                {
                    using (MySqlDataReader reader = MyCmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            nickname = reader.GetString(0);
                        }
                    }
                }
            }
            return nickname;
        }
    }
}
