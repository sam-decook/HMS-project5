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
    public class CatalogcoursesController : Controller
    {
        private readonly project5Context _context;

        public CatalogcoursesController(project5Context context)
        {
            _context = context;
        }

        // GET: Catalogcourses
        public async Task<IActionResult> Index()
        {
            return View(await _context.Catalogcourse.ToListAsync());
        }

        // GET: Catalogcourses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var catalogcourse = await _context.Catalogcourse
                .FirstOrDefaultAsync(m => m.year == id);
            if (catalogcourse == null)
            {
                return NotFound();
            }

            return View(catalogcourse);
        }

        // GET: Catalogcourses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Catalogcourses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("year,courseID")] Catalogcourse catalogcourse)
        {
            if (ModelState.IsValid)
            {
                _context.Add(catalogcourse);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(catalogcourse);
        }

        // GET: Catalogcourses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var catalogcourse = await _context.Catalogcourse.FindAsync(id);
            if (catalogcourse == null)
            {
                return NotFound();
            }
            return View(catalogcourse);
        }

        // POST: Catalogcourses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("year,courseID")] Catalogcourse catalogcourse)
        {
            if (id != catalogcourse.year)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(catalogcourse);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CatalogcourseExists(catalogcourse.year))
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
            return View(catalogcourse);
        }

        // GET: Catalogcourses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var catalogcourse = await _context.Catalogcourse
                .FirstOrDefaultAsync(m => m.year == id);
            if (catalogcourse == null)
            {
                return NotFound();
            }

            return View(catalogcourse);
        }

        // POST: Catalogcourses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var catalogcourse = await _context.Catalogcourse.FindAsync(id);
            if (catalogcourse != null)
            {
                _context.Catalogcourse.Remove(catalogcourse);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CatalogcourseExists(int id)
        {
            return _context.Catalogcourse.Any(e => e.year == id);
        }
    }
}
