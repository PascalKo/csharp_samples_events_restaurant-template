using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Core
{
    class Guest
    {
        private string _name;
        private double _bill = 0.0;
        public double Bill
        {
            get { return _bill; }


            set
            {
                _bill += value;
            }
        }

        public Guest(string name)
        {
            _name = name;
        }
        public Guest()
        {

        }
    }
}
