using ECommerceOMS.Data;
using ECommerceOMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerceOMS.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly AppDbContext _context;
        public CategoriesController(AppDbContext context) => _context = context;

        public async Task<IActionResult> Index()
            => View(await _context.Categories.Include(c => c.Products).ToListAsync());

        public IActionResult Create() => View();

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category category)
        {
            if (!ModelState.IsValid) return View(category);
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Category created!";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var cat = await _context.Categories.FindAsync(id);
            if (cat == null) return NotFound();
            return View(cat);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Category category)
        {
            if (id != category.Id) return BadRequest();
            if (!ModelState.IsValid) return View(category);
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Category updated!";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var cat = await _context.Categories.FindAsync(id);
            if (cat == null) return NotFound();
            return View(cat);
        }

        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cat = await _context.Categories.FindAsync(id);
            if (cat != null) _context.Categories.Remove(cat);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Category deleted.";
            return RedirectToAction(nameof(Index));
        }
    }
}