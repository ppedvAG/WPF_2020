using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ppedv.BooksTracker.Model;

namespace ppedv.BooksTracker.Data.EF.Tests
{
    [TestClass]
    public class EfContextTests
    {

        [TestMethod]
        public void EfContext_can_create_local_db()
        {
            //Arrange
            var con = new EfContext("Server=(localdb)\\mssqllocaldb;Database=BooksTracker;Trusted_Connection=true;");
            if (con.Database.Exists())
                con.Database.Delete();

            //Act
            con.Database.Create();


            //Assert
            Assert.IsTrue(con.Database.Exists());
        }

        [TestMethod]
        public void EfContext_create_tables_on_azure()
        {
            var con = new EfContext();

            con.Database.CreateIfNotExists();

            con.Books.ToList();
        }

        [TestMethod]
        public void EfContext_can_add_book()
        {
            var b = new Book() { Title = "testbuch",Published =DateTime.Now };

            using (var con = new EfContext())
            {
                con.Books.Add(b);
                con.SaveChanges();
            }

            using (var con = new EfContext())
            {
                var loaded = con.Books.Find(b.Id);
                Assert.AreEqual(b.Title, loaded.Title);
            }
        }
    }
}
