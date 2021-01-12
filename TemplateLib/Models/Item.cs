using System;

namespace TemplateLib.Models
{
    public class Item
    {
        private int _id;
        private string _name;
        private bool _sold;
        private int _price;

        public Item()
        {
        }

        public Item(int id, string name, bool sold, int price)
        {
            _id = id;
            _name = name;
            _sold = sold;
            _price = price;
        }

        public int Id
        {
            get => _id;
            set => _id = value;
        }

        public string Name
        {
            get => _name;
            set => _name = value;
        }

        public bool Sold
        {
            get => _sold;
            set => _sold = value;
        }

        public int Price
        {
            get => _price;
            set => _price = value;
        }

        public override string ToString()
        {
            return $"{nameof(Id)}: {Id}, {nameof(Name)}: {Name}, {nameof(Sold)}: {Sold}, {nameof(Price)}: {Price}";
        }
    }
}