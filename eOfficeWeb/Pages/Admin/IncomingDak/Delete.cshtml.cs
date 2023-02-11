using eOffice.DataAccess.Repository.IRepository;
using eOffice.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace eOfficeWeb.Pages.Admin.IncomingDak
{
    public class DeleteModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IConfiguration _configuration;

        public Dak Dak { get; set; }
        public string localserver { get; set; }
        public DeleteModel(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = webHostEnvironment;
            Dak = new();
            _configuration = configuration;
        }
        public void OnGet(int? id)
        {
            localserver = _configuration["ConnectionStrings:HostAddress"];
            if (id != null)
            {
                //Edit
                Dak = _unitOfWork.Dak.GetFirstOrDefault(u => u.Id == id);
            }
        }

        //POST
        public IActionResult OnPost(int id)
        {
            localserver = _configuration["ConnectionStrings:HostAddress"];
            var objFromDb = _unitOfWork.Dak.GetFirstOrDefault(Dak => Dak.Id == id);
            if (objFromDb == null)
            {
                return NotFound();
            }
            //delete the image
            string webRootPath = _hostEnvironment.WebRootPath;
            //string folderPath = Path.Combine(webRootPath, @"files\pdfs");
            //delete covering letter
            var oldImagePath = Path.Combine(webRootPath, objFromDb.CoveringLetter.TrimStart('\\'));
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }
            //delete enclosure1 if exists
            if (objFromDb.Enclosure1 != null)
            {
                oldImagePath = Path.Combine(webRootPath, objFromDb.Enclosure1.TrimStart('\\'));
                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }
            }
            //delete enclosure2 if exists
            if (objFromDb.Enclosure2 != null)
            {
                oldImagePath = Path.Combine(webRootPath, objFromDb.Enclosure2.TrimStart('\\'));
                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }
            }
            //delete enclosure3 if exists
            if (objFromDb.Enclosure3 != null)
            {
                oldImagePath = Path.Combine(webRootPath, objFromDb.Enclosure3.TrimStart('\\'));
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
