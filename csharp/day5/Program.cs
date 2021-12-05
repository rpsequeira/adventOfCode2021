using System;
using Utils;
using System.Collections.Generic;
using System.Linq;

namespace adventofcode2020
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Day 5 of AoC");
            IEnumerable<Tuple<Utils.Point, Utils.Point>> data = null;
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

        static IEnumerable<Tuple<Utils.Point, Utils.Point>> LoadData(string filename)
        {
            var raw = Utils.Utils.ReadAllLines(filename);

            foreach (var item in raw)
            {
                var points = item.Split("->");
                var part1 = points[0].Trim();
                var part2 = points[1].Trim();
                Utils.Point point1 = new Point(Int32.Parse(part1.Split(",")[0].Trim()), Int32.Parse(part1.Split(",")[1].Trim()));
                Utils.Point point2 = new Point(Int32.Parse(part2.Split(",")[0].Trim()), Int32.Parse(part2.Split(",")[1].Trim()));

                yield return new Tuple<Point, Point>(point1, point2);
            }
        }

        static int DoMagic(IEnumerable<Tuple<Utils.Point, Utils.Point>> input)
        {
            var mapSizeX = 0;
            var mapSizeY = 0;
            foreach (var item in input)
            {
                if (item.Item1.X > mapSizeX)
                {
                    mapSizeX = item.Item1.X;
                }
                if (item.Item1.Y > mapSizeY)
                {
                    mapSizeY = item.Item1.Y;
                }
                if (item.Item2.X > mapSizeX)
                {
                    mapSizeX = item.Item2.X;
                }
                if (item.Item2.Y > mapSizeY)
                {
                    mapSizeY = item.Item2.Y;
                }
            }

            var map = new int[mapSizeX + 1, mapSizeY + 1];

            foreach (var item in input)
            {
                if (item.Item1.X == item.Item2.X)
                {
                    if (item.Item2.Y >= item.Item1.Y)
                    {
                        var i = item.Item1.Y;
                        do
                        {
                            map[i, item.Item1.X]++;
                            i++;
                        }
                        while (i <= item.Item2.Y);
                    }
                    else
                    {
                        var i = item.Item1.Y;
                        do
                        {
                            map[i, item.Item1.X]++;
                            i--;
                        }
                        while (i >= item.Item2.Y);
                    }
                }
                else
                {
                    if (item.Item1.Y == item.Item2.Y)
                    {
                        if (item.Item2.X >= item.Item1.X)
                        {
                            var i = item.Item1.X;
                            do
                            {
                                map[item.Item1.Y, i]++;
                                i++;

                            }
                            while (i <= item.Item2.X);
                        }
                        else
                        {
                            var i = item.Item1.X;
                            do
                            {
                                map[item.Item1.Y, i]++;
                                i--;
                            }
                            while (i >= item.Item2.X);
                        }
                    }
                }
            }
            var count = 0;
            foreach (var item in map)
            {
                if (item > 1)
                {
                    count++;
                }
            }

            // for (int i = 0; i < map.GetLength(0); i++)
            // {
            //     for (int j = 0; j < map.GetLength(1); j++)
            //     {
            //         Console.Write(map[i,j] == 0 ? "." :map[i,j]);
            //     }
            //     Console.WriteLine(" ");
            // }

            return count;
        }

        static int DoMagic2(IEnumerable<Tuple<Utils.Point, Utils.Point>> input)
        {
            var mapSizeX = 0;
            var mapSizeY = 0;
            foreach (var item in input)
            {
                if (item.Item1.X > mapSizeX)
                {
                    mapSizeX = item.Item1.X;
                }
                if (item.Item1.Y > mapSizeY)
                {
                    mapSizeY = item.Item1.Y;
                }
                if (item.Item2.X > mapSizeX)
                {
                    mapSizeX = item.Item2.X;
                }
                if (item.Item2.Y > mapSizeY)
                {
                    mapSizeY = item.Item2.Y;
                }
            }

            var map = new int[mapSizeX + 1, mapSizeY + 1];

            foreach (var item in input)
            {
                if (item.Item1.X == item.Item2.X)
                {
                    if (item.Item2.Y >= item.Item1.Y)
                    {
                        var i = item.Item1.Y;
                        do
                        {
                            map[i, item.Item1.X]++;
                            i++;
                        }
                        while (i <= item.Item2.Y);
                    }
                    else
                    {
                        var i = item.Item1.Y;
                        do
                        {
                            map[i, item.Item1.X]++;
                            i--;
                        }
                        while (i >= item.Item2.Y);
                    }
                }
                else
                {
                    if (item.Item1.Y == item.Item2.Y)
                    {
                        if (item.Item2.X >= item.Item1.X)
                        {
                            var i = item.Item1.X;
                            do
                            {
                                map[item.Item1.Y, i]++;
                                i++;

                            }
                            while (i <= item.Item2.X);
                        }
                        else
                        {
                            var i = item.Item1.X;
                            do
                            {
                                map[item.Item1.Y, i]++;
                                i--;
                            }
                            while (i >= item.Item2.X);
                        }
                    }
                    else
                    {
                        if (item.Item1.X < item.Item2.X && item.Item1.Y < item.Item2.Y)
                        {
                            var i = item.Item1.Y;
                            var j = item.Item1.X;
                            do
                            {
                                map[i, j]++;
                                i++;
                                j++;
                            }
                            while (i <= item.Item2.Y && j <= item.Item2.X);
                        }
                        else
                        {
                            if (item.Item1.X > item.Item2.X && item.Item1.Y > item.Item2.Y)
                            {
                                var i = item.Item1.Y;
                                var j = item.Item1.X;
                                do
                                {
                                    map[i, j]++;
                                    i--;
                                    j--;
                                }
                                while (i >= item.Item2.Y && j >= item.Item2.X);
                            }
                        }
                        if (item.Item1.X < item.Item2.X && item.Item1.Y > item.Item2.Y)
                        {
                            var i = item.Item1.Y;
                            var j = item.Item1.X;
                            do
                            {
                                map[i, j]++;
                                i--;
                                j++;
                            }
                            while (i >= item.Item2.Y && j <= item.Item2.X);
                        }
                        else
                        {
                            if (item.Item1.X > item.Item2.X && item.Item1.Y < item.Item2.Y)
                            {
                                var i = item.Item1.Y;
                                var j = item.Item1.X;
                                do
                                {
                                    map[i, j]++;
                                    i++;
                                    j--;
                                }
                                while (i <= item.Item2.Y && j >= item.Item2.X);
                            }
                        }
                    }
                }
            }
            var count = 0;
            foreach (var item in map)
            {
                if (item > 1)
                {
                    count++;
                }
            }

            // for (int i = 0; i < map.GetLength(0); i++)
            // {
            //     for (int j = 0; j < map.GetLength(1); j++)
            //     {
            //         Console.Write(map[i,j] == 0 ? "." :map[i,j]);
            //     }
            //     Console.WriteLine(" ");
            // }

            return count;
        }

        private static IEnumerable<List<Tuple<int, bool>>> GetMatrix(IEnumerable<string> input)
        {
            const char separator = ' ';
            for (int i = 0; i < input.Count(); i++)
            {
                if (input.ToList()[i].Trim() == string.Empty)
                {
                    continue;
                }
                yield return input.ToList()[i].Split(separator).Where(s => s.Trim() != string.Empty).Select(s => new Tuple<int, bool>(Int32.Parse(s.Trim()), false)).ToList();
            }
        }

        private static bool CheckCard(List<List<Tuple<int, bool>>> card, int row, int col, int cardid = -1)
        {
            var bingo = true;
            for (int i = 0; i < card[row].Count(); i++)
            {
                if (!card[row][i].Item2)
                {
                    bingo = false;
                    break;
                }
            }
            if (bingo)
            {
                return bingo;
            }
            bingo = true;
            for (int i = 0; i < card.Count(); i++)
            {
                if (!card[i][col].Item2)
                {
                    bingo = false;
                    break;
                }
            }
            return bingo;
        }

        private static int GetSum(List<List<Tuple<int, bool>>> card)
        {
            int sum = 0;
            foreach (var row in card)
            {
                foreach (var col in row)
                {
                    if (!col.Item2)
                    {
                        sum += col.Item1;
                    }
                }
            }
            return sum;
        }

    }
}
