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
    }
}
