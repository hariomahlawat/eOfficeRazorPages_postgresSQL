using eOffice.DataAccess.Repository.IRepository;
using eOffice.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;
using static System.Collections.Specialized.BitVector32;

namespace eOfficeWeb.Pages.Admin.UserAdministration
{
    public class ManageOfficeBranchModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        public ManageOfficeBranchModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [BindProperty]
        public OfficeBranch OfficeBranch { get; set; }
        public IEnumerable<OfficeBranch> OfficeBranches { get; set; }
        public IEnumerable<SelectListItem> selectListOfficeBranches { get; set; }
        public IEnumerable<OfficeBranchSection> OfficeBranchSections { get; set; }
        public OfficeBranchSection OfficeBranchSection { get; set; }

        public void OnGet(OfficeBranchSection? officeBranchSection)
        {
            if (officeBranchSection != null)
            {
                int id = officeBranchSection.OfficeBranchId;
                OfficeBranchSections = _unitOfWork.OfficeBranchSection.GetAll().Where(u => u.OfficeBranchId == id);
            }

            OfficeBranches = _unitOfWork.OfficeBranch.GetAll();
            selectListOfficeBranches = OfficeBranches.Where(u => u.Name != "All" && u.Name != "None").Select(s => new SelectListItem
            {
                Value = s.Id.ToString(),
                Text = s.Name
            });
        }





        // POST - add a new office branch
        [HttpPost]
        public async Task<IActionResult> OnPostAddBranch(OfficeBranch officeBranch)
        {
            
            if (officeBranch.Name != null)
            {
                int checkName = _unitOfWork.OfficeBranch.GetAll().Where(u => u.Name == officeBranch.Name).Count();
                if (checkName > 0)
                {
                    TempData["error"] = "Branch Name already exists.";
                    return RedirectToPage("/Admin/UserAdministration/ManageOfficeBranch");
                }

                int count = _unitOfWork.OfficeBranch.GetAll().Where(u=>u.Name=="None").Count();
                if (count == 0)
                {
                    OfficeBranch model1 = new OfficeBranch();
                    model1.Name = "None";
                    OfficeBranch model2 = new OfficeBranch();
                    model2.Name = "All";
                    _unitOfWork.OfficeBranch.Add(model1);
                    _unitOfWork.OfficeBranch.Add(model2);
                }
                _unitOfWork.OfficeBranch.Add(officeBranch);
                _unitOfWork.Save();
                TempData["success"] = "Office branch added successfully";
            }
            else
            {
                TempData["error"] = "Please enter a valid branch name.";
            }
            return RedirectToPage("/Admin/UserAdministration/ManageOfficeBranch");
        }

        [HttpPost]
        public async Task<IActionResult> OnPostAddBranchSection(OfficeBranchSection officeBranchSection)
        {
            if (officeBranchSection.Name != null)
            {
                int checkName = _unitOfWork.OfficeBranchSection.GetAll().Where(u => u.Name == officeBranchSection.Name && u.OfficeBranchId== officeBranchSection.OfficeBranchId).Count();
                if (checkName > 0)
                {
                    TempData["error"] = "Branch Name already exists.";
                    return RedirectToPage("/Admin/UserAdministration/ManageOfficeBranch");
                }

                int count = _unitOfWork.OfficeBranchSection.GetAll().Where(u => u.Name == "None" && u.OfficeBranchId==officeBranchSection.OfficeBranchId).Count();
                if (count == 0)
                {
                    OfficeBranchSection model1 = new OfficeBranchSection();
                    model1.Name = "None";
                    model1.OfficeBranchId = officeBranchSection.OfficeBranchId;
                    OfficeBranchSection model2 = new OfficeBranchSection();
                    model2.Name = "All";
                    model2.OfficeBranchId = officeBranchSection.OfficeBranchId;
                    _unitOfWork.OfficeBranchSection.Add(model1);
                    _unitOfWork.OfficeBranchSection.Add(model2);
                }
                _unitOfWork.OfficeBranchSection.Add(officeBranchSection);
                _unitOfWork.Save();
                TempData["success"] = "section added successfully";
            }
            else
            {
                TempData["error"] = "Please enter a valid branch name.";
            }
            return RedirectToPage("/Admin/UserAdministration/ManageOfficeBranch");
        }

        public async Task<IActionResult> OnPostDeleteBranch(int id)
        {
            var branchFromDb = _unitOfWork.OfficeBranch.GetFirstOrDefault(u => u.Id == id);
            if (branchFromDb != null)
            {
                _unitOfWork.OfficeBranch.Remove(branchFromDb);
                _unitOfWork.Save();
                TempData["success"] = "Branch deleted successfully";
                return RedirectToPage("/Admin/UserAdministration/ManageOfficeBranch");

            }
            return RedirectToPage("/Admin/UserAdministration/ManageOfficeBranch");
        }

        public async Task<IActionResult> OnPostDeleteSection(int id)
        {
            var sectionFromDb = _unitOfWork.OfficeBranchSection.GetFirstOrDefault(u => u.Id == id);
            if (sectionFromDb != null)
            {
                _unitOfWork.OfficeBranchSection.Remove(sectionFromDb);
                _unitOfWork.Save();
                TempData["success"] = "Section deleted successfully";
                return RedirectToPage("/Admin/UserAdministration/ManageOfficeBranch");

            }
            return RedirectToPage("/Admin/UserAdministration/ManageOfficeBranch");
        }





    }
}



