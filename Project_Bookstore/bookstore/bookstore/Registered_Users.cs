using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

namespace bookstore
{
    internal class Registered_Users : User
    {
        public List<User> User { get; set; }

        public Registered_Users()
        {
            this.User = new List<User>();
        }
        public Registered_Users(string name, string phone_number, string email, string password) : base(name, phone_number, 
            email, password)
        {
            this.User = new List<User>();
        }

        public void Registration(User user)
        {
            Regex regex = null;
            bool isValid = false;
            do
            {
                try
                {
                    Console.Write("\nEnter your name: ");
                    Name = Console.ReadLine();
                    regex = new Regex(@"^[A-Z][a-z]{2,}$");
                    isValid = regex.IsMatch(Name);

                    if (!isValid)
                    {
                        throw new Exception("You enter incorrect name. Enter again:");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            } while (!isValid);

            isValid = false;
            do
            {
                try
                {
                    Console.Write("Enter your phone number: ");
                    Phone_number = Console.ReadLine();

                    if (Phone_number.StartsWith("0"))
                    {
                        Phone_number = "+38" + Phone_number;
                    }

                    if (User.Any(user => user.Phone_number.Equals(Phone_number, StringComparison.OrdinalIgnoreCase)))
                    {
                        throw new Exception("This phone number already registered");
                    }

                    regex = new Regex(@"^(?:\+380|0)\d{9}$");
                    isValid = regex.IsMatch(Phone_number);
                    if (!isValid)
                    {
                        throw new Exception("You enter incorrect phone number. Enter again:");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            } while (!isValid);

            isValid = false;
            do
            {
                try
                {
                    Console.Write("Enter your Email: ");
                    Email = Console.ReadLine();

                    if (User.Any(user => user.Email.Equals(Email, StringComparison.OrdinalIgnoreCase)))
                    {
                        throw new Exception("This email already registered");
                    }

                    regex = new Regex(@"^.+@+[a-z]{2,}\.[a-z]{2,}(\.[a-z]{2,})?$");
                    isValid = regex.IsMatch(Email);
                    if (!isValid)
                    {
                        throw new Exception("You enter incorrect Email. Enter again:");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            } while (!isValid);

            isValid = false;
            do
            {
                try
                {
                    Console.Write("Enter your password: ");
                    Password = Console.ReadLine();
                    if (Password.Length < 6)
                    {
                        throw new Exception("Password must have least 6 symbols. Enter again:");
                    }
                    isValid = true;
                    user = new User(Name, Phone_number, Email, Password);
                    User.Add(user);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            } while (!isValid);
        }

        public void EnterAccount()
        {
            bool isValid = false;
            User logged = null;
            do
            {
                try
                {
                    Console.Write("\nEnter your Email: ");
                    string email = Console.ReadLine();

                    logged = User.FirstOrDefault(u => u.Email == email);

                    if (logged == null)
                    {
                        throw new Exception("This email don't registered");
                    }

                    isValid = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            } while (!isValid);

            isValid = false;
            do
            {
                try
                {
                    Console.Write("Enter your password: ");
                    string password = Console.ReadLine();

                    if (logged.Password != password)
                    {
                        throw new Exception("This password don't match");
                    }

                    Console.WriteLine("\nNow you can buy something");
                    isValid = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            } while (!isValid);
        }

        public override void Show()
        {
            foreach (User u in User)
            {
                u.Show();
                Console.WriteLine();
            }
        }
    }
}
