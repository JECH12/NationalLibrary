using Infrastructure.Context;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces;

namespace Services.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly LibraryContext _context;

        public AuthorService(LibraryContext context)
        {
            _context = context;
        }
        public Task<Author> CreateAsync(Author entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Author>> GetAllAsync()
        {
            return await _context.Authors.ToListAsync();
        }

        public Task<Author?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Author> UpdateAsync(Author entity)
        {
            throw new NotImplementedException();
        }
    }
}
