using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Phonebook.Data;
using Phonebook.Models;

namespace Phonebook.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DirectorController : ControllerBase
    {
        private readonly DirectorsDBContext _context;

        public DirectorController(DirectorsDBContext context) => _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Director>>> Get() 
            => await _context.Directors.ToListAsync();

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Director), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetById(int id)
        {
            var director = await _context.Directors.FindAsync(id);
            return director == null ? NotFound() : Ok(director);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult> Create(Director director)
        {
            await _context.Directors.AddAsync(director);   
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = director.Id }, director);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Update(int id, Director director)
        {
            if (id != director.Id) return BadRequest();
            
            _context.Entry(director).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(int id)
        {
            var directorToDelete = await _context.Directors.FindAsync(id);
            if(directorToDelete == null) return NotFound();

            _context.Directors.Remove(directorToDelete);
            await _context.SaveChangesAsync();  
            return NoContent();
        }
    }
}
