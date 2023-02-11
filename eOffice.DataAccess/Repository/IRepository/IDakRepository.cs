using eOffice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eOffice.DataAccess.Repository.IRepository
{
    public interface IDakRepository : IRepository<Dak>
    {
        void Update(Dak obj);
    }
}
