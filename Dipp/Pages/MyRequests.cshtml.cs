using BLL.DTOs;
using BLL.RequestService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Dipp.Pages
{
    public class MyRequestsModel : PageModel
    {
        private readonly IRequestService _requestService;

        public MyRequestsModel(IRequestService requestService)
        {
            _requestService = requestService;
        }

        public List<RequestDTO> Requests { get; set; } = new();

        public async Task<IActionResult> OnGetAsync()
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToPage("/Account/Login");

            Requests = await _requestService.GetUserRequestsAsync(User.Identity.Name);
            return Page();
        }
    }

}
