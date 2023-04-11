using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EshopMVC.Data;
using EshopMVC.Models;

namespace EshopMVC.Controllers
{
    public class KategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public KategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Kategories
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Kategorie.Include(k => k.ParentKategorie);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Kategories/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Kategorie == null)
            {
                return NotFound();
            }

            var kategorie = await _context.Kategorie
                .Include(k => k.ParentKategorie)
                .FirstOrDefaultAsync(m => m.KategorieId == id);
            if (kategorie == null)
            {
                return NotFound();
            }

            return View(kategorie);
        }

        // GET: Kategories/Create
        public IActionResult Create()
        {
            ViewData["ParentKategorieId"] = new SelectList(_context.Kategorie, "KategorieId", "KategorieId");
            return View();
        }

        // POST: Kategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("KategorieId,Nazev,ParentKategorieId")] Kategorie kategorie)
        {
            if (ModelState.IsValid)
            {
                kategorie.KategorieId = Guid.NewGuid();
                _context.Add(kategorie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ParentKategorieId"] = new SelectList(_context.Kategorie, "KategorieId", "KategorieId", kategorie.ParentKategorieId);
            return View(kategorie);
        }

        // GET: Kategories/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Kategorie == null)
            {
                return NotFound();
            }

            var kategorie = await _context.Kategorie.FindAsync(id);
            if (kategorie == null)
            {
                return NotFound();
            }
            ViewData["ParentKategorieId"] = new SelectList(_context.Kategorie, "KategorieId", "KategorieId", kategorie.ParentKategorieId);
            return View(kategorie);
        }

        // POST: Kategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("KategorieId,Nazev,ParentKategorieId")] Kategorie kategorie)
        {
            if (id != kategorie.KategorieId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(kategorie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KategorieExists(kategorie.KategorieId))
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
            ViewData["ParentKategorieId"] = new SelectList(_context.Kategorie, "KategorieId", "KategorieId", kategorie.ParentKategorieId);
            return View(kategorie);
        }

        // GET: Kategories/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Kategorie == null)
            {
                return NotFound();
            }

            var kategorie = await _context.Kategorie
                .Include(k => k.ParentKategorie)
                .FirstOrDefaultAsync(m => m.KategorieId == id);
            if (kategorie == null)
            {
                return NotFound();
            }

            return View(kategorie);
        }

        // POST: Kategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Kategorie == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Kategorie'  is null.");
            }
            var kategorie = await _context.Kategorie.FindAsync(id);
            if (kategorie != null)
            {
                _context.Kategorie.Remove(kategorie);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KategorieExists(Guid id)
        {
          return (_context.Kategorie?.Any(e => e.KategorieId == id)).GetValueOrDefault();
        }
    }
}
