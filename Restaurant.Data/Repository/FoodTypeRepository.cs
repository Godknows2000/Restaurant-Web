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
    public class FoodTypeRepository : Repository<FoodType>, IFoodTypeRepository
    {
        private readonly RestaurantDbContext _db;

        public FoodTypeRepository(RestaurantDbContext db) : base(db)
        {
            _db = db;
        }

        public void Save(FoodType foodType)
        {
            _db.SaveChanges();
        }

        public void Update(FoodType foodType)
        {
            var objFromDb = _db.FoodTypes.FirstOrDefault(c => c.Id == foodType.Id);
            objFromDb.Name = foodType.Name;
        }
    }
}
