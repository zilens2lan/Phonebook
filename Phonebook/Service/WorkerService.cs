using Microsoft.EntityFrameworkCore;
using Phonebook.Data;
using Phonebook.Domain.Services;
using Phonebook.Exceptions;
using Phonebook.Models;
using System.Net;

namespace Phonebook.Service
{
    public class WorkerService : IWorkerService
    {
        private readonly DirectorsDBContext _context;
        public WorkerService(DirectorsDBContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(_context));
        }

        public async Task<Worker> Create(Worker worker)
        {
            if(worker == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest, "Worker is null");
            }
            await _context.Workers.AddAsync(worker);
            await _context.SaveChangesAsync();
            return worker;
        }

        public async Task<bool> Delete(int id)
        {
            string query = "SELECT * FROM Workers WHERE Id = {0}";
            var worker = await _context.Workers
                .FromSqlRaw(query, id)
                .AsNoTracking()
                .FirstAsync();
            if (worker == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound, "Worker is not found");
            }
            _context.Workers.Remove(worker);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Worker>> Get()
        {
            string selectQuery = "SELECT * FROM Workers";
            var workers = await _context.Workers
                .FromSqlRaw(selectQuery)
                .AsNoTracking()
                .ToListAsync();
            return workers;
        }

        public async Task<IEnumerable<Worker>> GetByDepartmentId(int departmentId)
        {
            string query = "SELECT * FROM Workers WHERE DepartmentId = {0}";
            var workers = await _context.Workers
                .FromSqlRaw(query, departmentId)
                .AsNoTracking()
                .ToListAsync();
            if (workers.Count == 0)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound, "Workers is not found");
            }
            return workers;
        }

        public async Task<Worker> GetById(int id)
        {
            string selectQuery = "SELECT * FROM Workers WHERE id = {0}";
            var worker = await _context.Workers
                .FromSqlRaw(selectQuery, id)
                .AsNoTracking()
                .FirstAsync();
            if (worker == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound, "Worker is not found");
            }
            return worker;
        }

        public async Task<Worker> Update(int id, Worker worker)
        {
            string selectQuery = "SELECT * FROM Workers WHERE id = {0}";
            var select = await _context.Workers
                .FromSqlRaw(selectQuery, id)
                .AsNoTracking()
                .FirstAsync();
            if (select == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound, "Worker is not found");
            }
            _context.Entry(worker).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return _context.Workers.FirstOrDefault(worker);
        }
    }
}
