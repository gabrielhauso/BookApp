using BookAPI.Models;

namespace BookAPI.Service.Interface
{
    public interface IAuthService
    {
        Task<User?> RegisterAsync(string username, string password);
        Task<User?> LoginAsync(string username, string password);
    }
}
