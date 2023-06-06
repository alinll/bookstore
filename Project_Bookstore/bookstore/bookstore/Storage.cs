using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bookstore
{
    internal class Storage : Book
    {
        public List<Book> Books { get; set; }
        public Storage() { 
            this.Books = new List<Book>(); 
        }
        public Storage (string id, string name, double price, string author_first_name, string author_last_name, string category,
            int count) : base (id, name, price, author_first_name, author_last_name, category, count) {
            this.Books = new List<Book>();
        }

        public void AddBook(Book book)
        {

            bool bookExists = false;

            foreach (Book b in Books)
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
                Books.Add(book);
            }
        }

        public Book Search_by_Name(string searchName)
        {
            foreach (Book book in Books)
            {
                if (string.Equals(book.Name, searchName, StringComparison.OrdinalIgnoreCase))
                {
                    return book;
                }
            }
            return null;
        }

        public Storage Search_by_Author(string searchLastName)
        {
            Storage books_by_Author = new Storage();
            foreach (Book book in Books)
            {
                if (book.Author_Last_Name.Equals(searchLastName, StringComparison.OrdinalIgnoreCase))
                {
                    books_by_Author.AddBook(book);
                }
            }
            return books_by_Author;
        }

        public Book ChoiceSort(string choice)
        {
            if (choice.ToUpper() == "Y")
            {
                try
                {
                    Console.WriteLine("You want to sort by increasing or decreasing? Enter I or D:");
                    choice = Console.ReadLine();
                    if (choice.ToUpper() == "I")
                    {
                        Books.Sort();
                        Console.WriteLine("Books sorted by increasing price:\n");
                        foreach (Book book in Books)
                        {
                            book.Show();
                            Console.WriteLine();
                        }
                    }
                    else if (choice.ToUpper() == "D")
                    {
                        Books.Sort();
                        Books.Reverse();
                        Console.WriteLine("Books sorted by decreasing price:\n");
                        foreach (Book book in Books)
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

        public void ShowCategory()
        {
            List<string> showedCategories = new List<string>();
            foreach (Book book in Books)
            {
                if (book is Book categoriesBook && !showedCategories.Contains(categoriesBook.Category))
                {
                    Console.WriteLine(categoriesBook.Category + "\n");
                    showedCategories.Add(categoriesBook.Category);
                }
            }
        }

        public void ChooseCategory(string searchCategory)
        {
            Storage selectedBook = new Storage();

            foreach (Book book in Books)
            {
                if (book is Book categoriesBook && string.Equals(categoriesBook.Category, searchCategory,
                    StringComparison.OrdinalIgnoreCase))
                {
                    selectedBook.AddBook(book);
                }
            }

            if (selectedBook.Count >= 0)
            {
                selectedBook.Show();

                Console.WriteLine("Do you want to sort this books? Y or N");
                string choice = Console.ReadLine();
                selectedBook.ChoiceSort(choice);
            }
            else
            {
                Console.WriteLine("We haven't books in this category(");
            }
        }

        public override void Show()
        {
            foreach(Book b in Books)
            {
                b.Show();
                Console.WriteLine();
            }
        }
    }
}
