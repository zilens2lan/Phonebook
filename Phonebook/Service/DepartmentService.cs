using Microsoft.EntityFrameworkCore;
using Phonebook.Data;
using Phonebook.Domain.Services;
using Phonebook.Models;

namespace Phonebook.Service
{
    public class DepartmentService : IDepartmentService
    {
        private readonly DirectorsDBContext _context;
        public DepartmentService(DirectorsDBContext context) 
        {
            _context = context ?? throw new ArgumentNullException(nameof(_context));
        }

        public async Task<Department> Create(Department department)
        {
            await _context.Departments.AddAsync(department);
            await _context.SaveChangesAsync();
            return department;
        }

        public async Task<Department> Delete(int id)
        {
            var departmentToDelete = await _context.Departments.FindAsync(id);
            _context.Departments.Remove(departmentToDelete);
            await _context.SaveChangesAsync();
            return departmentToDelete;
        }

        public async Task<IEnumerable<Department>> Get()
        {
            return await _context.Departments.ToListAsync();
        }

        public async Task<IEnumerable<Department>> GetByDirectorId(int directorId)
        {
            string query = "SELECT * FROM Departments WHERE DirectorId = {0}";
            var departments = await _context.Departments
                .FromSqlRaw(query, directorId)
                .AsNoTracking()
                .ToListAsync();
            return departments;
        }

        public async Task<Department> GetById(int id)
        {
            return await _context.Departments.FindAsync(id);
        }

        public async Task<Department> Update(Department department)
        {
            _context.Entry(department).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return _context.Departments.FirstOrDefault();
        }
    }
}
