using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eOffice.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        ISocialCalendarRepository socialCalendar { get; }
        IDakRepository Dak { get; }
        IDakCommentsRepository DakComments { get; }
        IToDoListRepository ToDoList { get; }
        IDraftLetterRepository DraftLetter { get; }
        IMarkedDakRepository MarkedDak { get; }
        IEventCalendarRepository EventCalendar { get; }
        IOrdersTodayRepository OrdersToday { get; }
        IOfficeBranchRepository OfficeBranch { get; }
        IOfficeBranchSectionRepository OfficeBranchSection { get; }
        IOrderImageRepository OrderImage { get; }
        IDakVisibilityTagRepository DakVisibilityTag { get; }
        void Save();
    }
}
