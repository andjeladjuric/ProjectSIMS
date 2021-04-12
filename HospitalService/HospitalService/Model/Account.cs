using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public enum UserType
    {
        Doctor,
        Patient,
        Secretary,
        Manager
    }

    class Account
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public UserType User { get; set; }


        public Account(string u, string  p, UserType ut) {
            this.Username = u;
            this.Password = p;
            this.User = ut;
        }

    }
}
