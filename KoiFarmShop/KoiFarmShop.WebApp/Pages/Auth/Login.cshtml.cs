using KoiFarmShop.Repository.Models;
using KoiFarmShop.Service.IServices;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using System.Security.Claims;

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

            if (user == null)
            {
                ValidateErrors["email"] = "Tài khoản không tồn tại";
            }
            else if (user.Password != Request.Form["password"])
            {
                ValidateErrors["password"] = "Password không đúng!!!";
            }
            else
            {
                var claims = new List<Claim>
                {
                    new Claim("userId", user.UserId.ToString()),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, user.Role)
                };

            
                if (user.Role == "Customer")
                {
                    Customer customer = await _userService.GetCustomerByUser(user.UserId);
                    if (customer != null)
                    {
                        claims.Add(new Claim("customerId", customer.CustomerId.ToString()));
                    }
                    else
                    {
                        return Page();
                    }
                }

                // Khởi tạo ClaimsIdentity và ClaimsPrincipal sau khi thêm tất cả claims
                var claimsIdentity = new ClaimsIdentity(claims, "Login");
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

                if (user.Role == "Customer")
                {
                    return RedirectToPage("/Index");
                }
                else if (user.Role == "Staff")
                {
                    return RedirectToPage("/Staff/Index");
                }
                else if (user.Role == "Admin")
                {
                    return RedirectToPage("/Staff/ViewAllUser");
                }
            }

            return Page();
        }
    }
}