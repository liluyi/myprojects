using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServerBase.database
{
    class User
    {
        public string name;
        public string passwd;
        public int groupId;

        public User(string name, string passwd, int groupId)
        {
            this.name = name;
            this.passwd = passwd;
            this.groupId = groupId;
        }
    }
}
