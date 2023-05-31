using System.Reflection.Metadata;

namespace bookstore
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Book> books = new List<Book>
            {
                new Categories("IM-00114320", "Pet Sematary", 326, "Stephen", "King", "Fiction", 7),
                new Categories("IM-00002141", "The Shining", 399, "Stephen", "King", "Fiction", 8),
                new Categories("IM-00001588", "Lord of the Flies", 281, "William", "Golding", "Fiction", 1),
                new Categories("IM-00001730", "Complete Collection of Prose Works. Volume 1", 476, "H. P.", "Lovecraft",
                "Fiction", 7),
                new Categories("IM-00004889", "The Art of War", 224, "Sun", "Tzu", "Non-fiction", 17)
            };
            Book newBook = new Categories("IL-00014572", "Murder on the Orient Express", 266, "Agatha", "Christie", "Fiction", 7);
            Book.AddBook(books, newBook);

            newBook = new Categories("IM-00114320", "Pet Sematary", 326, "Stephen", "King", "Fiction", 10);
            Book.AddBook(books, newBook);

            Console.WriteLine("All books:\n");
            foreach (Book b in books)
            {
                b.Show();
                Console.WriteLine();
            }

            Console.WriteLine("Do you want to sort this list? Enter Y or N:");
            string choice = Console.ReadLine();
            Book.ChoiceSort(books, choice);


            Console.Write("Enter name for searching a book: ");
            string searchName = Console.ReadLine();
            Book nameSearching = Book.Search_by_Name(books, searchName);

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

            List<Book> books_by_Author = Book.Search_by_Author(books, searchLastName);

            Console.WriteLine();
            if (books_by_Author.Count > 0)
            {
                foreach (Book b in books_by_Author)
                {
                    b.Show();
                    Console.WriteLine();
                }

                Console.WriteLine("Do you want to sort this list? Enter Y or N:");
                choice = Console.ReadLine();
                Book.ChoiceSort(books_by_Author, choice);
            }
            else
            {
                Console.WriteLine("We haven't books from this author(");
            }

            Console.WriteLine("\nCategories of books:\n");
            Categories.ShowCategory(books);

            Console.Write("Enter category for searching: ");
            string searchCategory = Console.ReadLine();

            Categories.ChooseCategory(books, searchCategory);
            List<User> users = new List<User>();
            User user1 = new User();

            user1.Registration(users);
            users.Add(user1);

            foreach (User user in users)
            {
                user.Show();
                Console.WriteLine();
            }

            List<Shopping_Cart> shopping_cart = new List<Shopping_Cart>();
            Shopping_Cart.AddToShoppingCart(books, shopping_cart);

            Console.WriteLine("\nShopping cart:");
            foreach (Shopping_Cart b in shopping_cart)
            {
                b.Show();
                Console.WriteLine();
            }
            Shopping_Cart.Calculate_total_price(shopping_cart);

            Shopping_Cart.DeleteShoppingCart(shopping_cart);
            Shopping_Cart.ReduceCountShoppingCart(shopping_cart);

            List<Place> place = new List<Place>();
            Shopping_Cart.Buy(users, place, shopping_cart, books);

            Console.WriteLine("\nAll books:\n");
            foreach (Book b in books)
            {
                b.Show();
                Console.WriteLine();
            }
        }
    }
}