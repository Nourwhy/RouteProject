using Microsoft.AspNetCore.Mvc;
using RouteProject.BLL.Interfaces;
using RouteProject.BLL.Repositories;
using RouteProject.DAL.Models;
using RouteProject.PL.Dtos;

namespace RouteProject.PL.Controllers
{
    public class EmployeeController : Controller
    {

        private readonly IEmployeeRepository _employeeRepository;
   
        public EmployeeController(IEmployeeRepository employeeRepository)

        {
            _employeeRepository = employeeRepository;
        }
        [HttpGet]
        public IActionResult Index()
        {

            var employees = _employeeRepository.GetAll();
            return View(employees);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View(new CreateEmployeeDto());

          
        }
        [HttpPost]
        public IActionResult Create(CreateEmployeeDto model)
        {
            if (ModelState.IsValid) 
            {

                var employee = new Employee()
                {

               Name=model.Name,
               Address = model.Address,
                    Age = model.Age,
                    CreateAt = model.CreateAt,
                    HiringDate = model.HiringDate,
                    Email = model.Email,
                    IsActive = model.IsActive,
                    IsDeleted = model.IsDeleted,
                    Phone = model.Phone,
                    Salary = model.Salary


                };
                var count = _employeeRepository.Add(employee);
                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(model);
        }
        [HttpGet]
        public IActionResult Details(int? id, string viewName = "Details")
        {

            if (id is null)
                return BadRequest("Invaild Id");
            var employee = _employeeRepository.Get(id.Value);


            if (employee == null)
            {
                return NotFound(new { statusCode = 404, message = $"Employee with Id : {id} not found" });
            }



            return View(viewName, employee);
        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {

            if (id is null)
                return BadRequest("Invaild Id");
            var employee = _employeeRepository.Get(id.Value);


            if (employee is null)
            
                return NotFound(new { statusCode = 404, message = $"Employee with Id : {id} not found" });
                var employeedto = new CreateEmployeeDto()
                {
                    Id = employee.Id,

                    Name = employee.Name,
                    Address = employee.Address,
                    Age = employee.Age,
                    CreateAt = employee.CreateAt,
                    HiringDate = employee.HiringDate,
                    Email = employee.Email,
                    IsActive = employee.IsActive,
                    IsDeleted = employee.IsDeleted,
                    Phone = employee.Phone,
                    Salary = employee.Salary


                };



                return View(employeedto);
            
            }
        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, CreateEmployeeDto model)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }

       
            var employee = _employeeRepository.Get(id);
            if (employee == null)
            {
                return NotFound(new { statusCode = 404, message = $"Employee with Id : {id} not found" });
            }

            
            employee.Name = model.Name;
            employee.Address = model.Address;
            employee.Age = model.Age;
            employee.CreateAt = model.CreateAt;
            employee.HiringDate = model.HiringDate;
            employee.Email = model.Email;
            employee.IsActive = model.IsActive;
            employee.IsDeleted = model.IsDeleted;
            employee.Phone = model.Phone;
            employee.Salary = model.Salary;

      
            var count = _employeeRepository.Update(employee);
            if (count > 0)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }


        
        [HttpGet]
        public IActionResult Delete(int? id)
        { 


            return Details(id, "Delete");

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id, Employee employee)
        {

            if (ModelState.IsValid)
            {
                if (id !=   employee.Id)
                    return BadRequest();
                var count = _employeeRepository.Delete(employee);
                if (count > 0)
                {

                    return RedirectToAction(nameof(Index));
                }

            }
            return View(employee);

        }

    }
}
