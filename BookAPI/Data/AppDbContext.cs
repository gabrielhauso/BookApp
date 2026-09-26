using BookAPI.Models;
using Microsoft.EntityFrameworkCore;


namespace BookAPI.Data
{
    public class AppDbContext : DbContext
    {

        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Quote> Quotes { get; set; }

       
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {

        }

      
    }
}
