using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ppedv.BooksTracker.UI.WPF.View
{
    /// <summary>
    /// Interaction logic for GeburtstagsControl.xaml
    /// </summary>
    public partial class GeburtstagsControl : UserControl
    {
        public DateTime Age
        {
            get { return (DateTime)GetValue(AgeProperty); }
            set { SetValue(AgeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AgeProperty = DependencyProperty.Register("Age", typeof(DateTime), typeof(GeburtstagsControl),
            new PropertyMetadata(new DateTime(2000, 1, 1), CallBack));

        private static void CallBack(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((GeburtstagsControl)d).UpdateUI();
        }

        private void UpdateUI()
        {
            if (Age.Date == DateTime.Now.Date)
            {
                notBDayEllipse.Visibility = Visibility.Hidden;
                cakeImg.Visibility = Visibility.Visible;
            }
            else
            {
                notBDayEllipse.Visibility = Visibility.Visible;
                cakeImg.Visibility = Visibility.Hidden;
            }
        }

        public GeburtstagsControl()
        {
            InitializeComponent();
        }
    }
}
