using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Phonebook.Data;
using Phonebook.Models;

namespace Phonebook.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentHeadController : ControllerBase
    {
        private readonly DepartmentHeadsDBContext _context;

        public DepartmentHeadController(DepartmentHeadsDBContext context) => _context = context;

        [HttpGet]
        public async Task<IEnumerable<DepartmentHead>> Get()
            => await _context.DepartmentHeads.ToListAsync();

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(DepartmentHead), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetById(int id)
        {
            var departmentHead = await _context.DepartmentHeads.FindAsync(id);
            return departmentHead == null ? NotFound() : Ok(departmentHead);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult> Create(DepartmentHead departmentHead)
        {
            await _context.DepartmentHeads.AddAsync(departmentHead);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = departmentHead.Id }, departmentHead);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Update(int id, DepartmentHead departmentHead)
        {
            if (id != departmentHead.Id) return BadRequest();

            _context.Entry(departmentHead).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(int id)
        {
            var departmentHeadToDelete = await _context.DepartmentHeads.FindAsync(id);
            if (departmentHeadToDelete == null) return NotFound();

            _context.DepartmentHeads.Remove(departmentHeadToDelete);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
