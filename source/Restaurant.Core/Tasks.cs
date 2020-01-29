using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Core
{
    public class Tasks : IComparable<Tasks>
    {
        public DateTime Delay { get; private set; }
        public string Name { get; private set; }
        public OrderType TaskType { get; private set; }
        public string Article { get; private set; }

        public Tasks(DateTime delay, string name, OrderType taskType, string article)
        {
            Delay = delay;
            Name = name;
            TaskType = taskType;
            Article = article;
        }

        public int CompareTo(Tasks other)
        {
            return Delay.CompareTo(other.Delay);
        }
    }
}
