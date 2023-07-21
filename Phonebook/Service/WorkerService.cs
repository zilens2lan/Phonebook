using Microsoft.EntityFrameworkCore;
using Phonebook.Data;
using Phonebook.Domain.Services;
using Phonebook.Models;

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
            await _context.Workers.AddAsync(worker);
            await _context.SaveChangesAsync();
            return worker;
        }

        public async Task<Worker> Delete(int id)
        {
            var workerToDelete = await _context.Workers.FindAsync(id);
            _context.Workers.Remove(workerToDelete);
            await _context.SaveChangesAsync();
            return workerToDelete;
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
            return workers;
        }

        public async Task<Worker> GetById(int id)
        {
            return await _context.Workers.FindAsync(id);
        }

        public async Task<Worker> Update(Worker worker)
        {
            _context.Entry(worker).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return _context.Workers.FirstOrDefault();
        }
    }
}
