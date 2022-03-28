using FinaltestLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace FinaltestLibrary.Data
{
    public class Context : DbContext
    {
        public Context()
        {
        }

        public Context(DbContextOptions<Context> options) : base (options)
        {
        }

        public DbSet<LibraryUser> LibraryUsers { get; set; }
        public DbSet<Rent> Rented { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<BookAuthor> BookAuthors { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=tcp:amandatestdbserver.database.windows.net,1433;Initial Catalog=FinalTestDB;Persist Security Info=False;User ID=amandatestdbserver;Password=password2022!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookAuthor>().HasKey(bw => new { bw.BookID, bw.AuthorID });

            modelBuilder.Entity<Rent>()
                .HasOne(p => p.Book)
                .WithMany(b => b.Rent)
                .HasForeignKey(p => p.BookID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Rent>()
              .HasOne(p => p.LibraryCard)
              .WithMany(b => b.Rent)
              .HasForeignKey(p => p.LibraryCardID)
              .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<BookAuthor>()
              .HasOne(p => p.Book)
              .WithMany(b => b.BookAuthor)
              .HasForeignKey(p => p.BookID)
              .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<BookAuthor>()
              .HasOne(p => p.Author)
              .WithMany(b => b.BookAuthor)
              .HasForeignKey(p => p.AuthorID)
              .OnDelete(DeleteBehavior.Cascade);


        }



    }
}
