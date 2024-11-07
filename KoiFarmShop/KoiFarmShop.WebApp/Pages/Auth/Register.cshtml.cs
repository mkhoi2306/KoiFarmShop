using KoiFarmShop.Repository.Models;
using KoiFarmShop.Service.IServices;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KoiFarmShop.WebApp.Pages.Auth
{
	public class RegisterModel : PageModel
	{
		private readonly IUserService _userService;

		private readonly ICustomerService _customerService;

		public Dictionary<string, string> ValidateErrors { get; set; } = new Dictionary<string, string>();

		[BindProperty]
		public User User { get; set; }

		public RegisterModel(IUserService userService, ICustomerService customerService)
		{
			_userService = userService;
			this._customerService = customerService;
		}
		public void OnGet()
		{
		}

		public async Task<IActionResult> OnPost()
		{
			User.UserId = GetUserId();
			User.UserName = User.FirstName + " " + User.LastName;
			User.Role = "Customer";
			Customer customer = new Customer();
			customer.CustomerId = GetCustomerId();
			customer.UserId = User.UserId;
			customer.Email = User.Email;
			customer.Phone = User.Phone;
			customer.CreatedDate = DateTime.Now;
			customer.CreatedBy = User.UserName;
			customer.IsDeleted = false;
			if (await _userService.CreateAccoutnForGuest(User) && await _customerService.CreateCustomer(customer))
			{
				TempData["SuccessMessage"] = "Đăng ký tài khoản thành công! Vui lòng đăng nhập.";
				return RedirectToPage("/Auth/Login");
			}
			return Page();
		}

		private long GetUserId()
		{
			return _userService.GetNextUserId();
		}

		private long GetCustomerId()
		{
			return _customerService.GetNextCustomerId();
		}
	}
}