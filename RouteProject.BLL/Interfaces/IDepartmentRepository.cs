using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RouteProject.DAL.Models;


namespace RouteProject.BLL.Interfaces
{
    public interface IDepartmentRepository
    {
        IEnumerable<Department> GetAll();


        Department? GetByCode(string code);

        Department? Get(int id);

        int Add(Department model);
        int Update(Department model);
        int Delete(Department model);

    }
}
