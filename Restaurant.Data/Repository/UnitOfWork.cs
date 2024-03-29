﻿using Restaurant.Data.Data;
using Restaurant.Data.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly RestaurantDbContext _db;
        public UnitOfWork(RestaurantDbContext db)
        {
            _db = db;
            Category = new CategoryRepository(_db);
            FoodType = new FoodTypeRepository(_db);
            MenuItem = new MenuItemRepository(_db);
            ShoppingCart = new ShoppingCartRepository(_db);
            OrderDetails = new OrderDetailsRepository(_db);
            OrderHeader = new OrderHeaderRepository(_db);
            User = new UserRepository(_db);
        }
        public ICategoryRepository Category { get;private set; }
        public IFoodTypeRepository FoodType { get;private set; }
        public IMenuItemRepository MenuItem { get;private set; }
        public IShoppingCartRepository ShoppingCart { get;private set; }
        public IOrderDetailsRepository  OrderDetails { get;private set; }
        public IOrderHeaderRepository OrderHeader { get;private set; }
        public IUserRepository User { get;private set; }
        public void Dispose()
        {
            _db.Dispose();
        }
        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
