using JinjiProject.BusinessLayer.Managers.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace JinjiProject.UI.Areas.Admin.Controllers
{
    public class SubscriberController : AdminBaseController
    {
        private readonly ISubscriberService subscriberService;

        public SubscriberController(ISubscriberService subscriberService)
        {
            this.subscriberService = subscriberService;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
