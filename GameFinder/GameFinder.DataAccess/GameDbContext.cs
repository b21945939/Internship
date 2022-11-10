using GameFinder.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameFinder.DataAccess
{
    public class GameDbContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=LAPTOP-CRQ8SAJD; Database=GameDb;uid=sa;pwd=11223344;");
        }

        public DbSet<Game> Games { get; set; }
    }
}
