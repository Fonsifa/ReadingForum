using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadingForumServer.Classes
{
    class Manager
    {
        public int Id;
        public String Account;
        public String Password;

        Manager(int id,String account,String password)
        {
            Id = id;
            Account = account;
            Password = password;
        }
    }
}
