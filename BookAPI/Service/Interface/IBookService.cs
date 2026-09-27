using BookAPI.DTO;
using BookAPI.Models;

namespace BookAPI.Service.Interface
{
    public interface IBookService
    {
        Task<List<Book>> GetAllAsync();
        Task<Book?> GetByIdAsync(int id);
        Task<Book> CreateAsync(BookRequestDTO dto);
        Task<Book?> UpdateAsync(int id, BookRequestDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
