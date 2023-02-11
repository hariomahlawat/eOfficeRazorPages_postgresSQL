using eOffice.DataAccess.Repository.IRepository;
using eOffice.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NuGet.Packaging.Signing;

namespace eOfficeWeb.Pages.AppUser.Orders
{
    [Authorize]
    public class OrderImagesModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IConfiguration _configuration;

        public OrderImagesModel(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
            _configuration = configuration;
        }

        public IEnumerable<OrderImage> OrderImages { get; set; }
        public OrderImage OrderImage { get; set; }
        public string localserver { get; set; }
        public void OnGet()
        {
            localserver = _configuration["ConnectionStrings:HostAddress"];
            OrderImages = _unitOfWork.OrderImage.GetAll();

        }

        public async Task<IActionResult> OnPost(OrderImage OrderImage)
        {
            
            string webRootPath = _hostEnvironment.WebRootPath;
            string folderPath = Path.Combine(webRootPath, @"images\orders");
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            var files = HttpContext.Request.Form.Files;
            if (files.Count == 0)
            {
                TempData["error"] = "Please select an image.";
                return RedirectToPage("/AppUser/Orders/OrderImages");
            }

            string fileName_new = Guid.NewGuid().ToString();
            var extension = Path.GetExtension(files[0].FileName);
            string[] extensionArray = { ".jpg", ".JPG", ".png", ".PNG", ".tif", ".TIF", };
            if (!extensionArray.Contains(extension))
            {
                TempData["error"] = "Please select an image only (png, jpg, jpeg or tif format). ";
                return RedirectToPage("/AppUser/Orders/OrderImages");
            }
            using (var fileStream = new FileStream(Path.Combine(folderPath, fileName_new + extension), FileMode.Create))
            {
                files[0].CopyTo(fileStream);
            }
            OrderImage.ImgUrl = @"images\orders\" + fileName_new + extension;

            _unitOfWork.OrderImage.Add(OrderImage);
            _unitOfWork.Save();
            TempData["success"] = "Uploaded successfully.";
            return RedirectToPage("/AppUser/Orders/OrderImages");
        }


        // delete a task from todo list.
        //POST
        public async Task<IActionResult> OnPostDelete(int id)
        {
            //int id = toDoList.Id;
            var objFromDb = _unitOfWork.OrderImage.GetFirstOrDefault(u => u.Id == id);
            if (objFromDb == null)
            {
                return NotFound();
            }

            //delete the image
            string webRootPath = _hostEnvironment.WebRootPath;
            var oldImagePath = Path.Combine(webRootPath, objFromDb.ImgUrl.TrimStart('\\'));
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }

            _unitOfWork.OrderImage.Remove(objFromDb);
            _unitOfWork.Save();

            return RedirectToPage("/AppUser/Orders/OrderImages");
        }





    }
}
