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
    public class SlevasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SlevasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Slevas
        public async Task<IActionResult> Index()
        {
              return _context.Sleva != null ? 
                          View(await _context.Sleva.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Sleva'  is null.");
        }

        // GET: Slevas/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Sleva == null)
            {
                return NotFound();
            }

            var sleva = await _context.Sleva
                .FirstOrDefaultAsync(m => m.SlevaId == id);
            if (sleva == null)
            {
                return NotFound();
            }

            return View(sleva);
        }

        // GET: Slevas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Slevas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SlevaId,Hodnota,JeProcentualni,MinimalniPocet,DatumZacatku,DatumKonce")] Sleva sleva)
        {
            if (ModelState.IsValid)
            {
                sleva.SlevaId = Guid.NewGuid();
                _context.Add(sleva);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sleva);
        }

        // GET: Slevas/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Sleva == null)
            {
                return NotFound();
            }

            var sleva = await _context.Sleva.FindAsync(id);
            if (sleva == null)
            {
                return NotFound();
            }
            return View(sleva);
        }

        // POST: Slevas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("SlevaId,Hodnota,JeProcentualni,MinimalniPocet,DatumZacatku,DatumKonce")] Sleva sleva)
        {
            if (id != sleva.SlevaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sleva);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SlevaExists(sleva.SlevaId))
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
            return View(sleva);
        }

        // GET: Slevas/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Sleva == null)
            {
                return NotFound();
            }

            var sleva = await _context.Sleva
                .FirstOrDefaultAsync(m => m.SlevaId == id);
            if (sleva == null)
            {
                return NotFound();
            }

            return View(sleva);
        }

        // POST: Slevas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Sleva == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Sleva'  is null.");
            }
            var sleva = await _context.Sleva.FindAsync(id);
            if (sleva != null)
            {
                _context.Sleva.Remove(sleva);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SlevaExists(Guid id)
        {
          return (_context.Sleva?.Any(e => e.SlevaId == id)).GetValueOrDefault();
        }
    }
}
