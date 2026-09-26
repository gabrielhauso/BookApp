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
        private readonly ITokenService _tokenService;

        public AuthController(IAuthService authService, ITokenService tokenService)
        {
            _authService = authService;
            _tokenService = tokenService;
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

        [HttpPost("Login")]

        public async Task<ActionResult<LoginResponseDTO>> Login([FromBody] LoginRequestDTO dto)
        {
            var user = await _authService.LoginAsync(dto.Username, dto.Password);

            if (user == null)
            {
                return Unauthorized("Fel användarnamn eller lösenord");
            }

            return Ok(new LoginResponseDTO
            {
                Token = _tokenService.GenerateToken(user),
                Username = user.Username
            });
        }
    }
}
