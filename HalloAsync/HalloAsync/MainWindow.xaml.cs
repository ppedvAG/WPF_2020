using Microsoft.Win32;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace HalloAsync
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void StartOhneThread(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 100; i++)
            {
                pb1.Value = i;
                Thread.Sleep(200);
            }
        }

        private void StartTask(object sender, RoutedEventArgs e)
        {
            Task.Run(() =>
            {
                for (int i = 0; i < 100; i++)
                {
                    pb1.Dispatcher.Invoke(() => pb1.Value = i);
                    Thread.Sleep(200);
                }
            });
        }

        CancellationTokenSource cts;

        private void StartTaskMitTS(object sender, RoutedEventArgs e)
        {
            var b = (Button)sender;
            b.IsEnabled = false;

            cts = new CancellationTokenSource();

            var ts = TaskScheduler.FromCurrentSynchronizationContext();

            Task.Run(() =>
            {
                for (int i = 0; i <= 100; i++)
                {
                    Task.Factory.StartNew(() => pb1.Value = i, cts.Token, TaskCreationOptions.None, ts);
                    Thread.Sleep(20);
                    if (cts.IsCancellationRequested)
                        break;//cleanup
                }
                Task.Factory.StartNew(() => b.IsEnabled = !false, CancellationToken.None, TaskCreationOptions.None, ts);
            });
        }

        private void Abbrechen(object sender, RoutedEventArgs e)
        {
            cts?.Cancel();
        }

        private async void StartAA(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 100; i++)
            {
                pb1.Value = i;
                await Task.Delay(200);
                //var sr = new StreamReader("datei.txt");
                //string line = await sr.ReadLineAsync();
            }
        }

        private async void StartDB(object sender, RoutedEventArgs e)
        {
            pb1.IsIndeterminate = true;
            var b = (Button)sender;
            b.IsEnabled = false;

            var con = new SqlConnection("Server=(localdb)\\mssqllocaldb;Database=NORTHWND;Trusted_Connection=true");
            await con.OpenAsync();
            var cmd = con.CreateCommand();
            cmd.CommandText = "WAITFOR DELAY '00:00:10'; SELECT COUNT(*) FROM Employees";
            var result = await cmd.ExecuteScalarAsync();
            MessageBox.Show($"Employees found: {result}");

            pb1.IsIndeterminate = !true;
            b.IsEnabled = !false;
        }

        private async void StartAlt(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"{await AlteMethodeAsync(245)}");
        }

        public Task<long> AlteMethodeAsync(int zahl)
        {
            return Task.Run(() => AlteMethode(zahl));
        }

        public long AlteMethode(int zahl)
        {
            Thread.Sleep(5000);
            return zahl * 12 / 2 * 6745;
        }
    }
}
