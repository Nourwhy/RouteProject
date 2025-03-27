using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RouteProject.DAL.Models;
using RouteProject.PL.Dtos;
using RouteProject.PL.Helper;

namespace RouteProject.PL.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
        [HttpGet]
        public async Task<IActionResult> Index(string? SearchInput)
        {
            IEnumerable<RoleToReturnDto> roles;


            if (string.IsNullOrEmpty(SearchInput))
            {
                roles = _roleManager.Roles.Select(R=> new RoleToReturnDto()
                {
                    Id = R.Id,
                    Name=R.Name
                 



                });
            }
            else
            {

                roles = _roleManager.Roles.Select(R => new RoleToReturnDto()
                {
                    Id = R.Id,
                    Name = R.Name



                }).Where(R=> R.Name.ToLower().Contains(SearchInput.ToLower()));
            }

            return View(roles);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            
            return View(new RoleToReturnDto());


        }
        [HttpPost]
        public async Task<IActionResult> Create(RoleToReturnDto model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var existingRole = await _roleManager.FindByNameAsync(model.Name);
            if (existingRole != null)
            {
                ModelState.AddModelError("", "Role already exists.");
                return View(model);
            }

            var role = new IdentityRole
            {
                Name = model.Name
            };

            var result = await _roleManager.CreateAsync(role);
            if (result.Succeeded)
            {
                TempData["Message"] = "Role created successfully!";
                return RedirectToAction("Index");
            }

            
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Details(string? id, string viewName = "Details")
        {

            if (id is null)
                return BadRequest("Invaild Id");
            var role =












                await _roleManager.FindByIdAsync(id);


            if (role == null)
            {
                return NotFound(new { statusCode = 404, message = $"Role with Id : {id} not found" });
            }

            var dto = new RoleToReturnDto()
            {
                Id = role.Id,
                Name = role.Name
     


            };

            return View(viewName, dto);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(string? id)
        {
            if (id is null)
                return BadRequest("Invalid Id");

            var role = await _roleManager.FindByIdAsync(id);
            if (role is null)
            {
                return NotFound(new
                {
                    statusCode = 404,
                    message = $"Role with Id: {id} not found"
                });
            }

            var roleDto = new RoleToReturnDto()
            {
                Id = role.Id,
                Name = role.Name
            
            };

            return View(roleDto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(RoleToReturnDto model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var role = await _roleManager.FindByIdAsync(model.Id);
            if (role is null)
            {
                return NotFound(new
                {
                    statusCode = 404,
                    message = $"Role with Id: {model.Id} not found"
                });
            }


            role.Name = model.Name;
           

            var result = await _roleManager.UpdateAsync(role);
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(model);
        }
        [HttpGet]
        public Task<IActionResult> Delete(string? id)
        {


            return Details(id, "Delete");

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id, RoleToReturnDto model)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest(new { statusCode = 400, message = "Invalid Role ID" });

            var user = await _roleManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound(new { statusCode = 404, message = $"Role with ID: {id} not found" });
            }

            var result = await _roleManager.DeleteAsync(user);

            if (result.Succeeded)
            {
                TempData["Message"] = "Role deleted successfully!";
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError("", "Error deleting user");
            return View("Delete", user);
        }

    }

}

