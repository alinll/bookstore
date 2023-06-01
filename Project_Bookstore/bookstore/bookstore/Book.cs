using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bookstore
{

    internal class Book : IComparable<Book>
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Author_First_Name { get; set; }
        public string Author_Last_Name { get; set; }
        public string Category { get; set; }
        public int Count { get; set; }

        public Book(string id, string name, double price, string author_first_name, string author_last_name, string category, 
            int count)
        {
            this.Id = id;
            this.Name = name;
            this.Price = price;
            this.Author_First_Name = author_first_name;
            this.Author_Last_Name = author_last_name;
            this.Category = category;
            this.Count = count;
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
                if (b.Id == book.Id)
                {
                    b.Count += book.Count;
                    bookExists = true;
                    break;
                }
            }
            if (!bookExists)
            {
                books.Add(book);
            }
        }

        private static List<string> showedCategories = new List<string>();
        public static void ShowCategory(List<Book> categories)
        {
            foreach (Book book in categories)
            {
                if (book is Book categoriesBook && !showedCategories.Contains(categoriesBook.Category))
                {
                    Console.WriteLine(categoriesBook.Category + "\n");
                    showedCategories.Add(categoriesBook.Category);
                }
            }
        }

        public static void ChooseCategory(List<Book> categories, string searchCategory)
        {
            List<Book> selectedBook = new List<Book>();

            foreach (Book book in categories)
            {
                if (book is Book categoriesBook && string.Equals(categoriesBook.Category, searchCategory,
                    StringComparison.OrdinalIgnoreCase))
                {
                    selectedBook.Add(book);
                }
            }

            if (selectedBook.Count > 0)
            {
                foreach (Book book in selectedBook)
                {
                    book.Show();
                    Console.WriteLine();
                }

                Console.WriteLine("Do you want to sort this books? Y or N");
                string choice = Console.ReadLine();
                Book.ChoiceSort(selectedBook, choice);
            }
            else
            {
                Console.WriteLine("We haven't books in this category(");
            }
        }

        public virtual void Show()
        {
            Console.WriteLine($"Name of the book: {Name}\nAuthor: {Author_First_Name} {Author_Last_Name}\nCategory: {Category}\n" +
                $"Price: {Price}");
        }
    }
}
