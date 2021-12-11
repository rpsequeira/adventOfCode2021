using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using Utils;

namespace adventofcode2020
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Day 11 of AoC");
            IEnumerable<List<PointInfo<int>>> data = null;
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

        static IEnumerable<List<PointInfo<int>>> LoadData(string filename)
        {
            var raw = Utils.Utils.ReadAllLines(filename);
            return Utils.Utils.GetMatrix(raw);
        }

        static long DoMagic(IEnumerable<List<PointInfo<int>>> input)
        {
            var matrix = input.ToList();
            ConcurrentBag<Point> flashes = new ConcurrentBag<Point>();
            for (int i = 0; i < 100; i++)
            {

                for (int y = 0; y < matrix.Count(); y++)
                {
                    for (int x = 0; x < matrix[y].Count(); x++)
                    {
                        if (!matrix[y][x].flag)
                        {
                            matrix[y][x].value++;
                            if (matrix[y][x].value > 9)
                            {
                                Flash(matrix, x, y, flashes);
                            }
                        }
                    }
                }
                Utils.Utils.ResetMatrix(matrix);
            }

            return flashes.Count();
        }

        static long DoMagic2(IEnumerable<List<PointInfo<int>>> input)
        {
            var matrix = input.ToList();
            var step = -1;
            for (int i = 0; i < 1000; i++)
            {
                ConcurrentBag<Point> flashes = new ConcurrentBag<Point>();
                for (int y = 0; y < matrix.Count(); y++)
                {
                    for (int x = 0; x < matrix[y].Count(); x++)
                    {
                        if (!matrix[y][x].flag)
                        {
                            matrix[y][x].value++;
                            if (matrix[y][x].value > 9)
                            {
                                Flash(matrix, x, y, flashes);
                            }
                        }
                    }
                }
                if(flashes.Count() == 100){
                    step = i+1;
                    break;
                }
                Utils.Utils.ResetMatrix(matrix);
            }            
            return step;
        }

        private static void Flash(List<List<PointInfo<int>>> matrix, int x, int y, ConcurrentBag<Point> points)
        {
            matrix[y][x].flag = true;
            matrix[y][x].value = 0;
            points.Add(new Point(x, y));
            if (y - 1 >= 0 && !matrix[y - 1][x].flag)
            {
                matrix[y - 1][x].value++;
                if (matrix[y - 1][x].value > 9)
                {
                    Flash(matrix, x, y - 1, points);
                }
            }
            if (y + 1 < matrix.Count() && !matrix[y + 1][x].flag)
            {
                matrix[y + 1][x].value++;
                if (matrix[y + 1][x].value > 9)
                {
                    Flash(matrix, x, y + 1, points);
                }
            }
            if (x - 1 >= 0 && !matrix[y][x - 1].flag)
            {
                matrix[y][x - 1].value++;
                if (matrix[y][x - 1].value > 9)
                {
                    Flash(matrix, x - 1, y, points);
                }
            }
            if (x + 1 < matrix[y].Count() && !matrix[y][x + 1].flag)
            {
                matrix[y][x + 1].value++;
                if (matrix[y][x + 1].value > 9)
                {
                    Flash(matrix, x + 1, y, points);
                }
            }
            if (y - 1 >= 0 && x - 1 >= 0 && !matrix[y - 1][x - 1].flag)
            {
                matrix[y - 1][x - 1].value++;
                if (matrix[y - 1][x - 1].value > 9)
                {
                    Flash(matrix, x - 1, y - 1, points);
                }
            }
            if (y + 1 < matrix.Count() && x - 1 >= 0 && !matrix[y + 1][x - 1].flag)
            {
                matrix[y + 1][x - 1].value++;
                if (matrix[y + 1][x - 1].value > 9)
                {
                    Flash(matrix, x - 1, y + 1, points);
                }
            }
            if (y - 1 >= 0 && x + 1 < matrix[y].Count() && !matrix[y - 1][x + 1].flag)
            {
                matrix[y - 1][x + 1].value++;
                if (matrix[y - 1][x + 1].value > 9)
                {
                    Flash(matrix, x + 1, y - 1, points);
                }
            }
            if (y + 1 < matrix.Count() && x + 1 < matrix[y].Count() && !matrix[y + 1][x + 1].flag)
            {
                matrix[y + 1][x + 1].value++;
                if (matrix[y + 1][x + 1].value > 9)
                {
                    Flash(matrix, x + 1, y + 1, points);
                }
            }
        }
    }
}
