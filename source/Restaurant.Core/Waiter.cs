using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Restaurant.Core
{
    public class Waiter
    {

        private Dictionary<string, Article> _articles;
        private List<Tasks> _tasks;
        private Dictionary<string, Guest> _guests;
        private event EventHandler<string> TaskReady;

        public Waiter(EventHandler<string> OnReadyTask)
        {
            _articles = new Dictionary<string, Article>();
            _tasks = new List<Tasks>();
            _guests = new Dictionary<string, Guest>();
            ReadAllArticles();
            ReadAllTasksAndGuests();
            FastClock.Instance.OneMinuteIsOver += Instance_OneMinuteIsOver;
            TaskReady += OnReadyTask;
        }



        protected virtual void Instance_OneMinuteIsOver(object sender, DateTime e)
        {


            while (_tasks.Count > 0 && FastClock.Instance.Time.Equals(_tasks[0].Delay))
            {
                Article article;
                Guest guest;
                string text = String.Empty;
                if (_guests.TryGetValue(_tasks[0].Name, out guest))
                {
                    if (_tasks[0].TaskType == OrderType.Order)
                    {
                        text = $"{_tasks[0].Article} für {guest.Name} ist bestellt!";
                    }
                    else if (_tasks[0].TaskType == OrderType.Ready && _articles.TryGetValue(_tasks[0].Article, out article))
                    {
                        text = $"{_tasks[0].Article} für {guest.Name} wird serviert!";
                        guest.AddArticle(article);
                    }
                    else if (_tasks[0].TaskType == OrderType.ToPay)
                    {
                        text = $"{guest.Name} bzeahlt {guest.Bill:f2} EUR";
                    }

                    OnReadyTask(text);
                    _tasks.RemoveAt(0);
                }
            }
        }

        private void OnReadyTask(string text)
        {
            TaskReady?.Invoke(this, text);
        }

        private void ReadAllArticles()
        {
            string path = MyFile.GetFullNameInApplicationTree("Articles.csv");

            string[] lines = File.ReadAllLines(path, UTF8Encoding.Default);

            for (int i = 1; i < lines.Length; i++)
            {
                string[] parts = lines[i].Split(';');
                string articleName = parts[0];
                Article article = new Article(articleName, Convert.ToDouble(parts[1]), Convert.ToInt32(parts[2]));
                _articles.Add(articleName, article);
            }

        }

        private void ReadAllTasksAndGuests()
        {
            string path = MyFile.GetFullNameInApplicationTree("Tasks.csv");
            OrderType orderType;
            Article article;
            string[] lines = File.ReadAllLines(path, UTF8Encoding.Default);

            for (int i = 1; i < lines.Length; i++)
            {
                string[] parts = lines[i].Split(';');

                if (Enum.TryParse(parts[2], out orderType))
                {

                    string name = parts[1];
                    if (!_guests.ContainsKey(name))
                    {
                        Guest guest = new Guest(name);
                        _guests.Add(name, guest);
                    }

                    DateTime taskTime = FastClock.Instance.Time.AddMinutes(Convert.ToInt32(parts[0]));

                    Tasks task = new Tasks(taskTime, name, orderType, parts[3]);
                    _tasks.Add(task);

                    if (orderType == OrderType.Order && _articles.TryGetValue(parts[3], out article))
                    {
                        taskTime = taskTime.AddMinutes(article.TimeToBuild);
                        Tasks readyTask = new Tasks(taskTime, name, OrderType.Ready, article.Name);
                        _tasks.Add(readyTask);
                    }
                }
            }
            _tasks.Sort();
        }

    }

}
