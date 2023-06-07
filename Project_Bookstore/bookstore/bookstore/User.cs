using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;

namespace bookstore
{
    internal class User
    {
        public string Name { get; set; }
        public string Phone_number { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public User()
        {
        }

        public User(string name, string phone_number, string email, string password)
        {
            this.Name = name;
            this.Phone_number = phone_number;
            this.Email = email;
            this.Password = password;
        }

        public virtual void Show()
        {
            Console.WriteLine($"\nName: {Name}\nPhone number: {Phone_number}\nEmail: {Email}");
        }
    }
}
