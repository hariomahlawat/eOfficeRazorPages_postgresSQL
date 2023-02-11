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

namespace eOfficeWeb.Pages.Admin.IncomingDak
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly UserManager<ApplicationUser> _userManager;
        public IEnumerable<Dak> Dak { get; set; }
        public IEnumerable<DakComments> DakComments { get; set; }
        public IEnumerable<ApplicationUser> Users { get; set; }
        public ApplicationUser LoggedInUser { get; set; }
        public IEnumerable<DakVisibilityTag> DakVisibilityTag { get; set; }
        public string UserBranchName { get; set; }
        public string UserSectionName { get; set; }
        public int CountTodayDak { get; set; }
        public int CountLastMonthDak{ get; set; }
        public int CountLastSixMonthDak { get; set; }
        public int CountLastOneYearDak { get; set; }
        public int CountUnseenDak { get; set; }
        public int CountAllDak { get; set; }


        public bool FlagNotSeen, FlagToday, FlagLastOneMonth, FlagLastSixMonth, FlagLastOneYear, FlagAll = false;

        public IndexModel(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
            _userManager = userManager;
        }
        public void OnGet(string? id, string? pageName)
        {
            // id here is no of days - passed and it can be "notSeen"
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

            // Select Dak as per the branch and section logged in user
            if (UserBranchName=="All")
            {
                Dak = _unitOfWork.Dak.GetAll();
            }
            else if (UserBranchName != "None" && UserSectionName=="All")
            {
                Dak = _unitOfWork.Dak.GetAll().Where(u=>u.OfficeBranch==UserBranchName);
            }
            else if (UserBranchName != "None" && UserSectionName != "None")
            {
                Dak = _unitOfWork.Dak.GetAll().Where(u => u.OfficeBranch == UserBranchName && u.OfficeSection==UserSectionName);
            }
            else // it should never be displayed
            {
                Dak = new List<Dak>();
            }

            // Filter Dak as per user role
            // if Cdr/CO or Dy/2IC - provide selected dak

            if (User.IsInRole(SD.CommandingOfficerRole))
            {
                DakVisibilityTag= _unitOfWork.DakVisibilityTag.GetAll().Where(u => u.Cdr == true);
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






            CountAllDak = Dak.Count();

            DateTime oldDate = DateTime.Now.Subtract(new TimeSpan(1, 0, 0, 0, 0));
            CountTodayDak = Dak.Where(u => u.DateReceived > oldDate).Count();

            oldDate = DateTime.Now.Subtract(new TimeSpan(30, 0, 0, 0, 0));
            CountLastMonthDak = Dak.Where(u => u.DateReceived > oldDate).Count();

            oldDate = DateTime.Now.Subtract(new TimeSpan(180, 0, 0, 0, 0));
                CountLastSixMonthDak = Dak.Where(u => u.DateReceived > oldDate).Count();

            oldDate = DateTime.Now.Subtract(new TimeSpan(365, 0, 0, 0, 0));
            CountLastOneYearDak = Dak.Where(u => u.DateReceived > oldDate).Count();

            //not seen dak
            DakComments = _unitOfWork.DakComments.GetAll().Where(u => u.ApplicationUserId == userId);
            var dakCommentsIds = DakComments.Select(u => u.DakId).ToArray();

            // not seen will be done only for last 15 days
            oldDate = DateTime.Now.Subtract(new TimeSpan(15, 0, 0, 0, 0));
            CountUnseenDak = Dak.Where(u => !dakCommentsIds.Contains(u.Id) && u.DateReceived > oldDate).Count();

            if (id == "notSeen")
            {
                FlagNotSeen = true;
                Dak = Dak.Where(u => !dakCommentsIds.Contains(u.Id) && u.DateReceived > oldDate);
            }
            else
            {
                if (id != null)
                {
                    if (id == "1") FlagToday = true;
                    if (id == "30") FlagLastOneMonth = true;
                    if (id == "180") FlagLastSixMonth = true;
                    if (id == "365") FlagLastOneYear = true;
                    oldDate = DateTime.Now.Subtract(new TimeSpan(Int32.Parse(id), 0, 0, 0, 0));
                    Dak = Dak.Where(u => u.DateReceived > oldDate).OrderByDescending(u => u.DateReceived);
                }
                else 
                {
                    FlagAll = true;
                }
            }
            
        }

        //POST
        public IActionResult OnPostDelete(int id)
        {
            var objFromDb = _unitOfWork.Dak.GetFirstOrDefault(Dak => Dak.Id == id);
            if (objFromDb == null)
            {
                return NotFound();
            }
            //delete the image
            string webRootPath = _hostEnvironment.WebRootPath;
            string folderPath = Path.Combine(webRootPath, @"files\pdfs");
            //delete covering letter
            var oldImagePath = Path.Combine(folderPath, objFromDb.CoveringLetter.TrimStart('\\'));
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }
            //delete enclosure1 if exists
            if (objFromDb.Enclosure1 != null)
            {
                oldImagePath = Path.Combine(folderPath, objFromDb.Enclosure1.TrimStart('\\'));
                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }
            }
            //delete enclosure2 if exists
            if (objFromDb.Enclosure2 != null)
            {
                oldImagePath = Path.Combine(folderPath, objFromDb.Enclosure2.TrimStart('\\'));
                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }
            }
            //delete enclosure3 if exists
            if (objFromDb.Enclosure3 != null)
            {
                oldImagePath = Path.Combine(folderPath, objFromDb.Enclosure3.TrimStart('\\'));
                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }
            }

            _unitOfWork.Dak.Remove(objFromDb);
            _unitOfWork.Save();

            return RedirectToPage("./Index");

        }

    }
}
