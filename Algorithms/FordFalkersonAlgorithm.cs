using AlgorithmOfGraphs.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmOfGraphs.Algorithms
{
    public class FordFalkersonAlgorithm
    {
        private HashSet<Vertex> visited = new HashSet<Vertex>();

     
        private List<string> _steps = new List<string>();
        
        //public (int, List<string>) FordFulkerson(Graph graph, Vertex start, Vertex end)
        //{
        //    if (graph == null || start == null || end == null)
        //    {
        //        Console.WriteLine("Invalid graph, start, or end vertex.");
        //        return (0, null);
        //    }

        //    _steps.Add("Инициализируем граф остаточного потока.");
        //    Graph residualGraph = InitializeGraph(graph);


        //    int maxFlow = 0;

        //    while (Bfs1(residualGraph, start, end))
        //    {

        //        foreach (Edge edge in path)
        //        {
        //            Console.WriteLine($"{edge.From} -> {edge.To}");
        //        }

        //        _steps.Add("Начинаем поиск минимального потока у найденного пути.");
        //        int minCapacity = GetMinCapacity(path);
        //        _steps.Add($"Минимальный поток определён: {minCapacity}");

        //        foreach (Edge edge in path)
        //        {
                    
        //            edge.Weight -= minCapacity;
        //            //_steps.Add($"Уменьшаем остаточную емкость на ребре {edge.From} -> {edge.To} на {minCapacity}");
        //        }
        //        maxFlow += minCapacity;
        //        path.Clear();
        //    }

        //    return (maxFlow, _steps);
        //}

        public (int, List<string>) FordFulkerson(Graph graph, Vertex start, Vertex end)
        {
            if (graph == null || start == null || end == null)
            {
                Console.WriteLine("Invalid graph, start, or end vertex.");
                return (0, null);
            }

            _steps.Add("Инициализируем граф остаточного потока.");
            Graph residualGraph = InitializeGraph(graph);

            int maxFlow = 0;

            while (true)
            {
                List<Edge> path = Bfs(residualGraph, start, end);
                if (path.Count == 0)
                {
                    break;
                }

                _steps.Add("Начинаем поиск минимального потока у найденного пути.");
                int minCapacity = GetMinCapacity(path);
                _steps.Add($"Минимальный поток определён: {minCapacity}");

                foreach (Edge edge in path)
                {

                    edge.Weight -= minCapacity;
                    _steps.Add($"Уменьшаем остаточную емкость на ребре {edge.From} -> {edge.To} на {minCapacity}");
                }
                _steps.Add($"Максимальный поток увеличивается: {maxFlow} + {minCapacity} = {maxFlow += minCapacity}");
            }

            return (maxFlow, _steps);
        }

        private List<Edge> Bfs(Graph graph, Vertex startVertex, Vertex endVertex)
        {
            visited.Clear();
            Queue<Vertex> queue = new Queue<Vertex>();
            Dictionary<Vertex, Edge> parentEdges = new Dictionary<Vertex, Edge>();
            List<Edge> path = new List<Edge>();

            queue.Enqueue(startVertex);
            visited.Add(startVertex);


            while (queue.Count > 0)
            {
                Vertex currentVertex = queue.Dequeue();

                foreach (Edge edge in graph.Edges)
                {
                    if (edge.From.Equals(currentVertex) && edge.Weight > 0 && !visited.Contains(edge.To))
                    {
                        queue.Enqueue(edge.To);
                        visited.Add(edge.To);
                        parentEdges[edge.To] = edge;

                        if (edge.To.Equals(endVertex))
                        {

                            while (parentEdges.ContainsKey(endVertex))
                            {
                                path.Add(parentEdges[endVertex]);
                                endVertex = parentEdges[endVertex].From;
                            }
                            return path;
                        }
                    }
                }

            }

            return path;
        }

        private Graph InitializeGraph(Graph graph)
        {
            Graph residualGraph = new Graph();

            foreach (Vertex vertex in graph.Vertexes)
            {
                residualGraph.AddVertex(vertex);
            }

            foreach (Edge edge in graph.Edges)
            {
                residualGraph.AddEdge(edge.From, edge.To, edge.Weight);
            }

            return residualGraph;
        }

        private int GetMinCapacity(List<Edge> path)
        {
            return path.Min(edge => edge.Weight);
        }
    }
}
