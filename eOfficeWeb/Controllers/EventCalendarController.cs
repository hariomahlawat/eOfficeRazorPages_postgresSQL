using eOffice.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace eOfficeWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventCalendarController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public EventCalendarController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var events = _unitOfWork.EventCalendar.GetAll();
            return Json(new { data = events });
        }
    }
}
