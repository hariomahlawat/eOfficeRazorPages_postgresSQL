using eOffice.DataAccess.Migrations;
using eOffice.DataAccess.Repository.IRepository;
using eOffice.Models;
using eOffice.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using DakVisibilityTag = eOffice.Models.DakVisibilityTag;

namespace eOfficeWeb.Pages.AppUser
{
    [Authorize]
    public class DashboardModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;

        public IEnumerable<ApplicationUser> Users { get; set; }
        public ApplicationUser LoggedInUser { get; set; }
        public string UserBranchName { get; set; }
        public string UserSectionName { get; set; }

        public IEnumerable<Dak> Dak { get; set; }
        public IEnumerable<Dak>  DakToday { get; set; }
        public IEnumerable<Dak> DakWeek { get; set; }
        public IEnumerable<Dak> DakRecentFive{ get; set; }
        public IEnumerable<Dak> UnseenDak { get; set; }
        public IEnumerable<DakComments> DakComments { get; set; }
        public IEnumerable<ToDoList> ToDoList { get; set; }
        public IEnumerable<DakVisibilityTag> DakVisibilityTag { get; set; }

        public int CountDakToday { get; set; }
        public int CountUnseenDak { get; set; }
        public int CountAllTasks { get; set; }
        public int CountDelayedTask{ get; set; }

        public IEnumerable<SocialCalendar> SocialCalendarWeek { get; set; }
        public IEnumerable<SocialCalendar> SocialCalendarToday { get; set; }

        public int CountSocialCalendarToday { get; set; }
        public int CountSocialCalendarWeek { get; set; }

        public IEnumerable<EventCalendar> Events { get; set; }
        public int CountUpcomingEvents { get; set; }

        public DashboardModel(IUnitOfWork unitOfWork,  UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public void OnGet()
        {
            // Get user btanch name and section name.
            Users = _userManager.Users;
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            foreach (var user in Users)
            {
                if (user.Id == userId)
                {
                    LoggedInUser = user;
                }
            }
            var userOfficeBranch = _unitOfWork.OfficeBranch.GetFirstOrDefault(u => u.Name == LoggedInUser.OfficeBranch);
            UserBranchName = userOfficeBranch.Name;
            var userOfficeSection = _unitOfWork.OfficeBranchSection.GetFirstOrDefault(u => u.Name == LoggedInUser.OfficeBranchSection);
            UserSectionName = userOfficeSection.Name;

            if (UserBranchName == "All")
            {
                Dak = _unitOfWork.Dak.GetAll();
            }
            else if (UserBranchName != "None" && UserSectionName == "All")
            {
                Dak = _unitOfWork.Dak.GetAll().Where(u => u.OfficeBranch == UserBranchName);
            }
            else if (UserBranchName != "None" && UserSectionName != "None")
            {
                Dak = _unitOfWork.Dak.GetAll().Where(u => u.OfficeBranch == UserBranchName && u.OfficeSection == UserSectionName);
            }
            else // it should never be displayed
            {
                Dak = new List<Dak>();
            }

            // Filter Dak as per user role
            // if Cdr/CO or Dy/2IC - provide selected dak

            if (User.IsInRole(SD.CommandingOfficerRole))
            {
                DakVisibilityTag = _unitOfWork.DakVisibilityTag.GetAll().Where(u => u.Cdr == true);
                var markedForCdrIds = DakVisibilityTag.Select(u => u.DakId).ToArray();

                Dak = Dak.Where(u => markedForCdrIds.Contains(u.Id));

            }
            else if (User.IsInRole(SD.SecondInCommandRole))
            {
                DakVisibilityTag = _unitOfWork.DakVisibilityTag.GetAll().Where(u => u.DyCdr == true);
                var markedForDyCdrIds = DakVisibilityTag.Select(u => u.DakId).ToArray();

                Dak = Dak.Where(u => markedForDyCdrIds.Contains(u.Id));
            }

            //==================================================================================

            if (Dak!=null)
            {
                int noOfDays = 7;
                DateTime oldDate = DateTime.Now.Subtract(new TimeSpan(noOfDays, 0, 0, 0, 0));
                DateTime futureDate = DateTime.Now.AddDays(7);


                DakWeek = Dak.Where(u => u.DateReceived > oldDate).OrderByDescending(u => u.DateReceived);
                DakToday = DakWeek.Where(
                        u => u.DateReceived.Year == DateTime.Today.Year
                        && u.DateReceived.Month == DateTime.Today.Month
                        && u.DateReceived.Day == DateTime.Today.Day
                    );
                DakRecentFive = DakWeek.Take(5);
                CountDakToday = DakToday.Count();

                //count unseen Dak
                // not seen will be done only for last 15 days
                DakComments = _unitOfWork.DakComments.GetAll().Where(u => u.ApplicationUserId == userId);
                var dakCommentsIds = DakComments.Select(u => u.DakId).ToArray();
                UnseenDak = Dak.Where(u => !dakCommentsIds.Contains(u.Id) && u.DateReceived > DateTime.Now.Subtract(new TimeSpan(15, 0, 0, 0, 0)));
                CountUnseenDak = UnseenDak.Count();
            }
            

            //----------------------------------------------------------------------------------------------------------
            SocialCalendarWeek = _unitOfWork.socialCalendar.GetAll().Where(
                u => u.EventDate.Month == DateTime.Today.Month
                && u.EventDate.Day >= DateTime.Today.Day
                && u.EventDate.Day < DateTime.Today.Day + 7
                ).OrderBy(u=>u.EventDate.Day);
            SocialCalendarToday = SocialCalendarWeek.Where(
                    u => u.EventDate.Month == DateTime.Today.Month
                    && u.EventDate.Day == DateTime.Today.Day
                );
            CountSocialCalendarToday = SocialCalendarToday.Count();
            CountSocialCalendarWeek = SocialCalendarWeek.Count();

            //get todo list
            ToDoList = _unitOfWork.ToDoList.GetAll(u => u.ApplicationUserId == userId);
            CountAllTasks = ToDoList.Count();
            CountDelayedTask = ToDoList.Where(u=> u.CompletionTime < DateTime.Now && u.CompletionTime.ToString("dd MMMM yyyy") != "01 January 0001").Count();

            //get events
            Events = _unitOfWork.EventCalendar.GetAll().Where(u => u.Start.Day >= DateTime.Today.Day && u.Start.Day < DateTime.Today.Day + 7);
            CountUpcomingEvents = Events.Count();

        }
    }
}
