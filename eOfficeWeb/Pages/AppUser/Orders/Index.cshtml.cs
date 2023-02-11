using eOffice.DataAccess.Repository.IRepository;
using eOffice.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace eOfficeWeb.Pages.AppUser.Orders
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        public OrdersToday OrdersToday { get; set; }
        public IndexModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void OnGet()
        {
            OrdersToday = _unitOfWork.OrdersToday.GetFirstOrDefault(u=>u.CreationDate.Day==DateTime.Now.Day);
        }

        // POST - create or update order
        public async Task<IActionResult> OnPost(OrdersToday ordersToday)
        {
            OrdersToday = _unitOfWork.OrdersToday.GetFirstOrDefault(u => u.CreationDate.Day == DateTime.Now.Day);

            //create the orders
            if (OrdersToday == null)
            {
                if (ModelState.IsValid)
                {
                    _unitOfWork.OrdersToday.Add(ordersToday);
                    _unitOfWork.Save();
                    return RedirectToPage("/Noticeboard");
                }
                
            }
            else // update the orders
            {
                if (ModelState.IsValid)
                {
                    _unitOfWork.OrdersToday.Update(ordersToday);
                    _unitOfWork.Save();
                    return RedirectToPage("/Noticeboard");
                }
                
            }
            return Page();
        }
    }
}
