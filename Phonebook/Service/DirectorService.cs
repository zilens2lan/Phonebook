using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Phonebook.Config;
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
            return await _context.Directors.ToListAsync();
        }

        public async Task<Director> GetById(int id)
        {
            if(id == 0)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound, "Director is not found");
            }
            return await _context.Directors.FindAsync(id);
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
            if(await _context.Directors.FindAsync(id) == null)
            {
                Logger.Error("Director is not found", id);
                throw new HttpResponseException(HttpStatusCode.NotFound, "Director is not found");
            }
            _context.Entry(director).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return _context.Directors.FirstOrDefault();
        }

        public async Task<bool> Delete(int id)
        {
            var directorToDelete = await _context.Directors.FindAsync(id);
            if(directorToDelete == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound, "Director is not found");
            }
            _context.Directors.Remove(directorToDelete);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
