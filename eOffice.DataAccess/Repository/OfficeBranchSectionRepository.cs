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
    internal class OfficeBranchSectionRepository : Repository<OfficeBranchSection>, IOfficeBranchSectionRepository
    {
        private readonly ApplicationDbContext _db;
        public OfficeBranchSectionRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(OfficeBranchSection obj)
        {
            //to be done later
        }
    }
}
