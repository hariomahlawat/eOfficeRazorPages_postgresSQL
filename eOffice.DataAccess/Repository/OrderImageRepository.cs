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
    internal class OrderImageRepository : Repository<OrderImage>, IOrderImageRepository
    {
        private readonly ApplicationDbContext _db;
        public OrderImageRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(OrderImage obj)
        {
            var objFromDb = _db.OrderImages.FirstOrDefault(u => u.Id==obj.Id);
            objFromDb.Name = obj.Name;
            objFromDb.ImgUrl = obj.ImgUrl;
            objFromDb.UploadedOn = obj.UploadedOn;
        }
    }
}
