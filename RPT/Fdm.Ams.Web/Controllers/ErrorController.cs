using Fdm.Ams.ViewModels;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Fdm.Ams.Web.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Index()
        {
            var exceptionObject = HttpContext.Features.Get<IExceptionHandlerFeature>();

            ErrorViewModel errorViewModel = new();

            var httpStatusCode = ((HttpRequestException)exceptionObject.Error).StatusCode;
            if (httpStatusCode != null) errorViewModel.ErrorCode = httpStatusCode.Value;
            errorViewModel.ErrorDetails = exceptionObject.Error.Message;
            errorViewModel.DateTime = DateTime.Now;
            return View(errorViewModel);
        }
    }
}