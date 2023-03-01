using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Restaurant.Data.Repository.IRepository;
using Restaurant.Models;
using Restaurant.Utility;
using Stripe.Checkout;
using System.Security.Claims;

namespace Restaurant.Web.Pages.Customer.Cart
{
	[Authorize]
	[BindProperties]
	public class SummaryModel : PageModel
	{
		public IEnumerable<ShoppingCart> ShoppingCartList { get; set; }
		public MenuItem MenuItem { get; set; }
		public OrderHeader OrderHeader { get; set; }
		private readonly IUnitOfWork _unitOfWork;
		public SummaryModel(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
			OrderHeader = new OrderHeader();
		}
		public void OnGet(int id)
		{
			var claimsIdentity = (ClaimsIdentity)User.Identity;
			var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
			if (claim != null)
			{
				ShoppingCartList = _unitOfWork.ShoppingCart.GetAll(filter: u => u.UserId == claim.Value, includeProperties: "menuItem,menuItem.Category,menuItem.FoodType");
			}
			foreach (var cart in ShoppingCartList)
			{
				OrderHeader.OrderTotal += (cart.menuItem.Price * cart.Count);
			}
			User user = _unitOfWork.User.GetFirstOrDefault(u => u.Id == claim.Value);
			OrderHeader.PickUpName = user.FirstName + " " + user.LastName;
			OrderHeader.PhoneNumber = user.PhoneNumber;
		}
		public IActionResult OnPost()
		{
			var claimsIdentity = (ClaimsIdentity)User.Identity;
			var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
			if (claim != null)
			{
				ShoppingCartList = _unitOfWork.ShoppingCart.GetAll(filter: u => u.UserId == claim.Value, includeProperties: "menuItem,menuItem.Category,menuItem.FoodType");

				foreach (var cart in ShoppingCartList)
				{
					OrderHeader.OrderTotal += (cart.menuItem.Price * cart.Count);
				}
				OrderHeader.Status = SD.StatusPending;
				OrderHeader.OrderDate = System.DateTime.Now;
				OrderHeader.UserId = claim.Value;
				OrderHeader.PickUpTime = Convert.ToDateTime(OrderHeader.PickUpDate.ToShortDateString() + " " + OrderHeader.PickUpTime.ToShortTimeString());
				_unitOfWork.OrderHeader.Add(OrderHeader);
				_unitOfWork.Save();

				foreach (var item in ShoppingCartList)
				{
					OrderDetails order = new()
					{
						MenuItemId = item.MenuItemId,
						OrderId = OrderHeader.Id,
						Name = item.menuItem.Name,
						Price = item.menuItem.Price,
						Count = item.Count
					};
					_unitOfWork.OrderDetails.Add(order);
				}
				int quantity = ShoppingCartList.ToList().Count;
				_unitOfWork.ShoppingCart.RemoveRange(ShoppingCartList);
				_unitOfWork.Save();

				var domain = "http://localhost:4242";
				var options = new SessionCreateOptions
				{
					LineItems = new List<SessionLineItemOptions>
				{
				  new SessionLineItemOptions
				  {
                    // Provide the exact Price ID (for example, pr_1234) of the product you want to sell
                    PriceData = new SessionLineItemPriceDataOptions
					{
						UnitAmount = (long)OrderHeader.OrderTotal*100,
						Currency = "usd",
						ProductData = new SessionLineItemPriceDataProductDataOptions
						{
							Name = "ML Restaurant Food Order"
						},
					},
					Quantity = quantity
				  },
				},
					Mode = "payment",
					SuccessUrl = domain + $"/Customer/Cart/ConfirmOrder?Id={OrderHeader.Id}",
					CancelUrl = domain + "/Customer/Cart/index",
				};
				var service = new SessionService();
				Session session = service.Create(options);

				Response.Headers.Add("Location", session.Url);
				return new StatusCodeResult(303);
			}
			return Page();
		}
	}
}
