using System.Text.Json;

namespace bookstore
{
    internal class Shopping_Cart
    {
        public List<Cart_Item> Cart_Item { get; set; }

        public Shopping_Cart() 
        {
            this.Cart_Item = new List<Cart_Item>();
        }

        public void AddToShoppingCart(Storage storage)
        {
            Console.Write("\nEnter name of book, which you want to buy: ");
            string item = Console.ReadLine();


            Book selectedBook = storage.Books.FirstOrDefault(b => b.Name.Equals(item, StringComparison.OrdinalIgnoreCase));

            if (selectedBook != null)
            {
                Cart_Item existingCartItem = Cart_Item.FirstOrDefault(b => b.Book.Name.Equals(item,
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
                                    Cart_Item cartItem = new Cart_Item(selectedBook, quantity);
                                    Cart_Item.Add(cartItem);
                                    File.WriteAllText("shopping_cart.json", JsonSerializer.Serialize(Cart_Item));
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
                        Cart_Item existingCartItem = Cart_Item.FirstOrDefault(b => b.Book.Name.Equals(item,
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
                                            Cart_Item cartItem = new Cart_Item(selectedBook, quantity);
                                            Cart_Item.Add(cartItem);
                                            File.WriteAllText("shopping_cart.json", JsonSerializer.Serialize(Cart_Item));
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
                double priceBook = b.Quantity * b.Book.Price;
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

                Cart_Item selectedBook = Cart_Item.FirstOrDefault(b => b.Book.Name.Equals(item,
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

                Cart_Item selectedBook = Cart_Item.FirstOrDefault(b => b.Book.Name.Equals(item, StringComparison.OrdinalIgnoreCase));

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
                Console.WriteLine("Your shopping cart is empty");
            }
        }

        public void Buy(Storage storage, Registered_Users users)
        {
            try
            {
                if (Cart_Item.Count <= 0)
                {
                    throw new Exception("Your shopping cart is empty");
                }

                User user = new User();
                bool isAccountEntered = users.EnterAccount(user);

                if (!isAccountEntered)
                {
                    throw new Exception("You must be registered to buy something");
                }

                Place place = new Place();
                place.EnterPlace();

                foreach (Cart_Item item in Cart_Item)
                {
                    Book book = storage.Books.FirstOrDefault(b => b.Id == item.Book.Id);
                    if (book != null)
                    {
                        book.Count -= item.Quantity;
                    }
                }

                File.WriteAllText("books.json", JsonSerializer.Serialize(storage.Books));

                ShowCheck();
                Console.WriteLine("\nBuyer:");
                users.Show();
                Console.WriteLine("Address:");
                place.Show();
                Console.WriteLine("-----------------------------");

                Cart_Item.Clear();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void ShowCheck()
        {
            Console.WriteLine("\n-----------------------------");
            Console.WriteLine("Check");
            Console.WriteLine(DateTime.Now);

            Show();
            Calculate_total_price();
        }

        public void MenuShopping_Cart()
        {
            Console.WriteLine("\nChoose action:\n1 - add to shopping cart\n2 - see shopping cart\n" +
                "3 - delete book from shopping cart\n4 - reduce count of book in shopping cart\n0 - exit");
        }

        public void ChoiceShopping_Cart(Storage storage)
        {
            MenuShopping_Cart();

            int function;
            try
            {
                do
                {
                    function = int.Parse(Console.ReadLine());
                    switch (function)
                    {
                        case 1:
                            AddToShoppingCart(storage);
                            MenuShopping_Cart();
                            break;
                        case 2:
                            Console.WriteLine("\nShopping cart:");
                            Show();
                            Calculate_total_price();
                            MenuShopping_Cart();
                            break;
                        case 3:
                            DeleteShoppingCart();
                            MenuShopping_Cart();
                            break;
                        case 4:
                            ReduceCountShoppingCart();
                            MenuShopping_Cart();
                            break;
                        case 0:
                            break;
                        default:
                            Console.WriteLine("You enter incorrect choice");
                            MenuShopping_Cart();
                            break;
                    }
                } while (function != 0);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void Show()
        {
            foreach (Cart_Item b in Cart_Item)
            {
                b.Show();
                Console.WriteLine();
            }
        }
    }
}
