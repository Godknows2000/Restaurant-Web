using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Restaurant.Data.Data;
using Restaurant.Models;

namespace Restaurant.Web.Pages.Admin.Categories
{
    public class DetailsModel : PageModel
    {
        private readonly RestaurantDbContext _db;
        public Category Category { get; set; }
        public DetailsModel(RestaurantDbContext db)
        {
            _db = db;
        }

        public void OnGet(int id)
        {
            Category = _db.Categories.FirstOrDefault(c => c.Id == id);

        }
    }
}
