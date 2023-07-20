﻿using Microsoft.EntityFrameworkCore;
using Phonebook.Models;

namespace Phonebook.Data
{
    public class DirectorsDBContext : DbContext
    {
        public DirectorsDBContext(DbContextOptions<DirectorsDBContext> option)
            : base(option)
        {
            Database.EnsureCreated();
        }

        public DbSet<Director> Directors { get; set; }

    }
}
