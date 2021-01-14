using System;

namespace TemplateLib.Models
{
    public class Food
    {
        private readonly int _id;

        public Food()
        {
        }

        public Food(int id, string name, int inStock, int lowLevel)
        {
            _id = id;
            Name = name;
            InStock = inStock;
            LowLevel = lowLevel;
        }

        public int Id
        {
            get; set;
        }

        public string Name { get; set; }

        public int InStock { get; set; }

        public int LowLevel { get; set; }

        public override string ToString()
        {
            return $"{nameof(Id)}: {Id}, {nameof(Name)}: {Name}, {nameof(InStock)}: {InStock}, {nameof(LowLevel)}: {LowLevel}";
        }
    }
}