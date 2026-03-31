using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EventApp.Models;
using EventApp.Data;

namespace EventApp.Pages.Events
{
    public class RegisterModel : PageModel
    {
        [BindProperty]
        public EventRegistration Registration { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Registration.Id = EventStore.Participants.Count + 1;
            EventStore.Participants.Add(Registration);

            return RedirectToPage("Index");
        }
    }
}