using KoiFarmShop.Repository.Models;
using KoiFarmShop.Service.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KoiFarmShop.WebApp.Pages.Auth
{
    public class ForgotPasswordModel : PageModel
    {
        public Dictionary<string, string> ValidateErrors { get; set; } = new Dictionary<string, string>();

        private readonly IUserService _userService;

        public ForgotPasswordModel(IUserService userService)
        {
            this._userService = userService;
        }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            User user = await _userService.GetUserByEmail(Request.Form["email"]);
            if(user == null)
            {
                ValidateErrors["email"] = "Tài khoản với email này không tồn tại";
                return Page();
            }
            else
            {
                return RedirectToPage("/Auth/ResetPassword", new { userId = user.UserId });
            }
        }
    }
}
