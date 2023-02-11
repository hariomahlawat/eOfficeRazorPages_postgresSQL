using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eOffice.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Rank { get; set; }
        public string PersonalNumber { get; set; }
        public string? OfficeBranch { get; set; } = "None";
        public string? OfficeBranchSection { get; set; } = "None";
        public bool? IsEnabled { get; set; } = true;
    }
}
