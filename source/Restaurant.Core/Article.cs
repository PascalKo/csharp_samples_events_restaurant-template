using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Core
{
    public class Article
    {
        private readonly string _name;
        private readonly double _price;
        private readonly int _timeToBuild;

        public Article(string name, double price, int timeToBuild)
        {
            _name = name;
            _price = price;
            _timeToBuild = timeToBuild;
        }

    }
}
