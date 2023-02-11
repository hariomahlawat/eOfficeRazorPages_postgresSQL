using eOffice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eOffice.DataAccess.Repository.IRepository
{
    public interface IOfficeBranchRepository : IRepository<OfficeBranch>
    {
        void Update(OfficeBranch obj);
    }
}
