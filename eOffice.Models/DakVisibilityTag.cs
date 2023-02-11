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
    public class DakVisibilityTag
    {
        public int Id { get; set; }
        public int DakId { get; set; }
        [ForeignKey("DakId")]
        [ValidateNever]
        public Dak Dak { get; set; }

        [Display(Name="Cdr")]
        public bool Cdr { get; set; } = false;
        [Display(Name = "Dy Cdr")]
        public bool DyCdr { get; set; } = false;

    }
}
