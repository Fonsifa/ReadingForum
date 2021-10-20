using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadingForum
{
    public class Manager:User
    {
        public bool IsManager { get; }
        public Manager(string Account,string Password)
        {
            this.IsManager = true;
            this.Account = Account;
            this.Password = Password;
        }
    }
}
