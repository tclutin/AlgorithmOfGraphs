using AlgorithmOfGraphs.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmOfGraphs.Algorithms
{
    public class FordFalkersonAlgorithm
    {
        private HashSet<Vertex> visited = new HashSet<Vertex>();
        private List<string> _steps = new List<string>();

        public (int, List<string>) FordFulkerson(Graph graph, Vertex start, Vertex end)
        {
            if (graph == null || start == null || end == null)
            {
                Console.WriteLine("Invalid graph, start, or end vertex.");
                return (0, null);
            }

            Graph residualGraph = InitializeGraph(graph);

            int maxFlow = 0;

            while (Bfs(residualGraph, start, end, out List<Edge> path))
            {
                int minCapacity = GetMinCapacity(path);

                foreach (Edge edge in path)
                {
                    edge.Weight -= minCapacity;
                    _steps.Add($"Decrease residual capacity on edge {edge.From} -> {edge.To} by {minCapacity}");
                }

                maxFlow += minCapacity;
            }

            return (maxFlow, _steps);
        }

        private bool Bfs(Graph graph, Vertex startVertex, Vertex endVertex, out List<Edge> path)
        {
            path = new List<Edge>();
            visited.Clear();
            Queue<Vertex> queue = new Queue<Vertex>();
            Dictionary<Vertex, Edge> parentEdges = new Dictionary<Vertex, Edge>();

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
                            while (parentEdges.ContainsKey(currentVertex))
                            {
                                path.Insert(0, parentEdges[currentVertex]);
                                currentVertex = parentEdges[currentVertex].From;
                            }
                            return true;
                        }
                    }
                }
            }

            return false;
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
                //reverse edge
                //residualGraph.AddEdge(edge.To, edge.From, 0);
            }

            return residualGraph;
        }

        private int GetMinCapacity(List<Edge> path)
        {
            return path.Min(edge => edge.Weight);
        }
    }
}
