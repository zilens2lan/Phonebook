

using Microsoft.EntityFrameworkCore;
using Phonebook.Data;
using Phonebook.Domain.Services;
using Phonebook.Exceptions;
using Phonebook.Models;
using System.IO;
using System.Net;

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
            if(department == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest, "Department is null");
            }
            //string query = "INSERT INTO VALEUES "
            //await _context.Departments.ExecuteSqlRaw(query);
            await _context.Departments.AddAsync(department);
            await _context.SaveChangesAsync();
            return department;
        }

        public async Task<bool> Delete(int id)
        {
            string selectQuery = "SELECT * FROM Departments WHERE Id = {0}";
            var department = await _context.Departments
                .FromSqlRaw(selectQuery, id)
                .AsNoTracking()
                .FirstAsync();
            if (department == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound, "Department is not found");
            }
            string deleteQuery = "DELETE FROM Departments WHERE Id = {0}";
            await _context.Departments.ExecuteSqlRaw(deleteQuery, id);

            //var departmentToDelete = await _context.Departments.FindAsync(id);
            //_context.Departments.Remove(departmentToDelete);
            //await _context.SaveChangesAsync();
            return true;
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
            if(departments.Count == 0) 
            {
                throw new HttpResponseException(HttpStatusCode.NotFound, "Departments is not found");
            }
            return departments;
        }

        public async Task<Department> GetById(int id)
        {
            if (await _context.Departments.FindAsync(id) == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound, "Department is not found");
            }
            string query = "SELECT * FROM Departments WHERE id = {0}";
            var departments = await _context.Departments
                .FromSqlRaw(query, id)
                .AsNoTracking()
                .ToListAsync();
            return departments[0];
        }

        public async Task<Department> Update(int id, Department department)
        {
            if(id == 0)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound, "Department is not found");
            }
            _context.Entry(department).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return await _context.Departments.FirstOrDefaultAsync();
        }
    }
}
