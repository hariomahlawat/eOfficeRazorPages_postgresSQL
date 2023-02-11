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
    internal class SocialCalendarRepository : Repository<SocialCalendar>, ISocialCalendarRepository
    {
        private readonly ApplicationDbContext _db;
        public SocialCalendarRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(SocialCalendar socialCalendarEvent)
        {
            var objFromDb = _db.SocialCalendar.FirstOrDefault(u => u.Id == socialCalendarEvent.Id);
            objFromDb.Name = socialCalendarEvent.Name;
            objFromDb.EventDate = socialCalendarEvent.EventDate;
            objFromDb.EventType = socialCalendarEvent.EventType;
        }

    }
}
