using EmployeeManager.ApiClient.Models;
using EmployeeManager.ApiClient.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManager.ApiClient.Controllers
{
    public class SecurityController : Controller
    {
        private readonly UserManager<AppIdentityUser> userManager;
        private readonly RoleManager<AppIdentityRole> roleManager;
        private readonly SignInManager<AppIdentityUser> signInManager;

        public SecurityController(UserManager<AppIdentityUser> userManager, RoleManager<AppIdentityRole> roleManager, SignInManager<AppIdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.signInManager = signInManager;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(Register register)
        {
            if (ModelState.IsValid)
            {
                if (!roleManager.RoleExistsAsync("Manager").Result)
                {
                    var role = new AppIdentityRole
                    {
                        Name = "Manager",
                        Description = "Manager can perform CRUD operations"
                    };
                    var roleResult = roleManager.CreateAsync(role).Result;
                }
                var user = new AppIdentityUser
                {
                    UserName = register.UserName,
                    Email = register.Email,
                    FullName = register.FirstName + ' ' + register.LastName,
                    BirthDate = register.BirthDate
                };

                var result = userManager.CreateAsync(user, register.Password).Result;
                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Manager").Wait();
                    return RedirectToAction("SignIn", "Security");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid User Details");
                }
            }
            return View(register);
        }
        [HttpGet]
    public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SignIn(SignIn signIn)
        {
            if (ModelState.IsValid)
            {
                var result = signInManager.PasswordSignInAsync(signIn.UserName, signIn.Password, signIn.RememberMe, false).Result;
                if (result.Succeeded)
                {
                    return RedirectToAction("List", "EmployeeManager");
                }else
                    ModelState.AddModelError("", "Invalid User Details");
            }
            return View(signIn);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult SignOut()
        {
            signInManager.SignOutAsync().Wait();
            return RedirectToAction("SignIn", "Security");
        }
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
