using BLL.DTOs;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.RouteBuilderService
{
    public interface IRouteBuilderService
    {
        Task<(List<Segment> pathSegments, double totalDistance)> CalculateRouteAsync(int startStationId, int endStationId);
        Task<double> CalculateCostAsync(int fromStationId, int toStationId, int cargoTypeId, double weightKg);
        Task<Route> SaveRouteAsync(string routeName, int startStationId, int endStationId, List<Segment> pathSegments, double totalDistance);
        Task AddCargoAsync(CargoDTO dto, int cargoTypeId);


    }
}
