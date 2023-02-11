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
	public class OfficeBranchSection
	{
        public int Id { get; set; }
        [Display(Name="Office branch")]
        public int OfficeBranchId { get; set; }
        [ForeignKey("OfficeBranchId")]
        [ValidateNever]
        public OfficeBranch OfficeBranch { get; set; }
        [Required]
        public string Name { get; set; }

    }
}
