using Microsoft.AspNetCore.Mvc;
using LibraryApp.Repositories;

namespace LibraryApp.Controllers
{
    public class BooksController : Controller
    {
        private readonly IBookRepository _repository;

        public BooksController(IBookRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Index()
        {
            var books = _repository.GetAllBooks();

            return View(books);
        }

        public IActionResult Details(int id)
        {
            var book = _repository.GetBookById(id);

            return View(book);
        }
    }
}