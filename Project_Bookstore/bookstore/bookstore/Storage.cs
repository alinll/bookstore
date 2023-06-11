using System.Text.Json;

namespace bookstore
{
    internal class Storage 
    {
        public List<Book> Books { get; set; }
        string jsonReader = File.ReadAllText("books.json");
        public Storage() { 
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

            File.WriteAllText("books.json", JsonSerializer.Serialize(Books, new JsonSerializerOptions() { WriteIndented = true }));
        }

        public void ChoiceSort(string choice, List<Book> selected)
        {
            if (choice.ToUpper() == "Y")
            {
                try
                {
                    Console.WriteLine("You want to sort by increasing or decreasing? Enter I or D:");
                    choice = Console.ReadLine();

                    List<Book>? booksJson = JsonSerializer.Deserialize<List<Book>>(jsonReader);
                    if (choice.ToUpper() == "I")
                    {
                        selected.Sort();
                        Console.WriteLine("Books sorted by increasing price:\n");
                        foreach (Book book in selected)
                        {
                            book.Show();
                            Console.WriteLine();
                        }
                    }
                    else if (choice.ToUpper() == "D")
                    {
                        selected.Sort();
                        selected.Reverse();
                        Console.WriteLine("Books sorted by decreasing price:\n");
                        foreach (Book book in selected)
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
        }

        public Book Search_by_Name(string searchName)
        {
            List<Book>? booksJson = JsonSerializer.Deserialize<List<Book>>(jsonReader);
            foreach (Book book in booksJson)
            {
                if (string.Equals(book.Name, searchName, StringComparison.OrdinalIgnoreCase))
                {
                    return book;
                }
            }
            return null;
        }

        public List<Book> Search_by_Author(string searchLastName)
        {
            List<Book>? booksJson = JsonSerializer.Deserialize<List<Book>>(jsonReader);
            List<Book> books_by_Author = new List<Book>();
            foreach (Book book in booksJson)
            {
                if (book.Author_Last_Name.Equals(searchLastName, StringComparison.OrdinalIgnoreCase))
                {
                    books_by_Author.Add(book);
                }
            }

            if (books_by_Author != null)
            {
                foreach (Book b in books_by_Author)
                {
                    b.Show();
                    Console.WriteLine();
                }

                Console.WriteLine("Do you want to sort this books? Y or N");
                string choice = Console.ReadLine();
                ChoiceSort(choice, books_by_Author);
            }
            else
            {
                Console.WriteLine("We haven't books from this author(");
            }

            return books_by_Author;
        }

        public void ShowCategory()
        {
            List<string> showedCategories = new List<string>();
            List<Book>? booksJson = JsonSerializer.Deserialize<List<Book>>(jsonReader);
            foreach (Book book in booksJson)
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
            List<Book> selectedBook = new List<Book>();
            List<Book>? booksJson = JsonSerializer.Deserialize<List<Book>>(jsonReader);
            foreach (Book book in booksJson)
            {
                if (book is Book categoriesBook && string.Equals(categoriesBook.Category, searchCategory,
                    StringComparison.OrdinalIgnoreCase))
                {
                    selectedBook.Add(book);
                }
            }

            if (selectedBook.Count >= 0)
            {
                foreach(Book b in selectedBook)
                {
                    b.Show();
                    Console.WriteLine();
                }

                Console.WriteLine("Do you want to sort this books? Y or N");
                string choice = Console.ReadLine();
                ChoiceSort(choice, selectedBook);
            }
            else
            {
                Console.WriteLine("We haven't books in this category(");
            }
        }

        public void MenuStorage()
        {
            Console.WriteLine("\nChoose action:\n1 - sort books\n2 - search book by name\n3 - search book by author\n" +
                "4 - see categories\n5 - search by category\n0 - exit");
        }

        public void ChoiceStorage()
        {
            Console.WriteLine("\nAll books:\n");
            Show();

            MenuStorage();

            int function;
            try
            {
                do
                {
                    function = int.Parse(Console.ReadLine());
                    switch (function)
                    {
                        case 1:
                            string choice = "Y";
                            ChoiceSort(choice, Books);
                            MenuStorage();
                            break;
                        case 2:
                            Console.Write("Enter name for searching a book: ");
                            string searchName = Console.ReadLine();
                            Book nameSearching = Search_by_Name(searchName);

                            if (nameSearching != null)
                            {
                                Console.WriteLine($"Name of the book: {nameSearching.Name}\nAuthor: {nameSearching.Author_First_Name} " +
                                    $"{nameSearching.Author_Last_Name}\nPrice: {nameSearching.Price}");
                            }
                            else
                            {
                                Console.WriteLine("We haven't this book(");
                            }
                            MenuStorage();
                            break;
                        case 3:
                            Console.Write("\nEnter last name of the author for searching a book: ");
                            string searchLastName = Console.ReadLine();

                            List<Book> books_by_Author = Search_by_Author(searchLastName);
                            MenuStorage();
                            break;
                        case 4:
                            Console.WriteLine("\nCategories of books:\n");
                            ShowCategory();
                            MenuStorage();
                            break;
                        case 5:
                            Console.Write("Enter category for searching: ");
                            string searchCategory = Console.ReadLine();
                            ChooseCategory(searchCategory);
                            MenuStorage();
                            break;
                        case 0:
                            break;
                        default:
                            Console.WriteLine("You enter incorrect choice");
                            MenuStorage();
                            break;
                    }
                } while (function != 0);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void Show()
        {
            string content = File.ReadAllText("books.json");
            List<Book>? booksJson = JsonSerializer.Deserialize<List<Book>>(content);
            foreach (Book b in booksJson)
            {
                b.Show();
                Console.WriteLine();
            }
        }
    }
}
