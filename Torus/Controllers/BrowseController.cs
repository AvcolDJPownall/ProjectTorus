using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Torus.Data;

namespace Torus.Controllers
{
    public class BrowseController : Controller
    {
        private readonly TorusContext _context;

        public BrowseController(TorusContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return _context.TorusPost != null ?
                View("Browse", await _context.TorusPost.ToListAsync()) :
                Problem("Something bad happened help");
        }
    }
}
