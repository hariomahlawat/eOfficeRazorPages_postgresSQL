using eOffice.DataAccess.Repository;
using eOffice.DataAccess.Repository.IRepository;
using eOffice.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;



namespace eOfficeWeb.Pages.AppUser
{
    [Authorize]

    public class UserDakViewModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;
        private UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        public ApplicationUser ApplicationUser { get; set; }
        
        public Dak Dak { get; set; }

        public DakComments DakComments { get; set; }
        public IEnumerable<DakComments> Comments { get; set; }
        public DraftLetter DraftLetter { get; set; }
        public IEnumerable<DraftLetter> DraftLetters { get; set; }
        public MarkedDak MarkedDak { get; set; }
        public DakVisibilityTag DakVisibilityTag { get; set; }
        public string localserver { get; set; }

        public UserDakViewModel(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment, UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
            _userManager = userManager;
            _configuration = configuration;
        }

        public void OnGet(int? id)
        {
            localserver = _configuration["ConnectionStrings:HostAddress"];
            Dak = _unitOfWork.Dak.GetFirstOrDefault(u => u.Id == id);
            Comments = _unitOfWork.DakComments.GetAll(u => u.DakId == id);
            DraftLetters = _unitOfWork.DraftLetter.GetAll(u => u.DakId == id);

            
            MarkedDak = _unitOfWork.MarkedDak.GetFirstOrDefault(u => u.DakId == id);
            DakVisibilityTag = _unitOfWork.DakVisibilityTag.GetFirstOrDefault(u => u.DakId == id);
           
        }

        public async Task<IActionResult> OnPost(DakComments dakComments)
        {
            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                ApplicationUser = await _userManager.FindByIdAsync(userId);
                dakComments.ApplicationUserName = ApplicationUser.Rank+" "+ApplicationUser.FirstName + " "+ApplicationUser.LastName;
                _unitOfWork.DakComments.Add(dakComments);
                _unitOfWork.Save();
                TempData["success"] = "Comment added successfully";
                return RedirectToPage("/AppUser/UserDakView", new { id = dakComments.DakId });
            }
            return Page();
        }

        //POST
        // To upload the draft letter
        public async Task<IActionResult> OnPostUploadDraftLetter(DraftLetter draftLetter)
        {
            string webRootPath = _hostEnvironment.WebRootPath;
            string folderPath = Path.Combine(webRootPath, @"files\pdfs");
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            var files = HttpContext.Request.Form.Files;
            int fileCount = files.Count;
            if (files.Count > 0)
            {

                string fileName_new = Guid.NewGuid().ToString();
                var extension = Path.GetExtension(files[0].FileName);
                using (var fileStream = new FileStream(Path.Combine(folderPath, fileName_new + extension), FileMode.Create))
                {
                    files[0].CopyTo(fileStream);
                }
                draftLetter.DraftLetterUrl = @"files\pdfs\" + fileName_new + extension;
                _unitOfWork.DraftLetter.Add(draftLetter);
                _unitOfWork.Save();
                TempData["success"] = "Draft letter uploaded successfully";
                return RedirectToPage("/AppUser/UserDakView", new { id = draftLetter.DakId });
            }
            TempData["error"] = "No file attached.";
            return RedirectToPage("/AppUser/UserDakView", new { id = draftLetter.DakId });
        }


        //POST
        public IActionResult OnPostDeleteDraftLetter(int id)
        {
            var objFromDb = _unitOfWork.DraftLetter.GetFirstOrDefault(u => u.Id == id);
            if (objFromDb == null)
            {
                return NotFound();
            }
            //delete the image
            string webRootPath = _hostEnvironment.WebRootPath;
            //delete covering letter
            var oldImagePath = Path.Combine(webRootPath, objFromDb.DraftLetterUrl.TrimStart('\\'));
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }
            _unitOfWork.DraftLetter.Remove(objFromDb);
            _unitOfWork.Save();
            TempData["success"] = "Draft letter deleted successfully";
            return RedirectToPage("/AppUser/UserDakView", new { id = objFromDb.DakId });
        }


        //POST - Create or update MarkedDak entry
        public IActionResult OnPostUpsertMarkedDak(MarkedDak markedDak, string OfficersToSee, string SainikSammelan, string RollCall)
        {
            var objFromDb = _unitOfWork.MarkedDak.GetFirstOrDefault(u => u.DakId == markedDak.DakId);

            if (objFromDb == null) //create
            {
                markedDak.OfficersToSee = bool.Parse(OfficersToSee);
                markedDak.SainikSammelan = bool.Parse(SainikSammelan);
                markedDak.RollCall = bool.Parse(RollCall);
                _unitOfWork.MarkedDak.Add(markedDak);
                _unitOfWork.Save();
                TempData["success"] = "Dak marked successfully";
            }
            else //update 
            {
                objFromDb = _unitOfWork.MarkedDak.GetFirstOrDefault(u => u.DakId == markedDak.DakId);
                objFromDb.OfficersToSee = bool.Parse(OfficersToSee);
                objFromDb.SainikSammelan = bool.Parse(SainikSammelan);
                objFromDb.RollCall = bool.Parse(RollCall);
                _unitOfWork.MarkedDak.Update(objFromDb);
                _unitOfWork.Save();
                TempData["success"] = "Dak marked successfully";
            }
            return RedirectToPage("/AppUser/UserDakView", new { id = markedDak.DakId });
        }

        //============================================================================================================
        //POST - Create or update DakVisibilityTag entry
        public IActionResult OnPostUpsertDakVisibilityTag(DakVisibilityTag dakVisibilityTag, string Cdr, string DyCdr)
        {
            var objFromDb = _unitOfWork.DakVisibilityTag.GetFirstOrDefault(u => u.DakId == dakVisibilityTag.DakId);

            if (objFromDb == null) //create
            {
                dakVisibilityTag.Cdr = bool.Parse(Cdr);
                dakVisibilityTag.DyCdr = bool.Parse(DyCdr);
                _unitOfWork.DakVisibilityTag.Add(dakVisibilityTag);
                _unitOfWork.Save();
                TempData["success"] = "Dak marked successfully";
            }
            else //update 
            {
                objFromDb = _unitOfWork.DakVisibilityTag.GetFirstOrDefault(u => u.DakId == dakVisibilityTag.DakId);
                objFromDb.Cdr = bool.Parse(Cdr);
                objFromDb.DyCdr = bool.Parse(DyCdr);
                _unitOfWork.DakVisibilityTag.Update(objFromDb);
                _unitOfWork.Save();
                TempData["success"] = "Dak marked successfully";
            }
            return RedirectToPage("/AppUser/UserDakView", new { id = dakVisibilityTag.DakId });
        }


    }
}
