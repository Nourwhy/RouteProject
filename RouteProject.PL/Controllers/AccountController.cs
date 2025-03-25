using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using RouteProject.DAL.Models;
using RouteProject.PL.Dtos;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RouteProject.PL.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager,SignInManager<AppUser> signInManager )
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        #region SignUp

        [HttpGet] //GET: /Account/SignUp
        public IActionResult SignUp()
        {
            return View();

        }
        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpDto model)
        {
            if (ModelState.IsValid)//Server Side Validation
            {
                var user = await _userManager.FindByNameAsync(model.UserName);
                if (user is null)

                {

                    user = await _userManager.FindByEmailAsync(model.Email);
                    if (user is null)
                    {

                        //Register

                        user = new AppUser()
                        {
                            UserName = model.UserName,
                            FirstName = model.FirstName,
                            LastName = model.LastName,
                            Email = model.Email,
                            IsAgree = model.IsAgree,



                        };
                        var result = await _userManager.CreateAsync(user, model.Password);
                        if (result.Succeeded)
                        {
                            return RedirectToAction("SignIn");

                        }
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                    }


                }
                ModelState.AddModelError("", "Invalid SignUp ! !");
            }
            return View(model);

        }
        #endregion

        #region SignIn
        [HttpGet] //GET: /Account/SignUp
        public IActionResult SignIn()
        {
            return View();

        }

        [HttpPost]

        [HttpPost]
        public async Task<IActionResult> SignIn(SignInDto model)
        {
            if (ModelState.IsValid) // Server-side validation
            {
                var user = await _userManager.FindByEmailAsync(model.Email);

                if (user is not null) 
                {
                    var flag = await _userManager.CheckPasswordAsync(user, model.Password);

                    if (flag)
                    {
                        var result= await _signInManager.PasswordSignInAsync(user, model.Password,false,false);
                        if (result.Succeeded)
                        {
                            return RedirectToAction(nameof(HomeController.Index), "Home");
                        }
                    }
                }

                ModelState.AddModelError("", "Invalid SignIn!");
            }

            return View();
        }


        #endregion

        #region SignOut
        [HttpGet] 
        public async Task<IActionResult> SignOut()
        {
         await _signInManager.SignOutAsync();

            return RedirectToAction(nameof(SignIn));

        }
        #endregion


    }
}
