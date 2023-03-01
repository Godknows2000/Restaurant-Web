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
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly RestaurantDbContext _db;

        public UserRepository(RestaurantDbContext db) : base(db)
        {
            _db = db;
        }
    }
}
