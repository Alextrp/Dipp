using BLL.AdminService;
using BLL.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace Dipp.Pages.Admin
{
    public class IndexModel : PageModel
    {
        private readonly IAdminService _adminService;

        public IndexModel(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [BindProperty(SupportsGet = true)]
        public string? SearchLogin { get; set; }

        public IEnumerable<UserDTO> Users { get; set; } = new List<UserDTO>();

        public async Task OnGetAsync()
        {
            Users = string.IsNullOrWhiteSpace(SearchLogin)
                ? await _adminService.GetAllUsersAsync()
                : await _adminService.SearchUsersByLoginAsync(SearchLogin);
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var user = await _adminService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            await _adminService.DeleteUserAsync(id);
            TempData["SuccessMessage"] = "Пользователь успешно удалён.";
            return RedirectToPage();
        }


    }

}
