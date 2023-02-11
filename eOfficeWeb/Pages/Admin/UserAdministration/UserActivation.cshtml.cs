using eOffice.DataAccess.Repository.IRepository;
using eOffice.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;

namespace eOfficeWeb.Pages.Admin.UserAdministration
{
    [Authorize(Roles = "Admin")]
    public class UserActivationModel : PageModel
    {
        private readonly Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> userManager;
        private readonly IUnitOfWork _unitOfWork;
        public UserActivationModel(Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> userManager, IUnitOfWork unitOfWork)
        {
            this.userManager = userManager;
            this._unitOfWork = unitOfWork;

        }
        public IEnumerable<ApplicationUser> Users { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public IEnumerable<SelectListItem> selectListStations { get; set; }

        public async void OnGet(string id)
        {
            Users = userManager.Users;
            foreach (var user in Users)
            {
                if (user.Id == id)
                {
                    ApplicationUser = user;
                }
            }
            if (ApplicationUser == null)
            {
                TempData["error"] = $"User with Id = {id} can not be found.";
            }

        }

        //POST
        public async Task<IActionResult> OnPost(ApplicationUser applicationUser)
        {
            var userFromDb = await userManager.FindByIdAsync(applicationUser.Id);
            if (userFromDb == null)
            {
                TempData["error"] = $"User with Id = {applicationUser.Id} can not be found.";
                return Page();
            }
            else
            {
                if (userFromDb.IsEnabled == true)
                {
                    userFromDb.IsEnabled = false;
                }
                else
                {
                    userFromDb.IsEnabled = true;
                }

                var result = await userManager.UpdateAsync(userFromDb);
                if (result.Succeeded)
                {
                    TempData["success"] = "User updated successfully.";
                    return RedirectToPage("/Admin/UserAdministration/UserManagement");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return Page();
            }
        }  //POST method ends


    }
}
