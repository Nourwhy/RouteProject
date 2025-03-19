using Microsoft.AspNetCore.Mvc;
using RouteProject.BLL.Interfaces;
using RouteProject.BLL.Repositories;
using RouteProject.DAL.Models;
using RouteProject.PL.Dtos;

using AutoMapper;
using RouteProject.PL.Helper;
using Microsoft.IdentityModel.Abstractions;
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
        public  async Task<IActionResult> Index(string? SearchInput)
        {
            IEnumerable<Employee> employees;

            if (string.IsNullOrEmpty(SearchInput))
            {
                employees = await _unitOfWork.EmployeeRepository.GetAllAsync();
            }
            else
            {
                employees =  await _unitOfWork.EmployeeRepository.GetByNameAsync(SearchInput);
            }

            return View(employees);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var departments =await _unitOfWork.DepartmentRepository.GetAllAsync();
            ViewData["departments"] = departments;
            return View(new CreateEmployeeDto());

          
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateEmployeeDto model)
        {
            if (ModelState.IsValid)
            {
           
                if (model.Image is not null)
                {
                    string imageName = DocumentSettings.UploadFile(model.Image, "images");
                    model.ImageName = imageName;
                }

              
                var employee = _mapper.Map<Employee>(model);

                await _unitOfWork.EmployeeRepository.AddAsync(employee);
                var count = await  _unitOfWork.CompleteAsync();

                if (count > 0)
                {
                    TempData["Message"] = "Employee is Created!";
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Details(int? id, string viewName = "Details")
        {

            if (id is null)
                return BadRequest("Invaild Id");
            var employee = 
                
                
                
                
                
                
                
                
                
                
                
                
                await _unitOfWork.EmployeeRepository.GetAsync(id.Value);


            if (employee == null)
            {
                return NotFound(new { statusCode = 404, message = $"Employee with Id : {id} not found" });
            }

        
            return View(viewName, employee);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            var departments = await  _unitOfWork.DepartmentRepository.GetAllAsync();
            ViewData["departments"] = departments;

            if (id is null)
                return BadRequest("Invalid Id");

            var employee = await _unitOfWork.EmployeeRepository.GetAsync(id.Value);
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
        public async Task< IActionResult> Edit(int id, CreateEmployeeDto model)
        {
            if (!ModelState.IsValid)
            {
                var departments = _unitOfWork.DepartmentRepository.GetAllAsync();
                ViewData["departments"] = departments;
                return View(model);
            }

            var employee = await _unitOfWork.EmployeeRepository.GetAsync(id);
            if (employee == null)
            {
                return NotFound(new { statusCode = 404, message = $"Employee with Id : {id} not found" });
            }

          
            if (model.Image != null && !string.IsNullOrEmpty(employee.ImageName))
            {
                DocumentSettings.DeleteFile(employee.ImageName, "images");
            }

           
            if (model.Image != null)
            {
                string imageName = DocumentSettings.UploadFile(model.Image, "images");
                model.ImageName = imageName;
            }
            else
            {
                model.ImageName = employee.ImageName;
            }

      
            _mapper.Map(model, employee);
            employee.Id = id;

            _unitOfWork.EmployeeRepository.Update(employee);
            var count = await _unitOfWork.CompleteAsync();

            if (count > 0)
            {
                TempData["Message"] = "Employee updated successfully!";
                return RedirectToAction(nameof(Index));
            }

            var departmentsList = await _unitOfWork.DepartmentRepository.GetAllAsync();
            ViewData["departments"] = departmentsList;

            return View(model);
        }


        [HttpGet]
        public Task<IActionResult >Delete(int? id)
        { 


            return Details(id, "Delete");

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {

            var employee = await  _unitOfWork.EmployeeRepository.GetAsync(id);
            if (employee == null)
            {
                return NotFound(new { statusCode = 404, message = $"Employee with Id: {id} not found" });
            }

            if (!string.IsNullOrEmpty(employee.ImageName))
            {
                DocumentSettings.DeleteFile(employee.ImageName, "images");
            }

         
            _unitOfWork.EmployeeRepository.Delete(employee);
            var count = await _unitOfWork.CompleteAsync();

            if (count > 0)
            {
                TempData["Message"] = "Employee deleted successfully!";
                return RedirectToAction(nameof(Index));
            }

            return View("Delete", employee);
        }

    }

    
}







