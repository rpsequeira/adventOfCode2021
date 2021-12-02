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
            Console.WriteLine("Day 2 of AoC");
            IEnumerable<Tuple<string, int>> data = null;
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

        static IEnumerable<Tuple<string, int>> LoadData(string filename)
        {                
            var raw = Utils.Utils.ReadAllLines(filename);
            foreach (var item in raw)
            {
                var l = item.Split(' ');
                yield return new Tuple<string, int>(l[0], Int32.Parse(l[1]));
            }
        }

        static int DoMagic(IEnumerable<Tuple<string, int>> input)
        {        
            var x = 0;
            var y = 0;
            foreach (var item in input)
            {
                switch (item.Item1)
                {
                    case "forward":
                        x += item.Item2;
                        break;
                    case "down":
                        y += item.Item2;
                        break;
                    case "up":
                        y -=item.Item2;
                        break;
                }
            }

            return x * y;
        }

        static int DoMagic2(IEnumerable<Tuple<string, int>> input)
        {
            var x = 0;
            var y = 0;
            var aim = 0;

            foreach (var item in input)
            {
                switch (item.Item1)
                {
                    case "forward":
                        x += item.Item2;
                        y = y + item.Item2*aim; 
                        break;
                    case "down":
                        aim += item.Item2;
                        break;
                    case "up":
                        aim -=item.Item2;
                        break;
                }
            }

            return x * y;
        }
    }
}
