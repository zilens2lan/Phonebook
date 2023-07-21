using Microsoft.AspNetCore.Mvc;
using Phonebook.Models;

namespace Phonebook.Domain.Services
{
    public interface IWorkerService
    {
        Task<IEnumerable<Worker>> Get();
        Task<Worker> GetById(int id);
        Task<Worker> Create(Worker worker);
        Task<Worker> Update(Worker worker);
        Task<Worker> Delete(int id);
        Task<IEnumerable<Worker>> GetByDepartmentId(int departmentId);
    }
}
