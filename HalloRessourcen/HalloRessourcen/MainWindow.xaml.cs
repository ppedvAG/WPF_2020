using System.Windows;
using System.Windows.Media;

namespace HalloRessourcen
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

        private void ChangeColor(object sender, RoutedEventArgs e)
        {
            this.Resources["myColor"] = new SolidColorBrush(Colors.Pink);
        }
    }
}
