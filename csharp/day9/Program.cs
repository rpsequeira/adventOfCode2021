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
            Console.WriteLine("Day 9 of AoC");
            IEnumerable<Tuple<IEnumerable<string>, IEnumerable<string>>> data = null;
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
            // Utils.Utils.MeasureActionTime("Part 2", () =>
            // {
            //     var res = DoMagic2(data);
            //     Console.WriteLine($"Result -> {res}");
            // });
            Console.WriteLine("END");
        }

        static IEnumerable<Tuple<IEnumerable<string>, IEnumerable<string>>> LoadData(string filename)
        {
            var raw = Utils.Utils.ReadAllLines(filename);
            foreach (var item in raw)
            {
                var line = item.Split('|');
                yield return new Tuple<IEnumerable<string>, IEnumerable<string>>(line[0].Trim().Split(), line[1].Trim().Split());
            }
        }

        static int DoMagic(IEnumerable<Tuple<IEnumerable<string>, IEnumerable<string>>> input)
        {
            var counter = 0;
            //                       1  4  7  8
            var lenghts = new int[] { 2, 4, 3, 7 };

            foreach (var line in input)
            {
                var second = line.Item2;
                counter += second.Where(s => lenghts.Contains(s.Length)).Count();
            }

            return counter;
        }

        static int DoMagic2(IEnumerable<Tuple<IEnumerable<string>, IEnumerable<string>>> input)
        {
            return -1;
        }
    }
}
