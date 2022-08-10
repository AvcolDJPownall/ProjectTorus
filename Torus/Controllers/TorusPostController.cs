﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Torus.Data;
using Torus.Models;

namespace Torus.Views.Posts
{
    public class TorusPostController : Controller
    {
        private readonly TorusContext _context;

        public TorusPostController(TorusContext context)
        {
            _context = context;
        }

        // GET: TorusPost
        public async Task<IActionResult> Index()
        {
              return _context.TorusPost != null ? 
                          View(await _context.TorusPost.ToListAsync()) :
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

            return View(torusPost);
        }

        // GET: TorusPost/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TorusPost/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PostID,Title,Description,PostType,ImageThumbnail,Cost,Likes,Dislikes")] TorusPost torusPost, [FromForm]IFormFile ImageThumbnail)
        {
            string path = Path.Combine(Environment.CurrentDirectory, "wwwroot/img/items");
            string sanitizedname = "item-" + Path.GetFileNameWithoutExtension(Uri.EscapeDataString(ImageThumbnail.FileName)) + ".png";
            using (var fileStream = new FileStream(Path.Combine(path, sanitizedname), FileMode.Create))
            {
                if (ImageThumbnail != null && ImageThumbnail.Length > 0)
                {
                    await ImageThumbnail.CopyToAsync(fileStream);
                }
            }

            if (ModelState.IsValid)
            {
                _context.Add(torusPost);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(torusPost);
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
