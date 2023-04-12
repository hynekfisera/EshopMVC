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
    public class VyrobeksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VyrobeksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Vyrobeks
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Vyrobek.Include(v => v.Kategorie);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Vyrobeks/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Vyrobek == null)
            {
                return NotFound();
            }

            var vyrobek = await _context.Vyrobek
                .Include(v => v.Kategorie)
                .FirstOrDefaultAsync(m => m.VyrobekId == id);
            if (vyrobek == null)
            {
                return NotFound();
            }

            return View(vyrobek);
        }

        // GET: Vyrobeks/Create
        public IActionResult Create()
        {
            ViewData["KategorieId"] = new SelectList(_context.Kategorie, "KategorieId", "Nazev");
            return View();
        }

        // POST: Vyrobeks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VyrobekId,Nazev,Popis,Obrazek,Cena,KategorieId")] Vyrobek vyrobek)
        {
            if (ModelState.IsValid)
            {
                vyrobek.VyrobekId = Guid.NewGuid();
                _context.Add(vyrobek);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["KategorieId"] = new SelectList(_context.Kategorie, "KategorieId", "Nazev", vyrobek.KategorieId);
            return View(vyrobek);
        }

        // GET: Vyrobeks/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Vyrobek == null)
            {
                return NotFound();
            }

            var vyrobek = await _context.Vyrobek.FindAsync(id);
            if (vyrobek == null)
            {
                return NotFound();
            }
            ViewData["KategorieId"] = new SelectList(_context.Kategorie, "KategorieId", "Nazev", vyrobek.KategorieId);
            return View(vyrobek);
        }

        // POST: Vyrobeks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("VyrobekId,Nazev,Popis,Obrazek,Cena,KategorieId")] Vyrobek vyrobek)
        {
            if (id != vyrobek.VyrobekId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vyrobek);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VyrobekExists(vyrobek.VyrobekId))
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
            ViewData["KategorieId"] = new SelectList(_context.Kategorie, "KategorieId", "Nazev", vyrobek.KategorieId);
            return View(vyrobek);
        }

        // GET: Vyrobeks/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Vyrobek == null)
            {
                return NotFound();
            }

            var vyrobek = await _context.Vyrobek
                .Include(v => v.Kategorie)
                .FirstOrDefaultAsync(m => m.VyrobekId == id);
            if (vyrobek == null)
            {
                return NotFound();
            }

            return View(vyrobek);
        }

        // POST: Vyrobeks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Vyrobek == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Vyrobek'  is null.");
            }
            var vyrobek = await _context.Vyrobek.FindAsync(id);
            if (vyrobek != null)
            {
                _context.Vyrobek.Remove(vyrobek);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VyrobekExists(Guid id)
        {
          return (_context.Vyrobek?.Any(e => e.VyrobekId == id)).GetValueOrDefault();
        }
    }
}
