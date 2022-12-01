using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Restaurant.Data.Data;
using Restaurant.Data.Repository.IRepository;
using Restaurant.Models;

namespace Restaurant.Web.Pages.Admin.Categories
{
    public class AddModel : PageModel
    {
        private readonly ICategoryRepository _dbCategory;
        
        [BindProperty]
        public Category Category { get; set; }
        public AddModel(ICategoryRepository dbCategory)
        {
            _dbCategory = dbCategory;
        }
        public void OnGet()
        {

        }
        public async Task<IActionResult> OnPost()
        {
            if (Category.Name == Category.DisplayOrder.ToString())
            {
                ModelState.AddModelError(string.Empty, "The display name cannot match the display order");
            }
            if (ModelState.IsValid)
            {
                _dbCategory.Add(Category);
                _dbCategory.Save();
                TempData["Success"] = "Category successfully created";
                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}
