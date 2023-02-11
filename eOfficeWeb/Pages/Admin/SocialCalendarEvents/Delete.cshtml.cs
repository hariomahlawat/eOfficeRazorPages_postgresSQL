using eOffice.DataAccess.Repository.IRepository;
using eOffice.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace eOfficeWeb.Pages.Admin.SocialCalendarEvents
{
    [BindProperties]
    [Authorize]
    public class DeleteModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public SocialCalendar SocialCalendarEvents { get; set; }
        public DeleteModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void OnGet(int id)
        {
            SocialCalendarEvents = _unitOfWork.socialCalendar.GetFirstOrDefault(u => u.Id == id);
        }

        public async Task<IActionResult> OnPost()
        {

            var eventFromDb = _unitOfWork.socialCalendar.GetFirstOrDefault(u => u.Id == SocialCalendarEvents.Id);
            if (eventFromDb != null)
            {
                _unitOfWork.socialCalendar.Remove(eventFromDb);
                _unitOfWork.Save();
                TempData["success"] = "Event deleted successfully";
                return RedirectToPage("Index");

            }

            return Page();
        }
    }
}
