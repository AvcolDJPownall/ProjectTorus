using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Torus.Pages
{
    public class BrowseModel : PageModel
    {
        private readonly ILogger<BrowseModel> _logger;

        public BrowseModel(ILogger<BrowseModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
    }
}