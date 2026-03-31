using Microsoft.AspNetCore.Mvc;
using StudentPortal.Models;
using StudentPortal.Services;

namespace StudentPortal.Controllers
{
    public class StudentsController : Controller
    {
        private readonly IRequestLogService _logService;

        public StudentsController(IRequestLogService logService)
        {
            _logService = logService;
        }

        public IActionResult Index()
        {
            List<Student> students = new List<Student>()
            {
                new Student{ Id=1, Name="Yash", Course="CSE"},
                new Student{ Id=2, Name="Rahul", Course="IT"},
                new Student{ Id=3, Name="Aman", Course="AI"}
            };

            ViewBag.Logs = _logService.GetLogs();

            return View(students);
        }
    }
}