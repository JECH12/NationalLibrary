namespace NacionalLibrary_Application.Models
{
    public class EditorialModel
    {
        public int Id { get; set; }
        public string? Name { get; set; } = null!;
        public string Headquarters { get; set; } = null!;

        public ICollection<BookModel> Books { get; set; } = new List<BookModel>();
    }
}
