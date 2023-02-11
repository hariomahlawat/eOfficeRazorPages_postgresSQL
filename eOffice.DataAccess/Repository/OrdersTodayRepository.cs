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
    internal class OrdersTodayRepository : Repository<OrdersToday>, IOrdersTodayRepository
    {
        private readonly ApplicationDbContext _db;
        public OrdersTodayRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(OrdersToday obj)
        {
            var objFromDb = _db.OrdersToday.FirstOrDefault(u => u.CreationDate.Day == obj.CreationDate.Day);
            objFromDb.Order = obj.Order;
        }
    }
}
