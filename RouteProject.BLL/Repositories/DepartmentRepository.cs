using RouteProject.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RouteProject.DAL.Models;
using RouteProject.DAL.Data.Contexts;
namespace RouteProject.BLL.Repositories
{

    public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository

    {

        public DepartmentRepository(CompanyDbContext context):base(context)
        {
            
        }
        //private readonly CompanyDbContext _context;//NULL

        //// ASK CKR Create
        //public DepartmentRepository(CompanyDbContext context)
        //{
        //    _context = context;
        //}
        //public int Add(Department model)
        //{

        //    _context.Departments.Add(model);
        //    return _context.SaveChanges();
        //}

        //public int Delete(Department model)
        //{

        //    _context.Departments.Remove(model);
        //    return _context.SaveChanges();
        //}

        //public Department? Get(int id)
        //{

        //    return _context.Departments.Find(id);
        //}

        //public IEnumerable<Department> GetAll()
        //{

        //    return _context.Departments.ToList();
        //}





        //public int Update(Department model)
        //{

        //    _context.Departments.Update(model);
        //    return _context.SaveChanges();
        //}

    }
}
