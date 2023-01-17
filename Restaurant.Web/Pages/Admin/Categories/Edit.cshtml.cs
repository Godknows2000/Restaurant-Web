using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Restaurant.Data.Data;
using Restaurant.Models;

namespace Restaurant.Web.Pages.Admin.Categories
{
    public class EditModel : PageModel
    {
        private readonly RestaurantDbContext _db;
        [BindProperty]
        public Category Category { get; set; }
        public EditModel(RestaurantDbContext db)
        {
            _db= db;
        }
        public void OnGet(int id)
        {
            Category = _db.Categories.Find(id);
        }
        public async Task<IActionResult> OnPost()
        {
            if (Category.Name == Category.DisplayOrder.ToString())
            {
                ModelState.AddModelError(string.Empty, "The Name cannot be same to Display Order");
            }
            if (ModelState.IsValid)
            {
                _db.Update(Category);
                await _db.SaveChangesAsync();
                TempData["success"] = "Category successfully updated";
                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}
