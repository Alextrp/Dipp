using BLL.AccountService;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace Dipp.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly IAccountService _accountService;

        public LoginModel(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        [TempData]
        public string? ErrorMessage { get; set; }

        public void OnGet()
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            var user = await _accountService.AuthenticateAsync(Input.Username, Input.Password);
            
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Неверный логин или пароль.");
                return Page();
            }

            // Создание ClaimsIdentity
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Name, user.Login),
                new Claim(ClaimTypes.Role, user.Role?.RoleName ?? "Клиент")
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

            if (user.Role?.RoleName == "Администратор")
            {
                return RedirectToPage("/Admin/Index");
            }

            return RedirectToPage("/Index");
        }

        public class InputModel
        {
            [Required(ErrorMessage = "Введите логин")]
            public string Username { get; set; }

            [Required(ErrorMessage = "Введите пароль")]
            [DataType(DataType.Password)]
            public string Password { get; set; }
        }
    }
}
