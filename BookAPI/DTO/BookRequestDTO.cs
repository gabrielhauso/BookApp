using System.ComponentModel.DataAnnotations;

namespace BookAPI.DTO
{
    public class BookRequestDTO
    {
        [Required(ErrorMessage = "Titel krävs")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Författare krävs")]
        public string Author { get; set; } = string.Empty;

        public DateTime PublishedDate { get; set; }
    }
}
