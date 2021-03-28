using System;

namespace Model
{
    public class User
    {
        public String Username { get; set; }
        public String Password { get; set; }
        public String Name { get; set; }
        public String Surname { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public String Jmbg { get; set; }
        public String Phone { get; set; }
        public String Address { get; set; }
        public String Email { get; set; }
        public Gender Gender { get; set; }

    }
}