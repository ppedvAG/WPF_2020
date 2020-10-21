using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace HalloBinding
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var timer = new System.Timers.Timer();
            timer.Interval = 500;
            timer.Elapsed += Timer_Elapsed;
            //timer.Start();

            ts = TaskScheduler.FromCurrentSynchronizationContext();
        }

        TaskScheduler ts = null;
        Random ran = new Random();

        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            Task.Factory.StartNew(() =>
            {
                r.Value = ran.Next(255);
                g.Value = ran.Next(255);
                b.Value = ran.Next(255);
            }, CancellationToken.None, TaskCreationOptions.None, ts);

            //this.Dispatcher.Invoke(() =>
            //{
            //    r.Value = ran.Next(255);
            //    g.Value = ran.Next(255);
            //    b.Value = ran.Next(255);
            //});
        }
    }
}
