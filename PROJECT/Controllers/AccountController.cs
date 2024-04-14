using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PROJECT.Models;
using PROJECT.ViewModels;

namespace PROJECT.Controllers
{
    public class AccountController : Controller
    {
        private SignInManager<Admin> _signInManager;
        private UserManager<Admin> _userManager;

        public AccountController(SignInManager<Admin> signInManager, UserManager<Admin> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public IActionResult Login()
        {
            if(User != null && User.Identity != null && User.Identity.IsAuthenticated)
               return RedirectToAction( "ListAll","Customer");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginModel)
        {
            if (ModelState.IsValid)
            {
                var resault = await _signInManager.PasswordSignInAsync(
                    loginModel.UserName,
                    loginModel.Password, 
                    loginModel.RememberMe, 
                    false
                );

                if (resault.Succeeded) return RedirectToAction("ListAll", "Customer");
            }

            ModelState.AddModelError("", "Failed to Login");
            return View(loginModel);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("HomeView", "Home"); 
        }

        public IActionResult Register() => View();

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel regModel)
        {
            if (ModelState.IsValid)
            {
                Admin newAdmin = new Admin()
                {
                    FirstName = regModel.FirstName,
                    LastName = regModel.LastName,
                    Email = regModel.Email,
                    UserName = regModel.UserName,
                };

                var resault = await _userManager.CreateAsync(newAdmin, regModel.Password);

                if (resault.Succeeded)
                {
                    return RedirectToAction("ListAll", "Customer");
                }

                foreach(var error in resault.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(regModel);
        }
    }

    
}
