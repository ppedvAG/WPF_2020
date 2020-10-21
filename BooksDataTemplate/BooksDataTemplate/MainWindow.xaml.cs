using System.IO;
using System.Net.Http;
using System.Windows;

namespace BooksDataTemplate
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

        private async void Search(object sender, RoutedEventArgs e)
        {
            var url = $"https://www.googleapis.com/books/v1/volumes?q={suchTb.Text}";
            var http = new HttpClient();
            var json = await http.GetStringAsync(url);

            jsonTb.Text = json;

        }

        private void SearchOffline(object sender, RoutedEventArgs e)
        {
            var json = File.ReadAllText("BooksOffline.json");

            jsonTb.Text = json;

        }
    }
}
