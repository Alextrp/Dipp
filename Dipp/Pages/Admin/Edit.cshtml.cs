using BLL.AdminService;
using BLL.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Dipp.Pages.Admin
{
    public class EditModel : PageModel
    {
        private readonly IAdminService _adminService;

        public EditModel(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [BindProperty]
        public UserDTO UserToUpdate { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var user = await _adminService.GetUserByIdAsync(id);
            if (user == null)
                return NotFound();

            UserToUpdate = user;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            await _adminService.UpdateUserAsync(UserToUpdate);
            return RedirectToPage("./Index");
        }

        public async Task<IActionResult> OnPostChangePasswordAsync()
        {
            var userIdStr = Request.Form["userId"];
            var newPassword = Request.Form["newPassword"];

            if (!int.TryParse(userIdStr, out var userId) || string.IsNullOrWhiteSpace(newPassword))
            {
                ModelState.AddModelError(string.Empty, "Некорректные данные для смены пароля.");
                return await OnGetAsync(userId);
            }

            await _adminService.ChangeUserPasswordAsync(userId, newPassword);

            TempData["PasswordChanged"] = true;
            return RedirectToPage(new { id = userId });
        }
    }

}
