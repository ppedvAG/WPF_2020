using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace HalloItemsControl
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

        private void LadeTestDaten(object sender, RoutedEventArgs e)
        {
            var faker = new Faker<Person>("de");
            faker.RuleFor(x => x.Name, f => f.Name.FullName());
            faker.RuleFor(x => x.GebDatum, f => f.Date.Past(30));
            faker.RuleFor(x => x.Stadt, f => f.Address.City());
            faker.RuleFor(x => x.Abteilung, f => f.Commerce.Department());

            var daten = new List<Person>();
            for (int i = 0; i < 100; i++)
            {
                var p = faker.Generate();
                p.Id = i;
                daten.Add(p);
            }

            myGrid.ItemsSource = daten;

            lb1.ItemsSource = daten;
            lb1.DisplayMemberPath = "Name";

            cb1.Items.Clear();

            foreach (var item in daten.GroupBy(x => x.Abteilung))
            {
                cb1.Items.Add(item.Key);
            }
        }

        private void ShowSelectedDepartment(object sender, RoutedEventArgs e)
        {
            //pattern-matching
            if (cb1.SelectedItem is string sel)
            {
                MessageBox.Show(sel);
            }

            //boxing (old-school)
            //string selectedAbteilung = cb1.SelectedItem as string;
            //if (selectedAbteilung != null)
            //{
            //    MessageBox.Show(selectedAbteilung);
            //}
        }

        private void ListBoxShowPerson(object sender, RoutedEventArgs e)
        {
            if (lb1.SelectedItem is Person p)
                MessageBox.Show($"{p.Name}: {p.Abteilung}");
        }

        private void DataGridShowPersonen(object sender, RoutedEventArgs e)
        {
            if (myGrid.SelectedItems.Count > 0)
                MessageBox.Show(string.Join(", ", myGrid.SelectedItems.Cast<Person>().Select(x => x.Name)));
        }
    }

    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Abteilung { get; set; }
        public string Stadt { get; set; }
        public DateTime GebDatum { get; set; }
    }
}
