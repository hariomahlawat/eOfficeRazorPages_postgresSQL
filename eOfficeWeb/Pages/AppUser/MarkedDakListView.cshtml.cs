using eOffice.DataAccess.Repository.IRepository;
using eOffice.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace eOfficeWeb.Pages.AppUser
{
    [Authorize]
    public class MarkedDakListViewModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        public IEnumerable<Dak> Dak { get; set; }
        public IEnumerable<MarkedDak> MarkedDak { get; set; }
        public IEnumerable<MarkedDak> MarkedDakOTS { get; set; }
        public IEnumerable<MarkedDak> MarkedDakSS { get; set; }
        public IEnumerable<MarkedDak> MarkedDakRollCall { get; set; }
        public int CountOfficersToSeeDak { get; set; }
        public int CountSainikSammelanDak { get; set; }
        public int CountRollCallDak { get; set; }
        public bool FlagOTS, FlagSS, FlagRollCall = false;

        public MarkedDakListViewModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void OnGet(string? markedType)
        {

            MarkedDak = _unitOfWork.MarkedDak.GetAll();

            MarkedDakOTS = _unitOfWork.MarkedDak.GetAll().Where(u => u.OfficersToSee == true);
            CountOfficersToSeeDak = MarkedDakOTS.Count();

            MarkedDakSS = _unitOfWork.MarkedDak.GetAll().Where(u => u.SainikSammelan == true);
            CountSainikSammelanDak = MarkedDakSS.Count();

            MarkedDakRollCall = _unitOfWork.MarkedDak.GetAll().Where(u => u.RollCall == true);
            CountRollCallDak = MarkedDakRollCall.Count();

            if (markedType=="OTS")
            {
                FlagOTS = true;
                var markedDakIdsOTS = MarkedDakOTS.Select(u => u.DakId).ToArray();
                //select all marked daks for OTS
                Dak = _unitOfWork.Dak.GetAll().Where(u => markedDakIdsOTS.Contains(u.Id));
            }
            else if (markedType=="SS")
            {
                FlagSS = true;
                var markedDakIdsSS = MarkedDakSS.Select(u => u.DakId).ToArray();
                //select all marked daks for SS
                Dak = _unitOfWork.Dak.GetAll().Where(u => markedDakIdsSS.Contains(u.Id));
            }
            else
            {
                FlagRollCall = true;
                var markedDakIdsRollCall = MarkedDakRollCall.Select(u => u.DakId).ToArray();
                //select all marked daks for Roll Call
                Dak = _unitOfWork.Dak.GetAll().Where(u => markedDakIdsRollCall.Contains(u.Id));
            }
            

        }
    }
}
