using System.Text.RegularExpressions;

namespace bookstore
{
    internal class Place
    {
        public string Region { get; set; }
        public string City { get; set; }
        public int Department { get; set; }

        public Place() { }
        public Place(string region, string city, int department)
        {
            this.Region = region;
            this.City = city;
            this.Department = department;
        }

        public Place EnterPlace()
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
                    Place place = new Place(Region, City, Department);
                    return place;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            } while (!isValid);

            return null;
        }

        public void Show()
        {
            Console.WriteLine($"Region: {Region}\nCity: {City}\nDepartment: {Department}");
        }
    }
}
