using KoiFarmShop.Repository.Models;
using KoiFarmShop.Service.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KoiFarmShop.WebApp.Pages.Admin
{
    public class DeleteUserModel : PageModel
    {
        private readonly IUserService _userService;

        public DeleteUserModel(IUserService userService)
        {
            _userService = userService;
        }

        [BindProperty]
        public User UserToDelete { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            UserToDelete = await _userService.GetUserByIdAsync(id);
            if (UserToDelete == null)
            {
                return RedirectToPage("/Admin/ViewAllUser");
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            await _userService.DeleteUserAsync(id);
            return RedirectToPage("/Admin/ViewAllUser");
        }
    }
}
