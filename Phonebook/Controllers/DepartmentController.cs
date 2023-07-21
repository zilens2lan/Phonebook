using Microsoft.AspNetCore.Mvc;
using Phonebook.Domain.Services;
using Phonebook.Models;
using Phonebook.Service;

namespace Phonebook.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService) => _departmentService = departmentService ?? throw new ArgumentNullException(nameof(_departmentService));

        [HttpGet]
        public async Task<IEnumerable<Department>> Get()
            => await _departmentService.Get();

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Department), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetById(int id)
        {
            return await _departmentService.GetById(id) == null ? NotFound() : Ok(await _departmentService.GetById(id));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult> Create(Department department)
        {
            await _departmentService.Create(department);
            return CreatedAtAction(nameof(GetById), new { id = department.Id }, department);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Update(int id, Department department)
        {
            if (id != department.Id) return BadRequest();

            await _departmentService.Update(department);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(int id)
        {
            if (id != null)
            {
                await _departmentService.Delete(id);
                return NoContent();
            }
            return NotFound();
        }

        [HttpGet("/directorId={directorId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetByDirectorId(int directorId)
        {
            return await _departmentService.GetByDirectorId(directorId) == null ? NotFound() : Ok(await _departmentService.GetByDirectorId(directorId));
        }
    }
}
