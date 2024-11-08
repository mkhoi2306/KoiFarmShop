using KoiFarmShop.Repository.Models;
using KoiFarmShop.Service.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KoiFarmShop.WebApp.Pages.Admin
{
    [Authorize (Roles = "Admin")]
    public class ViewAllUserModel : PageModel
    {
        private readonly IUserService _userService;

        public ViewAllUserModel(IUserService userService)
        {
            _userService = userService;
        }

        public IEnumerable<User> Users { get; private set; }

        public async Task OnGetAsync()
        {
            Users = await _userService.GetAllUsersAsync();
        }
    }
}
