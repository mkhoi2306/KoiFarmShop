using KoiFarmShop.Repository.Models;
using KoiFarmShop.Service.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KoiFarmShop.WebApp.Pages.Admin
{
    public class EditUserModel : PageModel
    {
        private readonly IUserService _userService;

        public EditUserModel(IUserService userService)
        {
            _userService = userService;
        }

        [BindProperty]
        public User EditUser { get; set; }

        public async Task<IActionResult> OnGetAsync(long id)
        {
            EditUser = await _userService.GetUserByIdAsync(id);
            if (EditUser == null)
            {
                return RedirectToPage("/Admin/ViewAllUser");
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _userService.UpdateUserAsync(EditUser);
            return RedirectToPage("/Admin/ViewAllUser");
        }
    }
}
