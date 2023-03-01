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
    public class OrderDetailsRepository : Repository<OrderDetails>, IOrderDetailsRepository
	{
        private readonly RestaurantDbContext _db;

        public OrderDetailsRepository(RestaurantDbContext db) : base(db)
        {
            _db = db;
        }

        public void Save(OrderDetails orderDetails)
        {
            _db.SaveChanges();
        }

        public void Update(OrderDetails orderDetails)
        {
            _db.OrderDetail.Update(orderDetails);
            
        }
    }
}
