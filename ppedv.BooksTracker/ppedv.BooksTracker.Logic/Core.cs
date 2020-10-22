using ppedv.BooksTracker.Model;
using ppedv.BooksTracker.Model.Contracts;
using System.Linq;

namespace ppedv.BooksTracker.Logic
{
    public class Core
    {
        public IRepository Repository { get; private set; }

        public Core(IRepository repo) //di
        {
            Repository = repo;
        }

        public Core() : this(new Data.EF.EfRepository())
        { }

        public Book GetMostExpensiveBook()
        {
            return Repository.GetAll<Book>().OrderByDescending(x => x.Preis).FirstOrDefault();
        }
    }
}
