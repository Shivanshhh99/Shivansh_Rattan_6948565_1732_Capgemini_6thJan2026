using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LibraryApplication.Models;

namespace LibraryApplication.Controllers
{
    public class BorrowersController : Controller
    {
        private readonly LibraryDatabaseContext _context;

        public BorrowersController(LibraryDatabaseContext context)
        {
            _context = context;
        }

        // GET: Borrowers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Borrowers.ToListAsync());
        }

        // GET: Borrowers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var borrower = await _context.Borrowers
                .FirstOrDefaultAsync(m => m.BorrowerId == id);
            if (borrower == null)
            {
                return NotFound();
            }

            return View(borrower);
        }

        // GET: Borrowers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Borrowers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BorrowerId,FullName,Email")] Borrower borrower)
        {
            if (ModelState.IsValid)
            {
                _context.Add(borrower);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(borrower);
        }

        // GET: Borrowers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var borrower = await _context.Borrowers.FindAsync(id);
            if (borrower == null)
            {
                return NotFound();
            }
            return View(borrower);
        }

        // POST: Borrowers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BorrowerId,FullName,Email")] Borrower borrower)
        {
            if (id != borrower.BorrowerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(borrower);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BorrowerExists(borrower.BorrowerId))
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
            return View(borrower);
        }

        // GET: Borrowers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var borrower = await _context.Borrowers
                .FirstOrDefaultAsync(m => m.BorrowerId == id);
            if (borrower == null)
            {
                return NotFound();
            }

            return View(borrower);
        }

        // POST: Borrowers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var borrower = await _context.Borrowers.FindAsync(id);
            if (borrower != null)
            {
                _context.Borrowers.Remove(borrower);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BorrowerExists(int id)
        {
            return _context.Borrowers.Any(e => e.BorrowerId == id);
        }
    }
}
