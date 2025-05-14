using BLL.DTOs;
using BLL.ManagerService;
using DAL.Models;
using DAL.UoW;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Dipp.Pages
{
    public class ManagerModel : PageModel
    {
        private readonly IManagerService _managerService;

        public ManagerModel(IManagerService managerService)
        {
            _managerService = managerService;
        }

        [BindProperty]
        public List<int> SelectedIds { get; set; } = new();

        public List<Request> Requests { get; set; } = new();

        public async Task OnGetAsync()
        {
            Requests = await _managerService.GetPendingRequestsAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (SelectedIds == null || !SelectedIds.Any())
                return RedirectToPage();

            await _managerService.AddToScheduleAsync(SelectedIds);
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostConfirmAsync(int id)
        {
            await _managerService.ConfirmRequestAsync(id);
            return RedirectToPage();
        }

    }
}

