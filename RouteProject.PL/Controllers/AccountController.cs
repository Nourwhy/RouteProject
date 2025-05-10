using Microsoft.AspNetCore.Authentication.Facebook;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using RouteProject.DAL.Models;
using RouteProject.PL.Dtos;
using RouteProject.PL.Helper;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RouteProject.PL.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IMailServices _mailServices;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
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
                        var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
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

        public IActionResult AccessDenied()
        {
            return View();
        
        }

        #region Forget Password 
        [HttpGet("ForgetPassword")]
        public IActionResult ForgetPassword()
        {
            return View();

        }
        [HttpPost("SentResetPasswordUrl")]
        public async Task<IActionResult> SentResetPasswordUrl(ForgetPasswordDto model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);

                if (user is not null)
                {
                    // Generate Token
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);

                    // Create URL
                    var url = Url.Action("ResetPassword", "Account", new { email = model.Email, token }, Request.Scheme);

                    // Create email
                    var email = new Email()
                    {
                        To = model.Email,
                        Subject = "Reset Password",
                        Body = url
                    };

                    // Send Email
                    var flag = EmailSettings.SendEmail(email);
                    if (flag)
                    {
                        return RedirectToAction("CheckYourInbox");
                    }
                }
                else
                {
                    Console.WriteLine($"User with email {model.Email} not found.");
                }
            }

            ModelState.AddModelError("", "Invalid Reset Password Operation !!");
            return View("ForgetPassword");
        }

        [HttpGet("CheckYourInbox")]
        public IActionResult CheckYourInbox()
        {
            return View();
        }


        #endregion

        #region Reset Password

        [HttpGet]
        public IActionResult ResetPassword(string email, string token)
        {
            TempData["email"] = email;
            TempData["token"] = token;
            return View();
        }
            [HttpPost]
            public async Task<IActionResult> ResetPassword(ResetPasswordDto model)
            {
                if (ModelState.IsValid)
                {
                    var email = TempData["email"] as string;
                    var token = TempData["token"] as string;

                    if (email is null || token is null) return BadRequest("Invalid Operations");

                    var user = await _userManager.FindByEmailAsync(email);

                    if (user is not null)
                    {
                        var result = await _userManager.ResetPasswordAsync(user, token, model.NewPassword);
                        if (result.Succeeded)
                        {
                            var emailObj = new Email
                            {
                                To = email,
                                Subject = "Password Reset Successful",
                                Body = "Your password has been successfully reset."
                            };

                            _mailServices.SendEmail(emailObj);

                            return RedirectToAction("SignIn");
                        }
                    }

                    ModelState.AddModelError("", "Invalid Reset Password Operations!!");
                }

                return View();
            }

        #endregion
        public IActionResult GoogleLogin()
        {
            var prop = new AuthenticationProperties()
            {
                RedirectUri = Url.Action("GoogleResponse")


            };
            return Challenge(prop, GoogleDefaults.AuthenticationScheme);

        }

        public async Task<IActionResult> GoogleResponse()
        {

            var result = await HttpContext.AuthenticateAsync(GoogleDefaults.AuthenticationScheme);
            var cliams = result.Principal.Identities.FirstOrDefault().Claims.Select(
                claim => new
                {
                    claim.Type,
                    claim.Value,
                    claim.Issuer,
                    claim.OriginalIssuer



                }


                );
            return RedirectToAction("Index", "Home");

        }
        public IActionResult FacebookLogin()
        {
            var prop = new AuthenticationProperties()
            {
                RedirectUri = Url.Action("FacebookResponse")


            };
            return Challenge(prop, FacebookDefaults.AuthenticationScheme);

        }

        public async Task<IActionResult> FacebookResponse()
        {

            var result = await HttpContext.AuthenticateAsync(FacebookDefaults.AuthenticationScheme);
            var cliams = result.Principal.Identities.FirstOrDefault().Claims.Select(
                claim => new
                {
                    claim.Type,
                    claim.Value,
                    claim.Issuer,
                    claim.OriginalIssuer



                }


                );
            return RedirectToAction("Index", "Home");

        }
    }

}

       