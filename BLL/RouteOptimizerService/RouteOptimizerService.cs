using BLL.RouteBuilderService;
using DAL.UoW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.RouteOptimizerService
{
    public class RouteOptimizerService: IRouteOptimizerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRouteBuilderService _routeBuilderService;

        public RouteOptimizerService(IUnitOfWork unitOfWork, IRouteBuilderService routeBuilderService)
        {
            _unitOfWork = unitOfWork;
            _routeBuilderService = routeBuilderService;
        }

        public async Task<OptimizedRouteResult> CalculateOptimalRoute()
        {
            var allRequests = await _unitOfWork.Requests.GetAllActiveRequests();
            var req = allRequests.Where(a => a.Status != "Ждет добавления в расписание" && a.Status != "В расписании").ToList();
            var stations = await _unitOfWork.Stations.GetAllAsync(); 
            var segments = await _unitOfWork.Segments.GetAllWithStationsAsync(); 
            var adjacencyMatrix = _routeBuilderService.BuildGraph(segments); 

            var bestRoute = RouteOptimizer.Calculate(req, adjacencyMatrix, stations.ToList(), segments.ToList());
            return bestRoute;
        }

    }

}
