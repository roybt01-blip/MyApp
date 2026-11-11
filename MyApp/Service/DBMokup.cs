using MyApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Service
{
    public class DBMokup
    {
        private List<User> _users = new List<User>();
        public DBMokup()
        {
            _users.Add(new User { UserEmail = "admin@mail.com", UserPassword = "admin" });
            _users.Add(new User { UserEmail = "user1@mail.com", UserPassword = "pass1" });
            _users.Add(new User { UserEmail = "user2@mail.com", UserPassword = "pass2" });
        }
        public bool isExist(string uEmail, string uPass)
        {
            return _users.Any(u => u.UserEmail == uEmail && u.UserPassword == uPass);
        }
    }
}
