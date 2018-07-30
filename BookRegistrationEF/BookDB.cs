using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookRegistrationEF
{
    public static class BookDB
    {
        public static List<Book> GetAllBooks()
        {
            BookContext context = new BookContext();
            List<Book> allBooks =
                (from b in context.Book
                 select b).ToList();

            return allBooks;
        }

        /// <summary>
        /// Adds a book to the database
        /// </summary>
        /// <param name="b">The book to be added</param>
        public static void Add(Book b)
        {
            BookContext context = new BookContext();

            // assume book is valid
            context.Book.Add(b);

            // You have to call savechanges
            context.SaveChanges();
        }

        /*
         * EF will track an object if you pull it
         * out of the database and then do modifacations
         * */
        public static void Update(Book b)
        {
            var context = new BookContext();

            // Get book from database
            Book originalBook = context.Book.Find(b.ISBN);

            originalBook.Price = b.Price;
            originalBook.Title = b.Title;

            context.SaveChanges();
        }

        public static void UpdateAlternate(Book b)
        {
            var context = new BookContext();

            // Add Book object to current context
            context.Book.Add(b);

            // Let EF know the book already exists
            context.Entry(b).State = EntityState.Modified;

            context.SaveChanges();
        }

        public static void Delete(Book b)
        {
            var context = new BookContext();
            context.Book.Add(b);

            //  
            context.Entry(b).State = EntityState.Deleted;

            context.SaveChanges();
        }

        // Connected scenario where the DB context
        // tracks entities in memory
        public static void Delete(string isbn)
        {
            var context = new BookContext();

            // Pull book from DB to make EF track it
            Book bookToDelete = context.Book.Find(isbn);

            // Mark book as deleted
            context.Book.Remove(bookToDelete);

            // Send delete query to DB
            context.SaveChanges();
        }
    }
}
