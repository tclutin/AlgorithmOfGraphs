using AlgorithmOfGraphs.Algorithms;
using AlgorithmOfGraphs.Models;
using Microsoft.VisualBasic;

namespace AlgorithmOfGraphs
{
    public class Program
    {
        public static void Main(string[] args)
        {
         
            var graph = new Graph();

            var v1 = new Vertex(1);
            var v2 = new Vertex(2);
            var v3 = new Vertex(3);
            var v4 = new Vertex(4);
            var v5 = new Vertex(5);

            graph.AddVertex(v1);
            graph.AddVertex(v2);
            graph.AddVertex(v3);
            graph.AddVertex(v4);
            graph.AddVertex(v5);



            graph.AddEdge(v1, v2, 30);
            graph.AddEdge(v1, v4, 20);
            graph.AddEdge(v1, v3, 40);

            graph.AddEdge(v2, v3, 50);
            graph.AddEdge(v2, v5, 40);

            graph.AddEdge(v3, v4, 20);
            graph.AddEdge(v3, v5, 30);

            graph.AddEdge(v4, v5, 30);

            //graph.AddEdge(v1, v2, 1);
            //graph.AddEdge(v1, v3, 3);
            //graph.AddEdge(v1, v4, 6);

            //graph.AddEdge(v3, v5, 8);
            //graph.AddEdge(v4, v5, 4);
            //graph.AddEdge(v2, v5, 12);
            //graph.AddEdge(v2, v4, 4);



            //var matrix = graph.GetMatrix();
            graph.PrintMatrix();

            ///////////////////////////////////////////////////////////////////

            List<string> x = new BfsAlgorithm().Bfs(graph, v1);
            foreach (var item in x)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine("№---");
            List<string> y = new DfsAlgorithm().Dfs(graph, v1);
            foreach (var item in y)
            {
                Console.WriteLine(item);
            }


            Console.WriteLine("№---");
            List<string> r = new DijkstraAlgorithm().FindShortestPath(graph, v1, v5);
            foreach (var item in r)
            {
                Console.WriteLine(item);
            }


            Console.WriteLine("№---");
            (int max, List<string> steps) = new FordFalkersonAlgorithm().FordFulkerson(graph, v1, v5);
            foreach (var item in steps)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine(max);


            Console.WriteLine("№---");
            List<string> steps1 = new KruskalAlgorithm().Kruskal(graph);
            foreach (var item in steps1)
            {
                Console.WriteLine(item);
            }
        }
    }
}