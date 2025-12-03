using System.ComponentModel.DataAnnotations;

namespace NacionalLibrary_Application.Models
{
    public class BookModel
    {
        public int Id { get; set; }
        public int EditorialId { get; set; }

        [Required]
        public string Title { get; set; } = null!;
        public string Synopsis { get; set; } = null!;
        public int PageCount { get; set; }

        public EditorialModel? Editorial { get; set; }

        public int[] SelectedAuthorIds { get; set; } = Array.Empty<int>();
        public ICollection<BookAuthorModel> BookAuthors { get; set; } = new List<BookAuthorModel>();
    }
}
