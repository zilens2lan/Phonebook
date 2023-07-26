using Phonebook.Models;

namespace Phonebook.Domain.Services
{
    public interface IDirectorService
    {
        Task<IEnumerable<Director>> Get();
        Task<Director> GetById(int id);
        Task<Director> Create(Director director);
        Task<Director> Update(int id, Director director);
        Task<bool> Delete(int id);
    }
}
