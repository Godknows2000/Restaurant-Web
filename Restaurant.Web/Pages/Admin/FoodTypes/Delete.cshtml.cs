using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Restaurant.Data.Data;
using Restaurant.Models;

namespace Restaurant.Web.Pages.Admin.FoodTypes
{
    public class DeleteModel : PageModel
    {
        private readonly RestaurantDbContext _db;
        [BindProperty]
        public FoodType FoodType { get; set; }
        public DeleteModel(RestaurantDbContext db)
        {
            _db= db;
        }
        public void OnGet(int Id)
        {
            FoodType = _db.FoodTypes.FirstOrDefault(c => c.Id==Id);
        }
        public async Task<IActionResult> OnPost()
        {
                var foodFromDb = _db.FoodTypes.FirstOrDefault(c => c.Id==FoodType.Id);
                if (foodFromDb != null)
                {
                    _db.FoodTypes.Remove(foodFromDb);
                    await _db.SaveChangesAsync();
                    TempData["success"] = "Food type deleted successfully ";
                    return RedirectToPage("Index");
                }
                
                
            return Page();
        }
    }
}
