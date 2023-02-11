using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using eOffice.Models;
using eOffice.Models.ViewModels;
using System.Data;
using System.Security.Claims;

namespace eOfficeWeb.Pages.Admin.UserAdministration
{
    [Authorize(Roles = "Admin")]
    public class ResetPasswordByAdminModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public IEnumerable<ApplicationUser> Users { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public ResetPassword ResetPassword { get; set; }
        public string UserId { get; set; }

        public ResetPasswordByAdminModel(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public void OnGet(string userId)
        {
            UserId = userId;
        }

        public async Task<IActionResult> OnPost(ResetPassword resetPassword)
        {
            if (ModelState.IsValid)
            {
                Users = _userManager.Users;
                foreach (var user in Users)
                {
                    if (user.Id == resetPassword.UserId)
                    {
                        ApplicationUser = user;
                    }
                }
                if (ApplicationUser != null)
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(ApplicationUser);
                    var result = await _userManager.ResetPasswordAsync(ApplicationUser, token, resetPassword.NewPassword);
                    if (result.Succeeded)
                    {
                        TempData["success"] = "Password changed successfully";
                        ModelState.Clear();
                        return Page();
                    }
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return Page();
        }

    }
}
