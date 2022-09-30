using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Torus.Data;
using Torus.Models;

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

        public async Task<IActionResult> Search(string q)
        {
            ICollection<TorusPost> dbPosts = await _context.TorusPost.ToListAsync();
            if (q == null) return View("Browse", dbPosts);
            q = q.ToLower();

            var filteredPosts = dbPosts.Where(p => p.Title.ToLower().Contains(q));
            return View("Browse", filteredPosts);
        }

    }
}
