using BookAPI.DTO;
using BookAPI.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]

        public async Task<ActionResult<UserResponseDTO>> Register([FromBody] RegisterRequestDTO dto)
        {
            var user = await _authService.RegisterAsync(dto.Username, dto.Password);

            if (user == null)
            {
                return Conflict("Användarnamnet är upptaget");
            }

            return Ok(new UserResponseDTO
            {
                Id = user.Id,
                Username = user.Username
            });
        }
    }
}
