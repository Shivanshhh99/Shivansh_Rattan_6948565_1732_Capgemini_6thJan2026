using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [Required]
        public string Author { get; set; }

        [Range(1800, 2100)]
        public int PublishedYear { get; set; }

        [Required]
        public string Genre { get; set; }
    }
}