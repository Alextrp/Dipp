using AutoMapper;
using BLL.DTOs;
using DAL.Models;
using DAL.UoW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.RouteBuilderService
{
    public class RouteBuilderService: IRouteBuilderService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public RouteBuilderService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _uow = unitOfWork;
            _mapper = mapper;
        }

        public async Task<(List<Segment> pathSegments, double totalDistance)> CalculateRouteAsync(int startStationId, int endStationId)
        {
            var segments = await _uow.Segments.GetAllWithStationsAsync();
            var graph = BuildGraph(segments);
            var path = FindShortestPath(graph, startStationId, endStationId);

            if (path == null || path.Count < 2)
                throw new InvalidOperationException("Путь не найден.");

            var pathSegments = new List<Segment>();

            for (int i = 0; i < path.Count - 1; i++)
            {
                int from = path[i];
                int to = path[i + 1];

                // Ищем сегмент в любом направлении
                var segment = segments.FirstOrDefault(s =>
                    (s.StationFromId == from && s.StationToId == to) ||
                    (s.StationFromId == to && s.StationToId == from));

                if (segment == null)
                    throw new Exception($"Сегмент не найден: {from} ↔ {to}");

                pathSegments.Add(segment);
            }

            var totalDistance = (double)pathSegments.Sum(s => s.DistanceKm);
            return (pathSegments, totalDistance);
        }


        public async Task<double> CalculateCostAsync(int fromStationId, int toStationId, int cargoTypeId, double weightKg)
        {
            var distance = await CalculateRouteAsync(fromStationId, toStationId);
            var baseRate = 0.75; // можно вынести в конфигурацию

            return baseRate * distance.totalDistance * weightKg/1000;
        }

        public async Task<Route> SaveRouteAsync(string routeName, int startStationId, int endStationId, List<Segment> pathSegments, double totalDistance)
        {
            var route = new Route
            {
                RouteName = routeName,
                StartStationId = startStationId,
                EndStationId = endStationId,
                TotalDistance = (decimal?)totalDistance,
                EstimatedTime = pathSegments.Count * 10
            };

            await _uow.Routes.AddAsync(route);
            await _uow.SaveAsync(); // Сохраняем, чтобы получить RouteId

            int order = 0;
            foreach (var segment in pathSegments)
            {
                var routeSegment = new RouteSegment
                {
                    RouteId = route.RouteId,
                    SegmentId = segment.SegmentId,
                    OrderNumber = order++
                };

                await _uow.RouteSegments.AddAsync(routeSegment);
            }

            await _uow.SaveAsync();
            return route;
        }

        public async Task AddCargoAsync(CargoDTO dto, int cargoTypeId)
        {
            var cargo = _mapper.Map<Cargo>(dto);
            cargo.CargoTypeId = cargoTypeId;

            await _uow.Cargoes.AddAsync(cargo);
            await _uow.SaveAsync();
        }

        // Построение направленного графа
        private Dictionary<int, List<int>> BuildGraph(IEnumerable<Segment> segments)
        {
            var graph = new Dictionary<int, List<int>>();

            foreach (var seg in segments)
            {
                // Прямое направление
                if (!graph.ContainsKey(seg.StationFromId))
                    graph[seg.StationFromId] = new List<int>();

                graph[seg.StationFromId].Add(seg.StationToId);

                // Обратное направление (делает граф двусторонним)
                if (!graph.ContainsKey(seg.StationToId))
                    graph[seg.StationToId] = new List<int>();

                graph[seg.StationToId].Add(seg.StationFromId);
            }

            return graph;
        }


        // BFS: кратчайший путь по узлам
        private List<int> FindShortestPath(Dictionary<int, List<int>> graph, int start, int end)
        {
            var queue = new Queue<List<int>>();
            var visited = new HashSet<int> { start }; // сразу отмечаем стартовую вершину
            queue.Enqueue(new List<int> { start });

            while (queue.Count > 0)
            {
                var path = queue.Dequeue();
                int current = path.Last();

                if (current == end)
                    return path;

                if (!graph.ContainsKey(current))
                    continue;

                foreach (var neighbor in graph[current])
                {
                    if (!visited.Contains(neighbor))
                    {
                        visited.Add(neighbor); // только теперь, когда реально собираемся обрабатывать
                        var newPath = new List<int>(path) { neighbor };
                        queue.Enqueue(newPath);
                    }
                }
            }

            return null!;
        }

    }

}
