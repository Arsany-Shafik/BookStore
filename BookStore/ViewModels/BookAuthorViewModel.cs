using System.ComponentModel.DataAnnotations;
using BookStore.Models;

namespace BookStore.ViewModels
{
    public class BookAuthorViewModel
    {
        public int BookId { get; set; }

        [Required]
        [StringLength(120, MinimumLength = 5)]
        public string? Title { get; set; }

        [Required]
        [StringLength(120,MinimumLength =5)]
        public string? Description { get; set; }

        public int AuthorId { get; set; }
        public IFormFile? ImageUrl { get; set; }
        public string? ImageEdit { get; set; }
        public List<Author>? Authors { get; set; }
    }
}
