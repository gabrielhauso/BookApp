using BookAPI.DTO;
using BookAPI.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BookAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class QuoteController : ControllerBase
    {
        private readonly IQuoteService _quoteService;

        public QuoteController(IQuoteService quoteService)
        {
            _quoteService = quoteService;
        }

        [HttpGet]

        public async Task<ActionResult<List<QuoteResponseDTO>>> GetAll()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var quotes = await _quoteService.GetAllAsync(userId);

            var quoteDto = quotes.Select(quote => new QuoteResponseDTO
            {
                Id = quote.Id,
                Text = quote.Text,
                Author = quote.Author
            }).ToList();

            return Ok(quoteDto);
        }

        [HttpPost]
        
        public async Task<ActionResult<QuoteResponseDTO>> Create([FromBody] QuoteRequestDTO dto)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var quote = await _quoteService.CreateAsync(dto, userId);

            var quoteDto = new QuoteResponseDTO
            {
                Id = quote.Id,
                Text = quote.Text,
                Author = quote.Author
            };

            return StatusCode(201, quoteDto);
        }

        [HttpPut("{id}")]

        public async Task<ActionResult<QuoteResponseDTO>> Update(int id, [FromBody] QuoteRequestDTO dto)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var quote = await _quoteService.UpdateAsync(id, dto, userId);

            if (quote == null)
            {
                return NotFound();
            }

            var quoteDto = new QuoteResponseDTO
            {
                Id = quote.Id,
                Text = quote.Text,
                Author = quote.Author
            };

            return Ok(quoteDto);
        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> Delete(int id)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var deleted = await _quoteService.DeleteAsync(id, userId);

            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
