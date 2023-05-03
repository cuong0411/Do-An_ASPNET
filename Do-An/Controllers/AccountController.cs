﻿using Do_An.Data;
using Do_An.Data.Static;
using Do_An.Models.DTO;
using Do_An.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Threading.Tasks;

namespace Do_An.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly AppDbContext appDbContext;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, AppDbContext appDbContext)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.appDbContext = appDbContext;
        }
        public IActionResult Login() => View(new Login());
        [HttpPost]
        public async Task<IActionResult> Login(Login login)
        {
            if (!ModelState.IsValid)
            {
                return View(login);
            }

            var user = await userManager.FindByEmailAsync(login.Email);
            if (user != null)
            {
                var passwordCheck = await userManager.CheckPasswordAsync(user, login.Password);
                if (passwordCheck)
                {
                    var result = await signInManager.PasswordSignInAsync(user, login.Password, false, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
            }

            TempData["Error"] = "Wrong credentials. Please try again";
            return View(login);
        }

        public IActionResult Register() => View(new Register());
        [HttpPost]
        public async Task<IActionResult> Register(Register register)
        {
            if (!ModelState.IsValid)
            {
                return View(register);
            }

            var user = await userManager.FindByEmailAsync(register.Email);
            if (user != null)
            {
                TempData["Error"] = "This email is already in use";
                return View(register);
            }

            var newUser = new ApplicationUser()
            {
                FullName = register.FullName,
                Email = register.Email,
                UserName = register.Email
            };
            var newUserReponse = await userManager.CreateAsync(newUser, register.Password);
            if (newUserReponse.Succeeded)
            {
                await userManager.AddToRoleAsync(newUser, UserRoles.User);
                return View("RegisterCompleted");
            }
            else
            {
                var errors = newUserReponse.Errors;
                StringBuilder sb = new StringBuilder();
                foreach (var error in errors)
                {
                    sb.Append(error.Description);
                    sb.AppendLine();
                }
                TempData["Error"] = sb.ToString();
                return View(register);
            }
        }
    }
}
