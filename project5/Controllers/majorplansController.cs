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
    public class majorplansController : Controller
    {
        private readonly project5Context _context;

        public majorplansController(project5Context context)
        {
            _context = context;
        }

        // GET: majorplans
        public async Task<IActionResult> Index()
        {
            return View(await _context.majorplan.ToListAsync());
        }

        // GET: majorplans/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var majorplan = await _context.majorplan
                .FirstOrDefaultAsync(m => m.userPlanID == id);
            if (majorplan == null)
            {
                return NotFound();
            }

            return View(majorplan);
        }

        // GET: majorplans/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: majorplans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("userPlanID,planID,majorID")] majorplan majorplan)
        {
            if (ModelState.IsValid)
            {
                _context.Add(majorplan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(majorplan);
        }

        // GET: majorplans/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var majorplan = await _context.majorplan.FindAsync(id);
            if (majorplan == null)
            {
                return NotFound();
            }
            return View(majorplan);
        }

        // POST: majorplans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("userPlanID,planID,majorID")] majorplan majorplan)
        {
            if (id != majorplan.userPlanID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(majorplan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!majorplanExists(majorplan.userPlanID))
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
            return View(majorplan);
        }

        // GET: majorplans/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var majorplan = await _context.majorplan
                .FirstOrDefaultAsync(m => m.userPlanID == id);
            if (majorplan == null)
            {
                return NotFound();
            }

            return View(majorplan);
        }

        // POST: majorplans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var majorplan = await _context.majorplan.FindAsync(id);
            if (majorplan != null)
            {
                _context.majorplan.Remove(majorplan);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool majorplanExists(int id)
        {
            return _context.majorplan.Any(e => e.userPlanID == id);
        }
    }
}
