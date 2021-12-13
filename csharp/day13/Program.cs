using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using Utils;
using System.Text.RegularExpressions;

namespace adventofcode2021
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Day 13 of AoC");
            IEnumerable<Point> data = null;
            IEnumerable<Point> data2 = null;

            Utils.Utils.MeasureActionTime("Load", () =>
            {
                string input = args[0];
                Console.WriteLine($"Loading data...");
                data = LoadData(input);
                data2 = LoadData2(input);
                Console.WriteLine($"Length -> {data.Count()}");
            });
            Utils.Utils.MeasureActionTime("Part 1", () =>
            {
                var res = DoMagic(data, data2);
                Console.WriteLine($"Result -> {res}");
            });
            Utils.Utils.MeasureActionTime("Part 2", () =>
            {
                var res = DoMagic2(data, data2);
                Console.WriteLine($"Result -> {res}");
            });
            Console.WriteLine("END");
        }

        static IEnumerable<Point> LoadData(string filename)
        {
            var raw = Utils.Utils.ReadAllLines(filename);
            foreach (var item in raw)
            {
                if (item == string.Empty)
                {
                    break;
                }
                yield return new Point(Int32.Parse(item.Split(',')[0]), Int32.Parse(item.Split(',')[1]));
            }
        }

        static IEnumerable<Point> LoadData2(string filename)
        {
            var raw = Utils.Utils.ReadAllLines(filename);
            var secondPart = false;
            foreach (var item in raw)
            {
                if (!secondPart)
                {
                    if (item == string.Empty)
                    {
                        secondPart = true;
                    }
                    continue;
                }

                var parts = Regex.Matches(item, "fold along (x|y)=(\\d+)");
                if (parts[0].Groups[1].Value == "x")
                {
                    yield return new Point(Int32.Parse(parts[0].Groups[2].Value), 0);
                }
                else
                {
                    yield return new Point(0, Int32.Parse(parts[0].Groups[2].Value));
                }

            }
        }

        static long DoMagic(IEnumerable<Point> input, IEnumerable<Point> folds)
        {
            var maxX = input.Select(p => p.X).Max() + 1;
            var maxY = input.Select(p => p.Y).Max() + 1;
            var map = new bool[maxY, maxX];

            foreach (var item in input)
            {
                map[item.Y, item.X] = true;
            }

            foreach (var item in folds)
            {
                if (item.X != 0)
                {
                    maxX = item.X;
                    for (int i = 0; i < maxY; i++)
                    {
                        for (int j = 0; j < maxX; j++)
                        {
                            map[i, j] = map[i, j] ? map[i, j] : map[i, (item.X + (item.X - j))];
                        }
                    }

                }
                if (item.Y != 0)
                {
                    maxY = item.Y;
                    for (int i = 0; i < maxY; i++)
                    {
                        for (int j = 0; j < maxX; j++)
                        {
                            map[i, j] = map[i, j] ? map[i, j] : map[(item.Y + (item.Y - i)), j];
                        }
                    }

                }
                break;
            }

            var count = 0;
            for (int i = 0; i < maxY; i++)
            {
                for (int j = 0; j < maxX; j++)
                {
                    // Console.Write(map[i, j] ? "#" : ".");
                    if (map[i, j])
                    {
                        count++;
                    }
                }
                // Console.WriteLine();
            }
            return count;
        }

        static long DoMagic2(IEnumerable<Point> input, IEnumerable<Point> folds)
        {
            var maxX = input.Select(p => p.X).Max() + 1;
            var maxY = input.Select(p => p.Y).Max() + 1;
            var map = new bool[maxY, maxX];

            foreach (var item in input)
            {
                map[item.Y, item.X] = true;
            }

            foreach (var item in folds)
            {
                if (item.X != 0)
                {
                    maxX = item.X;
                    for (int i = 0; i < maxY; i++)
                    {
                        for (int j = 0; j < maxX; j++)
                        {
                            map[i, j] = map[i, j] ? map[i, j] : map[i, (item.X + (item.X - j))];
                        }
                    }

                }
                if (item.Y != 0)
                {
                    maxY = item.Y;
                    for (int i = 0; i < maxY; i++)
                    {
                        for (int j = 0; j < maxX; j++)
                        {
                            map[i, j] = map[i, j] ? map[i, j] : map[(item.Y + (item.Y - i)), j];
                        }
                    }

                }
            }

            var count = 0;
            for (int i = 0; i < maxY; i++)
            {
                for (int j = 0; j < maxX; j++)
                {
                    Console.Write(map[i, j] ? "#" : ".");
                    if (map[i, j])
                    {
                        count++;
                    }
                }
                Console.WriteLine();
            }
            return count;
        }
    }
}
