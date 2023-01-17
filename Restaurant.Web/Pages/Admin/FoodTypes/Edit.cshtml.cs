using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Restaurant.Data.Data;
using Restaurant.Models;

namespace Restaurant.Web.Pages.Admin.FoodTypes
{
    public class EditModel : PageModel
    {
        private readonly RestaurantDbContext _db;
        [BindProperty]
        public FoodType FoodType { get; set; }
        public EditModel(RestaurantDbContext db)
        {
            _db= db;
        }
        public void OnGet(int id)
        {
            FoodType = _db.FoodTypes.Find(id);
        }
        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                _db.Update(FoodType);
                await _db.SaveChangesAsync();
                TempData["success"] = "Food type updated successfully ";
                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}
