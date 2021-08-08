using System.ComponentModel.DataAnnotations;

namespace Books.Models
{
    // internal representation of book object
    public class Book
    {
        [Key]
        public int Id { get; set; }

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