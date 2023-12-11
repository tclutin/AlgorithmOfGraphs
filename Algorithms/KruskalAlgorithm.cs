using AlgorithmOfGraphs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmOfGraphs.Algorithms
{
    public class KruskalAlgorithm
    {
        private List<string> _steps = new List<string>();

        public List<string> Kruskal(Graph graph)
        {
            // Sort edges based on their weights in ascending order
            var sortedEdges = graph.Edges.OrderBy(e => e.Weight).ToList();

            // Initialize disjoint set data structure for vertices
            var disjointSet = new DisjointSet(graph.VertexCount);

            // Resulting list of edges in the minimum spanning tree
            var resultEdges = new List<Edge>();

            foreach (var edge in sortedEdges)
            {
                var fromSet = disjointSet.Find(edge.From.Number - 1);
                var toSet = disjointSet.Find(edge.To.Number - 1);

                // Check if including this edge forms a cycle or not
                if (fromSet != toSet)
                {
                    // Include the edge in the result and union the sets
                    resultEdges.Add(edge);
                    disjointSet.Union(fromSet, toSet);
                }
            }

            // Create a list of strings representing the steps (optional)
            CreateStepsList(resultEdges);

            return _steps;
        }

        private void CreateStepsList(List<Edge> resultEdges)
        {
            // Optional: Create a list of strings representing the steps
            _steps.Add("Kruskal's Algorithm Steps:");

            foreach (var edge in resultEdges)
            {
                _steps.Add($"Add edge {edge.From} - {edge.To} with weight {edge.Weight}");
            }
        }
    }

    // Disjoint set data structure
    public class DisjointSet
    {
        private int[] parent;
        private int[] rank;

        public DisjointSet(int size)
        {
            parent = new int[size];
            rank = new int[size];

            for (int i = 0; i < size; i++)
            {
                parent[i] = i;
                rank[i] = 0;
            }
        }

        public int Find(int x)
        {
            if (parent[x] != x)
            {
                parent[x] = Find(parent[x]);
            }

            return parent[x];
        }

        public void Union(int x, int y)
        {
            int rootX = Find(x);
            int rootY = Find(y);

            if (rootX != rootY)
            {
                if (rank[rootX] < rank[rootY])
                {
                    parent[rootX] = rootY;
                }
                else if (rank[rootX] > rank[rootY])
                {
                    parent[rootY] = rootX;
                }
                else
                {
                    parent[rootY] = rootX;
                    rank[rootX]++;
                }
            }
        }
    }
}

