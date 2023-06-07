using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace bookstore
{
    internal class Place
    {
        public string Region { get; set; }
        public string City { get; set; }
        public int Department { get; set; }
        public List<Place> Places { get; set; }

        public Place() {
            this.Places = new List<Place>();

        }
        public Place(string region, string city, int department)
        {
            this.Region = region;
            this.City = city;
            this.Department = department;
        }

        public void EnterPlace(Place place)
        {
            bool isValid = false;
            Regex regex = null;
            do
            {
                try
                {
                    Console.Write("Enter a region: ");
                    Region = Console.ReadLine();
                    regex = new Regex(@"^[A-Z][a-zA-Z\-]*[a-z]+$");
                    isValid = regex.IsMatch(Region);

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
                    City = Console.ReadLine();
                    regex = new Regex(@"^[A-Z][a-zA-Z\-]*[a-z]+$");
                    isValid = regex.IsMatch(City);

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
                    Department = int.Parse(Console.ReadLine());
                    
                    if(Department <= 0)
                    {
                        throw new Exception("You enter incorrect department. Enter again");
                    }

                    isValid = true;
                    place = new Place(Region, City, Department);
                    Places.Add(place);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            } while (!isValid);
        }

        public void Show()
        {
            foreach (Place p in Places)
            {
                Console.WriteLine($"Region: {p.Region}\nCity: {p.City}\nDepartment: {p.Department}");
            }
        }
    }
}
