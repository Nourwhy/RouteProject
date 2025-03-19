﻿using RouteProject.BLL.Interfaces;
using RouteProject.DAL.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteProject.BLL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CompanyDbContext _context;

        public IDepartmentRepository DepartmentRepository { get; }


        public IEmployeeRepository EmployeeRepository { get; }


        public UnitOfWork(CompanyDbContext context)
        {
            _context = context;

            DepartmentRepository = new DepartmentRepository(_context);
            EmployeeRepository = new EmployeeRepository(_context);
        }

        public int Complete()
        {
            return _context.SaveChanges();
        
                }

        public void Dispose()
        {
             _context.Dispose();
        }
    }
}
