using Microsoft.AspNetCore.Mvc;
using RouteProject.BLL.Interfaces;
using RouteProject.BLL.Repositories;
using RouteProject.DAL.Models;
using RouteProject.PL.Dtos;

using AutoMapper;
namespace RouteProject.PL.Controllers
{
    public class EmployeeController : Controller
    {

        //private readonly IEmployeeRepository _employeeRepository;
        private readonly IUnitOfWork _unitOfWork;

        //private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;

        public EmployeeController(IUnitOfWork unitOfWork,IMapper mapper)

        {
            //_employeeRepository = employeeRepository;
            _unitOfWork = unitOfWork;
            //_departmentRepository = departmentRepository;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult Index(string? SearchInput)
        {
            IEnumerable<Employee> employees;

            if (string.IsNullOrEmpty(SearchInput))
            {
                employees = _unitOfWork.EmployeeRepository.GetAll();
            }
            else
            {
                employees = _unitOfWork.EmployeeRepository.GetByName(SearchInput);
            }

            return View(employees);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var departments = _unitOfWork.DepartmentRepository.GetAll();
            ViewData["departments"] = departments;
            return View(new CreateEmployeeDto());

          
        }
        [HttpPost]
        public IActionResult Create(CreateEmployeeDto model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //var employee = new Employee()
                    //{

                    //    Name = model.Name,
                    //    Address = model.Address,
                    //    Age = model.Age,
                    //    CreateAt = model.CreateAt,
                    //    HiringDate = model.HiringDate,
                    //    Email = model.Email,
                    //    IsActive = model.IsActive,
                    //    IsDeleted = model.IsDeleted,
                    //    Phone = model.Phone,
                    //    Salary = model.Salary,
                    //    DepartmentId=model.DepartmentId


                    //};
                    var employee=_mapper.Map <Employee>(model);
                    _unitOfWork.EmployeeRepository.Add(employee);
                    var count = _unitOfWork.Complete();
                    if (count > 0)
                    {
                        TempData["Message"] = "Employee is Created ! !";
                        return RedirectToAction(nameof(Index));
                    }
                }
                catch (Exception ex)
                {

                    ModelState.AddModelError("", ex.Message);

                }
            }

            return View(model);
        }
        [HttpGet]
        public IActionResult Details(int? id, string viewName = "Details")
        {

            if (id is null)
                return BadRequest("Invaild Id");
            var employee = _unitOfWork.EmployeeRepository.Get(id.Value);


            if (employee == null)
            {
                return NotFound(new { statusCode = 404, message = $"Employee with Id : {id} not found" });
            }

        
            return View(viewName, employee);
        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            var departments = _unitOfWork.DepartmentRepository.GetAll();
            ViewData["departments"] = departments;

            if (id is null)
                return BadRequest("Invalid Id");

            var employee = _unitOfWork.EmployeeRepository.Get(id.Value);
            if (employee is null)
            {
                return NotFound(new
                {
                    statusCode = 404,
                    message = $"Employee with Id: {id} not found"
                });
            }

    
            var employeedto = _mapper.Map<CreateEmployeeDto>(employee);

            return View(employeedto); 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, CreateEmployeeDto model)
        {
            if (!ModelState.IsValid)
            {
                var departments = _unitOfWork.DepartmentRepository.GetAll();
                ViewData["departments"] = departments;
                return View(model);
            }

            var employee = _unitOfWork.EmployeeRepository.Get(id);
            if (employee == null)
            {
                return NotFound(new { statusCode = 404, message = $"Employee with Id : {id} not found" });
            }

       
            _mapper.Map(model, employee);

          _unitOfWork.EmployeeRepository.Update(employee);
            var count = _unitOfWork.Complete();
            if (count > 0)
            {
                TempData["Message"] = "Employee updated successfully!"; 
                return RedirectToAction(nameof(Index));
            }

            var departmentsList = _unitOfWork.DepartmentRepository.GetAll();
            ViewData["departments"] = departmentsList;

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
                _unitOfWork.EmployeeRepository.Delete(employee);
                var count = _unitOfWork.Complete();
                if (count > 0)
                {

                    return RedirectToAction(nameof(Index));
                }

            }
            return View(employee);

        }

    }
}
