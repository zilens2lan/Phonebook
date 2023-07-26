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
        public async Task<ActionResult> Get()
        {
            try
            {
                return Ok(await _directorService.Get());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Director>> GetById(int id)
        {
            try
            {
                return Ok(await _directorService.GetById(id));
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Create(Director director)
        {
            try
            {
                return Ok(await _directorService.Create(director));
            }
            catch (ArgumentNullException ex)
            {
                return NotFound(ex);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut]
        public async Task<ActionResult> Update(int id, Director director)
        {
            try
            {
                return Ok(await _directorService.Update(id, director));
            }
            catch (ArgumentNullException ex)
            {
                return NotFound(ex);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _directorService.Delete(id);
                return new NoContentResult();
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
