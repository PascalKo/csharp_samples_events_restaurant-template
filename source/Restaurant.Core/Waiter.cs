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
        private List<Article> _articles;
        private List<Tasks> _tasks;

        private event EventHandler<DateTime> LogTasks;

        internal List<Article> Articles { get => _articles; set => _articles = value; }
        internal List<Tasks> Tasks { get => _tasks; set => _tasks = value; }
        public Waiter()
        {
            Articles = new List<Article>();
            Tasks = new List<Tasks>();
        }

        public void ReadAllArticlesAndTasks()
        {
            string[] lines = ReadLinesFromCsvFile("Articles.csv");
            for (int i = 0; i < lines.Length; i++)
            {
                string[] path = lines[i].Split(';');
                Article article = new Article(path[0], Convert.ToDouble(path[1]), Convert.ToInt32(path[2]));
                _articles.Add(article);
            }

            lines = ReadLinesFromCsvFile("Task.csv");
            for (int i = 0; i < lines.Length; i++)
            {
                string[] path = lines[i].Split(';');
                Tasks tasks = new Tasks(Convert.ToInt32(path[0]), path[1], path[2], path[3]);
                _tasks.Add(tasks);
            }

        }

        private string[] ReadLinesFromCsvFile(string filename)
        {
            string path = MyFile.GetFullNameInApplicationTree(filename);
            if (!File.Exists(path))
            {
                return null;
            }
            string[] lines = File.ReadAllLines(path, Encoding.UTF8);
            return lines;
        }
    }

}
