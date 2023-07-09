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
	[Authorize]
	public class SpeakMarkedForYouModel : PageModel
    {
		private readonly IUnitOfWork _unitOfWork;
		private readonly IWebHostEnvironment _hostEnvironment;
		private UserManager<ApplicationUser> _userManager;
		private readonly IConfiguration _configuration;
		public SpeakMarkedForYouModel(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment, UserManager<ApplicationUser> userManager, IConfiguration configuration)
		{
			_unitOfWork = unitOfWork;
			_hostEnvironment = hostEnvironment;
			_userManager = userManager;
			_configuration = configuration;
		}
		public DakSpeak DakSpeak { get; set; }
		public IEnumerable<DakSpeak> SpeakDaks { get; set; }
		public IEnumerable<Dak> Daks { get; set; }

        public IActionResult OnGet()
        {
            var loggedInUserId = _userManager.GetUserId(User);
            SpeakDaks = _unitOfWork.DakSpeak
                        .GetAll(u => u.MarkedForId == loggedInUserId,
                                 includeProperties: "MarkedFor,MarkedBy,Dak");
			return Page();
        }
	}
}
