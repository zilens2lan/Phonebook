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
        public async Task<ActionResult> Get()
        {
            try
            {
                return Ok(await _departmentService.Get());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            try
            {
                return Ok(await _departmentService.GetById(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Create(Department department)
        {
            try
            {
                await _departmentService.Create(department);
                return Ok(department);
            }
            catch (ArgumentNullException ex)
            {
                return NotFound(ex);
            }
            catch(Exception ex) 
            {
                return BadRequest(ex);
            }
        }

        [HttpPut]
        public async Task<ActionResult> Update(int id, Department department)
        {
            try
            {
                return Ok(await _departmentService.Update(id, department));
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
                await _departmentService.Delete(id);
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

        [HttpGet("/directorId={directorId}")]
        public async Task<ActionResult> GetByDirectorId(int directorId)
        {
            try
            {
                return Ok(await _departmentService.GetByDirectorId(directorId));
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
