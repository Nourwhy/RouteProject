﻿using RouteProject.BLL.Interfaces;
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
        public EmployeeRepository(CompanyDbContext context) : base(context)//ASK CLR Create object from CompanyDbContext
        {

        }
        //private readonly CompanyDbContext _context;
        //public EmployeeRepository(CompanyDbContext context)
        //{
        //    _context = context;
        //}
        //public int Add(Employee model)
        //{

        //    _context.Employees.Add(model);
        //    return _context.SaveChanges();
        //}

        //public int Delete(Employee model)
        //{
        //    _context.Employees.Remove(model);
        //    return _context.SaveChanges();
        //}

        //public Employee ?Get(int id)
        //{
        //    return _context.Employees.Find(id);
        //}

        //public IEnumerable<Employee> GetAll()
        //{
        //    return _context.Employees.ToList();
        //}

        //public int Update(Employee model)
        //{
        //    _context.Employees.Update(model);
        //    return _context.SaveChanges();
        //}
    }
}
