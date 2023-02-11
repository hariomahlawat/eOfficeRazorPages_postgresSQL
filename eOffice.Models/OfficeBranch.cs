using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eOffice.Models
{
    public class OfficeBranch
    {
        public int Id { get; set; }

        [Required]
        [Display(Name ="Office Branch Name")]
        public string Name { get; set; }
    }
}
