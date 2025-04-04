using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RouteProject.DAL.Models;
using RouteProject.PL.Dtos;
using RouteProject.PL.Helper;

namespace RouteProject.PL.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public UserController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        [HttpGet]
        public async Task<IActionResult> Index(string? SearchInput)
        {
            IEnumerable<UserToReturnDto> users;

            if (string.IsNullOrEmpty(SearchInput))
            {
                users = _userManager.Users.Select(U => new UserToReturnDto()
                {
                    Id = U.Id,
                    UserName = U.UserName,
                    Email = U.Email,
                    FirstName = U.FirstName,
                    LastName = U.LastName,
                    Roles = _userManager.GetRolesAsync(U).Result



                });
            }
            else
            {

                    users = _userManager.Users.Select(U => new UserToReturnDto()
                    {
                        Id = U.Id,
                        UserName = U.UserName,
                        Email = U.Email,
                        FirstName = U.FirstName,
                        LastName = U.LastName,
                        Roles = _userManager.GetRolesAsync(U).Result



                    }).Where(U => U.FirstName.ToLower().Contains(SearchInput.ToLower()));
                }

                return View(users);
            }
        [HttpGet]
        public async Task<IActionResult> Details(string? id, string viewName = "Details")
        {

            if (id is null)
                return BadRequest("Invaild Id");
            var user =












                await _userManager.FindByIdAsync(id);


            if (user == null)
            {
                return NotFound(new { statusCode = 404, message = $"User with Id : {id} not found" });
            }

            var dto= new UserToReturnDto()
            {
                Id = user.Id,
             UserName = user.UserName,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Roles = _userManager.GetRolesAsync(user).Result


            };

            return View(viewName, dto);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(string? id)
        {
            if (id is null)
                return BadRequest("Invalid Id");

            var user = await _userManager.FindByIdAsync(id);
            if (user is null)
            {
                return NotFound(new
                {
                    statusCode = 404,
                    message = $"User with Id: {id} not found"
                });
            }

            var userDto = new UserToReturnDto()
            {
                Id = user.Id,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email
            };

            return View(userDto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserToReturnDto model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _userManager.FindByIdAsync(model.Id);
            if (user is null)
            {
                return NotFound(new
                {
                    statusCode = 404,
                    message = $"User with Id: {model.Id} not found"
                });
            }

      
            user.UserName = model.UserName;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Email = model.Email;

            var result = await _userManager.UpdateAsync(user);
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
        public async Task<IActionResult> Delete(string id,UserToReturnDto model)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest(new { statusCode = 400, message = "Invalid user ID" });

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound(new { statusCode = 404, message = $"User with ID: {id} not found" });
            }

            var result = await _userManager.DeleteAsync(user);

            if (result.Succeeded)
            {
                TempData["Message"] = "User deleted successfully!";
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError("", "Error deleting user");
            return View("Delete", user);
        }

    }

}
