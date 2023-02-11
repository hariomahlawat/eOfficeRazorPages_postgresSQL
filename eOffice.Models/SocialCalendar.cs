using eOffice.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace eOffice.Models
{
    public class SocialCalendar
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Event Date")]
        public DateTime EventDate { get; set; }
        [Required]
        [Display(Name = "Event Type")]
        public string EventType { get; set; }
    }
}
