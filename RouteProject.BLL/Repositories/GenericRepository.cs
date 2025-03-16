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
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly CompanyDbContext _context;

        public GenericRepository(CompanyDbContext context)
        {
            _context = context;
        }

        public int Add(T model)
        {
            _context.Set<T>().Add(model);
            Console.WriteLine($"Adding: {model}");
            var result = _context.SaveChanges();
            Console.WriteLine($"Rows affected: {result}");
            return result;
        }

        public int Delete(T model)
        {
            _context.Set<T>().Remove(model);
            return _context.SaveChanges();
        }

        public T? Get(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public int Update(T model)
        {
            _context.Set<T>().Update(model);
            return _context.SaveChanges();
        }
    }
}
