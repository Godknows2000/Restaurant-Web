using Restaurant.Data.Data;
using Restaurant.Data.Repository.IRepository;
using Restaurant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Data.Repository
{
    public class OrderHeaderRepository : Repository<OrderHeader>, IOrderHeaderRepository
	{
        private readonly RestaurantDbContext _db;

        public OrderHeaderRepository(RestaurantDbContext db) : base(db)
        {
            _db = db;
        }

        public void Save(OrderHeader orderHeader)
        {
            _db.SaveChanges();
        }

        public void Update(OrderHeader orderHeader)
        {
            _db.OrderHeaders.Update(orderHeader);
            
        }
    }
}
