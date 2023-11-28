using AlgorithmOfGraphs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmOfGraphs.Algorithms
{
    //обход в глубину
    public class DfsAlgorithm
    {
        private List<string> _steps = new List<string>();

        public List<string> Dfs(Graph graph, Vertex startVertex)
        {
            if (graph == null || startVertex == null)
            {
                Console.WriteLine("Invalid graph or start vertex.");
                return null;
            }

            HashSet<Vertex> visited = new HashSet<Vertex>();

            DFSUtil(startVertex, visited, graph);

            _steps.Add("DFS traversal completed.");
            return _steps;
        }

        private void DFSUtil(Vertex currentVertex, HashSet<Vertex> visited, Graph graph)
        {
            visited.Add(currentVertex);
            _steps.Add($"Visit vertex {currentVertex}");

            foreach (Edge edge in graph.Edges)
            {
                if (edge.From.Equals(currentVertex) && !visited.Contains(edge.To))
                {
                    Vertex neighbor = edge.To;
                    DFSUtil(neighbor, visited, graph);
                }
            }
        }
    }
}
