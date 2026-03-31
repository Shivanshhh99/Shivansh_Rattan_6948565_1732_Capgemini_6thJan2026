using System.ComponentModel.DataAnnotations;

namespace EventApp.Models
{
    public class EventRegistration
    {
        public int Id { get; set; }

        [Required]
        public string ParticipantName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string EventName { get; set; }
    }
}