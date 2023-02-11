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
    internal class DakCommentsRepository : Repository<DakComments>, IDakCommentsRepository
    {
        private readonly ApplicationDbContext _db;
        public DakCommentsRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(DakComments obj)
        {
            //to be done later
        }
    }
}
