using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace bookstore
{
    internal class Shopping_Cart : Book
    {
        public int Quantity { get; set; }
        public User User { get; set; }
        public Shopping_Cart(string id, string name, double price, string author_first_name, string author_last_name, 
            string category, int count, int quantity) : base(id, name, price, author_first_name, author_last_name, category, count)
        {
            this.Quantity = quantity;
        }

        public static void AddToShoppingCart(List<Book> books, List<Shopping_Cart> shopping_cart)
        {
            Console.Write("Enter name of book, which you want to buy: ");
            string item = Console.ReadLine();

            Book selectedBook = null;

            foreach (Book book in books)
            {
                if (item == book.Name)
                {
                    selectedBook = book;
                    break;
                }
            }

            if (selectedBook != null)
            {
                Shopping_Cart existingCartItem = shopping_cart.FirstOrDefault(b => b.Name.Equals(item,
                            StringComparison.OrdinalIgnoreCase));
                int availableQuantity = selectedBook.Count - (existingCartItem?.Quantity ?? 0);
                bool isValid = false;
                Console.WriteLine($"Enter a quantity of this book. Available: {availableQuantity}");
                if(availableQuantity > 0)
                {
                    do
                    {
                        try
                        {
                            int quantity = int.Parse(Console.ReadLine());
                            if (quantity > 0 && quantity <= availableQuantity)
                            {
                                if (existingCartItem != null)
                                {
                                    existingCartItem.Quantity += quantity;
                                }
                                else
                                {
                                    Shopping_Cart cartItem = new Shopping_Cart(selectedBook.Id, selectedBook.Name,
                                        selectedBook.Price, selectedBook.Author_First_Name, selectedBook.Author_Last_Name,
                                        selectedBook.Category, selectedBook.Count, quantity);
                                    shopping_cart.Add(cartItem);
                                }
                                isValid = true;
                            }
                            else
                            {
                                throw new Exception("We haven't such quantity of book. Enter again");
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    } while (!isValid);
                }
            }
            else
            {
                Console.WriteLine("We haven't such book in stock(");
            }

            string choice;
            do
            {
                Console.WriteLine("Do you want to buy something else? Y or N");
                choice = Console.ReadLine().ToUpper();
                if (choice == "Y")
                {
                    Console.Write("Enter name of book, which you want to buy: ");
                    item = Console.ReadLine();

                    selectedBook = null;

                    foreach (Book book in books)
                    {
                        if (item == book.Name)
                        {
                            selectedBook = book;
                            break;
                        }
                    }

                    if (selectedBook != null)
                    {
                        Shopping_Cart existingCartItem = shopping_cart.FirstOrDefault(b => b.Name.Equals(item,
                                    StringComparison.OrdinalIgnoreCase));
                        int availableQuantity = selectedBook.Count - (existingCartItem?.Quantity ?? 0);
                        bool isValid = false;
                        Console.WriteLine($"Enter a quantity of this book. Available: {availableQuantity}");
                        if(availableQuantity > 0)
                        {
                            do
                            {
                                try
                                {
                                    int quantity = int.Parse(Console.ReadLine());
                                    if (quantity > 0 && quantity <= availableQuantity)
                                    {
                                        if (existingCartItem != null)
                                        {
                                            existingCartItem.Quantity += quantity;
                                        }
                                        else
                                        {
                                            Shopping_Cart cartItem = new Shopping_Cart(selectedBook.Id, selectedBook.Name,
                                                selectedBook.Price, selectedBook.Author_First_Name, selectedBook.Author_Last_Name,
                                                selectedBook.Category, selectedBook.Count, quantity);
                                            shopping_cart.Add(cartItem);
                                        }
                                        isValid = true;
                                    }
                                    else
                                    {
                                        throw new Exception("We haven't such quantity of book. Enter again");
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex.Message);
                                }
                            } while (!isValid);
                        }
                    }
                    else
                    {
                        Console.WriteLine("We haven't such book in stock(");
                    }
                }
            } while (choice.ToUpper() == "Y");
        }

        public static void Calculate_total_price(List<Shopping_Cart> shopping_cart)
        {
            double total_price = 0;
            foreach(Shopping_Cart b in shopping_cart)
            {
                double priceBook = b.Quantity * b.Price;
                total_price += priceBook;
            }
            Console.WriteLine($"Total price: {total_price}");
        }

        public static void DeleteShoppingCart(List<Shopping_Cart> shopping_cart)
        {
            if(shopping_cart.Count() > 0)
            {
                Console.Write("\nEnter a name of book, which you want to delete: ");
                string item = Console.ReadLine();

                Shopping_Cart selectedBook = shopping_cart.FirstOrDefault(b => b.Name.Equals(item, 
                    StringComparison.OrdinalIgnoreCase));

                if (selectedBook != null)
                {
                    shopping_cart.Remove(selectedBook);
                    Console.WriteLine("\nShopping cart:");
                    foreach (Shopping_Cart b in shopping_cart)
                    {
                        b.Show();
                        Console.WriteLine();
                    }
                    Calculate_total_price(shopping_cart);
                }
                else
                {
                    Console.WriteLine("You haven't such book in your shopping cart");
                }
            }
            else
            {
                Console.WriteLine("\nYour shopping cart is empty");
            }
        }

        public static void ReduceCountShoppingCart(List<Shopping_Cart> shopping_cart)
        {
            if(shopping_cart.Count() > 0)
            {
                Console.Write("Enter a name of book, which count you want to reduce: ");
                string item = Console.ReadLine();

                Shopping_Cart selectedBook = shopping_cart.FirstOrDefault(b => b.Name.Equals(item, StringComparison.OrdinalIgnoreCase));

                if (selectedBook != null && selectedBook.Quantity > 1)
                {
                    int quantity = 1;
                    --selectedBook.Quantity;

                    Console.WriteLine("\nShopping cart:");
                    foreach (Shopping_Cart b in shopping_cart)
                    {
                        b.Show();
                        Console.WriteLine();
                    }
                    Calculate_total_price(shopping_cart);
                }
                else if (selectedBook != null && selectedBook.Quantity == 1)
                {
                    shopping_cart.Remove(selectedBook);

                    Console.WriteLine("\nShopping cart:");
                    foreach (Shopping_Cart b in shopping_cart)
                    {
                        b.Show();
                        Console.WriteLine();
                    }
                    Calculate_total_price(shopping_cart);
                }
                else
                {
                    Console.WriteLine("This book isn't in your shopping cart");
                }
            }
            else
            {
                Console.WriteLine("Your shipping cart is empty");
            }
        }

        public static void Buy(List<User> users, List<Place> place, List<Shopping_Cart> shopping_cart, List<Book> books)
        {
            try
            {
                if(shopping_cart.Count <= 0)
                {
                    throw new Exception("Your shopping cart is empty");
                }

                User.EnterAccount(users);
                Place.EnterPlace(place);

                foreach (Shopping_Cart item in shopping_cart)
                {
                    Book book = books.FirstOrDefault(b => b.Id == item.Id);
                    if(book != null)
                    {
                        book.Count -= item.Quantity;
                    }
                }

                List<Shopping_Cart> boughtBooks = new List<Shopping_Cart>();

                foreach (Shopping_Cart b in shopping_cart)
                {
                    boughtBooks.Add(b);
                }

                shopping_cart.Clear();

                ShowCheck(boughtBooks);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void ShowCheck(List<Shopping_Cart> boughtBooks)
        {
            Console.WriteLine("\nCheck");
            Console.WriteLine(DateTime.Now);
            foreach (Shopping_Cart b in boughtBooks)
            {
                b.Show();
                Console.WriteLine();
            }

            Calculate_total_price(boughtBooks);
        }

        public override void Show()
        {
            Console.WriteLine($"Name of the book: {Name}\nAuthor: {Author_First_Name} {Author_Last_Name}\nPrice: {Price}\n" +
                $"Quantity: {Quantity}");
        }
    }
}
