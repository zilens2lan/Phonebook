using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Phonebook.Data;
using Phonebook.Domain.Services;
using Phonebook.Models;

namespace Phonebook.Service
{
    public class DirectorService : IDirectorService
    {
        private readonly DirectorsDBContext _context;
        public DirectorService(DirectorsDBContext context) => _context = context ?? throw new ArgumentNullException(nameof(context));

        public async Task<IEnumerable<Director>> Get()
        {
            return await _context.Directors.ToListAsync();
        }

        public async Task<Director> GetById(int id)
        {
            return await _context.Directors.FindAsync(id);
        }

        public async Task<Director> Create(Director director)
        {
            await _context.Directors.AddAsync(director);
            await _context.SaveChangesAsync();
            return director;
        }

        public async Task<Director> Update(int id, Director director)
        {
            _context.Entry(director).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return _context.Directors.FirstOrDefault();
        }

        public async Task<Director> Delete(int id)
        {
            var directorToDelete = await _context.Directors.FindAsync(id);
            _context.Directors.Remove(directorToDelete);
            await _context.SaveChangesAsync();
            return directorToDelete;
        }
    }
}
