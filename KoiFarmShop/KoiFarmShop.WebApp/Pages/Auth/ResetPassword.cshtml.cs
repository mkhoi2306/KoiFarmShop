using KoiFarmShop.Repository.Models;
using KoiFarmShop.Service.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KoiFarmShop.WebApp.Pages.Auth
{
    public class ResetPasswordModel : PageModel
    {
        private readonly IUserService _userService;

		public string Message { get; set; }

		public User User { get; set; } = new User();

        public ResetPasswordModel(IUserService userService)
        {
            this._userService = userService;
        }
        public async Task OnGet(long userId)
        {
            User = await _userService.GetUserById(userId);
        }

        public async Task<IActionResult> OnPost()
        {
            if (Request.Form["NewPassword"] != Request.Form["ConfirmPassword"])
            {
                Message = "Mật khẩu mới và confirm password không trùng khớp!!!";
                return Page();
            }
            User = await _userService.GetUserById(long.Parse(Request.Form["userId"]));
            User.Password = Request.Form["NewPassword"];
            if (await _userService.ResetPassword(User))
            {
				TempData["SuccessMessage"] = "Mật khẩu đã được thay đổi thành công!";
				return RedirectToPage("/Auth/Login");
            }
            else
            {
                Message = "Reset password fail";
                return Page();
            }
        }
    }
}
