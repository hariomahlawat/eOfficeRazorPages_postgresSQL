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
    internal class DakSpeakRepository : Repository<DakSpeak>, IDakSpeakRepository
	{
        private readonly ApplicationDbContext _db;
        public DakSpeakRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(DakSpeak obj)
        {
            var objFromDb = _db.DakSpeak.FirstOrDefault(u => u.Id == obj.Id);
            objFromDb.Remarks = obj.Remarks;
        }
    }
}
