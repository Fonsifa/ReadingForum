using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadingForum
{
    public class UserFactory
    {
        public  static User CreateUser(string[] args)
        {
            int len = args.Length;
            if (len == 3)
            {
                return new CommonUser(args[0], args[1], args[2]);
            }
            else if (len == 2)
            {
                return new Manager(args[0], args[1]);
            }
            else return null;
        }
    }
}
