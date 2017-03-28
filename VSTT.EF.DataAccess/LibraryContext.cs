
using Microsoft.EntityFrameworkCore;
using System;
using System.Configuration;

using VSTT.EFDomain;

namespace VSTT.EF.DataAccess
{
    public class LibraryContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["LibraryDb"].ConnectionString);
        }

        // nel DbContext
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            // DefaultValue mapping
            modelBuilder
                .Entity<Book>(e =>
                {
                    e.Property<DateTime>("CreatedDate")
                     .HasField("_createdDate")
                     .HasDefaultValueSql("getdate()");

                    e.HasAlternateKey(p => p.Isbn);

                });
        }
    }
}
