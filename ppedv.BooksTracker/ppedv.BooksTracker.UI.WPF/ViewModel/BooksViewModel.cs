using ppedv.BooksTracker.Logic;
using ppedv.BooksTracker.Model;
using System.Collections.Generic;

namespace ppedv.BooksTracker.UI.WPF.ViewModel
{
    class BooksViewModel
    {
        Core core = new Core();

        public List<Book> Booklist { get; set; } = new List<Book>();

        public BooksViewModel()
        {
            Booklist = new List<Book>(core.Repository.GetAll<Book>());
        }

    }
}
