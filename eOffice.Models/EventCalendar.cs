using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eOffice.Models
{
    [Authorize]
    public class EventCalendar
    {
        public int Id { get; set; }
        [Required]
        [Display(Name ="Title")]
        public string Subject { get; set; }
        public string? Description { get; set; }
        public DateTime Start { get; set; }
        public DateTime? End { get; set; }
        public string? ThemeColor { get; set; }
        [ValidateNever]
        public bool IsFullDay { get; set; }

    }
}
