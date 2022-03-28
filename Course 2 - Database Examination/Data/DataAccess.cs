using FinaltestLibrary.Models;

namespace FinaltestLibrary.Data
{
    public class DataAccess
    {
        Context context = new Context();

        public void RecreateDatabase()
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }

        public void SeedDatabase()
        {
            AddBooks();
            AddLibraryUsers();
            AddWriters();
            AddBookAuthors();
            AddRented();

        }

        public void AddWriters()
        {
            Author writer = new Author();
            writer.FirstName = "Astrid";
            writer.LastName = "Lindgren";

            Author writer2 = new Author();
            writer2.FirstName = "Harper";
            writer2.LastName = "Lee";

            Author writer3 = new Author();
            writer3.FirstName = "Selma";
            writer3.LastName = "Lagerlöf";

            Author writer4 = new Author();
            writer4.FirstName = "Amanda";
            writer4.LastName = "Carlen";

            Author writer5 = new Author();
            writer5.FirstName = "Ada";
            writer5.LastName = "Lovelace";

            context.Authors.Add(writer);
            context.Authors.Add(writer2);
            context.Authors.Add(writer3);
            context.Authors.Add(writer4);
            context.Authors.Add(writer5);

            context.SaveChanges();
        }

        public void AddBooks()
        {
            Book book = new Book();
            book.Title = "Alla barnen på Bullerbyn";
            book.ReleaseYear = 2003;
            book.ISBN = 9789129657548;
            book.Rating = 10;
            book.IsRented = true;

            Book book2 = new Book();
            book2.Title = "Att döda en härmtrast";
            book2.ReleaseYear = 2020;
            book2.ISBN = 9789100168186;
            book2.Rating = 9;
            book2.IsRented = false;

            Book book3 = new Book();
            book3.Title = "Kejsaren av Portugallien";
            book3.ReleaseYear = 2017;
            book3.ISBN = 9789174296006;
            book3.Rating = 8;
            book3.IsRented = false;

            Book book4 = new Book();
            book4.Title = "Att programmera";
            book4.ReleaseYear = 2021;
            book4.ISBN = 9789174296007;
            book4.Rating = 7;
            book4.IsRented = false;

            
            context.Books.Add(book);
            context.Books.Add(book2);
            context.Books.Add(book3);
            context.Books.Add(book4);

            context.SaveChanges();
        }

        public void AddLibraryUsers()
        { 
            LibraryUser libraryUser = new LibraryUser();
            libraryUser.FirstName = "Alice";
            libraryUser.LastName = "Edström";

            LibraryUser libraryUser2 = new LibraryUser();
            libraryUser2.FirstName = "Erika";
            libraryUser2.LastName = "Ellbrant";

            LibraryUser libraryUser3 = new LibraryUser();
            libraryUser3.FirstName = "Rebecka";
            libraryUser3.LastName = "Carlen";

            context.LibraryUsers.Add(libraryUser);
            context.LibraryUsers.Add(libraryUser2);
            context.LibraryUsers.Add(libraryUser3);
            context.SaveChanges();

        }

        public void AddBookAuthors()
        {
            BookAuthor bookAuthor = new BookAuthor();
            bookAuthor.BookID = 1;
            bookAuthor.AuthorID = 4;

            BookAuthor bookAuthor2 = new BookAuthor();
            bookAuthor2.BookID = 2;
            bookAuthor2.AuthorID = 3;

            BookAuthor bookAuthor3 = new BookAuthor();
            bookAuthor3.BookID = 3;
            bookAuthor3.AuthorID = 2;

            BookAuthor bookAuthor4 = new BookAuthor();
            bookAuthor4.BookID = 4;
            bookAuthor4.AuthorID = 5;

            BookAuthor bookAuthor5 = new BookAuthor();
            bookAuthor5.BookID = 4;
            bookAuthor5.AuthorID = 1;

            context.BookAuthors.Add(bookAuthor);
            context.BookAuthors.Add(bookAuthor2);
            context.BookAuthors.Add(bookAuthor3);
            context.BookAuthors.Add(bookAuthor4);
            context.BookAuthors.Add(bookAuthor5);

            context.SaveChanges();


        }
        public void AddRented()
        {
            Rent rent = new Rent();
            rent.BookID = 1;
            rent.LibraryCardID = 1;
            rent.RentedDate = System.DateTime.Now;

            context.Rented.Add(rent);
            context.SaveChanges();


        }
    }

}
