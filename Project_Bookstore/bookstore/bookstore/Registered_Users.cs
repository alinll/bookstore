using System.Formats.Asn1;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace bookstore
{
    internal class Registered_Users 
    {
        public List<User> Users { get; set; }

        public Registered_Users()
        {
            this.Users = new List<User>();
        }
        public Registered_Users(string name, string phone_number, string email, string password) 
        {
            this.Users = new List<User>();
        }

        public void Registration(User user)
        {
            string jsonReader = File.ReadAllText("users.json");
            List<User>? userJson;
            if (!string.IsNullOrEmpty(jsonReader))
            {
                userJson = JsonSerializer.Deserialize<List<User>>(jsonReader);

                foreach (User u in userJson)
                {
                    Users.Add(u);
                }
            }
            else
            {
                userJson = new List<User>();
            }

            Regex regex = null;
            bool isValid = false;
            do
            {
                try
                {
                    Console.Write("\nEnter your name: ");
                    user.Name = Console.ReadLine();
                    regex = new Regex(@"^[A-Z][a-z]{2,}$");
                    isValid = regex.IsMatch(user.Name);

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
                    user.Phone_number = Console.ReadLine();

                    if (user.Phone_number.StartsWith("0"))
                    {
                        user.Phone_number = "+38" + user.Phone_number;
                    }

                    var existingPhone_number = userJson.FirstOrDefault(u => u.Phone_number.Equals(user.Phone_number, 
                        StringComparison.OrdinalIgnoreCase));
                    if (existingPhone_number != null)
                    {
                        Console.WriteLine("This phone number already registered");
                        return;
                    }

                    regex = new Regex(@"^(?:\+380|0)\d{9}$");
                    isValid = regex.IsMatch(user.Phone_number);
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
                    user.Email = Console.ReadLine();


                    regex = new Regex(@"^.+@+[a-z]{2,}\.[a-z]{2,}(\.[a-z]{2,})?$");
                    isValid = regex.IsMatch(user.Email);
                    if (!isValid)
                    {
                        throw new Exception("You enter incorrect Email. Enter again:");
                    }


                    var existingEmail = userJson.FirstOrDefault(u => u.Email.Equals(user.Email,
                        StringComparison.OrdinalIgnoreCase));
                    if (existingEmail != null)
                    {
                        Console.WriteLine("This email already registered");
                        return;
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
                    user.Password = Console.ReadLine();
                    if (user.Password.Length < 6)
                    {
                        throw new Exception("Password must have least 6 symbols. Enter again:");
                    }
                    isValid = true;
                    user = new User(user.Name, user.Phone_number, user.Email, user.Password);
                    userJson.Add(user);
                    File.WriteAllText("users.json", JsonSerializer.Serialize(userJson, 
                        new JsonSerializerOptions() { WriteIndented = true }));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            } while (!isValid);
        }

        public bool EnterAccount(User user)
        {
            try
            {
                string jsonReader = File.ReadAllText("users.json");
                List<User>? userJson;
                if (!string.IsNullOrEmpty(jsonReader))
                {
                    userJson = JsonSerializer.Deserialize<List<User>>(jsonReader);

                    bool isValid = false;
                    User logged = null;
                    try
                    {
                        do
                        {
                            Console.Write("\nEnter your Email: ");
                            string email = Console.ReadLine();

                            logged = userJson.FirstOrDefault(u => u.Email == email);

                            if (logged == null)
                            {
                                throw new Exception("This email doesn't registered. To buy something you must be registered");
                            }

                            isValid = true;
                        } while (!isValid);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        return false;
                    }

                    isValid = false;
                    do
                    {
                        try
                        {
                            Console.Write("Enter your password: ");
                            string password = Console.ReadLine();

                            if (logged.Password != password)
                            {
                                throw new Exception("This password doesn't match");
                            }

                            Console.WriteLine("\nNow you can buy something");
                            isValid = true;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            return false;
                        }
                    } while (!isValid);

                    return true;
                }
                else
                {
                    throw new Exception("You don't registered");
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public void Show()
        {
            string jsonReader = File.ReadAllText("users.json");
            List<User>? userJson;
            if (!string.IsNullOrEmpty(jsonReader))
            {
                userJson = JsonSerializer.Deserialize<List<User>>(jsonReader);

                foreach (User u in userJson)
                {
                    u.Show();
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine();
            }
        }
    }
}
