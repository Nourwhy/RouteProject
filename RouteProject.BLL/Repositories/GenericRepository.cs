﻿using Microsoft.EntityFrameworkCore;
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

           return _context.SaveChanges();
        
        }

        public int Delete(T model)
        {
            _context.Set<T>().Remove(model);
            return _context.SaveChanges();
        }

        public T? Get(int id)
        {
            return _context.Employees.Include(E => E.Department).FirstOrDefault(E => E.Id == id) as T;
        }

        public IEnumerable<T> GetAll()
        {
            if (typeof(T) == typeof(Employee))
            {
                return(IEnumerable<T>) _context.Employees.Include(E => E.Department).ToList();
            }
                return _context.Set<T>().ToList();
            
        }

        public int Update(T model)
        {
            _context.Set<T>().Update(model);
            return _context.SaveChanges();
        }
    }
}
