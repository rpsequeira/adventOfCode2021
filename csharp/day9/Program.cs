using System;
using Utils;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;

namespace adventofcode2020
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Day 9 of AoC");
            IEnumerable<List<int>> data = null;
            Utils.Utils.MeasureActionTime("Load", () =>
            {
                string input = args[0];
                Console.WriteLine($"Loading data...");
                data = LoadData(input);
                Console.WriteLine($"Length -> {data.Count()}");
            });
            Utils.Utils.MeasureActionTime("Part 1", () =>
            {
                var res = DoMagic(data);
                Console.WriteLine($"Result -> {res}");
            });
            Utils.Utils.MeasureActionTime("Part 2", () =>
            {
                var res = DoMagic2(data);
                Console.WriteLine($"Result -> {res}");
            });
            Console.WriteLine("END");
        }

        static IEnumerable<List<int>> LoadData(string filename)
        {
            var raw = Utils.Utils.ReadAllLines(filename);
            foreach (var item in raw)
            {
                yield return Array.ConvertAll(item.ToCharArray(), c => (int)Char.GetNumericValue(c)).ToList();
            }
        }

        static int DoMagic(IEnumerable<List<int>> input)
        {
            var min = new List<int>();
            var matrix = input.ToList();
            for (int i = 0; i < matrix.Count(); i++)
            {
                for (int j = 0; j < matrix[i].Count(); j++)
                {
                    if (isLower(matrix, i, j))
                    {
                        min.Add(matrix[i][j] + 1);
                    }
                }
            }
            return min.Sum();
        }

        static int DoMagic2(IEnumerable<List<int>> input)
        {
            var result = 1;
            var basinSize = new List<int>();
            var matrix = input.ToList();
            for (int i = 0; i < matrix.Count(); i++)
            {
                for (int j = 0; j < matrix[i].Count(); j++)
                {
                    if (isLower(matrix, i, j))
                    {
                        var points = new ConcurrentBag<Point>();
                        var lowerPoint = new Point(j, i);
                        points.Add(lowerPoint);
                        var basin = BasinDetector(matrix, lowerPoint, points).ToList();                       
                        basinSize.Add(basin.Distinct().Count());
                    }
                }
            }
            foreach (var item in basinSize.OrderByDescending(n => n).Take(3))
            {
                result = result * item;
            }
            return result;
        }

        private static bool isLower(List<List<int>> matrix, int y, int x)
        {
            var number = matrix[y][x];
            if (y - 1 >= 0 && matrix[y - 1][x] <= number)
            {
                return false;
            }
            if (y + 1 < matrix.Count() && matrix[y + 1][x] <= number)
            {
                return false;
            }
            if (x - 1 >= 0 && matrix[y][x - 1] <= number)
            {
                return false;
            }
            if (x + 1 < matrix[y].Count() && matrix[y][x + 1] <= number)
            {
                return false;
            }
            return true;
        }

        private static ConcurrentBag<Point> BasinDetector(List<List<int>> matrix, Point p, ConcurrentBag<Point> points)
        {
            if (p.Y - 1 >= 0 && matrix[p.Y - 1][p.X] != 9)
            {
                var nP = new Point(p.X, p.Y - 1);
                if (!points.Contains(nP))
                {
                    points.Add(nP);
                    BasinDetector(matrix, nP, points);
                }
            }
            if (p.Y + 1 < matrix.Count() && matrix[p.Y + 1][p.X] != 9)
            {
                var nP = new Point(p.X, p.Y + 1);
                if (!points.Contains(nP))
                {
                    points.Add(nP);
                    BasinDetector(matrix, nP, points);
                }
            }
            if (p.X - 1 >= 0 && matrix[p.Y][p.X - 1] != 9)
            {
                var nP = new Point(p.X - 1, p.Y);
                if (!points.Contains(nP))
                {
                    points.Add(nP);
                    BasinDetector(matrix, nP, points);
                }
            }
            if (p.X + 1 < matrix[p.Y].Count() && matrix[p.Y][p.X + 1] != 9)
            {
                var nP = new Point(p.X + 1, p.Y);
                if (!points.Contains(nP))
                {
                    points.Add(nP);
                    BasinDetector(matrix, nP, points);
                }
            }
            return points;
        }
    }
}
