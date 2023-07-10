using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using eOffice.DataAccess.Data;
using eOffice.DataAccess.Repository.IRepository;
using eOffice.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;

namespace eOfficeWeb.Pages.Admin.IncomingDak
{
    [BindProperties]
    public class UpsertModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;

        public Dak Dak { get; set; }

		public UpsertModel(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment, UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
            _userManager = userManager;
            _configuration = configuration;
            Dak = new();
        }
        public IEnumerable<OfficeBranch> OfficeBranches { get; set; }
        public IEnumerable<SelectListItem> selectListOfficeBranches { get; set; }
        public IEnumerable<OfficeBranchSection> OfficeBranchSections { get; set; }
        public IEnumerable<SelectListItem> selectListOfficeBranchSections { get; set; }
        public IEnumerable<ApplicationUser> Users { get; set; }
        public ApplicationUser LoggedInUser { get; set; }
        public string UserBranchName { get; set; }
        public string localserver { get; set; }


        public void OnGetAsync(int? id)
        {

			localserver = _configuration["ConnectionStrings:HostAddress"];
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
            int userOfficeBranchId = userOfficeBranch.Id;
            OfficeBranchSections = _unitOfWork.OfficeBranchSection.GetAll().Where(u => u.OfficeBranchId== userOfficeBranchId && u.Name != "All" && u.Name != "None");
            selectListOfficeBranchSections = OfficeBranchSections.Select(s => new SelectListItem
            {
                //Value = s.Id.ToString(),
                Value = s.Name,
                Text = s.Name
            });

            if (id != null)
            {
                //Edit - get Dak details from database
                Dak = _unitOfWork.Dak.GetFirstOrDefault(u => u.Id == id);
            }
        }

        public async Task<IActionResult> OnPost()
        {
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


            string webRootPath = _hostEnvironment.WebRootPath;
            string folderPath = Path.Combine(webRootPath,@"files\pdfs");
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            var files = HttpContext.Request.Form.Files;
            int fileCount = files.Count;
            if (Dak.Id == 0) //create
            {
                Dak.OfficeBranch = UserBranchName;
                if (files.Count > 0)
                {
                    for (int i = 0; i < files.Count; i++)
                    {
                        string fileName_new = Guid.NewGuid().ToString();
                        var extension = Path.GetExtension(files[i].FileName);

                        using (var fileStream = new FileStream(Path.Combine(folderPath, fileName_new + extension), FileMode.Create))
                        {
                            files[i].CopyTo(fileStream);
                        }
                        if (files[i].Name == "coveringLetter")
                        {
                            Dak.CoveringLetter = @"files\pdfs\" + fileName_new + extension;
                        }
                        else if (files[i].Name == "enclosure1")
                        {
                            Dak.Enclosure1 = @"files\pdfs\" + fileName_new + extension;
                        }
                        else if (files[i].Name == "enclosure2")
                        {
                            Dak.Enclosure2 = @"files\pdfs\" + fileName_new + extension;
                        }
                        else
                        {
                            Dak.Enclosure3 = @"files\pdfs\" + fileName_new + extension;
                        }
                        _unitOfWork.Dak.Add(Dak);
                    }
                    
                }
                _unitOfWork.Save();
            }
            else
            {
                //edit
                var objFromDb = _unitOfWork.Dak.GetFirstOrDefault(u => u.Id == Dak.Id);
                Dak.CoveringLetter = objFromDb.CoveringLetter; // To make sure that coverinng letter is not null in any case.
                Dak.Enclosure1 = objFromDb.Enclosure1;
                Dak.Enclosure2 = objFromDb.Enclosure2;
                Dak.Enclosure3 = objFromDb.Enclosure3;
                if (files.Count > 0)
                {
                    for (int i = 0; i < files.Count; i++)
                    {
                        string fileName_new = Guid.NewGuid().ToString();
                        //var uploads = Path.Combine(folderPath, @"dak\pdfs");
                        var extension = Path.GetExtension(files[i].FileName);
                        
                        //new upload
                        using (var fileStream = new FileStream(Path.Combine(folderPath, fileName_new + extension), FileMode.Create))
                        {
                            files[i].CopyTo(fileStream);
                        }
                        //update the corrseponding field in database table.
                        if (files[i].Name == "coveringLetter")
                        {
                            //delete the old image
                            var oldImagePath = Path.Combine(webRootPath, objFromDb.CoveringLetter.TrimStart('\\'));
                            if (System.IO.File.Exists(oldImagePath))
                            {
                                System.IO.File.Delete(oldImagePath);
                            }
                            Dak.CoveringLetter = @"files\pdfs\" + fileName_new + extension;
                        }
                        else if (files[i].Name == "enclosure1")
                        {
                            //delete the old image
                            if (objFromDb.Enclosure1!=null)
                            {
                                var oldImagePath = Path.Combine(webRootPath, objFromDb.Enclosure1.TrimStart('\\'));
                                if (System.IO.File.Exists(oldImagePath))
                                {
                                    System.IO.File.Delete(oldImagePath);
                                }
                            }
                            
                            Dak.Enclosure1 = @"files\pdfs\" + fileName_new + extension;
                        }
                        else if (files[i].Name == "enclosure2")
                        {
                            //delete the old image
                            if (objFromDb.Enclosure2 != null)
                            {
                                var oldImagePath = Path.Combine(webRootPath, objFromDb.Enclosure2.TrimStart('\\'));
                                if (System.IO.File.Exists(oldImagePath))
                                {
                                    System.IO.File.Delete(oldImagePath);
                                }
                            }

                            Dak.Enclosure2 = @"files\pdfs\" + fileName_new + extension;
                        }
                        else
                        {
                            //delete the old image
                            if (objFromDb.Enclosure3 != null)
                            {
                                var oldImagePath = Path.Combine(webRootPath, objFromDb.Enclosure3.TrimStart('\\'));
                                if (System.IO.File.Exists(oldImagePath))
                                {
                                    System.IO.File.Delete(oldImagePath);
                                }
                            }

                            Dak.Enclosure3 = @"files\pdfs\" + fileName_new + extension;
                        }
                        
                    }
                    
                }
                _unitOfWork.Dak.Update(Dak);
                _unitOfWork.Save();
            }
        
            return RedirectToPage("./Index");
        }


        


    }
}
