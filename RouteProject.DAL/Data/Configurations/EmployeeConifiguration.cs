using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RouteProject.DAL.Models;
using Microsoft.Identity.Client;

namespace RouteProject.DAL.Data.Configurations
{
    public class EmployeeConifiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Employee> builder)
        {
            builder.Property(E => E.Salary).HasColumnType("decimal(18,2)");
        }
    }
}
