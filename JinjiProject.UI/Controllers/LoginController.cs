using JinjiProject.Core.Entities.Concrete;
using JinjiProject.UI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace JinjiProject.UI.Controllers
{
    public class LoginController : BaseController
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;

        public LoginController(UserManager<AppUser> userManager,SignInManager<AppUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            if (TempData["Login"] != null)
            {
                var user = await userManager.GetUserAsync(User);
                var userRole = await userManager.GetRolesAsync(user);
                return RedirectToAction("Index", "Home", new { Area = userRole[0].ToString() });
            }
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Index(LoginVM loginVM)
        {
            if (!ModelState.IsValid)
                return View(loginVM);
            var user = await userManager.FindByEmailAsync(loginVM.Email);

            if (user is null)
            {
                NotifyError("Email veya şifre hatalı");
                return View(loginVM);
            }

            var checkPass = await signInManager.PasswordSignInAsync(user, loginVM.Password, loginVM.RememberMe, false);

            if (!checkPass.Succeeded)
            {
                NotifyError("Email veya şifre hatalı");
                return View(loginVM);
            }

            var userRole = await userManager.GetRolesAsync(user);
            if (userRole is null)
            {
                NotifyError("Kullanıcıya ait rol bulunamadı");
                return View(loginVM);
            }

            TempData["Login"] = "ok";
            Json(new { success = true });
            return RedirectToAction("Index", "Home", new { Area = userRole[0].ToString() });
        }
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
