using eOffice.DataAccess.Repository;
using eOffice.DataAccess.Repository.IRepository;
using eOffice.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace eOfficeWeb.Pages.Admin.IncomingDak
{
    [Authorize]
    public class DetailModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        public Dak Dak { get; set; }
        public IEnumerable<DakComments> DakComments { get; set; }
        public string localserver { get; set; }
        public DetailModel(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }
        public void OnGet(int id)
        {
            localserver = _configuration["ConnectionStrings:HostAddress"];
            Dak = _unitOfWork.Dak.GetFirstOrDefault(u => u.Id == id);
            DakComments= _unitOfWork.DakComments.GetAll(u=>u.DakId==id);
        }
    }
}
