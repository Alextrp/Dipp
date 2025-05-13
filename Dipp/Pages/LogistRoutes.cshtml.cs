using BLL.RouteOptimizerService;
using DAL.Models;
using DAL.UoW;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Dipp.Pages
{
    public class LogistRoutesModel : PageModel
    {
        private readonly IRouteOptimizerService _routeOptimizerService;
        private readonly IUnitOfWork _unitOfWork;

        public List<Request> AllRequests { get; set; } = new();
        public OptimizedRouteResult OptimizedRoute { get; set; }

        public LogistRoutesModel(IRouteOptimizerService routeOptimizerService, IUnitOfWork unitOfWork)
        {
            _routeOptimizerService = routeOptimizerService;
            _unitOfWork = unitOfWork;
        }

        public async Task OnGetAsync()
        {
            var allRequests = await _unitOfWork.Requests.GetAllActiveRequests();
            AllRequests = allRequests.Where(a => a.Status != "���� ���������� � ����������").ToList();
        }

        public async Task<IActionResult> OnPostCalculate()
        {
            OptimizedRoute = await _routeOptimizerService.CalculateOptimalRoute();
            var allRequests = await _unitOfWork.Requests.GetAllActiveRequests();
            AllRequests = allRequests.Where(a => a.Status != "���� ���������� � ����������").ToList();
            return Page();
        }


        public async Task<IActionResult> OnPostConfirmAsync()
        {
            // ��������: ��������� ������� � ��, �������� ������ ��� ������������
            var result = _routeOptimizerService.CalculateOptimalRoute();
            foreach (var req in result.Result.RequestsInRoute)
            {
                req.Status = "���� ���������� � ����������";
            }

            _unitOfWork.SaveAsync();
            return RedirectToPage();
        }
    }

}
