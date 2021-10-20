using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadingForumServer.Classes
{
    public class User : INotifyPropertyChanged
    {
        public int Id;
        public String NickName;
        public String Account;
        public String Password;
        public bool isForbid;
        public string Ip;

        public event PropertyChangedEventHandler PropertyChanged;

        public User(int id, String nickname, string account, string password, bool isforbid,string ip)
        {
            Id = id;
            NickName = nickname;
            Account = account;
            Password = password;
            isForbid = isforbid;
            Ip = ip;
        }
        protected void OnPropertyChanged(string PropertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }

        public void ChangePsw(string Newpassword) { this.Password = Newpassword; }

    }
}
