using AlgorithmOfGraphs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmOfGraphs.Data
{
    public class DataLoader
    {
        public Graph GetAdjacencyMatrix(string filename)
        {
            var graph = new Graph();

            try
            {
                using (var reader = new StreamReader(filename))
                {
                    var header = reader.ReadLine().Split(' ');
                    if (header == null || header.Length < 2)
                    {
                        Console.WriteLine("Ошибка при загрузке из CSV: Некорректный формат файла.");
                        return null;
                    }

                    foreach (var vertexName in header)
                    {
                        int vertexNumber;
                        if (int.TryParse(vertexName, out vertexNumber))
                        {
                            graph.AddVertex(new Vertex(vertexNumber));
                        }
                        else
                        {
                            Console.WriteLine("Ошибка при загрузке из CSV: Некорректное значение вершины.");
                            return null;
                        }
                    }

                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var values = line.Split(' ');

                        if (values.Length != header.Length)
                        {
                            Console.WriteLine("Ошибка при загрузке из CSV: Некорректное количество полей в строке.");
                            return null;
                        }

                        int row = 0;
                        while (!reader.EndOfStream)
                        {
 
                            if (values.Length != header.Length)
                            {
                                Console.WriteLine("Ошибка при загрузке из CSV: Некорректное количество полей в строке.");
                                return null;
                            }

                            for (int col = 1; col < values.Length + 1; col++)
                            {
                                int weight;
                                if (int.TryParse(values[col], out weight))
                                {
                                    graph.AddEdge(graph.Vertexes[row], graph.Vertexes[col - 1], weight);
                                }
                                else
                                {
                                    Console.WriteLine($"Ошибка при загрузке из CSV: Некорректное значение в строке {line}.");
                                    return null;
                                }
                            }

                            row++;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при загрузке из CSV: {ex.Message}");
            }

            return graph;
        }

        public Graph Get(string filePath)
        {
            var graph = new Graph();

            try
            {
                string[] lines = File.ReadAllLines(filePath);

                foreach (string line in lines)
                {
                    string[] values = line.Split(" ");

                    int vertexNumber = int.Parse(values[1]);

                    Vertex vertex = new Vertex(vertexNumber);

                    if (values[0] == "S")
                    {
                        graph.AddVertex(vertex);

                        Console.WriteLine(line);
                        continue;
                    }

                    for (int i = 1; i < values.Length; i++)
                    {
                        int weight = int.Parse(values[i]);
                        Console.WriteLine(weight);

                        if (weight != 0)
                        {
                            var toVertex = new Vertex(i);
                            graph.AddEdge(vertex, toVertex, weight);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading CSV file: {ex.Message}");
            }
            return graph;
        }
    }
}
