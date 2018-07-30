using System;
using System.Collections.Generic;
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
            context.Entry(b).State = System.Data.Entity.EntityState.Modified;

            context.SaveChanges();
        }
    }
}
