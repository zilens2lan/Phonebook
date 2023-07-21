using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Phonebook.Data;
using Phonebook.Domain.Services;
using Phonebook.Models;

namespace Phonebook.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WorkerController : ControllerBase
    {
        private readonly IWorkerService _workerService;

        public WorkerController(IWorkerService workerService) => _workerService = workerService ?? throw new ArgumentNullException(nameof(_workerService));

        [HttpGet]
        public async Task<IEnumerable<Worker>> Get() 
            => await _workerService.Get();

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Worker), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetById(int id)
        {
            return await _workerService.GetById(id) == null ? NotFound() : Ok(await _workerService.GetById(id));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult> Create(Worker worker)
        {
            await _workerService.Create(worker);
            return CreatedAtAction(nameof(GetById), new { id = worker.Id }, worker);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Update(int id, Worker worker)
        {
            if (id != worker.Id) return BadRequest();
            
            await _workerService?.Update(worker);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(int id)
        {

            if (id != null)
            {
                await _workerService.Delete(id);
                return NoContent();
            }

            return NotFound();
        }

        [HttpGet("/departmentId={departmentId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetByDepartmentId(int departmentId)
        {
            var workers = await _workerService.GetByDepartmentId(departmentId);
            return workers == null ? NotFound() : Ok(workers);
        }
    }
}
