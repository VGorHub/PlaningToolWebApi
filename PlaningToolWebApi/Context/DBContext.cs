using PlaningToolWebApi.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PlaningToolWebApi.Context
{
    public class DBContext : DbContext
    {
        
        
        public DbSet<User> users { get; set; }

        public DbSet<Auditorie> auditories { get; set; }


        public DbSet<Event> events { get; set; }
        public DbSet<Member> members { get; set; }


        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=92.63.178.148;Port=5432;Database=hackaton;Username=postgres;Password=123qwe");
        }
    }






}
