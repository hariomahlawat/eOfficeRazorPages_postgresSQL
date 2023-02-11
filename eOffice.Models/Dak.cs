using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eOffice.Models
{
    public class Dak
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Subject { get; set; }
        [Required]
        [Display(Name = "Office Branch")]
        public string OfficeBranch { get; set; } = "None";

        [Required]
        [Display(Name = "Office Section / Table")]
        public string OfficeSection { get; set; } = "None";

        [Display(Name = "Received From")]
        public string From { get; set; }

        
        [Display(Name = "Letter No")]
        public string LetterNo { get; set; }  

        [Required]
        [Display(Name = "Dated (date signed by sender)")]
        //[UIHint("Date")]
        public DateTime LetterDate { get; set; } = DateTime.Now.AddDays(-1);


        [Display(Name = "Date Received")]
        public DateTime DateReceived { get; set; } = DateTime.Now;

        [Required]
        [Display(Name = "Covering Letter or Main Letter") ]    
        public string CoveringLetter { get; set; }

        [Display(Name = "Encl 1 (If Any)")]
        public string? Enclosure1 { get; set; }

        [Display(Name = "Encl 2 (If Any)")]
        public string? Enclosure2 { get; set; }

        [Display(Name = "Encl 3 (If Any)")]
        public string? Enclosure3 { get; set; }

        [Display(Name = "Remarks (If Any)")]
        public string? Remarks { get; set; }

    }
}
