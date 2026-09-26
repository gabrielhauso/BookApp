using BookAPI.Models;

namespace BookAPI.Service.Interface
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }
}
