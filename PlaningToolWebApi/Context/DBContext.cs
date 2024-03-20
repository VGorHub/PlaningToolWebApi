using PlaningToolWebApi.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PlaningToolWebApi.Context
{
    public class DBContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DBContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=92.63.178.148;Port=5432;Database=postgres;Username=postgres;Password=123qwe");
        }
    }






}
