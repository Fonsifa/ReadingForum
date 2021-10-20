using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadingForum
{
    public class CommonUser:User
    {
        private string _nickname;
        public string Nickname
        {
            get { return _nickname; }
            set { _nickname = value;OnPropertyChanged("Nickname"); }
        }
        private bool Isforbidden;
        public CommonUser() { }
        public CommonUser(string Nickname,string Account,string Password)
        {
            this.Account = Account;
            this.Password = Password;
            this.Nickname = Nickname;
        }
        public string GetNickname() { return this.Nickname; }
        public bool GetIsForbidden() { return this.Isforbidden; }
        public void UnForbidden(bool flag) { this.Isforbidden = flag; }
    }
}
