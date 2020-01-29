using Restaurant.Core;
using System;
using System.Text;

namespace Restaurant.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MetroWindow_Initialized(object sender, EventArgs e)
        {
            FastClock.Instance.Time = DateTime.Parse("12:00");
            FastClock.Instance.Factor = 360;
            FastClock.Instance.OneMinuteIsOver += Instance_OneMinuteIsOver;
            Waiter waiter = new Waiter(OnReadyTask);
            FastClock.Instance.IsRunning = true;
        }

        protected virtual void OnReadyTask(object sender, string text)
        {
            StringBuilder sb = new StringBuilder(TextBlockLog.Text);
            sb.Append("\n");
            sb.Append($"{FastClock.Instance.Time.ToShortTimeString()}" + "\t" + text);
            TextBlockLog.Text = sb.ToString();
        }

        protected virtual void Instance_OneMinuteIsOver(object sender, DateTime e)
        {
            Title = $"RESTAURANTSIMULATION, {FastClock.Instance.Time.ToShortTimeString()}";
        }
    }
}
