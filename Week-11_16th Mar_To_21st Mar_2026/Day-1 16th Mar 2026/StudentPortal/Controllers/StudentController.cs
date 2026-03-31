using Microsoft.AspNetCore.Mvc;
using StudentPortal.Models;

namespace StudentPortal.Controllers
{
    public class StudentController : Controller
    {
        // Static list (temporary storage instead of DB)
        private static List<Student> students = new List<Student>();
        private static int nextId = 1;

        // GET: Register Page
        public IActionResult Register()
        {
            return View();
        }

        // POST: Register Student
        [HttpPost]
        public IActionResult Register(Student student)
        {
            if (ModelState.IsValid)
            {
                student.Id = nextId++;
                students.Add(student);

                TempData["Success"] = "Student registered successfully!";

                return RedirectToAction("Details", new { id = student.Id });
            }

            return View(student);
        }

        // GET: Details Page
        public IActionResult Details(int id)
        {
            var student = students.FirstOrDefault(s => s.Id == id);

            if (student == null)
                return NotFound();

            return View(student);
        }

        public IActionResult List()
        {
            return View(students);
        }
    }
}