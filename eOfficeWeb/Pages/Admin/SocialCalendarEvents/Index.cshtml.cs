using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using eOffice.DataAccess.Data;
using eOffice.Models;
using eOffice.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace eOffice.Pages.Admin.SocialCalendarEvents
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        public IEnumerable<SocialCalendar> SocialCalendarEvents { get; set; }

        public IndexModel(IUnitOfWork unitOfWork)
        { 
            _unitOfWork = unitOfWork;
        }
        public void OnGet()
        {
            SocialCalendarEvents = _unitOfWork.socialCalendar.GetAll().OrderBy(x => x.EventDate).Take(5) ;
        }
    }
}
