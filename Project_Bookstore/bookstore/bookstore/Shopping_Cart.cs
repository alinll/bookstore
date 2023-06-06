using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

namespace bookstore
{
    internal class Shopping_Cart : Cart_Item
    {
        public List<Cart_Item> Cart_Item { get; set; }

        public Shopping_Cart()
        {
            this.Cart_Item = new List<Cart_Item>();
        }

        public Shopping_Cart(string id, string name, double price, string author_first_name, string author_last_name,
            string category, int count, int quantity) : base(id, name, price, author_first_name, author_last_name, category, count,
                quantity)
        {
            this.Cart_Item = new List<Cart_Item>();
        }

        public void AddToShoppingCart(Storage storage)
        {
            Console.Write("Enter name of book, which you want to buy: ");
            string item = Console.ReadLine();

            Book selectedBook = storage.Books.FirstOrDefault(b => b.Name.Equals(item, StringComparison.OrdinalIgnoreCase));

            if (selectedBook != null)
            {
                Cart_Item existingCartItem = Cart_Item.FirstOrDefault(b => b.Name.Equals(item,
                            StringComparison.OrdinalIgnoreCase));
                int availableQuantity = selectedBook.Count - (existingCartItem?.Quantity ?? 0);
                if (availableQuantity > 0)
                {
                    bool isValid = false;
                    Console.WriteLine($"Enter a quantity of this book. Available: {availableQuantity}");
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
                                    Cart_Item cartItem = new Cart_Item(selectedBook.Id, selectedBook.Name,
                                        selectedBook.Price, selectedBook.Author_First_Name, selectedBook.Author_Last_Name,
                                        selectedBook.Category, selectedBook.Count, quantity);
                                    Cart_Item.Add(cartItem);
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

                    selectedBook = storage.Books.FirstOrDefault(b => b.Name.Equals(item, StringComparison.OrdinalIgnoreCase));

                    if (selectedBook != null)
                    {
                        Cart_Item existingCartItem = Cart_Item.FirstOrDefault(b => b.Name.Equals(item,
                                    StringComparison.OrdinalIgnoreCase));
                        int availableQuantity = selectedBook.Count - (existingCartItem?.Quantity ?? 0);
                        if (availableQuantity > 0)
                        {
                            bool isValid = false;
                            Console.WriteLine($"Enter a quantity of this book. Available: {availableQuantity}");
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
                                            Cart_Item cartItem = new Cart_Item(selectedBook.Id, selectedBook.Name,
                                                selectedBook.Price, selectedBook.Author_First_Name, selectedBook.Author_Last_Name,
                                                selectedBook.Category, selectedBook.Count, quantity);
                                            Cart_Item.Add(cartItem);
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

        public void Calculate_total_price()
        {
            double total_price = 0;
            foreach (Cart_Item b in Cart_Item)
            {
                double priceBook = b.Quantity * b.Price;
                total_price += priceBook;
            }
            Console.WriteLine($"Total price: {total_price}");
        }

        public void DeleteShoppingCart()
        {
            if (Cart_Item.Count() > 0)
            {
                Console.Write("\nEnter a name of book, which you want to delete: ");
                string item = Console.ReadLine();

                Cart_Item selectedBook = Cart_Item.FirstOrDefault(b => b.Name.Equals(item,
                    StringComparison.OrdinalIgnoreCase));

                if (selectedBook != null)
                {
                    Cart_Item.Remove(selectedBook);
                    Console.WriteLine("\nShopping cart:");
                    foreach (Cart_Item b in Cart_Item)
                    {
                        b.Show();
                        Console.WriteLine();
                    }
                    Calculate_total_price();
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

        public void ReduceCountShoppingCart()
        {
            if (Cart_Item.Count() > 0)
            {
                Console.Write("Enter a name of book, which count you want to reduce: ");
                string item = Console.ReadLine();

                Cart_Item selectedBook = Cart_Item.FirstOrDefault(b => b.Name.Equals(item, StringComparison.OrdinalIgnoreCase));

                if (selectedBook != null && selectedBook.Quantity > 1)
                {
                    int quantity = 1;
                    --selectedBook.Quantity;

                    Console.WriteLine("\nShopping cart:");
                    foreach (Cart_Item b in Cart_Item)
                    {
                        b.Show();
                        Console.WriteLine();
                    }
                    Calculate_total_price();
                }
                else if (selectedBook != null && selectedBook.Quantity == 1)
                {
                    Cart_Item.Remove(selectedBook);

                    Console.WriteLine("\nShopping cart:");
                    foreach (Cart_Item b in Cart_Item)
                    {
                        b.Show();
                        Console.WriteLine();
                    }
                    Calculate_total_price();
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

        public void Buy(Storage storage)
        {
            try
            {
                if (Cart_Item.Count <= 0)
                {
                    throw new Exception("Your shopping cart is empty");
                }

                //User.EnterAccount(users);
                //Place.EnterPlace(place);

                foreach (Cart_Item item in Cart_Item)
                {
                    Book book = storage.Books.FirstOrDefault(b => b.Id == item.Id);
                    if (book != null)
                    {
                        book.Count -= item.Quantity;
                    }
                }

                Storage boughtBooks = new Storage();

                foreach(Cart_Item b in Cart_Item)
                {
                    boughtBooks.AddBook(b);
                }

                ShowCheck(boughtBooks);
                Calculate_total_price();

                Cart_Item.Clear();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void ShowCheck(Storage boughtBooks)
        {
            Console.WriteLine("\nCheck");
            Console.WriteLine(DateTime.Now);
            boughtBooks.Show();
        }

        public override void Show()
        {
            foreach (Cart_Item b in Cart_Item)
            {
                b.Show();
                Console.WriteLine();
            }
        }
    }
}
