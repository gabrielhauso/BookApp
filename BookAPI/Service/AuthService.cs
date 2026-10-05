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
                new() { Text = "Jag tänker, alltså finns jag", Author = "René Descartes", UserId = newUser.Id },
                new() { Text = "Kunskap är makt", Author = "Francis Bacon", UserId = newUser.Id },
                new() { Text = "Det enda jag vet är att jag ingenting vet", Author = "Sokrates", UserId = newUser.Id },
                new() { Text = "Premature optimization is the root of all evil", Author = "Donald Knuth", UserId = newUser.Id },
                new() { Text = "Talk is cheap. Show me the code", Author = "Linus Torvalds", UserId = newUser.Id }
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
