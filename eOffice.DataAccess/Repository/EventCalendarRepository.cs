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
    internal class EventCalendarRepository : Repository<EventCalendar>, IEventCalendarRepository
    {
        private readonly ApplicationDbContext _db;
        public EventCalendarRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(EventCalendar obj)
        {
            var objFromDb = _db.EventCalendar.FirstOrDefault(u => u.Id == obj.Id);
            objFromDb.Subject = obj.Subject;
            objFromDb.Description = obj.Description;
            objFromDb.ThemeColor = obj.ThemeColor;
            objFromDb.Start = obj.Start;
            objFromDb.End = obj.End;
            objFromDb.IsFullDay = obj.IsFullDay;
        }

    }
}
