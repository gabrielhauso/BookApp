using BookAPI.Data;
using BookAPI.DTO;
using BookAPI.Models;
using BookAPI.Service.Interface;
using Microsoft.EntityFrameworkCore;

namespace BookAPI.Service
{
    public class QuoteService : IQuoteService
    {
        private readonly AppDbContext _dbContext;

        public QuoteService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Quote>> GetAllAsync(int userId)
        {
            return await _dbContext.Quotes
                .Where(q => q.UserId == userId)
                .ToListAsync();
        }

        public async Task<Quote> CreateAsync(QuoteRequestDTO dto, int userId)
        {
            var quote = new Quote
            {
                Text = dto.Text,
                Author = dto.Author,
                UserId = userId
            };

            _dbContext.Quotes.Add(quote);
            await _dbContext.SaveChangesAsync();
            return quote;
        }

        public async Task<Quote?> UpdateAsync(int id, QuoteRequestDTO dto, int userId)
        {
            var quote = await _dbContext.Quotes
                .FirstOrDefaultAsync(q => q.Id == id && q.UserId == userId);

            if (quote == null)
            {
                return null;
            }

            quote.Text = dto.Text;
            quote.Author = dto.Author;

            await _dbContext.SaveChangesAsync();
            return quote;
        }

        public async Task<bool> DeleteAsync(int id, int userId)
        {
            var quote = await _dbContext.Quotes
                .FirstOrDefaultAsync(q => q.Id == id && q.UserId == userId);

            if (quote == null)
            {
                return false;
            }

            _dbContext.Quotes.Remove(quote);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
