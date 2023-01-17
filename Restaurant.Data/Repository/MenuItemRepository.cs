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
    public class MenuItemRepository : Repository<MenuItem>, IMenuItemRepository
    {
        private readonly RestaurantDbContext _db;

        public MenuItemRepository(RestaurantDbContext db) : base(db)
        {
            _db = db;
        }

        public void Save(MenuItem menuItem)
        {
            _db.SaveChanges();
        }

        public void Update(MenuItem menuItem)
        {
            var objFromDb = _db.MenuItems.FirstOrDefault(c => c.Id == menuItem.Id);
            objFromDb.Name = menuItem.Name;
            objFromDb.Description = menuItem.Description;
            objFromDb.Price = menuItem.Price;
            objFromDb.CategoryId = menuItem.CategoryId;
            objFromDb.FoodTypeId = menuItem.FoodTypeId;
            if(objFromDb.Image != null)
            {
                objFromDb.Image = menuItem.Image;
            }
            _db.Update(objFromDb);
        }
    }
}
