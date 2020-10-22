using ppedv.BooksTracker.Logic;
using ppedv.BooksTracker.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace ppedv.BooksTracker.UI.WPF.ViewModel
{
    class BooksViewModel : INotifyPropertyChanged
    {
        Core core = new Core();
        private Book selectedBook;

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<Book> Booklist { get; set; } = new ObservableCollection<Book>();

        public Book SelectedBook
        {
            get => selectedBook;
            set
            {
                selectedBook = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedBook)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Alter)));
            }
        }

        public string Alter
        {
            get
            {
                if (SelectedBook == null)
                    return "---";

                // Save today's date.
                var today = DateTime.Today;

                // Calculate the age.
                var age = today.Year - SelectedBook.Published.Year;

                // Go back to the year in which the person was born in case of a leap year
                if (SelectedBook.Published.Date > today.AddYears(-age)) age--;

                return $"Alter: {age}";
            }
        }

        public SaveCommand Save { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand NewCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        public BooksViewModel()
        {
            Booklist = new ObservableCollection<Book>(core.Repository.GetAll<Book>());

            Save = new SaveCommand(core);

            SaveCommand = new RelayCommand(o => core.Repository.SaveAll());
            DeleteCommand = new RelayCommand(o =>
            {
                if (SelectedBook != null)
                {
                    core.Repository.Delete(SelectedBook);
                    Booklist.Remove(SelectedBook);
                }
            });

            NewCommand = new RelayCommand(UserWantsToAddANewBook);
        }

        private void UserWantsToAddANewBook(object obj)
        {
            var newBook = new Book() { Title = "NEU", Published = DateTime.Now };

            core.Repository.Add(newBook);

            Booklist.Add(newBook);
        }
    }

    class SaveCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        Core core;

        public SaveCommand(Core core)
        {
            this.core = core;
        }

        public void Execute(object parameter)
        {
            core.Repository.SaveAll();
        }
    }
}
