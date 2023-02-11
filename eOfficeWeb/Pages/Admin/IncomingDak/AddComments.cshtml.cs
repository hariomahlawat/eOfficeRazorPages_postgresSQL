using eOffice.DataAccess.Repository.IRepository;
using eOffice.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace eOfficeWeb.Pages.Admin.IncomingDak
{
    [Authorize]
    public class AddCommentsModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        public Dak Dak { get; set; }
        public DakComments DakComments { get; set; }
        public IEnumerable<DakComments> comments { get; set; }
        
        public AddCommentsModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void OnGet(int? id)
        {
            Dak = _unitOfWork.Dak.GetFirstOrDefault(u => u.Id == id);
            comments = _unitOfWork.DakComments.GetAll(u=>u.DakId==id);
             
        }

        public async Task<IActionResult> OnPost(DakComments dakComments)
        {
            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                _unitOfWork.DakComments.Add(dakComments);
                _unitOfWork.Save();
                TempData["success"] = "Comment added successfully";
                return RedirectToPage("Detail", new { id = dakComments.DakId });
            }
            return Page();
        }
    }
}
