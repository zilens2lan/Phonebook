using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Phonebook.Data;
using Phonebook.Domain.Services;
using Phonebook.Exceptions;
using Phonebook.Models;
using System.IO;
using System.Linq;
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
            _context.Departments.Remove(department);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Department>> Get()
        {
            string selectQuery = "SELECT * FROM Departments";
            var departments = await _context.Departments
                .FromSqlRaw(selectQuery)
                .AsNoTracking()
                .ToListAsync();
            return departments;
        }

        public async Task<IEnumerable<Department>> GetByDirectorId(int directorId)
        {
            string selectQuery = "SELECT * FROM Departments WHERE DirectorId = {0}";
            var departments = await _context.Departments
                .FromSqlRaw(selectQuery, directorId)
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
            string selectQuery = "SELECT * FROM Departments WHERE id = {0}";
            var department = await _context.Departments
                .FromSqlRaw(selectQuery, id)
                .AsNoTracking()
                .FirstAsync();
            if (department == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound, "Department is not found");
            }
            return department;
        }

        public async Task<Department> Update(int id, Department department)
        {
            string selectQuery = "SELECT * FROM Departments WHERE id = {0}";
            var select = await _context.Departments
                .FromSqlRaw(selectQuery, id)
                .AsNoTracking()
                .FirstAsync();
            if (select == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound, "Department is not found");
            }
            _context.Entry(department).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return _context.Departments.FirstOrDefault(department);
        }
    }
}
