﻿using Abby.Models;
using Microsoft.EntityFrameworkCore;

namespace Abby.DataAccess.Data
{
    public class ApplicationDbContext : DbContext
    {
        // passes DbContextOptions for ApplicationDbContext via Dependancy Injection, and then passes that to the base class
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Category> Category { get; set; }
        public DbSet<FoodType> FoodType { get; set; }
    }
}
