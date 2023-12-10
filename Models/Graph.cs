using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmOfGraphs.Models
{
    public class Graph
    {
        public List<Vertex> Vertexes = new List<Vertex>();
        public List<Edge> Edges = new List<Edge>();

        public int VertexCount => Vertexes.Count;
        public int EdgeCount => Edges.Count;

            public void AddVertex(Vertex vertex)
            {
                Vertexes.Add(vertex);
            }

            public void AddEdge(Vertex from, Vertex to, int вес)
            {
                var edge = new Edge(from, to, вес);
                Edges.Add(edge);
            }

        public int[,] GetMatrix()
        {
            var matrix = new int[Vertexes.Count, Vertexes.Count];

            foreach(var edge in Edges)
            {
                var row = edge.From.Number - 1;
                var column = edge.To.Number - 1;

                matrix[row, column] = edge.Weight;
            }

            return matrix;
        }

        public void PrintMatrix()
        {
            Console.Write("   ");
            foreach (var vertex in Vertexes)
            {
                Console.Write($"{vertex} ");
            }
            Console.WriteLine();

            var matrix = GetMatrix();
            for (int i = 0; i < VertexCount; i++)
            {
                Console.Write($"{Vertexes[i]}  ");
                for (int j = 0; j < VertexCount; j++)
                {
                    Console.Write($"{matrix[i, j]} ");
                }
                Console.WriteLine();
            }
        }
    }
}
