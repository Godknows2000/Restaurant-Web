using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Restaurant.Data.Data;
using Restaurant.Models;

namespace Restaurant.Web.Pages.Admin.Categories
{
    public class DeleteModel : PageModel
    {
        private readonly RestaurantDbContext _db;
        [BindProperty]
        public Category Category { get; set; }
        public DeleteModel(RestaurantDbContext db)
        {
            _db= db;
        }
        public void OnGet(int Id)
        {
            Category = _db.Categories.FirstOrDefault(c => c.Id==Id);
        }
        public async Task<IActionResult> OnPost()
        {
                var categoryFromDb = _db.Categories.FirstOrDefault(c => c.Id==Category.Id);
                if (categoryFromDb != null)
                {
                    _db.Categories.Remove(categoryFromDb);
                    await _db.SaveChangesAsync();
                    TempData["success"] = "Category successfully deleted";
                    return RedirectToPage("Index");
                }
                
                
            return Page();
        }
    }
}
