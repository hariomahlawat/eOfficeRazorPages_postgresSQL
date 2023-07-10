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
using eOffice.Models.ViewModels;

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
        public IEnumerable<SpeakDakViewModel> SpeakDakViewModels { get; set; }
        public IEnumerable<Dak> Daks { get; set; }
        public string UserRoleColor { get; set; }


        public async Task<IActionResult> OnGetAsync()
        {
            var loggedInUserId = _userManager.GetUserId(User);
            var speakDaks = _unitOfWork.DakSpeak
                            .GetAll(u => u.MarkedForId == loggedInUserId,
                                     includeProperties: "MarkedFor,MarkedBy,Dak");

            // Create a list to store your ViewModel objects
            List<SpeakDakViewModel> speakDakViewModelList = new List<SpeakDakViewModel>();

            // Iterate over each SpeakDak
            foreach (var speakDak in speakDaks)
            {
                // Create a new ViewModel
                SpeakDakViewModel viewModel = new SpeakDakViewModel();
                viewModel.SpeakDak = speakDak;

                // Get roles for the 'MarkedBy' user
                var roles = await _userManager.GetRolesAsync(speakDak.MarkedBy);

                // Set UserRoleColor based on the role of 'MarkedBy' user
                if (roles.Contains("CommandingOfficer"))
                {
                    viewModel.UserRoleColor = "bg-danger";
                }
                else if (roles.Contains("SecondInCommand")) // Adjusted for other roles
                {
                    viewModel.UserRoleColor = "bg-success";
                }
                else
                {
                    viewModel.UserRoleColor = "bg-info";
                }
                // ... Add more roles as needed

                // Add the ViewModel to the list
                speakDakViewModelList.Add(viewModel);
            }

            // Assign the list to your IEnumerable property
            SpeakDakViewModels = speakDakViewModelList;

            // Use speakDakViewModels in your page
            return Page();
        }



    }



}
