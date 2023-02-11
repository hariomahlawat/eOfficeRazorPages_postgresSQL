using eOffice.DataAccess.Repository.IRepository;
using eOffice.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace eOfficeWeb.Pages.AppUser.AppEventCalendar
{
    public class IndexModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        public EventCalendar EventCalendar;
        

        public IndexModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void OnGet()
        {
            
        }

        public IActionResult OnPostAddEvent(EventCalendar eventCalendar)
        {
            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                _unitOfWork.EventCalendar.Add(eventCalendar);
                _unitOfWork.Save();
                TempData["success"] = "Event added successfully";
                return Page();
            }
            return Page();
        }

    }
}
