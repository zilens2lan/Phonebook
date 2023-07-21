using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Phonebook.Data;
using Phonebook.Models;

namespace Phonebook.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WorkerController : ControllerBase
    {
        private readonly DirectorsDBContext _context;

        public WorkerController(DirectorsDBContext context) => _context = context;

        [HttpGet]
        public async Task<IEnumerable<Worker>> Get() 
            => await _context.Workers.ToListAsync();

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Worker), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetById(int id)
        {
            var worker = await _context.Workers.FindAsync(id);
            return worker == null ? NotFound() : Ok(worker);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult> Create(Worker worker)
        {
            await _context.Workers.AddAsync(worker);   
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = worker.Id }, worker);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Update(int id, Worker worker)
        {
            if (id != worker.Id) return BadRequest();
            
            _context.Entry(worker).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(int id)
        {
            var workerToDelete = await _context.Workers.FindAsync(id);
            if(workerToDelete == null) return NotFound();

            _context.Workers.Remove(workerToDelete);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpGet("/departmentId={departmentId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetByDepartmentId(int departmentId)
        {
            string query = "SELECT * FROM Workers WHERE DepartmentId = {0}";
            var workers = await _context.Workers
                .FromSqlRaw(query, departmentId)
                .AsNoTracking()
                .ToListAsync();

            return workers == null ? NotFound() : Ok(workers);
        }
    }
}
