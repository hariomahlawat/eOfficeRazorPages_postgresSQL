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
    internal class DakRepository : Repository<Dak>, IDakRepository
    {
        private readonly ApplicationDbContext _db;
        public DakRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Dak obj)
        {
            var objFromDb = _db.Dak.FirstOrDefault(u => u.Id == obj.Id);
            objFromDb.Subject = obj.Subject;
            objFromDb.From = obj.From;
            objFromDb.LetterDate = obj.LetterDate;
            objFromDb.DateReceived = obj.DateReceived;
            objFromDb.LetterNo = obj.LetterNo;
            objFromDb.OfficeSection = obj.OfficeSection;
            objFromDb.CoveringLetter = obj.CoveringLetter;
            objFromDb.Enclosure1 = obj.Enclosure1;
            objFromDb.Enclosure2 = obj.Enclosure2;
            objFromDb.Enclosure3 = obj.Enclosure3;
            objFromDb.Remarks = obj.Remarks;

        }
    }
}
