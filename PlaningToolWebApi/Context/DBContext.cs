using PlaningToolWebApi.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PlaningToolWebApi.Context
{
    public class DBContext : DbContext
    {
        
        
        public DbSet<User> users { get; set; }

        public DbSet<Auditorie> Auditories { get; set; }

        public DbSet<Event> Events { get; set; }

        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=92.63.178.148;Port=5432;Database=hackaton;Username=postgres;Password=123qwe");
        }
    }






}
