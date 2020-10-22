using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ppedv.BooksTracker.Model
{
    public abstract class Entity
    {
        public int Id { get; set; }
        public DateTime Modified { get; set; } = DateTime.Now;
        public DateTime Created { get; set; } = DateTime.Now;
    }

    public class Book : Entity
    {
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public DateTime Published { get; set; }
        public string Beschreibung { get; set; }
        public string ISBN { get; set; }
        public decimal Preis { get; set; }
        public virtual Genre Genre { get; set; }
        public virtual ICollection<Author> Authoren { get; set; } = new HashSet<Author>();

    }

    public class Genre : Entity
    {
        public string Name { get; set; }
        public virtual ICollection<Book> Books { get; set; } = new HashSet<Book>();
    }

    public class Author : Entity
    {
        public string Name { get; set; }
        public virtual ICollection<Book> Books { get; set; } = new HashSet<Book>();
    }


}
