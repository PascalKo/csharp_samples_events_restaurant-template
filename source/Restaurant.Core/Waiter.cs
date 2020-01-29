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
            string[] tasks = ReadLinesFromCsvFile("Tasks,csv");
            FastClock.Instance.OneMinuteIsOver += Instance_OneMinuteIsOver;
            TaskReady += OnReadyTask;
        }



        private void Instance_OneMinuteIsOver(object sender, DateTime e)
        {
            throw new NotImplementedException();
        }

        private void ReadAllArticles()
        {
            string path = MyFile.GetFullNameInApplicationTree("Articles.csv");
            if (!File.Exists(path))
            {
                throw new ArgumentNullException();
            }
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
            string path = MyFile.GetFullFolderNameInApplicationTree("Tasks.csv");
            if (!File.Exists(path))
            {
                throw new ArgumentNullException();
            }
            string[] lines = File.ReadAllLines(path, UTF8Encoding.Default);

            for (int i = 1; i < lines.Length; i++)
            {
                string[] parts = lines[i].Split(';');
                string name = parts[1];
                if (!_guests.ContainsKey(name))
                {
                    Guest guest = new Guest(name);
                    _guests.Add(name,guest);
                }

                Tasks tasks = new Tasks(Convert.ToInt32(parts[0]), name, parts[2], parts[3]);


            }

        }
    }

}
