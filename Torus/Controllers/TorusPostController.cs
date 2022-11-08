using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Torus.Areas.Identity.Data;
using Torus.Data;
using Torus.Models;
using SkiaSharp;
using Microsoft.AspNetCore.Authorization;

namespace Torus.Views.Posts
{
    public class TorusPostController : Controller
    {
        private readonly TorusContext _context;
        private readonly UserManager<TorusUser> _userManager;
        private readonly SignInManager<TorusUser> _signInManager;


        public TorusPostController(TorusContext context, UserManager<TorusUser> userManager, SignInManager<TorusUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // GET: TorusPost
        public async Task<IActionResult> Index()
        {
              return _context.TorusPost != null ?
                          RedirectToAction("Index", "Browse", await _context.TorusPost.ToListAsync()) :
                          Problem("Entity set 'TorusContext.TorusPost'  is null.");
        }

        // GET: TorusPost/Details/5
        public async Task<IActionResult> Details(uint? id)
        {
            if (id == null || _context.TorusPost == null)
            {
                return NotFound();
            }

            var torusPost = await _context.TorusPost
                .FirstOrDefaultAsync(m => m.PostID == id);
            if (torusPost == null)
            {
                return NotFound();
            }

            torusPost.PageViews += 1;

            return View(torusPost);
        }

        // GET: TorusPost/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: TorusPost/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PostID,Title,Description,PostType,ImageThumbnail,Cost,Likes,Dislikes")] TorusPost torusPost, [FromForm]IFormFile ImageThumbnail, [FromForm]IFormFile AssetFile)
        {
            string imgpath = Path.Combine(Environment.CurrentDirectory, "wwwroot/img/items");
            string imageGUID = "item-" + Guid.NewGuid().ToString();
            string imgFileName = imageGUID + ".png";

            // Write image file to disk/cdn's filesystem
            using (var fileStream = new FileStream(Path.Combine(imgpath, imgFileName), FileMode.Create))
            {
                if (ImageThumbnail != null && ImageThumbnail.Length > 0)
                {
                    var imgstream = new SKManagedStream(ImageThumbnail.OpenReadStream());
                    var image = SKBitmap.Decode(imgstream);
                    if (image == null) ModelState.AddModelError("ImageThumbnail", "Your thumbnail's format is not supported.");
                    else if (image.Width - image.Height != 0) ModelState.AddModelError("ImageThumbnail", "Your thumbnail must have an aspect ratio of 1:1.");
                    else {
                        await ImageThumbnail.CopyToAsync(fileStream);
                        torusPost.ImageFileGUID = imageGUID;
                    }
                    imgstream.Dispose();
                }
            }

            // Sanitize user's filename input
            string assetpath = Path.Combine(Environment.CurrentDirectory, "wwwroot/cdn/downloads");
            string assetFileName = torusPost.Title + "-" + imageGUID.Split('-').Last();
            IEnumerable<char> invalidchars = Path.GetInvalidFileNameChars().Intersect(assetFileName.ToCharArray());
            foreach (char character in invalidchars)
            {
                assetFileName = assetFileName.Replace(character, '-');
            }
            assetFileName = assetFileName.Replace(' ', '_');
            assetFileName += "." + AssetFile.FileName.Split('.').LastOrDefault("asset");

            // Write uploaded asset file to disk/cdn's filesystem
            using (var fileStream = new FileStream(Path.Combine(assetpath, assetFileName), FileMode.Create))
            {
                if (AssetFile != null && AssetFile.Length > 0)
                {
                    await AssetFile.CopyToAsync(fileStream);
                    torusPost.AssetFileGUID = assetFileName;
                }
            }

            var user = await _userManager.GetUserAsync(User);
            torusPost.AuthorID = user.Id;

            if (ModelState.IsValid)
            {
                _context.Add(torusPost);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(torusPost);
        }

        public async Task<IActionResult> LikePost(uint id)
        {
            TorusPost? torusPost = await _context.TorusPost.FindAsync(id);
            if (!_signInManager.IsSignedIn(this.User))
            {
                return Redirect("/Identity/Account/Login");
            }

            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user.LikedPosts == null) user.LikedPosts = new List<TorusPost>();
                if (user.DiskedPosts == null) user.DiskedPosts = new List<TorusPost>();


                //if (!user.LikedPosts.Contains(torusPost)) return Redirect("Details/" + id.ToString());

                if (user.LikedPosts.Contains(torusPost)) return Redirect("Details/" + id.ToString());
                user.DiskedPosts.Remove(torusPost);
                user.LikedPosts.Add(torusPost);

                torusPost.Likes += 1;
                await _context.SaveChangesAsync();
                return Redirect("Details/" + id.ToString());
            }
            return Redirect("Details/" + id.ToString());
        }

        public async Task<IActionResult> DislikePost(uint id)
        {
            TorusPost? torusPost = await _context.TorusPost.FindAsync(id);
            if (!_signInManager.IsSignedIn(this.User))
            {
                return Redirect("/Identity/Account/Login");
            }


            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user.LikedPosts == null) user.LikedPosts = new List<TorusPost>();
                if (user.DiskedPosts == null) user.DiskedPosts = new List<TorusPost>();


                if (user.DiskedPosts.Contains(torusPost)) return Redirect("Details/" + id.ToString());
                if (user.LikedPosts.Contains(torusPost)) user.LikedPosts.Remove(torusPost);

                user.DiskedPosts.Add(torusPost);

                torusPost.Dislikes += 1;
                await _context.SaveChangesAsync();
                return Redirect("Details/" + id.ToString());
            }
            return Redirect("Details/" + id.ToString());
        }

        // GET: TorusPost/Edit/5
        public async Task<IActionResult> Edit(uint? id)
        {
            if (id == null || _context.TorusPost == null)
            {
                return NotFound();
            }

            var torusPost = await _context.TorusPost.FindAsync(id);
            if (torusPost == null)
            {
                return NotFound();
            }
            return View(torusPost);
        }

        // POST: TorusPost/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(uint id, [Bind("PostID,Title,Description,PostType,Cost,Likes,Dislikes")] TorusPost torusPost)
        {
            if (id != torusPost.PostID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(torusPost);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TorusPostExists(torusPost.PostID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(torusPost);
        }

        // GET: TorusPost/Delete/5
        public async Task<IActionResult> Delete(uint? id)
        {
            if (id == null || _context.TorusPost == null)
            {
                return NotFound();
            }

            var torusPost = await _context.TorusPost
                .FirstOrDefaultAsync(m => m.PostID == id);
            if (torusPost == null)
            {
                return NotFound();
            }

            return View(torusPost);
        }

        // POST: TorusPost/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(uint id)
        {
            if (_context.TorusPost == null)
            {
                return Problem("Entity set 'TorusContext.TorusPost'  is null.");
            }
            var torusPost = await _context.TorusPost.FindAsync(id);
            if (torusPost != null)
            {
                _context.TorusPost.Remove(torusPost);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TorusPostExists(uint id)
        {
          return (_context.TorusPost?.Any(e => e.PostID == id)).GetValueOrDefault();
        }
    }
}
