using Microsoft.EntityFrameworkCore;
using RouteProject.DAL.Models;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RouteProject.DAL.Data.Contexts
{

    public class CompanyDbContext : DbContext
    {
        //CLR
        public CompanyDbContext(DbContextOptions<CompanyDbContext> options):base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server=LAPTOP-IVLHNKPM;Database=RouteProject;Trusted_Connection=True;TrustServerCertificate=True;");
       
        
        public DbSet<Department> Departments { get; set; } 
        public DbSet<Employee> Employees { get; set; }
    }
}
