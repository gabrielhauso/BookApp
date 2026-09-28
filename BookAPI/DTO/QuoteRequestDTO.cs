using System.ComponentModel.DataAnnotations;

namespace BookAPI.DTO
{
    public class QuoteRequestDTO
    {
        [Required(ErrorMessage = "Citat krävs")]
        public string Text { get; set; } = string.Empty;

        [Required(ErrorMessage = "författare krävs")]
        public string Author { get; set; } = string.Empty;
    }
}
