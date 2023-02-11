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
    internal class DraftLetterRepository : Repository<DraftLetter>, IDraftLetterRepository
    {
        private readonly ApplicationDbContext _db;
        public DraftLetterRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(DraftLetter obj)
        {
            //to be done later
        }
    }
}
