using ppedv.BooksTracker.Model;
using System.Data.Entity;

namespace ppedv.BooksTracker.Data.EF
{
    public class EfContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Genre> Genres { get; set; }

        public EfContext(string conString) : base(conString)
        { }

        //public EfContext() : this("Server=(localdb)\\mssqllocaldb;Database=BooksTracker;Trusted_Connection=true;")

        public EfContext() : this("Server=tcp:einetolledb.database.windows.net,1433;Initial Catalog=BookTracker_Andre;Persist Security Info=False;User ID=Fred;Password=Hallo1234;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<EfContext, Migrations.Configuration>());
        }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }
    }
}
