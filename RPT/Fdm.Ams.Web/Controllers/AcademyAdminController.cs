using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Fdm.Ams.Web.Controllers
{
    public class AcademyAdminController : Controller
    {
        private readonly ILogger logger;

        public AcademyAdminController(ILogger<AcademyAdminController> logger)
        {
            this.logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}