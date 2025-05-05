using BLL.AdminService;
using BLL.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Dipp.Pages.Admin
{
    public class CreateModel : PageModel
    {
        private readonly IAdminService _adminService;

        public CreateModel(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [BindProperty]
        public UserDTO NewUser { get; set; } = new();
        public string plainPassword { get; set; }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            await _adminService.AddUserAsync(NewUser, plainPassword);
            return RedirectToPage("./Index");
        }
    }

}
