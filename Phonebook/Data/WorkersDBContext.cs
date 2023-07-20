using Microsoft.EntityFrameworkCore;
using Phonebook.Models;

namespace Phonebook.Data
{
    public class WorkersDBContext : DbContext
    {
        public WorkersDBContext(DbContextOptions<WorkersDBContext> option)
            : base(option)
        {
            Database.EnsureCreated();
        }
        public DbSet<Worker> Workers { get; set; }
    }
}
