using KoiFarmShop.Repository.Models;
using KoiFarmShop.Service.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KoiFarmShop.WebApp.Pages.Auth
{
    public class ResetPasswordModel : PageModel
    {
        private readonly IUserService _userService;

        public User User { get; set; }

        public ResetPasswordModel(IUserService userService)
        {
            this._userService = userService;
        }
        public async Task OnGet(long userId)
        {
            User = await _userService.GetUserById(userId);
        }
    }
}
