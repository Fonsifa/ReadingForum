using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using MySql.Data.MySqlClient;

namespace WebReading
{
    /// <summary>
    /// ReadingWebService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class ReadingWebService : WebService
    {
        private static MySqlConnection GetConnection()
        {
            string ConnectString = "server=localhost;User Id=root;password=root;Database=reading";
            MySqlConnection conn = new MySqlConnection(ConnectString);
            return conn;
        }
        private string Read(string path)
        {
            string Content = string.Empty;
            StreamReader sr = new StreamReader(path, Encoding.UTF8);
            String line;
            while ((line = sr.ReadLine()) != null)
            {
                Content += line.Trim() + "\r\n";
            }
            return Content;
        }
        [WebMethod(Description ="获取某一章节的内容")]
        public string GetContent(string BookName,int Chapter)
        {
            string path = string.Empty;
            string content = string.Empty;
            using (MySqlConnection conn=GetConnection())
            {
                conn.Open();
                using (MySqlCommand MyCmd=new MySqlCommand($"select path from book where bookname='{BookName}'",conn))
                {
                    MySqlDataReader reader = MyCmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        reader.Read();
                        path = reader.GetString(0);
                        path = Path.Combine(path, $"{Chapter}.txt");
                        content = Read(path);
                    }
                    reader.Close();
                }
            }
            return content;
        }
        [WebMethod(Description ="获取阅读进度")]
        public int GetProcess(string BookName,int UserId)
        {
            int ch = 0;
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                using (MySqlCommand MyCmd = new MySqlCommand($"select chapter from process,book where book.bookname='{BookName}' and " +
                    $"book.idBook=process.idBook and process.idUser={UserId}", conn))
                {
                    MySqlDataReader reader = MyCmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        reader.Read();
                        ch = reader.GetInt32(0);
                    }
                    reader.Close();
                }
            }
            return ch;
        }
        [WebMethod(Description = "获取书库")]
        public List<string> GetBooks()
        {
            List<string> books = new List<string>();
            using (MySqlConnection conn=GetConnection())
            {
                conn.Open();
                using (MySqlCommand MyCmd=new MySqlCommand("select bookname from book",conn))
                {
                    MySqlDataReader reader = MyCmd.ExecuteReader();
                    while (reader.Read())
                    {
                        if(reader.HasRows)
                        books.Add(reader.GetString(0));
                    }
                    reader.Close();
                }
            }
            return books;
        }
    }
}
