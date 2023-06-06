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
    internal class Cart_Item : Storage
    {
        public int Quantity { get; set; }

        public Cart_Item() { }
        public Cart_Item(string id, string name, double price, string author_first_name, string author_last_name, 
            string category, int count, int quantity) : base(id, name, price, author_first_name, author_last_name, category, count)
        {
            this.Quantity = quantity;
        }

        public override void Show()
        {
            Console.WriteLine($"Name of the book: {Name}\nAuthor: {Author_First_Name} {Author_Last_Name}\nPrice: {Price}\n" +
                $"Quantity: {Quantity}");
        }
    }
}
