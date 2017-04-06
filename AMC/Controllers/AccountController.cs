using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AMC.BLL.Interfaces;
using AMC.WEB.ViewModels.Account;
using AMC.BLL.Models;

namespace AMC.WEB.Controllers
{
    public class AccountController : Controller
    {
        IUserManager _userManager;

        public AccountController(IUserManager userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                LoginResult loginResult = _userManager.Login(model.Username, model.Password);
                if (loginResult.Success)
                {
                    await HttpContext.Authentication.SignInAsync("Cookies", loginResult.Principal);
                    
                    return RedirectToAction("Index", "Home");
                }

                if (!loginResult.IsRegistered)
                {
                    ModelState.AddModelError("Username", "Username must be registered before logging in");
                }
                else
                {
                    ModelState.AddModelError("Username", "Invalid Login Atempt");
                }
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                RegisterResult registerResult = _userManager.Register(model.Username, model.Password);
                if (registerResult.Success)
                {
                    return RedirectToAction("Login", "Account");
                }

                ModelState.AddModelError("Username", "Invalid Registration Atempt");
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.Authentication.SignOutAsync("Cookies");
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}