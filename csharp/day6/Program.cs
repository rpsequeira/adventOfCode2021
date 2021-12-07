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
            Console.WriteLine("Day 6 of AoC");
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
            return Utils.Utils.ReadAlllLinesAndSeparateComas(filename);
        }

        static int DoMagic(IEnumerable<int> input)
        {
            var school = input.ToList();
            for (int day = 0; day < 80; day++)
            {
                var newborn = new List<int>();

                for (int i = 0; i < school.Count(); i++)
                {
                    if (school[i] == 0)
                    {
                        newborn.Add(8);
                        school[i] = 7;
                    }
                    school[i]--;
                }

                school.AddRange(newborn);
                newborn.Clear();
            }

            return school.Count();
        }

        static long DoMagic2(IEnumerable<int> input)
        {
            var schools = new Queue<long>();
            var preschools = new Queue<long>();
            for (int i = 0; i < 7; i++)
            {
                schools.Enqueue(input.Where(d => d == i).Count());
            }
            for (int i = 0; i < 2; i++)
            {
                preschools.Enqueue(0);
            }

            for (int day = 0; day < 256; day++)
            {
                var s = schools.Dequeue();
                var p = preschools.Dequeue();
                var newborn = s;

                schools.Enqueue(s+p);
                preschools.Enqueue(s);
            }
            long count = 0;
            while(schools.Count() != 0 ){
                count += schools.Dequeue();
            }
            while(preschools.Count() != 0 ){
                count += preschools.Dequeue();
            }
            return count;
        }
    }
}
