namespace bookstore
{
    internal class Cart_Item
    {
        public Book Book { get; set; }
        public int Quantity { get; set; }

        public Cart_Item(Book Book, int quantity)
        {
            this.Quantity = quantity;
            this.Book = Book;
        }

        public void Show()
        {
            Console.WriteLine($"Name of the book: {Book.Name}\nAuthor: {Book.Author_First_Name} {Book.Author_Last_Name}\n" +
                $"Category: {Book.Category}\nPrice: {Book.Price}\nQuantity: {Quantity}");
        }
    }
}
