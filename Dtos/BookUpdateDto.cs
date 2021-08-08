using System.ComponentModel.DataAnnotations;

namespace Books.Dtos
{
    public class BookUpdateDto
    {
        [Required]
        [MaxLength(250)]
        public string Title { get; set; }

        [Required]
        [MaxLength(250)]
        public string Author { get; set; }

        [Required]
        [MaxLength(250)]
        public string Genre { get; set; }

        [Required]
        [MaxLength(250)]
        public string Publisher { get; set; }
    }
}