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

        public IEnumerable<ApplicationUser> AllUsers { get; set; }
        public IEnumerable<ApplicationUser> Users { get; set; }
        public ApplicationUser LoggedInUser { get; set; }


        public UserManagementModel(Microsoft.AspNetCore.Identity.RoleManager<IdentityRole> roleManager, Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> userManager, IUnitOfWork unitOfWork)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            _unitOfWork = unitOfWork;
        }

        public void OnGet()
        {
            AllUsers = userManager.Users;

            Users = new List<ApplicationUser>();
            List<ApplicationUser> users = new List<ApplicationUser>();
            foreach (var user in AllUsers)
            {
                if (user.Email != "hariomahlawat@619")
                {
                    users.Add(user);
                }
            }
            Users = users;
        }




    }
}
