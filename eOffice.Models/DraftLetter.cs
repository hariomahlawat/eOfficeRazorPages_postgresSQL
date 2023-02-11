using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eOffice.Models
{
    public class DraftLetter
    {
        public int Id { get; set; }
        public int DakId { get; set; }
        [ForeignKey("DakId")]
        [ValidateNever]
        public Dak Dak { get; set; }
        public string ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        [ValidateNever]
        public ApplicationUser ApplicationUser { get; set; }

        public string ApplicationUserName { get; set; }
        [Required]
        [Display(Name ="Select Draft Letter")]
        public string DraftLetterUrl { get; set; }
        public DateTime DateTimeUpload { get; set; } = DateTime.Now;
    }
}
