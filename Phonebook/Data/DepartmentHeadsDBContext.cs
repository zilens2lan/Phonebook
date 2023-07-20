using Microsoft.EntityFrameworkCore;
using Phonebook.Models;

namespace Phonebook.Data
{
    public class DepartmentHeadsDBContext : DbContext
    {
        public DepartmentHeadsDBContext(DbContextOptions<DepartmentHeadsDBContext> option)
            : base(option)
        {
            Database.EnsureCreated();
        }

        public DbSet<DepartmentHead> DepartmentHeads{ get; set; }
    }
}
