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
    internal class MarkedDakRepository : Repository<MarkedDak>, IMarkedDakRepository
    {
        private readonly ApplicationDbContext _db;
        public MarkedDakRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(MarkedDak obj)
        {
            var objFromDb = _db.MarkedDak.FirstOrDefault(u => u.DakId == obj.DakId);
            objFromDb.OfficersToSee = obj.OfficersToSee;
            objFromDb.SainikSammelan = obj.SainikSammelan;
            objFromDb.RollCall = obj.RollCall;
        }
    }
}
