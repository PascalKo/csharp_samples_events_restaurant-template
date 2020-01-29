using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Core
{
    public class Tasks
    {
        private readonly int _delay;
        private string _name;
        private string _taskType;
        private string _article;

        public Tasks(int delay, string name, string taskType, string article)
        {
            _delay = delay;
            _name = name;
            _taskType = taskType;
            _article = article;
        }
    }
}
