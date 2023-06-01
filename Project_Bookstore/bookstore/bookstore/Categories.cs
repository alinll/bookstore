using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bookstore
{
    internal class Categories : Book
    {
        public string Category { get; set; }
        public Categories(string id, string name, double price, string author_first_name, string author_last_name,
            string category, int count) : base(id, name, price, author_first_name, author_last_name, count)
        {
            this.Category = category;
        }

        private static List<string> showedCategories = new List<string>();
        public static void ShowCategory(List<Book> categories)
        {
            foreach (Book book in categories)
            {
                if (book is Categories categoriesBook && !showedCategories.Contains(categoriesBook.Category))
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
                if (book is Categories categoriesBook && string.Equals(categoriesBook.Category, searchCategory,
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

        public override void Show()
        {
            Console.WriteLine($"Category: {Category}\nName of the book: {Name}\nAuthor: {Author_First_Name} {Author_Last_Name}\n" +
                $"Price: {Price}\n{Count} books are available\n");
        }
    }
}
