using BLL.DispatcherService;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Dipp.Pages
{
    public class DispatcherModel : PageModel
    {
        private readonly IDispatcherService _dispatcherService;

        public DispatcherModel(IDispatcherService dispatcherService)
        {
            _dispatcherService = dispatcherService;
        }

        public List<MovementStatus> Statuses { get; set; } = new();

        public async Task OnGetAsync()
        {
            await _dispatcherService.UpdateMovementStatusesAsync();
            Statuses = await _dispatcherService.GetCurrentStatusesAsync();
        }
    }
}
