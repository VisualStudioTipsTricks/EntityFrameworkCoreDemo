using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VSTT.EF.DataAccess;
using VSTT.EF.DataAccess.LogProvider.EFLogging;
using VSTT.EFDomain;

namespace VSTT.EF.Test
{
    [TestClass]
	public class MainTest
	{
        [ClassInitialize]
        public static void Setup(TestContext testContext)
        {
            var ctx = new LibraryContext();
            var p = ctx.GetInfrastructure<IServiceProvider>();
            var loggerFactory = p.GetService<ILoggerFactory>();
            loggerFactory.AddProvider(new CustomLogProvider());
        }

        [TestMethod]
        public void Create_Book()
        {        
            using (var ctx = new LibraryContext())
            {
                var book = new Book
                {
                    Title = "Visual Studio 2017 Succinctly",
                    Author = new Author
                    {
                        FirstName = "Alessandro",
                        LastName = "Del Sole"
                    },
                    
                    Isbn = "067233450X",
                    PublishDate = new DateTime(2017, 3, 7)
                };

                ctx.Books.Add(book);
                ctx.SaveChanges();

                Assert.IsTrue(book.Id > 0);
            }           
        }

        [TestMethod]
        public void Create_Book_2()
        {
            using(var ctx = new LibraryContext())
            {
                var book = new Book
                {
                    Title = "Visual Basic 2015 Unleashed",
                    AuthorId = 1,
                    Isbn = "067233450Y",
                    PublishDate = new DateTime(2015, 3, 7)
                };

                ctx.Add<Book>(book);
                ctx.SaveChanges();

                Assert.IsNotNull(book.CreatedDate);
            }
        }

	    [TestMethod]
	    public void Batch_Add()
	    {
	        using (var ctx = new LibraryContext())
	        {
	            ctx.AddRange
	            (
	                new Book {Title = "Title 1", Isbn = "XXXX1", AuthorId = 1},
	                new Book {Title = "Title 2", Isbn = "XXXX2", AuthorId = 1},
	                new Book {Title = "Title 3", Isbn = "XXXX3", AuthorId = 1}
	            );
	            ctx.SaveChanges();
	        }

	    }

	    [TestMethod]
        public void Test_Shadow_property()
        {
            using (var ctx = new LibraryContext())
            {
                var c = ctx.Books.FirstOrDefault();
                var prop = ctx.Entry(c).Property<DateTime>("LastUpdate");
                Assert.AreEqual(new DateTime(2017, 3, 5), prop.CurrentValue);
            }
        }

        [TestMethod]
        public void Test_Raw_Sql()
        {
            using (var ctx = new LibraryContext())
            {
                // Restituisce l'entità Book
                var q1 = ctx.Books.FromSql("SELECT * From dbo.Books");

                // Esempio d'uso con LINQ
                var q2 = ctx.Books
                    .FromSql("SELECT * From dbo.Books")
                    .Where(b => b.Title.StartsWith("a"));

            }
        }
    }
}