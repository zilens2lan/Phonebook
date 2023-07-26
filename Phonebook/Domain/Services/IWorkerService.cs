using Microsoft.AspNetCore.Mvc;
using Phonebook.Models;

namespace Phonebook.Domain.Services
{
    public interface IWorkerService
    {
        Task<IEnumerable<Worker>> Get();
        Task<Worker> GetById(int id);
        Task<Worker> Create(Worker worker);
        Task<Worker> Update(int id, Worker worker);
        Task<bool> Delete(int id);
        Task<IEnumerable<Worker>> GetByDepartmentId(int departmentId);
    }
}
