using System;
using System.Collections.Generic;

namespace ppedv.BooksTracker.Model
{
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


}
