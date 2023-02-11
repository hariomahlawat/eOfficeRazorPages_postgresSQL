using eOffice.DataAccess.Repository.IRepository;
using eOffice.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace eOfficeWeb.Pages
{
    public class NoticeboardModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        public string localserver { get; set; }
        public OrdersToday OrdersToday { get; set; }
        public IEnumerable<SocialCalendar> SocialCalendarToday { get; set; }
        public int CountSocialCalendarToday { get; set; }
        public IEnumerable<OrderImage> OrderImages { get; set; }
        public NoticeboardModel(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }
        public void OnGet()
        {
            localserver = _configuration["ConnectionStrings:HostAddress"];
            //OrdersToday = _unitOfWork.OrdersToday.GetFirstOrDefault(u => u.CreationDate.Day == DateTime.Now.Day);
            OrderImages = _unitOfWork.OrderImage.GetAll();
            SocialCalendarToday = _unitOfWork.socialCalendar.GetAll().Where(
                    u => u.EventDate.Month == DateTime.Today.Month
                    && u.EventDate.Day == DateTime.Today.Day
                );
            CountSocialCalendarToday = SocialCalendarToday.Count();
        }
    }
}
