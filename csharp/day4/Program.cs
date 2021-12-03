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
            Console.WriteLine("Day 4 of AoC");
            IEnumerable<string> data = null;
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

        static IEnumerable<string> LoadData(string filename)
        {                

        }

        static int DoMagic(IEnumerable<string> input)
        {        

        }

        static int DoMagic2(IEnumerable<string> input)
        {
        
        }

    }
}
