using eOffice.DataAccess.Repository;
using eOffice.DataAccess.Repository.IRepository;
using eOffice.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using System.Web.WebPages;
using Microsoft.CodeAnalysis.Completion;

namespace eOfficeWeb.Pages.AppUser
{
    public class SpeakMarkedByYouModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;
        private UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        public SpeakMarkedByYouModel(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment, UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
            _userManager = userManager;
            _configuration = configuration;
        }
        public DakSpeak DakSpeak { get; set; }
        public IEnumerable<DakSpeak> SpeakDaks { get; set; }
        public IEnumerable<Dak> Daks { get; set; }

        public void OnGet()
        {
            var loggedInUserId = _userManager.GetUserId(User);
            SpeakDaks = _unitOfWork.DakSpeak
                        .GetAll(u => u.MarkedById == loggedInUserId,
                                 includeProperties: "MarkedFor,MarkedBy,Dak");
        }



        public IActionResult OnPostDeleteSpeakDak(string speakDakId)
        {
            // Convert dakIdString to an integer
            if (!int.TryParse(speakDakId, out int speakDakIdInt))
            {
                TempData["error"] = "Invalid Dak ID.";
                return RedirectToPage("/AppUser/SpeakMarkedByYou");
            }

            var loggedInUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (loggedInUserId == null)
            {
                TempData["error"] = "User not logged in.";
                return RedirectToPage("/AppUser/SpeakMarkedByYou");
            }

            // Fetch existing records for the current user
            var existingRecords = _unitOfWork.DakSpeak.GetAll(u => u.MarkedById == loggedInUserId && u.Id == speakDakIdInt).ToList();

            // Remove records for users that were unmarked
            foreach (var record in existingRecords)
            {
                    _unitOfWork.DakSpeak.Remove(record);

            }

            _unitOfWork.Save();
            TempData["success"] = "Dak marked as spoken.";
            return RedirectToPage("/AppUser/SpeakMarkedByYou");
        }



    }
}
