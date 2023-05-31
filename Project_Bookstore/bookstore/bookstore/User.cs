using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;

namespace bookstore
{
    public class User
    {
        private string name;
        private string phone_number;
        private string email;
        private string password;

        public string Name { get { return name; } set { name = value; } }
        public string Phone_number { get { return phone_number; } set { phone_number = value; } }
        public string Email { get { return email; } set { email = value; } }
        public string Password { get { return password; } set { password = value; } }

        public User()
        {
            this.name = name;
            this.phone_number = phone_number;
            this.email = email;
            this.password = password;
        }

        public User(string name, string phone_number, string email, string password)
        {
            this.name = name;
            this.phone_number = phone_number;
            this.email = email;
            this.password = password;
        }

        public void Registration(List<User> users)
        {
            Regex regex = null;
            bool isValid = false;
            do
            {
                try
                {
                    Console.Write("\nEnter your name: ");
                    name = Console.ReadLine();
                    regex = new Regex(@"^[A-Z][a-z]{2,}$");
                    isValid = regex.IsMatch(name);

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
                    phone_number = Console.ReadLine();

                    if (phone_number.StartsWith("0"))
                    {
                        phone_number = "+38" + phone_number;
                    }

                    if (users.Any(user => user.Phone_number.Equals(phone_number, StringComparison.OrdinalIgnoreCase)))
                    {
                        throw new Exception("This phone number already registered");
                    }

                    regex = new Regex(@"^(?:\+380|0)\d{9}$");
                    isValid = regex.IsMatch(phone_number);
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
                    email = Console.ReadLine();

                    if (users.Any(user => user.Email.Equals(email, StringComparison.OrdinalIgnoreCase)))
                    {
                        throw new Exception("This email already registered");
                    }

                    regex = new Regex(@"^.+@+[a-z]{2,}\.[a-z]{2,}(\.[a-z]{2,})?$");
                    isValid = regex.IsMatch(email);
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
                    password = Console.ReadLine();
                    if (password.Length < 6)
                    {
                        throw new Exception("Password must have least 6 symbols. Enter again:");
                    }
                    isValid = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            } while (!isValid);
        }

        public static void EnterAccount(List<User> users)
        {
            bool isValid = false;
            User logged = null;
            do
            {
                try
                {
                    Console.Write("\nEnter your Email: ");
                    string email = Console.ReadLine();

                    logged = users.FirstOrDefault(u => u.Email == email);

                    if (logged == null)
                    {
                        throw new Exception("This email don't registered");
                    }

                    isValid = true;
                }
                catch(Exception ex)
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

        public void Show()
        {
            Console.WriteLine($"\nName: {Name}\nPhone number: {Phone_number}\nEmail: {Email}");
        }
    }
}
