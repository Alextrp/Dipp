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
                ModelState.AddModelError(string.Empty, "Пользователь с таким логином или email уже существует.");
                return Page();
            }

            return RedirectToPage("/Account/Login");
        }

        public class InputModel
        {
            [Required(ErrorMessage = "Введите ФИО")]
            public string FullName { get; set; }

            [Required(ErrorMessage = "Введите логин")]
            public string Username { get; set; }

            [Required(ErrorMessage = "Введите email")]
            [EmailAddress(ErrorMessage = "Некорректный email")]
            public string Email { get; set; }

            [Required(ErrorMessage = "Введите номер телефона")]
            [Phone(ErrorMessage = "Некорректный номер телефона")]
            public string PhoneNumber { get; set; }

            [Required(ErrorMessage = "Введите пароль")]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Required(ErrorMessage = "Подтвердите пароль")]
            [DataType(DataType.Password)]
            [Compare("Password", ErrorMessage = "Пароли не совпадают")]
            public string ConfirmPassword { get; set; }
        }
    }
}
