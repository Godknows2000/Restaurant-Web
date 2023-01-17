using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Restaurant.Data.Data;
using Restaurant.Models;

namespace Restaurant.Web.Pages.Admin.MenuItems
{
    public class UpsertModel : PageModel
    {
        private readonly RestaurantDbContext _db;
        
        [BindProperty]
        public MenuItem MenuItem { get; set; }
        public UpsertModel(RestaurantDbContext db)
        {
            _db = db;
            MenuItem = new();
        }
        public void OnGet()
        {

        }
        public async Task<IActionResult> OnPost()
        {
            //if (ModelState.IsValid)
            //{
            //    await _db.FoodTypes.AddAsync(MenuItems);
            //    await _db.SaveChangesAsync();
            //    TempData["success"] = "Food type created successfully ";
            //    return RedirectToPage("Index");
            //}
            return Page();
        }
    }
}
