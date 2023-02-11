using eOffice.Models;
using eOffice.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace eOfficeWeb.Pages.Admin.UserAdministration
{
    [Authorize(Roles= "Admin,SuperAdmin")]
    public class ManageRolesModel : PageModel
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        public ManageRolesModel(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public IEnumerable<IdentityRole> Roles { get; set; }
        public IdentityRole Role { get; set; }
        public IdentityRole IdentityRole { get; set; }

        public string roleId { get; set; }
        [BindProperty]
        public UserRoleViewModel UserRoleViewModel { get; set; }

        public List<UserRoleViewModel> UserRoleModels  = new List<UserRoleViewModel>();
        public async Task<IActionResult> OnGetAsync(IdentityRole? identityRole)
        {
            Roles = _roleManager.Roles;
            if (identityRole.Id != null) 
            {
                Role = await _roleManager.FindByIdAsync(identityRole.Id);
                if (Role != null)
                {
                    var userList = await _userManager.Users.ToListAsync();
                    foreach (var user in userList)
                    {
                        var userRoleModel = new UserRoleViewModel
                        {
                            UserId = user.Id,
                            UserName = user.UserName
                        };
                        if (await _userManager.IsInRoleAsync(user, Role.Name))
                        {
                            userRoleModel.IsSelected = true;
                        }
                        else
                        {
                            userRoleModel.IsSelected = false;
                        }
                        UserRoleModels.Add(userRoleModel);
                    }
                    return Page();
                }
                return Page();
            }
            return Page();
        } //GET method ends

        [HttpPost]
        public async Task<IActionResult> OnPostEditUsersInRole(List<UserRoleViewModel> UserRoleModels, string roleId)
        {
            Roles = _roleManager.Roles;
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role == null) 
            {
                TempData["error"] = "Role can not be found.";
                return Page();
            }
            for (int i = 0; i < UserRoleModels.Count; i++)
            {
                var user = await _userManager.FindByIdAsync(UserRoleModels[i].UserId);
                IdentityResult result;
                if (UserRoleModels[i].IsSelected && !(await _userManager.IsInRoleAsync(user, role.Name)))
                {
                    result = await _userManager.AddToRoleAsync(user, role.Name);
                }
                else if(!UserRoleModels[i].IsSelected && (await _userManager.IsInRoleAsync(user,role.Name)))
                {
                    result = await _userManager.RemoveFromRoleAsync(user, role.Name);
                }
                else 
                {
                    continue;
                }
                if (result.Succeeded) 
                {
                    if (i< UserRoleModels.Count-1)
                        continue;
                    else
                    {
                        TempData["success"] = "The users for the selected roles are updated.";
                        return Page();
                    }
                        
                }
            }
                
            return Page();
        }




        }
}
