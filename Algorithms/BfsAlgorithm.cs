using AlgorithmOfGraphs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmOfGraphs.Algorithms
{
    //обход в ширину
    public class BfsAlgorithm
    {
        private List<string> _steps = new List<string>();
            
        public List<string> Bfs(Graph graph, Vertex startVertex)
        {
            if (graph == null || startVertex == null)
            {
                Console.WriteLine("Invalid graph or start vertex.");
                return null;
            }

            HashSet<Vertex> visited = new HashSet<Vertex>();
            Queue<Vertex> queue = new Queue<Vertex>();

            queue.Enqueue(startVertex);
            visited.Add(startVertex);

            while (queue.Count > 0)
            {
                Vertex currentVertex = queue.Dequeue();

                foreach (Edge edge in graph.Edges)
                {
                    if (edge.From.Equals(currentVertex) && !visited.Contains(edge.To))
                    {
                        queue.Enqueue(edge.To);
                        visited.Add(edge.To);
                        _steps.Add($"Visit vertex {edge.To}");
                    }
                }
            }

            return _steps;
        }
    }
}
