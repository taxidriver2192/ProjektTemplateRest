using System;

namespace TemplateLib.Models
{
    public class Item
    {
        private readonly int _id;

        public Item()
        {
        }

        public Item(int id, string name, bool sold, int price)
        {
            _id = id;
            Name = name;
            Sold = sold;
            Price = price;
        }

        public int Id
        {
            get; set;
        }

        public string Name { get; set; }

        public bool Sold { get; set; }

        public int Price { get; set; }

        public override string ToString()
        {
            return $"{nameof(Id)}: {Id}, {nameof(Name)}: {Name}, {nameof(Sold)}: {Sold}, {nameof(Price)}: {Price}";
        }
    }
}