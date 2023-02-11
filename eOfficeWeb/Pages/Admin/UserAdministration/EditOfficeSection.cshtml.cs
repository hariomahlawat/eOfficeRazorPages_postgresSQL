using eOffice.DataAccess.Repository.IRepository;
using eOffice.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace eOfficeWeb.Pages.Admin.UserAdministration
{
    public class EditOfficeSectionModel : PageModel
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IUnitOfWork _unitOfWork;

        public EditOfficeSectionModel(UserManager<ApplicationUser> userManager, IUnitOfWork unitOfWork)
        {
            this.userManager = userManager;
            _unitOfWork = unitOfWork;
        }
        public IEnumerable<ApplicationUser> Users { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public IEnumerable<OfficeBranch> OfficeBranches { get; set; }
        public IEnumerable<SelectListItem> selectListOfficeBranches { get; set; }
        public IEnumerable<OfficeBranchSection> OfficeBranchSections { get; set; }
        public IEnumerable<SelectListItem> selectListOfficeBranchSections { get; set; }

        //GET Method
        public void OnGet(string? userId)
        {
            Users = userManager.Users;
            foreach (var user in Users)
            {
                if (user.Id == userId)
                {
                    ApplicationUser = user;
                }
            }

            var userOfficeBranch = _unitOfWork.OfficeBranch.GetFirstOrDefault(u => u.Name == ApplicationUser.OfficeBranch);
            int userOfficeBranchId = userOfficeBranch.Id;
            OfficeBranchSections = _unitOfWork.OfficeBranchSection.GetAll().Where(u => u.OfficeBranchId == userOfficeBranchId &&  u.Name != "None");
            selectListOfficeBranchSections = OfficeBranchSections.Select(s => new SelectListItem
            {
                //Value = s.Id.ToString(),
                Value = s.Name,
                Text = s.Name
            });
        } // GET method ends

        //POST method
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
                userFromDb.OfficeBranchSection = applicationUser.OfficeBranchSection;

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
        }






        }
}
