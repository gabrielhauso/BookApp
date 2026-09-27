using BookAPI.DTO;
using BookAPI.Models;
using BookAPI.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]

        public async Task<ActionResult<List<BookResponseDTO>>> GetAll()
        {
            var allBooks = await _bookService.GetAllAsync();

            var bookDto = allBooks.Select(book => new BookResponseDTO
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                PublishedDate = book.PublishedDate
            }).ToList();

            return Ok(bookDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BookResponseDTO>> GetById(int id)
        {
            var book = await _bookService.GetByIdAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            var bookDto = new BookResponseDTO
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                PublishedDate = book.PublishedDate
            };

            return Ok(bookDto);
        }

        [HttpPost]

        public async Task<ActionResult<BookResponseDTO>> Create([FromBody] BookRequestDTO dto)
        {
            var book = await _bookService.CreateAsync(dto);

            var bookDto = new BookResponseDTO
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                PublishedDate = book.PublishedDate
            };

            return CreatedAtAction(nameof(GetById), new { id = book.Id }, bookDto);

        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BookResponseDTO>> Update(int id, [FromBody] BookRequestDTO dto)
        {
            var book = await _bookService.UpdateAsync(id, dto);

            if (book == null)
            {
                return NotFound();
            }

            var bookDto = new BookResponseDTO
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                PublishedDate = book.PublishedDate
            };

            return Ok(book);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _bookService.DeleteAsync(id);

            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
     }
}
