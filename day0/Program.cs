using System;
using Tools;
using System.Collections.Generic;
using System.Linq;

namespace adventofcode2020
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Day 7 of AoC");
            IEnumerable<Tuple<string, Dictionary<string, int>>> data = null;
            Tools.Tools.MeasureActionTime("Load", () =>
            {
                string input = args[0];
                Console.WriteLine($"Loading data from " + input + "...");
                var raw = Tools.Tools.ReadAllLines(input);
                data = LoadData(raw);
                Console.WriteLine($"Length -> {data.Count()}");
            });
            Tools.Tools.MeasureActionTime("Part 1", () =>
            {
                var res = DoMagic(data);
                Console.WriteLine($"Result -> {res}");
            });
            Tools.Tools.MeasureActionTime("Part 2", () =>
            {
                var res = DoMagic2(data);
                Console.WriteLine($"Result -> {res}");
            });
            Console.WriteLine("END");
        }

        static IEnumerable<Tuple<string, Dictionary<string, int>>> LoadData(IEnumerable<string> input)
        {
            foreach (var item in input)
            {
                var parts = item.Split(" contain ");
                var a = parts[0].Replace("bags", "").Trim();
                var tempB = parts[1].Replace("bags", "").Replace("bag", "").Replace(".", "").Split(",");
                var b = new Dictionary<string, int>();
                if (!tempB[0].Contains("no other"))
                {
                    foreach (var item2 in tempB)
                    {
                        var c = item2.Trim().Split(" ", 2);
                        b.Add(c[1].Trim(), Int32.Parse(c[0].Trim()));
                    }
                }

                yield return new Tuple<string, Dictionary<string, int>>(a, b);
            }
        }

        static int DoMagic(IEnumerable<Tuple<string, Dictionary<string, int>>> input)
        {
            var keys = new HashSet<string>(new[] { "shiny gold" });
            var resList = new HashSet<string>();
            while (true)
            {
                List<string> temp = new List<string>();
                foreach (var item in keys)
                {
                    temp.AddRange(input.Where(i => i.Item2.ContainsKey(item)).Select(i => i.Item1));
                }
                if (temp.Count() == 0)
                {
                    break;
                }
                keys.Clear();
                foreach (var item in temp)
                {
                    keys.Add(item);
                    resList.Add(item);
                }
            }

            return resList.Count();
        }

        static int DoMagic2(IEnumerable<Tuple<string, Dictionary<string, int>>> input)
        {
            var res = 0;
            res = GetChildrenBags("shiny gold", 1, input);            
            return res-1;
        }

        static int GetChildrenBags(string key, int value, IEnumerable<Tuple<string, Dictionary<string, int>>> input)
        {
            var x = input.Where(i => i.Item1 == key).Select(i => i.Item2).First();
            var res = 1;
            foreach (var item2 in x)
            {            
                res += GetChildrenBags(item2.Key, item2.Value, input);
            }
            return res * value;
        }
    }
}
