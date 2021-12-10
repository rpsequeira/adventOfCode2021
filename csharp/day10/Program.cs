using System;
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
            Utils.Utils.MeasureActionTime("Part 2", () =>
            {
                var res = DoMagic2(data);
                Console.WriteLine($"Result -> {res}");
            });
            Console.WriteLine("END");
        }

        static IEnumerable<string> LoadData(string filename)
        {
            return Utils.Utils.ReadAllLines(filename);
        }

        static int DoMagic(IEnumerable<string> input)
        {
            var counter = 0;

            foreach (var s in input)
            {
                var stack = new Stack<char>();
                foreach (var c in s.ToCharArray())
                {
                    if (c == '(' || c == '[' || c == '{' || c == '<')
                    {
                        stack.Push(c);
                    }
                    if (c == ')')
                    {
                        var p = stack.Pop();
                        if (p != '(')
                        {
                            counter += 3;
                        }
                    }
                    if (c == ']')
                    {
                        var p = stack.Pop();
                        if (p != '[')
                        {
                            counter += 57;
                        }
                    }
                    if (c == '}')
                    {
                        var p = stack.Pop();
                        if (p != '{')
                        {
                            counter += 1197;
                        }
                    }
                    if (c == '>')
                    {
                        var p = stack.Pop();
                        if (p != '<')
                        {
                            counter += 25137;
                        }
                    }
                }
            }

            return counter;
        }

        static long DoMagic2(IEnumerable<string> input)
        {
            var scores = new List<long>();

            foreach (var s in input)
            {
                var corrupted = false;
                var stack = new Stack<char>();
                foreach (var c in s.ToCharArray())
                {
                    if (c == '(' || c == '[' || c == '{' || c == '<')
                    {
                        stack.Push(c);
                    }
                    if (c == ')')
                    {
                        var p = stack.Pop();
                        if (p != '(')
                        {
                            corrupted = true;
                            break;
                        }
                    }
                    if (c == ']')
                    {
                        var p = stack.Pop();
                        if (p != '[')
                        {
                            corrupted = true;
                            break;
                        }
                    }
                    if (c == '}')
                    {
                        var p = stack.Pop();
                        if (p != '{')
                        {
                            corrupted = true;
                            break;
                        }
                    }
                    if (c == '>')
                    {
                        var p = stack.Pop();
                        if (p != '<')
                        {
                            corrupted = true;
                            break;
                        }
                    }
                }
                if (corrupted)
                {
                    continue;
                }
                long score = 0;
                while (stack.Count() > 0)
                {
                    var c = stack.Pop();
                    if (c == '(')
                    {
                        score = score * 5 + 1;
                    }
                    if (c == '[')
                    {
                        score = score * 5 + 2;
                    }
                    if (c == '{')
                    {
                        score = score * 5 + 3;
                    }
                    if (c == '<')
                    {
                        score = score * 5 + 4;
                    }
                }
                scores.Add(score);
            }
            Utils.Utils.PrintArray(scores);
            return scores.OrderBy(c => c).ToList()[((scores.Count() - 1) / 2)];
        }
    }
}
