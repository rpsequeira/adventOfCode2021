using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text.RegularExpressions;

namespace adventofcode2020
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Day 12 of AoC");
            IEnumerable<Tuple<string, string>> data = null;
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

        static IEnumerable<Tuple<string, string>> LoadData(string filename)
        {
            var raw = Utils.Utils.ReadAllLines(filename);
            foreach (var item in raw)
            {
                var path = item.Split('-').ToList();
                yield return new Tuple<string, string>(path[0], path[1]);
                yield return new Tuple<string, string>(path[1], path[0]);
            }
        }

        static long DoMagic(IEnumerable<Tuple<string, string>> input)
        {
            var paths = new ConcurrentBag<List<string>>();
            GetPath("start", new List<string>(), paths, input);
            return paths.Count();
        }

        static long DoMagic2(IEnumerable<Tuple<string, string>> input)
        {
            var paths = new ConcurrentBag<List<string>>();
            GetPath2("start", new List<string>(), paths, input);
            return paths.Count();
        }

        static void GetPath(string cave, List<string> path, ConcurrentBag<List<string>> paths, IEnumerable<Tuple<string, string>> map)
        {
            if (cave == "end")
            {
                path.Add("end");
                paths.Add(path);
                return;
            }
            if (!CanIVisit(cave, path))
            {
                return;
            }
            var children = map.Where(p => p.Item1 == cave).ToList();
            foreach (var item in children)
            {
                var copy = new List<string>(path);
                copy.Add(cave);
                GetPath(item.Item2, copy, paths, map);
            }
        }

        static void GetPath2(string cave, List<string> path, ConcurrentBag<List<string>> paths, IEnumerable<Tuple<string, string>> map)
        {
            if (cave == "end")
            {
                path.Add("end");
                paths.Add(path);
                return;
            }
            if (!CanIVisit2(cave, path))
            {
                return;
            }
            var children = map.Where(p => p.Item1 == cave).ToList();
            foreach (var item in children)
            {
                var copy = new List<string>(path);
                copy.Add(cave);
                GetPath2(item.Item2, copy, paths, map);
            }
        }

        static bool CanIVisit(string cave, IEnumerable<string> path)
        {
            var regex = new Regex("[a-z]+");
            if ((cave == "start" || regex.IsMatch(cave)) && path.Contains(cave))
            {
                return false;
            }
            return true;
        }

        static bool CanIVisit2(string cave, IEnumerable<string> path)
        {
            var regex = new Regex("[a-z]+");
            if (cave == "start" && path.Contains(cave))
            {
                return false;
            }
            if (regex.IsMatch(cave) && path.Where(p => regex.IsMatch(p)).GroupBy(p => p, p => p, (k, c) => new { key = k, cout = c.Count() }).Any(g => g.cout > 1) && path.Contains(cave))
            {
                return false;
            }
            return true;
        }
    }
}
