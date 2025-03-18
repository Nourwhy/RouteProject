using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteProject.BLL.Interfaces
{
    public interface IUnitOfWork
    {

         IDepartmentRepository DepartmentRepository { get;  }
         IEmployeeRepository EmployeeRepository { get; }

        int Complete();
        void Dispose();

    }
}
