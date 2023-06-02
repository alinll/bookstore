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
        public Book() { }

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

        public virtual void Show()
        {
            Console.WriteLine($"Name of the book: {Name}\nAuthor: {Author_First_Name} {Author_Last_Name}\nCategory: {Category}\n" +
                $"Price: {Price}\nCount: {Count}");
        }
    }
}
