using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Restaurant.Data.Data;
using Restaurant.Models;

namespace Restaurant.Web.Pages.Admin.FoodTypes
{
    public class AddModel : PageModel
    {
        private readonly RestaurantDbContext _db;
        
        [BindProperty]
        public FoodType FoodType { get; set; }
        public AddModel(RestaurantDbContext db)
        {
            _db = db;
        }
        public void OnGet()
        {

        }
        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                await _db.FoodTypes.AddAsync(FoodType);
                await _db.SaveChangesAsync();
                TempData["Success"] = "Food type created successfully ";
                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}
