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

            string query = "SELECT * FROM Director WHERE Id = {0}";
            var department = await _context.Departments
                .FromSqlRaw(query, id)
                .AsNoTracking()
                .FirstAsync();
            if (department == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound, "Director is not found");
            }

            var workerToDelete = await _context.Workers.FindAsync(id);
            _context.Workers.Remove(workerToDelete);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Worker>> Get()
        {
            return await _context.Workers.ToListAsync();
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
            if (await _context.Workers.FindAsync(id) == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound, "Worker is not found");
            }
            return await _context.Workers.FindAsync(id);
        }

        public async Task<Worker> Update(int id, Worker worker)
        {

            if (await _context.Workers.FindAsync(id) == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound, "Worker is not found");
            }
            _context.Entry(worker).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return _context.Workers.FirstOrDefault();
        }
    }
}
