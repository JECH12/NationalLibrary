using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Context
{
    public class LibraryContext :DbContext
    {
        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options)
        {
        }

        public LibraryContext() { }

        public DbSet<Author> Authors => Set<Author>();
        public DbSet<Book> Books => Set<Book>();
        public DbSet<Editorial> Editorials => Set<Editorial>();
        public DbSet<AuthorBook> AuthorBooks => Set<AuthorBook>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AuthorBook>().HasKey(x => new { x.AuthorId, x.BookId });

            modelBuilder.Entity<AuthorBook>()
                .HasOne(ab => ab.Author)
                .WithMany(a => a.AuthorBooks)
                .HasForeignKey(ab => ab.AuthorId);

            modelBuilder.Entity<AuthorBook>()
                .HasOne(ab => ab.Book)
                .WithMany(b => b.AuthorBooks)
                .HasForeignKey(ab => ab.BookId);

            modelBuilder.Entity<Author>().HasData(
                new Author { Id = 1, Name = "Esteban", LastName="Carrillo" },
                new Author { Id = 2, Name = "Nicolas", LastName = "Gonzalez" },
                new Author { Id = 3, Name = "Cristian", LastName = "Sanchez" }
            );
        }
    }
}
