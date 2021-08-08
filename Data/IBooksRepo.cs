using System.Collections.Generic;
using Books.Models;

namespace Books.Data
{
    public interface IBooksRepo
    {
        // persist the changes in SQL server
        bool SaveChanges();

        // return all books
        IEnumerable<Book> GetAllBooks();
        // return a book by ID
        Book GetBookById(int id);
        // create a new book
        void CreateBook(Book book);
        // update a book
        void UpdateBook(Book book);
        // delete a book
        void DeleteBook(Book book);
    }
}