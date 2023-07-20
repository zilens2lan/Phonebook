using Microsoft.EntityFrameworkCore;
using Phonebook.Models;

namespace Phonebook.Data
{
    public class DirectorsDBContext : DbContext
    {
        public DbSet<Director> Directors { get; set; }
        public DirectorsDBContext(DbContextOptions<DirectorsDBContext> option)
            : base(option)
        {
            Database.EnsureCreated();
        }

        public DbSet<Director> directors { get; set; }
    }
}
