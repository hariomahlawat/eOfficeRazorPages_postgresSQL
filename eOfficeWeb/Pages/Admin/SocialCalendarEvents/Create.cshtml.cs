using eOffice.DataAccess.Repository.IRepository;
using eOffice.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace eOfficeWeb.Pages.Admin.SocialCalendarEvents
{
    [BindProperties]
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public SocialCalendar SocialCalendar { get; set; }
        public CreateModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.socialCalendar.Add(SocialCalendar);
                _unitOfWork.Save();
                TempData["success"] = "Event created successfully";
                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}
