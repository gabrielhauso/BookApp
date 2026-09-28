using BookAPI.Data;
using BookAPI.Models;
using BookAPI.Service.Interface;
using Microsoft.EntityFrameworkCore;

namespace BookAPI.Service
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _dbContext;

        public AuthService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<User?> RegisterAsync(string username, string password)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Username == username);

            if (user != null)
            {
                return null;
            }

            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

            var newUser = new User
            {
                Username = username,
                PasswordHash = hashedPassword
            };

            _dbContext.Users.Add(newUser);
            await _dbContext.SaveChangesAsync();

            var startQuotes = new List<Quote>
            {
                new() { Text = "Citat 1", Author = "Person 1", UserId = newUser.Id },
                new() { Text = "Citat 2", Author = "Person 2", UserId = newUser.Id },
                new() { Text = "Citat 3", Author = "Person 3", UserId = newUser.Id },
                new() { Text = "Citat 4", Author = "Person 4", UserId = newUser.Id },
                new() { Text = "Citat 5", Author = "Person 5", UserId = newUser.Id }
            };


            _dbContext.Quotes.AddRange(startQuotes);
            await _dbContext.SaveChangesAsync();

            return newUser;
        }

        public async Task<User?> LoginAsync(string username, string password)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Username == username);

            if (user == null)
            {
                return null;
            }

            bool passwordValid = BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);

            if (!passwordValid)
            {
                return null;
            }

            return user;
        }

    }
}
