using eOffice.DataAccess.Repository.IRepository;
using eOffice.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace eOfficeWeb.Pages.AppUser.AppEventCalendar
{
    [Authorize]
    public class EventListViewModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        public IEnumerable<EventCalendar> EventCalendar;

        public EventListViewModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void OnGet()
        {
            EventCalendar = _unitOfWork.EventCalendar.GetAll();
        }



        // delete an event.
        //POST
        public async Task<IActionResult> OnPostDelete(int id)
        {
            //int id = toDoList.Id;
            var taskFromDb = _unitOfWork.EventCalendar.GetFirstOrDefault(u => u.Id == id);
            if (taskFromDb != null)
            {
                _unitOfWork.EventCalendar.Remove(taskFromDb);
                _unitOfWork.Save();
                TempData["success"] = "Event deleted successfully";
                return RedirectToPage("/AppUser/AppEventCalendar/EventListView");

            }

            return Page();
        }


    }
}
