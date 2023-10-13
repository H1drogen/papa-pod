using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Fdm.Ams.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger logger;

        public HomeController(ILogger<HomeController> logger)
        {
            this.logger = logger;
        }

        public IActionResult Index()
        {
            logger.LogInformation("HomeController - Index");
            return View("../AcademyAdmin/Index");
        }
    }
}