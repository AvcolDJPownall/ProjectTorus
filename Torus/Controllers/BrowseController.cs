using Microsoft.AspNetCore.Mvc;

namespace Torus.Controllers
{
    public class BrowseController : Controller
    {
        public IActionResult Index()
        {
            return View("Browse");
        }
    }
}
