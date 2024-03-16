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
        private readonly IGenreService _genreService;

        public HomeController(ILogger<HomeController> logger, ISendMailService sendMailService,IGenreService genreService)
        {
            _logger = logger;
            _genreService = genreService;
            this.sendMailService = sendMailService;

        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> HomePageGenreList()
        {
            var genreList = await _genreService.GetAllByExpression(genre => genre.IsOnHomePage == true);
            return PartialView("_HomePageGenreListPartialView", genreList.Data);

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
