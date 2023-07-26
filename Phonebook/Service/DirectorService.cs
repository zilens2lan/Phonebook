using Microsoft.EntityFrameworkCore;
using Phonebook.Data;
using Phonebook.Domain.Services;
using Phonebook.Exceptions;
using Phonebook.Models;
using System.Net;

namespace Phonebook.Service
{
    public class DirectorService : IDirectorService
    {
        private readonly DirectorsDBContext _context;
        public DirectorService(DirectorsDBContext context) => _context = context ?? throw new ArgumentNullException(nameof(context));

        public async Task<IEnumerable<Director>> Get()
        {
            string selectQuery = "SELECT * FROM Directors";
            var directors = await _context.Directors
                .FromSqlRaw(selectQuery)
                .AsNoTracking()
                .ToListAsync();
            return directors;
        }

        public async Task<Director> GetById(int id)
        {
            string selectQuery = "SELECT * FROM Directors WHERE id = {0}";
            var director = await _context.Directors
                .FromSqlRaw(selectQuery, id)
                .AsNoTracking()
                .FirstAsync();
            if (director == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound, "Department is not found");
            }
            return director;
        }

        public async Task<Director> Create(Director director)
        {
            if(director == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest, "Director is null");
            }
            await _context.Directors.AddAsync(director);
            await _context.SaveChangesAsync();
            return director;
        }

        public async Task<Director> Update(int id, Director director)
        {
            string selectQuery = "SELECT * FROM Directors WHERE id = {0}";
            var select = await _context.Directors
                .FromSqlRaw(selectQuery, id)
                .AsNoTracking()
                .FirstAsync();
            if (select == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound, "Director is not found");
            }
            _context.Entry(director).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return _context.Directors.FirstOrDefault(director);
        }

        public async Task<bool> Delete(int id)
        {
            string selectQuery = "SELECT * FROM Director WHERE Id = {0}";
            var director = await _context.Directors
                .FromSqlRaw(selectQuery, id)
                .AsNoTracking()
                .FirstAsync();
            if(director == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound, "Director is not found");
            }
            _context.Directors.Remove(director);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
