using System;

namespace TemplateLib.Models
{
    public class QueryItems
    {
        private string _sold;
        private string _searchName;

        public QueryItems()
        {
        }

        public QueryItems(string sold, string searchName)
        {
            _sold = sold;
            _searchName = searchName;
        }

        public string Sold
        {
            get => _sold;
            set => _sold = value;
        }

        public string SearchName
        {
            get => _searchName;
            set => _searchName = value;
        }

        public override string ToString()
        {
            return $"{nameof(Sold)}: {Sold}, {nameof(SearchName)}: {SearchName}";
        }
    }
}