using eOffice.DataAccess.Repository.IRepository;
using eOffice.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using eOffice.DataAccess.Data;

namespace eOfficeWeb.Pages.AppUser.UserToDoList
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        public IndexModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<ToDoList> ToDoLists { get; set; }
        public ToDoList ToDoList { get; set; }

        public void OnGet()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ToDoLists = _unitOfWork.ToDoList.GetAll(u => u.ApplicationUserId == userId);
            ToDoLists = ToDoLists.Where(u => u.IsCompleted== false);
        }

        // POST - add a new task in todo list
        public async Task<IActionResult> OnPost(ToDoList toDoList)
        {
            if (toDoList.Task!=null)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                _unitOfWork.ToDoList.Add(toDoList);
                _unitOfWork.Save();
                TempData["success"] = "Task added successfully";
                
            }
            else {
                TempData["error"] = "You just tried to create a task without a task. This time, lets's try again by providing a task.";
            }
            return RedirectToPage("/AppUser/UserToDoList/Index");


        }

        // delete a task from todo list.
        //POST
        public async Task<IActionResult> OnPostDelete(int id)
        {
            //int id = toDoList.Id;
            var taskFromDb = _unitOfWork.ToDoList.GetFirstOrDefault(u => u.Id == id);
            if (taskFromDb != null)
            {
                _unitOfWork.ToDoList.Remove(taskFromDb);
                _unitOfWork.Save();
                TempData["success"] = "Task deleted successfully";
                return RedirectToPage("/AppUser/UserToDoList/Index");

            }

            return Page();
        }
    }
}
