using BLL.AccountService;
using BLL.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Dipp.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly IAccountService _accountService;

        public RegisterModel(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        [TempData]
        public string? ErrorMessage { get; set; }

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            var dto = new RegisterDTO
            {
                FullName = Input.FullName,
                Login = Input.Username,
                Email = Input.Email,
                Phone = Input.PhoneNumber,
                Password = Input.Password
            };

            var success = await _accountService.RegisterAsync(dto);

            if (!success)
            {
                ModelState.AddModelError(string.Empty, "������������ � ����� ������� ��� email ��� ����������.");
                return Page();
            }

            return RedirectToPage("/Account/Login");
        }

        public class InputModel
        {
            [Required(ErrorMessage = "������� ���")]
            public string FullName { get; set; }

            [Required(ErrorMessage = "������� �����")]
            public string Username { get; set; }

            [Required(ErrorMessage = "������� email")]
            [EmailAddress(ErrorMessage = "������������ email")]
            public string Email { get; set; }

            [Required(ErrorMessage = "������� ����� ��������")]
            [Phone(ErrorMessage = "������������ ����� ��������")]
            public string PhoneNumber { get; set; }

            [Required(ErrorMessage = "������� ������")]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Required(ErrorMessage = "����������� ������")]
            [DataType(DataType.Password)]
            [Compare("Password", ErrorMessage = "������ �� ���������")]
            public string ConfirmPassword { get; set; }
        }
    }
}
