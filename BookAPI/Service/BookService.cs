using BookAPI.Data;
using BookAPI.DTO;
using BookAPI.Models;
using BookAPI.Service.Interface;
using Microsoft.EntityFrameworkCore;

namespace BookAPI.Service
{
    public class BookService : IBookService
    {
        private readonly AppDbContext _dbContext;

        public BookService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Book>> GetAllAsync()
        {
            return await _dbContext.Books.ToListAsync();
        }

        public async Task<Book?> GetByIdAsync(int id)
        {
            return await _dbContext.Books.FindAsync(id);
        }

        public async Task<Book> CreateAsync(BookRequestDTO dto)
        {
            var book = new Book
            {
                Title = dto.Title,
                Author = dto.Author,
                PublishedDate = dto.PublishedDate,
            };

            _dbContext.Books.Add(book);
            await _dbContext.SaveChangesAsync();
            return book;
        }

        public async Task<Book?> UpdateAsync(int id, BookRequestDTO dto)
        {
            var book = await _dbContext.Books.FindAsync(id);

            if (book == null)
            {
                return null;
            }

            book.Title = dto.Title;
            book.Author = dto.Author;
            book.PublishedDate = dto.PublishedDate;

            await _dbContext.SaveChangesAsync();
            return book;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var book = await _dbContext.Books.FindAsync(id);

            if (book == null)
            {
                return false;
            }

            _dbContext.Books.Remove(book);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
