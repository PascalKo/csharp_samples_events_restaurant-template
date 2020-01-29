using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Core
{
    class Guest
    {
        public string Name { get; private set; }
        private double _bill = 0.0;
        private List<Article> _articles;

        public double Bill
        {
            get

            {               
                foreach (Article item in _articles)
                {
                    _bill += item.Price;
                }

                return _bill; 
            }

        }

        public Guest(string name)
        {
            Name = name;
            _articles = new List<Article>();
        }

        public void AddArticle(Article article)
        {
            _articles.Add(article);
        }
       
    }
}
