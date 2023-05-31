using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bookstore
{
    internal class Book : IComparable<Book>
    {
        private string id;
        private string name;
        private double price;
        private string author_first_name;
        private string author_last_name;
        private int count;

        public string Id { get { return id; } set { id = value; } }
        public string Name { get { return name; } set { name = value; } }
        public double Price { get { return price; } set { price = value; } }
        public string Author_First_Name { get { return author_first_name; } set { author_first_name = value; } }
        public string Author_Last_Name { get { return author_last_name; } set { author_last_name = value; } }
        public int Count { get { return count; } set { count = value; } }

        public Book(string id, string name, double price, string author_first_name, string author_last_name, int count)
        {
            this.id = id;
            this.name = name;
            this.price = price;
            this.author_first_name = author_first_name;
            this.author_last_name = author_last_name;
            this.count = count;
        }

        public int CompareTo(Book? other)
        {
            return Price.CompareTo(other.Price);
        }

        public static Book Search_by_Name(List<Book> books, string searchName)
        {
            foreach (Book book in books)
            {
                if (string.Equals(book.Name, searchName, StringComparison.OrdinalIgnoreCase))
                {
                    return book;
                }
            }
            return null;
        }

        public static List<Book> Search_by_Author(List<Book> books, string searchLastName)
        {
            List<Book> searchLastNameBooks = new List<Book>();
            foreach (Book book in books)
            {
                if (book.Author_Last_Name.Equals(searchLastName, StringComparison.OrdinalIgnoreCase))
                {
                    searchLastNameBooks.Add(book);
                }
            }
            return searchLastNameBooks;
        }
        public static Book ChoiceSort(List<Book> books, string choice)
        {
            if (choice.ToUpper() == "Y")
            {
                try
                {
                    Console.WriteLine("You want to sort by increasing or decreasing? Enter I or D:");
                    choice = Console.ReadLine();
                    if (choice.ToUpper() == "I")
                    {
                        books.Sort();
                        Console.WriteLine("Books sorted by increasing price:\n");
                        foreach (Book book in books)
                        {
                            book.Show();
                            Console.WriteLine();
                        }
                    }
                    else if (choice.ToUpper() == "D")
                    {
                        books.Sort();
                        books.Reverse();
                        Console.WriteLine("Books sorted by decreasing price:\n");
                        foreach (Book book in books)
                        {
                            book.Show();
                            Console.WriteLine();
                        }
                    }
                    else
                    {
                        throw new Exception("You entered incorrect choice!");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return null;
        }

        public static void AddBook(List<Book> books, Book book)
        {
            bool bookExists = false;

            foreach (Book b in books)
            {
                if (b.id == book.id)
                {
                    b.count += book.count;
                    bookExists = true;
                    break;
                }
            }
            if (!bookExists)
            {
                books.Add(book);
            }
        }

        public virtual void Show()
        {
            Console.WriteLine($"Name of the book: {Name}\nAuthor: {Author_First_Name} {Author_Last_Name}\nPrice: {Price}");
        }
    }
}
