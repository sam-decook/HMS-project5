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
    public class RequirementsController : Controller
    {
        private readonly project5Context _context;

        public RequirementsController(project5Context context)
        {
            _context = context;
        }

        // GET: Requirements
        public async Task<IActionResult> Index()
        {
            return View(await _context.Requirements.ToListAsync());
        }

        // GET: Requirements/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var requirements = await _context.Requirements
                .FirstOrDefaultAsync(m => m.majorID == id);
            if (requirements == null)
            {
                return NotFound();
            }

            return View(requirements);
        }

        // GET: Requirements/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Requirements/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("majorID,courseID,category")] Requirements requirements)
        {
            if (ModelState.IsValid)
            {
                _context.Add(requirements);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(requirements);
        }

        // GET: Requirements/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var requirements = await _context.Requirements.FindAsync(id);
            if (requirements == null)
            {
                return NotFound();
            }
            return View(requirements);
        }

        // POST: Requirements/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("majorID,courseID,category")] Requirements requirements)
        {
            if (id != requirements.majorID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(requirements);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RequirementsExists(requirements.majorID))
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
            return View(requirements);
        }

        // GET: Requirements/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var requirements = await _context.Requirements
                .FirstOrDefaultAsync(m => m.majorID == id);
            if (requirements == null)
            {
                return NotFound();
            }

            return View(requirements);
        }

        // POST: Requirements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var requirements = await _context.Requirements.FindAsync(id);
            if (requirements != null)
            {
                _context.Requirements.Remove(requirements);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RequirementsExists(int id)
        {
            return _context.Requirements.Any(e => e.majorID == id);
        }
    }
}
