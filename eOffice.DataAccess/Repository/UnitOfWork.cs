using eOffice.DataAccess.Data;
using eOffice.DataAccess.Repository.IRepository;
using eOffice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eOffice.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;
        public ISocialCalendarRepository socialCalendar { get; private set; }
        public IDakRepository Dak { get; private set; }
        public IDakCommentsRepository DakComments { get; private set; }
        public IToDoListRepository ToDoList { get; private set; }
        public IDraftLetterRepository DraftLetter { get; private set; }
        public IMarkedDakRepository MarkedDak { get; private set; }
        public IEventCalendarRepository EventCalendar { get; private set; }
        public IOrdersTodayRepository OrdersToday { get; private set; }
        public IOfficeBranchRepository OfficeBranch { get; private set; }
        public IOfficeBranchSectionRepository OfficeBranchSection { get; private set; }
        public IOrderImageRepository OrderImage { get; private set; }
        public IDakVisibilityTagRepository DakVisibilityTag { get; private set; }
        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            socialCalendar = new SocialCalendarRepository(_db);
            Dak = new DakRepository(_db);
            DakComments = new DakCommentsRepository(_db);
            ToDoList = new ToDoListRepository(_db);
            DraftLetter = new DraftLetterRepository(_db);
            MarkedDak = new MarkedDakRepository(_db);
            EventCalendar = new EventCalendarRepository(_db);
            OrdersToday = new OrdersTodayRepository(_db);
            OfficeBranch = new OfficeBranchRepository(_db);
            OfficeBranchSection = new OfficeBranchSectionRepository(_db);
            OrderImage = new OrderImageRepository(_db);
            DakVisibilityTag = new DakVisibilityTagRepository(_db);
        }

        


        public void Dispose()
        {
            _db.Dispose();
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
