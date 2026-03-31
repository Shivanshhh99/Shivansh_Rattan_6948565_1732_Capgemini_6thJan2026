using Microsoft.AspNetCore.Mvc;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.Controllers
{
    public class StudentController : Controller
    {
        // In-memory "database" — static so data persists across requests
        private static List<Student> _students = new List<Student>();
        private static int _nextId = 1;

        // GET: /Student/Register
        public IActionResult Register()
        {
            return View();
        }

        // POST: /Student/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(Student student)
        {
            if (ModelState.IsValid)
            {
                student.Id = _nextId++;
                student.RegisteredOn = DateTime.Now;
                _students.Add(student);

                // TempData survives exactly one redirect
                TempData["SuccessMessage"] = $"Student '{student.Name}' registered successfully!";

                return RedirectToAction("Details", new { id = student.Id });
            }

            // Validation failed — redisplay form with error messages
            return View(student);
        }

        // GET: /Student/Details/1
        public IActionResult Details(int id)
        {
            var student = _students.FirstOrDefault(s => s.Id == id);
            if (student == null)
                return NotFound();

            return View(student);
        }

        // GET: /Student/List
        public IActionResult List()
        {
            return View(_students);
        }
    }
}