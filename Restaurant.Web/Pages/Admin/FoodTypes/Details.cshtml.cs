using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Restaurant.Data.Data;
using Restaurant.Models;

namespace Restaurant.Web.Pages.Admin.FoodTypes
{
    public class DetailsModel : PageModel
    {
        private readonly RestaurantDbContext _db;
        public FoodType FoodType { get; set; }
        public DetailsModel(RestaurantDbContext db)
        {
            _db = db;
        }

        public void OnGet(int id)
        {
            FoodType = _db.FoodTypes.FirstOrDefault(c => c.Id == id);

        }
    }
}
