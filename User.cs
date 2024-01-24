using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindMemo
{
    internal class User
    {
        private static User _user;

        public string? username { get; set; } = null;

        private User() { }

        public static User getInstance()
        {
            if (_user == null)
            {
                _user = new User();
            }
            return _user;
        }
    }
}
