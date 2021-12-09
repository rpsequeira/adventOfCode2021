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
            Console.WriteLine("Day 10 of AoC");
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
            // Utils.Utils.MeasureActionTime("Part 2", () =>
            // {
            //     var res = DoMagic2(data);
            //     Console.WriteLine($"Result -> {res}");
            // });
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
            return -1;
        }

        static int DoMagic2(IEnumerable<List<int>> input)
        {
            return -1;
        }        
    }
}
