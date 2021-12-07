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
            Console.WriteLine("Day 8 of AoC");
            IEnumerable<int> data = null;
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

        static IEnumerable<int> LoadData(string filename)
        {
            return Utils.Utils.ReadAlllLinesAndSeparateComas(filename);
        }

        static int DoMagic(IEnumerable<int> input)
        {
            var crabs = input.ToList();
            var fuel = new int[input.Count()];
            var totalFuel = Int32.MaxValue;
            for (int pos = 0; pos < crabs.Max(); pos++)
            {
                for (int i = 0; i < crabs.Count(); i++)
                {
                    fuel[i] = Math.Abs(crabs[i] - pos);
                }

                var temp = fuel.Sum();
                if (temp < totalFuel)
                {
                    totalFuel = temp;
                }
            }
            return totalFuel;
        }

        static int DoMagic2(IEnumerable<int> input)
        {
            return -1;
        }
    }
}
