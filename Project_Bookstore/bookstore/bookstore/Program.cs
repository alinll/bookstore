using System.IO;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;

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

            Console.WriteLine("All books:\n");
            storage.Show();

            Console.WriteLine("Do you want to sort this list? Enter Y or N:");
            string choice = Console.ReadLine();
            storage.ChoiceSort(choice);


            Console.Write("Enter name for searching a book: ");
            string searchName = Console.ReadLine();
            Book nameSearching = storage.Search_by_Name(searchName);

            if (nameSearching != null)
            {
                Console.WriteLine($"Name of the book: {nameSearching.Name}\nAuthor: {nameSearching.Author_First_Name} " +
                    $"{nameSearching.Author_Last_Name}\nPrice: {nameSearching.Price}");
            }
            else
            {
                Console.WriteLine("We haven't this book(");
            }

            Console.Write("\nEnter last name of the author for searching a book: ");
            string searchLastName = Console.ReadLine();

            Storage books_by_Author = storage.Search_by_Author(searchLastName);

            Console.WriteLine();
            if (books_by_Author.Count >= 0)
            {
                books_by_Author.Show();

                Console.WriteLine("Do you want to sort this list? Enter Y or N:");
                choice = Console.ReadLine();
                books_by_Author.ChoiceSort(choice);
            }
            else
            {
                Console.WriteLine("We haven't books from this author(");
            }

            Console.WriteLine("\nCategories of books:\n");
            storage.ShowCategory();

            Console.Write("Enter category for searching: ");
            string searchCategory = Console.ReadLine();

            storage.ChooseCategory(searchCategory);
            List<User> users = new List<User>();
            User user1 = new User();

            user1.Registration(users);
            users.Add(user1);

            foreach (User user in users)
            {
                user.Show();
                Console.WriteLine();
            }

            List<Cart_Item> shopping_cart = new List<Cart_Item>();
            Cart_Item.AddToShoppingCart(books, shopping_cart);

            Console.WriteLine("\nShopping cart:");
            foreach (Cart_Item b in shopping_cart)
            {
                b.Show();
                Console.WriteLine();
            }
            Cart_Item.Calculate_total_price(shopping_cart);

            Cart_Item.DeleteShoppingCart(shopping_cart);
            Cart_Item.ReduceCountShoppingCart(shopping_cart);

            List<Place> place = new List<Place>();
            Cart_Item.Buy(users, place, shopping_cart, books);

            Console.WriteLine("\nAll books:\n");
            foreach (Book b in books)
            {
                b.Show();
                Console.WriteLine();
            }
        }
    }
}