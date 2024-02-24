using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Security.Claims;

namespace JinjiProject.UI.Controllers
{
    public class BaseController : Controller
    {
        protected string? UserIdentityId => User.FindFirstValue(ClaimTypes.NameIdentifier);
        protected string? UserId => User.FindAll(ClaimTypes.NameIdentifier).LastOrDefault()?.Value;
        protected INotyfService NotyfService => HttpContext.RequestServices.GetService(typeof(INotyfService)) as INotyfService;

        protected void NotifySuccess(string message)
        {
            NotyfService.Success(message);
        }

        protected void NotifyError(string message)
        {
            NotyfService.Error(message);
        }

        protected void NotifyWarning(string message)
        {
            NotyfService.Warning(message);
        }

    }
}
