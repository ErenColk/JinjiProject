using JinjiProject.BusinessLayer.Managers.Abstract;
using JinjiProject.BusinessLayer.Validator.ResetPasswordValidations;
using JinjiProject.Core.Entities.Concrete;
using JinjiProject.Dtos.SendMails;
using JinjiProject.UI.Models;
using JinjiProject.VMs.ResetPassword;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace JinjiProject.UI.Controllers
{
    public class LoginController : BaseController
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly ISendMailService sendMailService;

        public LoginController(UserManager<AppUser> userManager,SignInManager<AppUser> signInManager,ISendMailService sendMailService)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.sendMailService = sendMailService;
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
            if(user.LockoutEnabled == true)
            {
                NotifyError("Kullanıcı pasifize edilmiş");
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
            return RedirectToAction("Index", "Home", new { Area = userRole[0].ToString() });
        }
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            TempData["Login"] = null;
            return RedirectToAction("Index");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordVM forgetPasswordVM)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(forgetPasswordVM.Mail);
                if (user == null)
                {
                    NotifyError("Kayıtlı E-mail bulunamadı!");
                    return View();
                }
                string passwordResetToken = await userManager.GeneratePasswordResetTokenAsync(user);
                var passwordResetTokenLink = Url.Action("ResetPassword", "Login", new
                {
                    userId = user.Id,
                    token = passwordResetToken
                }, HttpContext.Request.Scheme);
                try
                {
                   await sendMailService.SendEmailRenewPassword(new RenewPasswordDto() { Email = forgetPasswordVM.Mail, Link = passwordResetTokenLink });
                }
                catch (Exception)
                {

                    NotifyError("Mail Gönderimi Başarısız");
                }

                NotifySuccess("Şifre yenileme bağlantısı başarıyla iletildi!");
                return View();
            }
            NotifyError("Kayıtlı E-mail bulunamadı!");
            return View();
        }

        [HttpGet]
        public IActionResult ResetPassword(string userID, string token)
        {
            TempData["userID"] = userID;
            TempData["token"] = token;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordVM resetPasswordVM)
        {
            var userID = TempData["userID"];
            var token = TempData["token"];

            if (userID == null || token == null)
            {
                NotifyError("Hata!");
                return View();
            }

            ResetPasswordVMValidator validator = new ResetPasswordVMValidator();
            var valid = validator.Validate(resetPasswordVM);
            if (valid.IsValid)
            {
                var user = await userManager.FindByIdAsync(userID.ToString());

                var result = await userManager.ResetPasswordAsync(user, token.ToString(), resetPasswordVM.Password);

                if (result.Succeeded)
                {
                    NotifySuccess("Şifre değiştirme işlemi başarılı!");
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("Hata", item.Description);
                    }
                    TempData["userID"] = userID;
                    TempData["token"] = token;
                    return View();
                }
            }
            foreach (var item in valid.Errors)
            {
                ModelState.AddModelError("Hata", item.ErrorMessage);
                NotifyError(item.ErrorMessage);
            }
            TempData["userID"] = userID;
            TempData["token"] = token;
            return View();
        }
    }
}
