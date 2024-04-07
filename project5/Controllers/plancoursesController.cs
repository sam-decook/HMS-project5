using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using project5.Data;
using project5.Models;

namespace project5.Controllers
{
    public class plancoursesController : Controller
    {
        private readonly project5Context _context;

        public plancoursesController(project5Context context)
        {
            _context = context;
        }

        // GET: plancourses
        public async Task<IActionResult> Index()
        {
            return View(await _context.plancourses.ToListAsync());
        }

        // GET: plancourses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plancourses = await _context.plancourses
                .FirstOrDefaultAsync(m => m.planID == id);
            if (plancourses == null)
            {
                return NotFound();
            }

            return View(plancourses);
        }

        // GET: plancourses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: plancourses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("planID,courseID,yearTaken,termTaken")] plancourses plancourses)
        {
            if (ModelState.IsValid)
            {
                _context.Add(plancourses);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(plancourses);
        }

        // GET: plancourses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plancourses = await _context.plancourses.FindAsync(id);
            if (plancourses == null)
            {
                return NotFound();
            }
            return View(plancourses);
        }

        // POST: plancourses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("planID,courseID,yearTaken,termTaken")] plancourses plancourses)
        {
            if (id != plancourses.planID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(plancourses);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!plancoursesExists(plancourses.planID))
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
            return View(plancourses);
        }

        // GET: plancourses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plancourses = await _context.plancourses
                .FirstOrDefaultAsync(m => m.planID == id);
            if (plancourses == null)
            {
                return NotFound();
            }

            return View(plancourses);
        }

        // POST: plancourses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var plancourses = await _context.plancourses.FindAsync(id);
            if (plancourses != null)
            {
                _context.plancourses.Remove(plancourses);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool plancoursesExists(int id)
        {
            return _context.plancourses.Any(e => e.planID == id);
        }
    }
}
