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
    internal class DakVisibilityTagRepository : Repository<DakVisibilityTag>, IDakVisibilityTagRepository
    {
        private readonly ApplicationDbContext _db;
        public DakVisibilityTagRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(DakVisibilityTag obj)
        {
            var objFromDb = _db.DakVisibilityTag.FirstOrDefault(u => u.DakId == obj.DakId);
            objFromDb.Cdr = obj.Cdr;
            objFromDb.DyCdr = obj.DyCdr;
        }
    }
}
