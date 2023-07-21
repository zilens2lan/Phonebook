using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Phonebook.Data;
using Phonebook.Models;

namespace Phonebook.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentController : ControllerBase
    {
        private readonly DirectorsDBContext _context;

        public DepartmentController(DirectorsDBContext context) => _context = context;

        [HttpGet]
        public async Task<IEnumerable<Department>> Get()
            => await _context.Departments.ToListAsync();

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Department), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetById(int id)
        {
            var department= await _context.Departments.FindAsync(id);
            return department == null ? NotFound() : Ok(department);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult> Create(Department department)
        {
            await _context.Departments.AddAsync(department);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = department.Id }, department);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Update(int id, Department department)
        {
            if (id != department.Id) return BadRequest();

            _context.Entry(department).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(int id)
        {
            var departmentToDelete = await _context.Departments.FindAsync(id);
            if (departmentToDelete == null) return NotFound();

            _context.Departments.Remove(departmentToDelete);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpGet("/directorId={directorId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetByDirectorId(int directorId)
        {
            string query = "SELECT * FROM Departments WHERE DirectorId = {0}";
            var departments = await _context.Departments
                .FromSqlRaw(query, directorId)
                .AsNoTracking()
                .ToListAsync();
            
            return departments == null ? NotFound() : Ok(departments);
        }
    }
}
