using Microsoft.AspNetCore.Mvc;
using Phonebook.Domain.Services;
using Phonebook.Models;
using Phonebook.Service;

namespace Phonebook.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DirectorController : ControllerBase
    {
        private readonly IDirectorService _directorService;

        public DirectorController(IDirectorService directorService) => _directorService = directorService ?? throw new ArgumentNullException(nameof(_directorService));

        [HttpGet]
        public async Task<IEnumerable<Director>> Get() 
            => await _directorService.Get();

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Director), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Director>> GetById(int id)
        {
            return await _directorService.GetById(id) == null ? NotFound() : Ok(await _directorService.GetById(id));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult> Create(Director director)
        {
            await _directorService.Create(director);
            return CreatedAtAction(nameof(GetById), new { id = director.Id }, director);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Update(int id, Director director)
        {
            if (id != director.Id) return BadRequest();
            await _directorService.Update(id, director);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(int id)
        {
            if (id != null)
            {
                await _directorService.Delete(id);
                return NoContent();
            }
            return NotFound();
        }
    }
}
