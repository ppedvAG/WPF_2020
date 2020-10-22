﻿using System.Collections.Generic;

namespace ppedv.BooksTracker.Model
{
    public class Author : Entity
    {
        public string Name { get; set; }
        public virtual ICollection<Book> Books { get; set; } = new HashSet<Book>();
    }
}
