using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Restaurant.Data.Repository.IRepository;
using Restaurant.Models;
using System.Security.Claims;

namespace Restaurant.Web.Pages.Customer.Cart
{
    [Authorize]
    public class IndexModel : PageModel
    {
        public IEnumerable<ShoppingCart> ShoppingCartList { get; set; }
        public MenuItem MenuItem { get; set; }
        public double CartTotal { get; set; }
        private readonly IUnitOfWork _unitOfWork;
        public IndexModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            CartTotal= 0;
        }
        public void OnGet(int id)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if(claim != null)
            {
                ShoppingCartList = _unitOfWork.ShoppingCart.GetAll(filter: u => u.UserId == claim.Value, includeProperties: "menuItem,menuItem.Category,menuItem.FoodType");
            }
            foreach(var cart in ShoppingCartList)
            {
                CartTotal += (cart.menuItem.Price * cart.Count);
            }
        }

		public IActionResult OnPostPlus(int cartId)
		{
			var cart = _unitOfWork.ShoppingCart.GetFirstOrDefault(u => u.Id == cartId);
			_unitOfWork.ShoppingCart.IncrementCount(cart, 1);
			return RedirectToPage("/Customer/Cart/Index");
		}
		public IActionResult OnPostMinus(int cartId)
		{
			var cart = _unitOfWork.ShoppingCart.GetFirstOrDefault(u => u.Id == cartId);
			if (cartId == 1)
            {
                var count = _unitOfWork.ShoppingCart.GetAll(u => u.UserId == cart.UserId).ToList().Count - 1;
                _unitOfWork.ShoppingCart.Remove(cart);
                _unitOfWork.Save();
                return RedirectToPage("/Customer/Cart/Index");
            }
            else
            {
				_unitOfWork.ShoppingCart.DecrementCount(cart, 1);
			}
            return RedirectToPage("/Customer/Cart/Index");
		}
		public IActionResult OnPostRemove(int cartId)
		{
			var cart = _unitOfWork.ShoppingCart.GetFirstOrDefault(u => u.Id == cartId);
			var count = _unitOfWork.ShoppingCart.GetAll(u => u.UserId == cart.UserId).ToList().Count - 1;
			_unitOfWork.ShoppingCart.Remove(cart);
            _unitOfWork.Save();
			return RedirectToPage("/Customer/Cart/Index");
		}
	}
}
