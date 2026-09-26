using System.ComponentModel.DataAnnotations;

namespace BookAPI.DTO
{
    public class RegisterRequestDTO
    {
        [Required(ErrorMessage = "Användarnamn krävs")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Lösenord krävs")]
        public string Password { get; set; } = string.Empty;
    }
}
