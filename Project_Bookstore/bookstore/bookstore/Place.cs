using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace bookstore
{
    public class Place
    {
        public string Region { get; set; }
        public string City { get; set; }
        public int Department { get; set; }

        public Place(string region, string city, int department)
        {
            this.Region = region;
            this.City = city;
            this.Department = department;
        }

        public static void EnterPlace(List<Place> place)
        {
            bool isValid = false;
            Regex regex = null;
            do
            {
                try
                {
                    Console.Write("Enter a region: ");
                    string region = Console.ReadLine();
                    regex = new Regex(@"^[A-Z][a-zA-Z\-]*[a-z]+$");
                    isValid = regex.IsMatch(region);

                    if (!isValid)
                    {
                        throw new Exception("You enter incorrect region. Enter again");
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
                    Console.Write("Enter a city: ");
                    string city = Console.ReadLine();
                    regex = new Regex(@"^[A-Z][a-zA-Z\-]*[a-z]+$");
                    isValid = regex.IsMatch(city);

                    if (!isValid)
                    {
                        throw new Exception("You enter incorrect city. Enter again");
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
                    Console.Write("Enter a department: ");
                    int department = int.Parse(Console.ReadLine());
                    
                    if(department <= 0)
                    {
                        throw new Exception("You enter incorrect department. Enter again");
                    }

                    isValid = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            } while (!isValid);
        }
    }
}
