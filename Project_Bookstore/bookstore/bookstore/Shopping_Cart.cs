using System.Text.Json;
using System.Xml.Linq;

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

            string jsonReader = File.ReadAllText("shopping_cart.json");
            List<Cart_Item>? cart_itemJson;
            if (!string.IsNullOrEmpty(jsonReader))
            {
                cart_itemJson = JsonSerializer.Deserialize<List<Cart_Item>>(jsonReader);

                foreach (Cart_Item b in cart_itemJson)
                {
                    Cart_Item.Add(b);
                }
            }
            else
            {
                cart_itemJson = new List<Cart_Item>();
            }

            if (selectedBook != null)
            {
                Cart_Item existingCartItem = cart_itemJson.FirstOrDefault(b => b.Book.Name.Equals(item,
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
                                    cart_itemJson.Add(cartItem);
                                }
                                File.WriteAllText("shopping_cart.json", JsonSerializer.Serialize(cart_itemJson, 
                                    new JsonSerializerOptions() { WriteIndented = true }));
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
                        Cart_Item existingCartItem = cart_itemJson.FirstOrDefault(b => b.Book.Name.Equals(item,
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
                                            cart_itemJson.Add(cartItem);
                                        }
                                        File.WriteAllText("shopping_cart.json", JsonSerializer.Serialize(cart_itemJson, 
                                            new JsonSerializerOptions() { WriteIndented = true }));
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
            string jsonReader = File.ReadAllText("shopping_cart.json");
            List<Cart_Item>? cart_itemJson;
            if (!string.IsNullOrEmpty(jsonReader))
            {
                cart_itemJson = JsonSerializer.Deserialize<List<Cart_Item>>(jsonReader);

                foreach (Cart_Item b in cart_itemJson)
                {
                    double priceBook = b.Quantity * b.Book.Price;
                    total_price += priceBook;
                }
                Console.WriteLine($"Total price: {total_price}");
            }
            else
            {
                Console.WriteLine();
            }
        }

        public void DeleteShoppingCart()
        {
            string jsonReader = File.ReadAllText("shopping_cart.json");
            List<Cart_Item>? cart_itemJson;
            if (!string.IsNullOrEmpty(jsonReader))
            {
                cart_itemJson = JsonSerializer.Deserialize<List<Cart_Item>>(jsonReader);

                if (cart_itemJson.Count() > 0)
                {
                    Console.Write("\nEnter a name of book, which you want to delete: ");
                    string item = Console.ReadLine();

                    Cart_Item selectedBook = cart_itemJson.FirstOrDefault(b => b.Book.Name.Equals(item,
                        StringComparison.OrdinalIgnoreCase));

                    if (selectedBook != null)
                    {
                        cart_itemJson.Remove(selectedBook);
                        File.WriteAllText("shopping_cart.json", JsonSerializer.Serialize(cart_itemJson, 
                            new JsonSerializerOptions() { WriteIndented = true }));

                        Console.WriteLine("\nShopping cart:");
                        Show();
                    }
                    else
                    {
                        Console.WriteLine("You haven't such book in your shopping cart");
                    }
                }
            }
            else
            {
                Console.WriteLine("\nYour shopping cart is empty");
            }
        }

        public void ReduceCountShoppingCart()
        {
            string jsonReader = File.ReadAllText("shopping_cart.json");
            List<Cart_Item>? cart_itemJson;
            if (!string.IsNullOrEmpty(jsonReader))
            {
                cart_itemJson = JsonSerializer.Deserialize<List<Cart_Item>>(jsonReader);

                if (cart_itemJson.Count() > 0)
                {
                    Console.Write("Enter a name of book, which count you want to reduce: ");
                    string item = Console.ReadLine();

                    Cart_Item selectedBook = cart_itemJson.FirstOrDefault(b => b.Book.Name.Equals(item, 
                        StringComparison.OrdinalIgnoreCase));

                    if (selectedBook != null && selectedBook.Quantity > 1)
                    {
                        int quantity = 1;
                        --selectedBook.Quantity;

                        File.WriteAllText("shopping_cart.json", JsonSerializer.Serialize(cart_itemJson, 
                            new JsonSerializerOptions() { WriteIndented = true }));
                        Console.WriteLine("\nShopping cart:");
                        Show();
                    }
                    else if (selectedBook != null && selectedBook.Quantity == 1)
                    {
                        cart_itemJson.Remove(selectedBook);

                        File.WriteAllText("shopping_cart.json", JsonSerializer.Serialize(cart_itemJson, 
                            new JsonSerializerOptions() { WriteIndented = true }));
                        Console.WriteLine("\nShopping cart:");
                        Show();
                    }
                    else
                    {
                        Console.WriteLine("This book isn't in your shopping cart");
                    }
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
                string jsonReader = File.ReadAllText("shopping_cart.json");
                List<Cart_Item>? cart_itemJson;
                if (!string.IsNullOrEmpty(jsonReader))
                {
                    cart_itemJson = JsonSerializer.Deserialize<List<Cart_Item>>(jsonReader);

                    if (cart_itemJson.Count() > 0)
                    {
                        User user = new User();
                        bool isAccountEntered = users.EnterAccount();

                        if (!isAccountEntered)
                        {
                            throw new Exception("You must be registered to buy something");
                        }

                        Place place = new Place();
                        place.EnterPlace();

                        foreach (Cart_Item item in cart_itemJson)
                        {
                            Book book = storage.Books.FirstOrDefault(b => b.Id == item.Book.Id);
                            if (book != null)
                            {
                                book.Count -= item.Quantity;
                            }
                        }

                        File.WriteAllText("books.json", JsonSerializer.Serialize(storage.Books,
                            new JsonSerializerOptions() { WriteIndented = true }));

                        ShowCheck();

                        cart_itemJson.Clear();
                        File.WriteAllText("shopping_cart.json", JsonSerializer.Serialize(cart_itemJson,
                            new JsonSerializerOptions() { WriteIndented = true }));
                    }
                    else
                    {
                        throw new Exception("Your shopping cart is empty");
                    }
                }
                else
                {
                    throw new Exception("Your shopping cart is empty");
                }
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
            Console.WriteLine("-----------------------------");
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
            string jsonReader = File.ReadAllText("shopping_cart.json");
            List<Cart_Item>? cart_itemJson;
            if (!string.IsNullOrEmpty(jsonReader))
            {
                cart_itemJson = JsonSerializer.Deserialize<List<Cart_Item>>(jsonReader);

                foreach (Cart_Item b in cart_itemJson)
                {
                    b.Show();
                    Console.WriteLine();
                }

                Calculate_total_price();
            }
            else
            {
                Console.WriteLine("Your shopping cart is empty");
            }
        }
    }
}
