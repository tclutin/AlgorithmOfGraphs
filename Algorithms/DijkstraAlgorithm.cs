using AlgorithmOfGraphs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmOfGraphs.Algorithms
{
    //поиск кратчайшего пути
    public class DijkstraAlgorithm
    {
        private Dictionary<Vertex, int> _distances;
        private Dictionary<Vertex, Vertex> _previous;
        private HashSet<Vertex> _visited = new HashSet<Vertex>();
        private List<string> _steps = new List<string>();

        public List<string> FindShortestPath(Graph graph, Vertex startVertex, Vertex endVertex)
        {
            if (graph == null || startVertex == null || endVertex == null)
            {
                Console.WriteLine("Invalid graph or vertices.");
                return null;
            }


            if (graph.Edges.Any(e => e.Weight < 0))
            {
                Console.WriteLine("Graph contains negative weights. Dijkstra's algorithm cannot handle negative weights.");
                return null;
            }

            Initialize(graph, startVertex);
            

            while (_visited.Count < graph.VertexCount)
            {
                Vertex currentVertex = GetClosestVertex();
                _visited.Add(currentVertex);

                foreach (Edge edge in graph.Edges.Where(e => e.From.Equals(currentVertex) && !_visited.Contains(e.To)))
                {
                    int potentialDistance = _distances[currentVertex] + edge.Weight;

                    if (potentialDistance < _distances[edge.To])
                    {
                        _distances[edge.To] = potentialDistance;
                        _previous[edge.To] = currentVertex;
                        _steps.Add($"Step: Visit vertex {edge.To}, Distance: {_distances[edge.To]}");
                    }
                }
            }

            PrintShortestPath(startVertex, endVertex);
            return _steps;
        }

        private void Initialize(Graph graph, Vertex startVertex)
        {
            _distances = new Dictionary<Vertex, int>();
            _previous = new Dictionary<Vertex, Vertex>();


            foreach (Vertex vertex in graph.Vertexes)
            {
                _distances[vertex] = int.MaxValue;
                _previous[vertex] = null;
            }

            _distances[startVertex] = 0;
        }

        private Vertex GetClosestVertex()
        {
            return _distances.Where(pair => !_visited.Contains(pair.Key))
                .OrderBy(pair => pair.Value)
                .FirstOrDefault().Key;
        }

        private void PrintShortestPath(Vertex startVertex, Vertex endVertex)
        {
            if (_distances[endVertex] == int.MaxValue)
            {
                _steps.Add("No path found.");
                return;
            }

            _steps.Add($"Shortest Path from {startVertex} to {endVertex}: Distance {_distances[endVertex]}");
            List<Vertex> path = ReconstructPath(endVertex);
            _steps.Add("Path: " + string.Join(" -> ", path));
        }

        private List<Vertex> ReconstructPath(Vertex endVertex)
        {
            List<Vertex> path = new List<Vertex>();
            Vertex currentVertex = endVertex;

            while (currentVertex != null)
            {
                path.Insert(0, currentVertex);
                currentVertex = _previous[currentVertex];
            }

            return path;
        }
    }
}
