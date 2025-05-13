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
        private readonly IUnitOfWork _unitOfWork;
        public ManagerModel(IManagerService managerService, IUnitOfWork unitOfWork)
        {
            _managerService = managerService;
            _unitOfWork = unitOfWork;
        }

        public List<Request> Requests { get; set; } = new();

        [BindProperty]
        public List<int> SelectedIds { get; set; } = new();

        public async Task OnGetAsync()
        {
            var request = await _unitOfWork.Requests.GetAllAsync();
            Requests = request.Where(r => r.Status == "Ждет добавления в расписание").ToList();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (SelectedIds.Any())
                await _managerService.AddOrdersToScheduleAsync(SelectedIds);
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostConfirmAsync(int id)
        {
            await _managerService.ConfirmOrderAsync(id);
            return RedirectToPage();
        }
    }

}

