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
    public class MarkedDak
    {
        public int Id { get; set; }
        public int DakId { get; set; }
        [ForeignKey("DakId")]
        [ValidateNever]
        public Dak Dak { get; set; }

        [Display(Name="Officers To See")]
        public bool OfficersToSee { get; set; } = false;
        [Display(Name = "Sainik Sammelan")]
        public bool SainikSammelan { get; set; } = false;
        [Display(Name = "Roll Call")]
        public bool RollCall { get; set; } = false;

    }
}
