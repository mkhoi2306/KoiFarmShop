using KoiFarmShop.Repository.Models;
using KoiFarmShop.Service.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KoiFarmShop.WebApp.Pages.Admin
{
    public class CreateUserModel : PageModel
    {
        private readonly IUserService _userService;

        public CreateUserModel(IUserService userService)
        {
            _userService = userService;
        }

        [BindProperty]
        public User NewUser { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _userService.CreateUserAsync(NewUser);
            return RedirectToPage("/Admin/ViewAllUser");
        }
    }
}
