using Books.Models;
using Microsoft.EntityFrameworkCore;

namespace Books.Data
{
    public class BooksContext : DbContext
    {
        public BooksContext(DbContextOptions<BooksContext> options) : base(options)
        {
        }

        // represent book objects as a dbset
        public DbSet<Book> Books { get; set; }
    }
}