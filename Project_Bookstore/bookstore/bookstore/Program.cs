namespace bookstore
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Storage storage = new Storage();

            List<Book> books = new List<Book>
            {
                new Book("IM-00114320", "Pet Sematary", 326, "Stephen", "King", "Fiction", 7),
                new Book("IM-00001851", "11/22/63", 483, "Stephen", "King", "Fiction", 9),
                new Book("IM-00001588", "Lord of the Flies", 281, "William", "Golding", "Fiction", 1),
                new Book("IM-00002141", "The Shining", 399, "Stephen", "King", "Fiction", 8),
                new Book("IM-00001730", "Complete Collection of Prose Works. Volume 1", 476, "H. P.", "Lovecraft",
                "Fiction", 7),
                new Book("IM-00004889", "The Art of War", 224, "Sun", "Tzu", "Non-fiction", 17)
            };

            foreach(Book b in books)
            {
                storage.AddBook(b);
            }

            Book newBook = new Book("IL-00014572", "Murder on the Orient Express", 266, "Agatha", "Christie", "Fiction", 7);
            storage.AddBook(newBook);

            newBook = new Book("IM-00114320", "Pet Sematary", 326, "Stephen", "King", "Fiction", 10);
            storage.AddBook(newBook);

            Registered_Users users = new Registered_Users();
            User user = new User();

            Shopping_Cart shopping_cart = new Shopping_Cart();
            Place places = new Place();

            void Show()
            {
                Console.WriteLine("\nChoose action:\n1 - all books\n2 - registration\n3 - shopping cart\n4 - buy books\n0 - exit");
            }

            Show();
            int function;
            try
            {
                do
                {
                    function = int.Parse(Console.ReadLine());
                    switch (function)
                    {
                        case 1:
                            storage.ChoiceStorage();
                            Show();
                            break;
                        case 2:
                            users.Registration(user);
                            Show();
                            break;
                        case 3:
                            shopping_cart.ChoiceShopping_Cart(storage);
                            Show();
                            break;
                        case 4:
                            shopping_cart.Buy(storage, users, places);
                            Show();
                            break;
                        case 0:
                            break;
                        default:
                            Console.WriteLine("You enter incorrect choice");
                            Show();
                            break;
                    }
                } while (function != 0);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}