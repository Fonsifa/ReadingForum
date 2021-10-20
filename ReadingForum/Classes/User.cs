using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace ReadingForum
{
    public abstract class User :INotifyPropertyChanged
    {
        private int _id;
        public int Id 
        {
            get { return _id; }
            set { _id = value;OnPropertyChanged("Id"); }
        }
        private string _account;
        public string Account
        {
            get { return _account; }
            set { _account = value;OnPropertyChanged("Account"); }
        }
        protected string Password;
        //EventHandler
        public event PropertyChangedEventHandler PropertyChanged;

        //构造函数
        public User() { }
        public User(string Account,string Password)
        {
            this.Account = Account;
            this.Password = Password;
        }
        //属性值改变
        protected void OnPropertyChanged(string PropertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }

        public void ChangePsw(string Newpassword) { this.Password = Newpassword; }

    }
}
