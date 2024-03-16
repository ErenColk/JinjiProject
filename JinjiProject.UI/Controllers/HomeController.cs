using JinjiProject.BusinessLayer.Managers.Abstract;
using JinjiProject.UI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace JinjiProject.UI.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ISendMailService sendMailService;

        public HomeController(ILogger<HomeController> logger, ISendMailService sendMailService)
        {
            _logger = logger;
            this.sendMailService = sendMailService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AboutUs()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
