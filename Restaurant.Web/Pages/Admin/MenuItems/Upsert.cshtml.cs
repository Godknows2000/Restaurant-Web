using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Restaurant.Data.Data;
using Restaurant.Data.Repository;
using Restaurant.Data.Repository.IRepository;
using Restaurant.Models;

namespace Restaurant.Web.Pages.Admin.MenuItems
{
    [BindProperties]
    public class UpsertModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;

        public MenuItem MenuItem { get; set; }
        public IEnumerable<SelectListItem> CategoryList {get; set; }
        public IEnumerable<SelectListItem> FoodTypeList { get; set; }
        public UpsertModel(IUnitOfWork UnitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = UnitOfWork;
            MenuItem = new();
            _hostEnvironment = hostEnvironment;
        }
        public void OnGet(int? id)
        {
            if(id != null)
            {
                //edit
                MenuItem = _unitOfWork.MenuItem.GetFirstOrDefault(x => x.Id == id);
            }
            CategoryList = _unitOfWork.Category.GetAll().Select(c => new SelectListItem()
            {
                Text = c.Name,
                Value = c.Id.ToString()
            });
            FoodTypeList = _unitOfWork.FoodType.GetAll().Select(c => new SelectListItem()
            {
                Text = c.Name,
                Value = c.Id.ToString()
            });
        }
        public async Task<IActionResult> OnPost()
        {
            string webRootPath = _hostEnvironment.WebRootPath;
            var files = HttpContext.Request.Form.Files;
            if (MenuItem.Id == 0)
            {
                //create
                string new_FileName = Guid.NewGuid().ToString();
                var uploads = Path.Combine(webRootPath, @"images/menuItems");
                var extensions = Path.GetExtension(files[0].FileName);

                using(var fileStream = new FileStream(Path.Combine(uploads, new_FileName + extensions), FileMode.Create))
                {
                    files[0].CopyTo(fileStream);
                }
                MenuItem.Image = "/images/menuItems/" + new_FileName + extensions;
                _unitOfWork.MenuItem.Add(MenuItem);
                _unitOfWork.Save();
            }
            else
            {
                //edit
                var objFromDb = _unitOfWork.MenuItem.GetFirstOrDefault(u => u.Id == MenuItem.Id);
                if(files.Count>0)
                {
                    string new_FileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(webRootPath, @"images/menuItems");
                    var extensions = Path.GetExtension(files[0].FileName);

                    //delete old image in the project
                    var oldImage = Path.Combine(webRootPath, objFromDb.Image.TrimStart('\\'));
                    if (System.IO.File.Exists(oldImage))
                    {
                        System.IO.File.Delete(oldImage);
                    }
                    //upload new image path
                    using (var fileStream = new FileStream(Path.Combine(uploads, new_FileName + extensions), FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);
                    }
                    MenuItem.Image = "/images/menuItems/" + new_FileName + extensions;
                }
                else
                {
                    MenuItem.Image = objFromDb.Image;
                }
                _unitOfWork.MenuItem.Update(MenuItem);
                _unitOfWork.Save();
            }
            return RedirectToPage("./Index");
        }
    }
}
