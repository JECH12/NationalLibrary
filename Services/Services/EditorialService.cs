using Infrastructure.Context;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces;

namespace Services.Services
{
    public class EditorialService : IEditorialService
    {
        private readonly LibraryContext _context;

        public EditorialService(LibraryContext context)
        {
            _context = context;
        }

        public async Task<List<Editorial>> GetAllAsync()
            => await _context.Editorials.Include(b => b.Books).ToListAsync();

        public async Task<Editorial?> GetByIdAsync(int id)
            => await _context.Editorials.Include(b => b.Books).FirstOrDefaultAsync(e => e.Id == id);

        public async Task<Editorial> CreateAsync(Editorial entity)
        {
            _context.Editorials.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Editorial> UpdateAsync(Editorial entity)
        {
            _context.Editorials.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var item = await _context.Editorials.FindAsync(id);
            if (item == null) return false;

            _context.Editorials.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
