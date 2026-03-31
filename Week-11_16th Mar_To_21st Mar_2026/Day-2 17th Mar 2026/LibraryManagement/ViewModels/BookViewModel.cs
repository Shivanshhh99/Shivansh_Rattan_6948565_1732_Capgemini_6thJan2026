using LibraryManagement.Models;

namespace LibraryManagement.ViewModels
{
    public class BookViewModel
    {
        public Book Book { get; set; }

        public bool IsAvailable { get; set; }

        // Advanced (Practice requirement)
        public string BorrowerName { get; set; }
    }
}