using eOffice.DataAccess.Repository.IRepository;
using eOffice.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace eOfficeWeb.Pages.Admin.UserAdministration
{
    public class UserManagementModel : PageModel
    {
        private readonly Microsoft.AspNetCore.Identity.RoleManager<IdentityRole> roleManager;
        private readonly Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> userManager;
        private readonly IUnitOfWork _unitOfWork;

        public IEnumerable<ApplicationUser> Users { get; set; }
        

        public UserManagementModel(Microsoft.AspNetCore.Identity.RoleManager<IdentityRole> roleManager, Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> userManager, IUnitOfWork unitOfWork)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            _unitOfWork = unitOfWork;
        }

        public void OnGet()
        {
            Users = userManager.Users;
        }




    }
}
