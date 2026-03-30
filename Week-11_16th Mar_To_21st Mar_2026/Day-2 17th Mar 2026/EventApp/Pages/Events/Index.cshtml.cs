using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EventApp.Data;
using EventApp.Models;

namespace EventApp.Pages.Events
{
    public class IndexModel : PageModel
    {
        public List<EventRegistration> Participants { get; set; }

        public void OnGet()
        {
            Participants = EventStore.Participants;
        }

        // DELETE HANDLER
        public IActionResult OnPostDelete(int id)
        {
            var item = EventStore.Participants.FirstOrDefault(x => x.Id == id);

            if (item != null)
            {
                EventStore.Participants.Remove(item);
            }

            return RedirectToPage();
        }
    }
}