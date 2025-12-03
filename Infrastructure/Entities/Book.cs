namespace Infrastructure.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public int EditorialId { get; set; }
        public string Title { get; set; } = null!;
        public string Synopsis { get; set; } = null!;
        public int PageCount { get; set; }

        public Editorial Editorial { get; set; } = null!;
        public ICollection<AuthorBook>? AuthorBooks { get; set; }
    }
}
