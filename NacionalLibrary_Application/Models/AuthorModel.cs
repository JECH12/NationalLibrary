namespace NacionalLibrary_Application.Models
{
    public class AuthorModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string LastName { get; set; } = null!;

        public ICollection<BookAuthorModel> BookAuthors { get; set; } = new List<BookAuthorModel>();

        public string FullName => $"{Name} {LastName}";
    }
}
