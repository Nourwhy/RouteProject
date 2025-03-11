using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
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

            return View(new CreateDepartmentDto());
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
        public IActionResult Details(int? id,string viewName="Details")
        {

            if (id is null)
                return BadRequest("Invaild Id");//400
            var department = _departmentRepository.Get(id.Value);


            if (department == null)
            {
                return NotFound(new { statusCode = 404, message = $"Department with Id : {id} not found" });
            }



            return View(viewName,department);
        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id is null)
                return BadRequest("Invalid Id");

            var department = _departmentRepository.Get(id.Value);
            if (department is null)
                return NotFound(new { statusCode = 404, message = $"Department with Id : {id} not found" });


            var departdto = new CreateDepartmentDto()
            {
                Id = department.Id,
                Code = department.Code,
                Name = department.Name,
                CreateAt = department.CreateAt
            };

            return View("Edit", departdto);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, CreateDepartmentDto model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

     
            var department = _departmentRepository.Get(id);
            if (department == null)
            {
                return NotFound(new { statusCode = 404, message = $"Department with Id : {id} not found" });
            }

        
            department.Code = model.Code;
            department.Name = model.Name;
            department.CreateAt = model.CreateAt;

        
            var count = _departmentRepository.Update(department);
            if (count > 0)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }


        //[HttpPost]
        ////[ValidateAntiForgeryToken]
        //public IActionResult Edit([FromRoute] int id, UpdateDepartmentDto model)
        //{

        //    if (ModelState.IsValid)
        //    {
        //        var department = new Department()
        //        {
        //            Id = id,
        //            Name = model.Name,
        //            Code = model.Code,
        //            CreateAt = model.CreateAt
        //        };
        //        var count = _departmentRepository.Update(department);
        //        if (count > 0)
        //        {

        //            return RedirectToAction(nameof(Index));
        //        }

        //    }
        //    return View(model);

        [HttpGet]
            public IActionResult Delete(int? id)
            {

                //if (id is null)
                //    return BadRequest("Invaild Id");//400
                //var department = _departmentRepository.Get(id.Value);


                //if (department == null)
                //{
                //    return NotFound(new { statusCode = 404, message = $"Department with Id : {id} not found" });
                //}



            return Details(id, "Delete");

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id, Department department)
        {

            if (ModelState.IsValid)
            {
                if (id != department.Id)
                    return BadRequest();
                var count = _departmentRepository.Delete(department);
                if (count > 0)
                {

                    return RedirectToAction(nameof(Index));
                }

            }
            return View(department);

        }
        }
    }
