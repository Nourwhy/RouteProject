using Microsoft.EntityFrameworkCore;
using RouteProject.BLL.Interfaces;
using RouteProject.DAL.Data.Contexts;
using RouteProject.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteProject.BLL.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>,IEmployeeRepository
    {
        private readonly CompanyDbContext _context;
        public EmployeeRepository(CompanyDbContext context) : base(context)
        {
            _context = context;

        }

        public async Task< List<Employee>> GetByNameAsync(string name)
        {

           return  await _context.Employees.Include(E=>E.Department).Where(E => E.Name.ToLower().Contains(name.ToLower())).ToListAsync();

        }
    }
}
