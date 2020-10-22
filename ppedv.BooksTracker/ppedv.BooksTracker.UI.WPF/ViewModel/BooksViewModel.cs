using ppedv.BooksTracker.Logic;
using ppedv.BooksTracker.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ppedv.BooksTracker.UI.WPF.ViewModel
{
    class BooksViewModel
    {
        Core core = new Core();

        public ObservableCollection<Book> Booklist { get; set; } = new ObservableCollection<Book>();

        public Book SelectedBook { get; set; }

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
