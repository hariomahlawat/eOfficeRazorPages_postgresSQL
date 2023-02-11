using eOffice.DataAccess.Repository.IRepository;
using eOffice.Models;
using eOffice.Models.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace eOfficeWeb.Pages.Admin.UserAdministration
{
    [BindProperties]
    public class EditUserModel : PageModel
    {
        private readonly Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> userManager;
        private readonly IUnitOfWork _unitOfWork;
        public EditUserModel(Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> userManager, IUnitOfWork unitOfWork)
        {
            this.userManager = userManager;
            this._unitOfWork = unitOfWork;
   
        }
        public IEnumerable<ApplicationUser> Users { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public IEnumerable<OfficeBranch> OfficeBranches { get; set; }
        public IEnumerable<SelectListItem> selectListOfficeBranches { get; set; }


        public async void OnGet(string id)
        {
            Users = userManager.Users;
            foreach (var user in Users)
            {
                if (user.Id==id)
                {
                    ApplicationUser = user;
                }
            }
            if (ApplicationUser == null)
            {
                TempData["error"] = $"User with Id = {id} can not be found.";
            }
            OfficeBranches = _unitOfWork.OfficeBranch.GetAll();
            selectListOfficeBranches = _unitOfWork.OfficeBranch.GetAll().Select(s => new SelectListItem
            {
                Value = s.Name,
                Text = s.Name
            });
        }

        //POST
        public async Task<IActionResult> OnPost(ApplicationUser applicationUser)
        {
            //TempData["success"] = $"User with Id = {applicationUser.Id} seen.";
            var userFromDb = await userManager.FindByIdAsync(applicationUser.Id);
            if (userFromDb == null)
            {
                TempData["error"] = $"User with Id = {applicationUser.Id} can not be found.";
                return Page();
            }
            else
            {
                userFromDb.UserName = applicationUser.UserName;
                userFromDb.Rank = applicationUser.Rank;
                userFromDb.FirstName = applicationUser.FirstName;
                userFromDb.LastName = applicationUser.LastName;
                userFromDb.OfficeBranch = applicationUser.OfficeBranch;
                userFromDb.OfficeBranchSection = "None";

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
