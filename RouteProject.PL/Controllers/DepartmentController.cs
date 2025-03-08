using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RouteProject.BLL.Interfaces;
using RouteProject.BLL.Repositories;
using RouteProject.DAL.Models;
using RouteProject.PL.Dtos;
namespace RouteProject.PL.Controllers
{
    //MVC Controller
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _departmentRepository;
        //ASK CKR TO CREATE object From DepartmentRepository 
        public DepartmentController(IDepartmentRepository departmentRepository)

        {
            _departmentRepository = departmentRepository;
        }
        [HttpGet]// GET : /Department/Index
        public IActionResult Index()
        {
          
            var departments = _departmentRepository.GetAll();
            return View(departments);
        }
        [HttpGet]
        public IActionResult Create()
        {

           
            return View();
        }  
        [HttpPost]
        public IActionResult Create(CreateDepartmentDto model)
        {
            if (ModelState.IsValid) //Server Side
            {

                var department = new Department()
                {
                    Code = model.Code,
                    Name = model.Name,
                    CreateAt = model.CreateAt


                };
                var count = _departmentRepository.Add(department);
                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
           
            return View();
        }
        [HttpGet]
        public IActionResult Details(string code)
        {
           
            var department = _departmentRepository.GetByCode(code);

          
            if (department == null)
            {
                return NotFound($"Department with Code {code} not found.");
            }

   
            var model = new CreateDepartmentDto
            {
                Code = department.Code,
                Name = department.Name,
                CreateAt = department.CreateAt
            };

            return View(model);
        }
        [HttpGet]
        public IActionResult Edit() 
        {
            
            return View();
        }

        [HttpPost]
        public IActionResult Edit(CreateDepartmentDto model)
        {
            if (ModelState.IsValid)
            {
                var department = _departmentRepository.GetByCode(model.Code);

                if (department == null)
                {
                    return NotFound($"Department with Code {model.Code} not found.");
                }

        
                department.Name = model.Name;
                department.CreateAt = model.CreateAt;

                _departmentRepository.Update(department);

                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }


    }
}
