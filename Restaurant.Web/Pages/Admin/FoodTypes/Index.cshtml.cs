using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Restaurant.Data.Data;
using Restaurant.Models;

namespace Restaurant.Web.Pages.Admin.FoodTypes
{
    public class IndexModel : PageModel
    {
        private readonly RestaurantDbContext _db;
        public IEnumerable<FoodType> FoodTypes { get; set; }
        public IndexModel(RestaurantDbContext db)
        {
            _db = db;
        }

        public void OnGet()
        {
            FoodTypes = _db.FoodTypes;
        }
    }
}
