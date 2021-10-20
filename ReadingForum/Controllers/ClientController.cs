using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace ReadingForum
{
    public class ClientController
    {
        public CommonUser User;
        public LandForm LandForm;
        public RegForm RegForm;
        public MainForm MainForm;
        public ClientController(LandForm form1,RegForm form2,MainForm form3)
        {
            //双向绑定
            LandForm = form1;
            RegForm = form2;
            MainForm = form3;
            LandForm.ClientController = this;
            RegForm.ClientController = this;
            MainForm.ClientController = this;
        }
        private Socket ClientSocket;
        private Thread ClientThread;//接收信息的线程
        private string State;
        public async Task<bool> Init(string IP, int Port)
        {
            IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Parse(IP), Port);
            ClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            return await Task.Run(() =>
            {
                try
                {
                    ClientSocket.Connect(iPEndPoint);
                    ClientThread = new Thread(RecvMsg);
                    ClientThread.IsBackground = true;
                    ClientThread.Start();
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
            });
        }
        private void RecvMsg()
        {
            while(true)
            {
                try
                {
                    byte[] RecvByte = new byte[1024 * 1024];
                    int len = ClientSocket.Receive(RecvByte);
                    if (len <= 0) throw new Exception();
                    string msg = Encoding.UTF8.GetString(RecvByte, 0, len);
                    Console.WriteLine(msg);
                    string[] ss = msg.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                    if (msg.StartsWith("[REG]")||msg.StartsWith("[LOG]"))
                    {
                        if (ss[1].Contains("OK")) State = "Succ";
                        else State = "Fail";
                    }
                }
                catch(Exception)
                {
                    break;
                }
            }
        }
        public async Task<bool> Regist(string[] args)
        {
            State = string.Empty;
            ClientSocket.Send(Encoding.UTF8.GetBytes("[REG]\n"+args[0]+"\n"+args[1]+"\n"+args[2]));
            return await Task.Run(() =>
            {
                while (string.IsNullOrEmpty(State)) { };
                return(State == "Succ");
            });
        }
        public async Task<bool> Login(string[] args)
        {
            State = string.Empty;
            ClientSocket.Send(Encoding.UTF8.GetBytes("[LOG]\n"+args[0]+"\n"+args[1]));
            return await Task.Run(() =>
            {
                while (string.IsNullOrEmpty(State)) { };
                bool flag=(State == "Succ");
                if (flag) User = (CommonUser)UserFactory.CreateUser(args);
                return flag;
            });
        }
    }
}
