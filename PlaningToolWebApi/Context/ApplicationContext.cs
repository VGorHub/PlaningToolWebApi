using Microsoft.EntityFrameworkCore;
using PlaningToolWebApi.Models;
using System.IO;
using System.Net.NetworkInformation;

namespace PlaningToolWebApi.Context
{
    public class ApplicationContext : DbContext
    {
        public DbSet<FileModel> Files { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
