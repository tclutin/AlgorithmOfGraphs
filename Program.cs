using AlgorithmOfGraphs.Algorithms;
using AlgorithmOfGraphs.Data;
using AlgorithmOfGraphs.Models;

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
            var v6 = new Vertex(6);
            var v7 = new Vertex(7);

            graph.AddVertex(v1);
            graph.AddVertex(v2);
            graph.AddVertex(v3);
            graph.AddVertex(v4);
            graph.AddVertex(v5);
            graph.AddVertex(v6);
            graph.AddVertex(v7);

            //graph.AddEdge(v1, v2);
            //graph.AddEdge(v1, v3);
            //graph.AddEdge(v3, v4);
            //graph.AddEdge(v2, v5);
            //graph.AddEdge(v2, v6);
            //graph.AddEdge(v6, v5);
            //graph.AddEdge(v5, v6);

            graph.AddEdge(v1, v2, 1);
            graph.AddEdge(v1, v3, 1);
            graph.AddEdge(v3, v4, 1);
            graph.AddEdge(v3, v6, 1);
            graph.AddEdge(v3, v5, 2);
            graph.AddEdge(v2, v3, 2);
            graph.AddEdge(v2, v4, 3);

            graph.AddEdge(v4, v5, 1);
            graph.AddEdge(v5, v6, 3);
            graph.AddEdge(v5, v7, 1);
            graph.AddEdge(v6, v7, 4);


            //var matrix = graph.GetMatrix();
            var граф = new DataLoader().Get("test.csv");
            граф.PrintMatrix();

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
        }
    }
}