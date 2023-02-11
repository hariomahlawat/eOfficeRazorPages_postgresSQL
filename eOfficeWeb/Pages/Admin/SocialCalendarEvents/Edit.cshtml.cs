using eOffice.DataAccess.Repository.IRepository;
using eOffice.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace eOfficeWeb.Pages.Admin.SocialCalendarEvents
{
    [BindProperties]
    [Authorize]
    public class EditModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public SocialCalendar SocialCalendarEvents { get; set; }


        public EditModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void OnGet(int id)
        {
            SocialCalendarEvents = _unitOfWork.socialCalendar.GetFirstOrDefault(u => u.Id == id);
        }

        public async Task<IActionResult> OnPost()
        {
            
            if (ModelState.IsValid)
            {
                _unitOfWork.socialCalendar.Update(SocialCalendarEvents);
                _unitOfWork.Save();
                TempData["success"] = "Event updated successfully";
                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}
