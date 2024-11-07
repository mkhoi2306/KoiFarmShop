using KoiFarmShop.Repository.Models;
using KoiFarmShop.Service.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KoiFarmShop.WebApp.Pages.Auth
{
    public class LoginModel : PageModel
    {
        private readonly IUserService _userService;

        public Dictionary<string, string> ValidateErrors { get; set; } = new Dictionary<string, string>();

        public LoginModel(IUserService userService)
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
                ValidateErrors["email"] = "Tài khoản không tồn tại";
   
            }else if(user.Password != Request.Form["password"])
            {
                ValidateErrors["password"] = "Password không đúng!!!";

            }
            else
            {
                if(user.Role == "Customer")
                {
                    return RedirectToPage("/Index");
                }else if(user.Role == "Staff")
                {
                    return Page();
                }else if(user.Role == "Admin")
                {
                    return Page();
                }
            }
            return Page();
        }
    }
}
