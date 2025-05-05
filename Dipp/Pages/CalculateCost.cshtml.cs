using BLL.DTOs;
using BLL.RouteBuilderService;
using DAL.Models;
using DAL.UoW;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;

namespace Dipp.Pages
{
    public class CalculateCostModel : PageModel
    {
        private readonly IRouteBuilderService _routeService;
        private readonly IUnitOfWork _uow;

        public CalculateCostModel(IRouteBuilderService routeService, IUnitOfWork uow)
        {
            _routeService = routeService;
            _uow = uow;
        }

        [BindProperty]
        public CalculateCostViewModel Input { get; set; } = new();

        public IEnumerable<Station> Stations { get; set; } = null;
        public IEnumerable<CargoType> CargoTypes { get; set; } = null;
        public IEnumerable<Cargo> Cargos { get; set; } = null;

        public async Task OnGetAsync()
        {
            Stations = await _uow.Stations.GetAllAsync();
            CargoTypes = await _uow.CargoTypes.GetAllAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            Stations = await _uow.Stations.GetAllAsync();
            CargoTypes = await _uow.CargoTypes.GetAllAsync();

            if (!ModelState.IsValid)
                return Page();

            var action = Request.Form["action"];

            if (action == "calculate")
            {
                var cost = await _routeService.CalculateCostAsync(
                    Input.FromStationId,
                    Input.ToStationId,
                    Input.CargoTypeId,
                    Input.WeightKg);

                Input.CalculatedCost = Math.Round(cost, 2);
                return Page();
            }

            if (action == "submit" && User.Identity?.IsAuthenticated == true)
            {
                // Повторный расчет
                var cost = await _routeService.CalculateCostAsync(
                    Input.FromStationId,
                    Input.ToStationId,
                    Input.CargoTypeId,
                    Input.WeightKg);
                Input.CalculatedCost = Math.Round(cost, 2);

                var user = await _uow.Users.GetByLoginAsync(User.Identity.Name);
                var (segments, distance) = await _routeService.CalculateRouteAsync(Input.FromStationId, Input.ToStationId);
                var fromStation = await _uow.Stations.GetByIdAsync(Input.FromStationId);
                var toStation = await _uow.Stations.GetByIdAsync(Input.ToStationId);
                var route = await _routeService.SaveRouteAsync($"{fromStation.StationName} - {toStation.StationName}", Input.FromStationId, Input.ToStationId, segments, distance);

                var cargo = new CargoDTO
                {
                    CargoName = Input.CargoName,
                    CargoType = (await _uow.CargoTypes.GetByIdAsync(Input.CargoTypeId)).TypeName,
                    WeightKg = Input.WeightKg,
                    AdditionalInfo = Input.AdditionalInfo,
                };

                await _routeService.AddCargoAsync(cargo, Input.CargoTypeId);
                var cargoId = (await _uow.Cargoes.GetAllAsync()).Max(c => c.CargoId);

                var order = new Request
                {
                    RouteId = route.RouteId,
                    CargoId = cargoId,
                    Status = "На одобрении",
                    Cost = (decimal)Input.CalculatedCost.Value,
                    RequestDate = DateTime.UtcNow,
                    UserId = user.UserId
                };

                await _uow.Requests.AddAsync(order);
                await _uow.SaveAsync();
                Input.OrderCreated = true;

                return Page();
            }

            return Page();
        }


        public class CalculateCostViewModel
        {
            [Required]
            public int FromStationId { get; set; }

            [Required]
            public int ToStationId { get; set; }

            [Required]
            public int CargoTypeId { get; set; }

            [Required]
            [Range(1, 100_000)]
            public double WeightKg { get; set; }

            [Required]
            public string CargoName { get; set; } = null!;

            public string? AdditionalInfo { get; set; }

            public double? CalculatedCost { get; set; }

            public bool OrderCreated { get; set; } = false;
        }

    }

}
