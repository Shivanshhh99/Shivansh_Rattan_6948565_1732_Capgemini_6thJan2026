using Microsoft.AspNetCore.Mvc;
using LibraryManagement.Models;
using LibraryManagement.ViewModels;

namespace LibraryManagement.Controllers
{
    public class BooksController : Controller
    {
        // Temporary data (no DB)
        private static List<BookViewModel> books = new List<BookViewModel>();

        // INDEX
        public IActionResult Index()
        {
            ViewBag.Message = "Welcome to Library Management System 📚";
            ViewData["TotalBooks"] = books.Count;

            return View(books);
        }

        // GET CREATE
        public IActionResult Create()
        {
            return View();
        }

        // POST CREATE
        [HttpPost]
        public IActionResult Create(BookViewModel vm)
        {
            if (ModelState.IsValid)
            {
                vm.Book.Id = books.Count + 1;
                vm.IsAvailable = true;

                books.Add(vm);

                return RedirectToAction("Index");
            }

            return View(vm);
        }
    }
}