using Phonebook.Models;

namespace Phonebook.Domain.Services
{
    public interface IDepartmentService
    {
        Task<IEnumerable<Department>> Get();
        Task<Department> GetById(int id);
        Task<Department> Create(Department department);
        Task<Department> Update(int id, Department department);
        Task<bool> Delete(int id);
        Task<IEnumerable<Department>> GetByDirectorId(int directorId);
    }
}
