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
            Utils.Utils.MeasureActionTime("Part 2", () =>
            {
                var res = DoMagic2(data);
                Console.WriteLine($"Result -> {res}");
            });
            Console.WriteLine("END");
        }

        static IEnumerable<int> LoadData(string filename)
        {                
            var raw = Utils.Utils.ReadAllLines(filename);
            foreach (var item in raw)
            {
                yield return Int32.Parse(item);
            }
        }

        static int DoMagic(IEnumerable<int> input)
        {
            var previous = Int32.MaxValue;
            var counter = 0;
            foreach (var item in input)
            {
                if(item > previous){
                    counter++;
                }
                previous = item;
            }

            return counter;
        }

        static int DoMagic2(IEnumerable<int> input)
        {
            var previous = Int32.MaxValue;
            var counter = 0;
            var list = input.ToList();
            for (int i = 0; i < list.Count() - 2; i++)
            {
                var x = list[i] + list[i+1] + list[i+2];
                if(x > previous){
                    counter++;
                }
                previous = x;
            }

            return counter;
        }
    }
}
