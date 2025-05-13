using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL.RouteOptimizerService
{
    public class RouteOptimizer
    {
        public static OptimizedRouteResult Calculate(
            List<Request> requests,
            Dictionary<int, List<int>> graph,
            List<Station> stations,
            List<Segment> segments)
        {
            ;
            ;
            var bestRoute = FindShortestPath(graph, requests.MaxBy(r => r.Route.TotalDistance).Route.StartStationId, requests.MaxBy(r => r.Route.TotalDistance).Route.EndStationId);
            var matchedRequests = MatchRequestsToPath(bestRoute, requests, segments);
            var totalDistance = CalculateDistance(bestRoute, segments);

            return new OptimizedRouteResult
            {
                RouteStations = bestRoute.Select(id => stations.First(s => s.StationId == id)).ToList(),
                RequestsInRoute = matchedRequests,
                TotalDistance = totalDistance
            };
        }

        //private static List<int> FindLongestPath(Dictionary<int, List<int>> graph)
        //{
        //    var longestPath = new List<int>();

        //    foreach (var start in graph.Keys)
        //    {
        //        var visited = new HashSet<int>();
        //        var currentPath = new List<int>();
        //        DFSLongest(start, graph, visited, currentPath, ref longestPath);
        //    }

        //    return longestPath;
        //}

        private static List<int> FindShortestPath(Dictionary<int, List<int>> graph, int start, int end)
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

        private static List<Request> MatchRequestsToPath(
            List<int> path,
            List<Request> requests,
            List<Segment> segments)
        {
            var result = new List<Request>();

            foreach (var req in requests)
            {
                int startIndex = path.IndexOf(req.Route.StartStationId);
                int endIndex = path.IndexOf(req.Route.EndStationId);

                if (startIndex != -1 && endIndex != -1 && startIndex < endIndex)
                {
                    bool valid = true;
                    for (int i = startIndex; i < endIndex; i++)
                    {
                        int from = path[i];
                        int to = path[i + 1];

                        if (!segments.Any(s =>
                            (s.StationFromId == from && s.StationToId == to) ||
                            (s.StationFromId == to && s.StationToId == from)))
                        {
                            valid = false;
                            break;
                        }
                    }

                    if (valid)
                        result.Add(req);
                }
            }

            return result;
        }

        private static double CalculateDistance(List<int> path, List<Segment> segments)
        {
            if (path == null || path.Count < 2)
                throw new InvalidOperationException("Путь не найден.");

            double totalDistance = 0;

            for (int i = 0; i < path.Count - 1; i++)
            {
                int from = path[i];
                int to = path[i + 1];

                var segment = segments.FirstOrDefault(s =>
                    (s.StationFromId == from && s.StationToId == to) ||
                    (s.StationFromId == to && s.StationToId == from));

                if (segment == null)
                    throw new Exception($"Сегмент не найден: {from} ↔ {to}");

                totalDistance += (double)segment.DistanceKm;
            }

            return totalDistance;
        }
    }
}
