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

        public void Registration(List<User> users)
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

                    if (users.Any(user => user.Phone_number.Equals(Phone_number, StringComparison.OrdinalIgnoreCase)))
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

                    if (users.Any(user => user.Email.Equals(Email, StringComparison.OrdinalIgnoreCase)))
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
