using BookAPI.DTO;
using BookAPI.Models;

namespace BookAPI.Service.Interface
{
    public interface IQuoteService
    {
        Task<List<Quote>> GetAllAsync(int userId);
        Task<Quote> CreateAsync(QuoteRequestDTO dto, int userId);
        Task<Quote?> UpdateAsync(int id, QuoteRequestDTO dto, int userId);
        Task<bool> DeleteAsync(int id, int userId);
    }
}
