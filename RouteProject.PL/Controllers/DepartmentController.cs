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
        //private readonly IDepartmentRepository _departmentRepository;
        private readonly IUnitOfWork _unitOfWork;

        //ASK CKR TO CREATE object From DepartmentRepository 
        public DepartmentController(IUnitOfWork unitOfWork)

        {
            //_departmentRepository = departmentRepository;
            _unitOfWork = unitOfWork;
        }
        [HttpGet]// GET : /Department/Index
        public async Task<IActionResult> Index()
        {

            var departments =await _unitOfWork.DepartmentRepository.GetAllAsync();
            return View(departments);
        }
        [HttpGet]
        public IActionResult Create()
        {

            return View(new CreateDepartmentDto());
        }
        [HttpPost]
        public async Task< IActionResult> Create(CreateDepartmentDto model)
        {
            if (ModelState.IsValid) //Server Side
            {

                var department = new Department()
                {
                    Code = model.Code,
                    Name = model.Name,
                    CreateAt = model.CreateAt


                };
                await   _unitOfWork.DepartmentRepository.AddAsync(department);
                var count = await _unitOfWork.CompleteAsync();
                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Details(int? id,string viewName="Details")
        {

            if (id is null)
                return BadRequest("Invaild Id");//400
            var department  = await _unitOfWork.DepartmentRepository.GetAsync(id.Value);


            if (department == null)
            {
                return NotFound(new { statusCode = 404, message = $"Department with Id : {id} not found" });
            }



            return View(viewName,department);
        }
        [HttpGet]
        public async  Task<IActionResult> Edit(int? id)
        {
            if (id is null)
                return BadRequest("Invalid Id");

            var department = await _unitOfWork.DepartmentRepository.GetAsync(id.Value);
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
        public async Task<IActionResult> Edit(int id, CreateDepartmentDto model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var department = await _unitOfWork.DepartmentRepository.GetAsync(id);
            if (department == null)
            {
                return NotFound(new { statusCode = 404, message = $"Department with Id: {id} not found" });
            }

            department.Code = model.Code;
            department.Name = model.Name;
            department.CreateAt = model.CreateAt;

            _unitOfWork.DepartmentRepository.Update(department);

         
            var count = await _unitOfWork.CompleteAsync();

            if (count > 0)
            {
                TempData["Message"] = "Department updated successfully!";
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError("", "Something went wrong while updating the department.");
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
            public Task<IActionResult> Delete(int? id)
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
        public async Task<IActionResult> Delete(int id)
        {
            var department =  await _unitOfWork.DepartmentRepository.GetAsync(id);
            if (department == null)
            {
                return NotFound(new { statusCode = 404, message = $"Department with Id: {id} not found" });
            }

            _unitOfWork.DepartmentRepository.Delete(department);

         
            var count = await _unitOfWork.CompleteAsync();

            if (count > 0)
            {
                TempData["Message"] = "Department deleted successfully!";
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError("", "Something went wrong while deleting the department.");
            return View("Delete", department);
        }


    }
}
