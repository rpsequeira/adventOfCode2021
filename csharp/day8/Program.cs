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
            Utils.Utils.MeasureActionTime("Part 2", () =>
            {
                var res = DoMagic2(data);
                Console.WriteLine($"Result -> {res}");
            });
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
            /*
                       7
                 ────────────
                │            │
                │            │
                │            │
              6 │            │ 1
                │            │
                │            │
                      5
                 ────────────
                │            │
                │            │
                │            │
              4 │            │ 2
                │            │
                │            │
                 ────────────
                      3                    
                         
            */


            var counter = 0;
            //                       1  4  7  8
            var lenghts = new int[] { 2, 4, 3, 7 };

            foreach (var line in input)
            {
                var coding = new string[7] { "", "", "", "", "", "", "" };

                var zero = "";
                var one = String.Concat(line.Item1.Where(s => s.Length == 2).First().OrderBy(c => c));
                var oneRaw = line.Item1.Where(s => s.Length == 2).First();
                var two = "";
                var three = "";
                var four = String.Concat(line.Item1.Where(s => s.Length == 4).First().OrderBy(c => c));
                var fourRaw = line.Item1.Where(s => s.Length == 4).First();
                var five = "";
                var six = "";
                var seven = String.Concat(line.Item1.Where(s => s.Length == 3).First().OrderBy(c => c));
                var sevenRaw = line.Item1.Where(s => s.Length == 3).First();
                var eight = String.Concat(line.Item1.Where(s => s.Length == 7).First().OrderBy(c => c));
                var eightRaw = line.Item1.Where(s => s.Length == 7).First();
                var nine = "";

                var first = line.Item1.Where(s => s != oneRaw && s != fourRaw && s != sevenRaw && s != eightRaw);
                var seg6 = first.Where(s => s.Length == 6);
                foreach (var item in seg6)
                {
                    var ordered = String.Concat(item.OrderBy(c => c));
                    if (!one.All(x => ordered.Contains(x)))
                    {
                        six = ordered;
                        coding[1] = six.Intersect(one).First().ToString();
                        continue;
                    }
                    if (four.All(x => ordered.Contains(x)))
                    {
                        nine = ordered;
                    }
                    else
                    {
                        zero = ordered;
                    }
                    if (!four.All(x => ordered.Contains(x)))
                    {
                        zero = ordered;
                    }
                    else
                    {
                        nine = ordered;
                    }                    
                }

                var seg5 = first.Where(s => s.Length == 5);
                foreach (var item in seg5)
                {
                    var ordered = String.Concat(item.OrderBy(c => c));
                    // 3
                    if (one.All(x => ordered.Contains(x)))
                    {
                        three = ordered;
                        continue;
                    }
                    if (!one.All(x => ordered.Contains(x)))
                    {
                        if (ordered.Contains(coding[1]))
                        {
                            five = ordered;
                        }
                        else
                        {
                            two = ordered;
                        }
                    }
                    else
                    {
                        three = ordered;
                    }

                }

                var number = "";
                foreach (var item in line.Item2)
                {
                    var n = String.Concat(item.OrderBy(c => c));
                    if (n == eight)
                    {
                        number += "8";
                    }
                    else
                    {
                        if (n == zero)
                        {
                            number += "0";
                        }
                        else
                        {
                            if (n == nine)
                            {
                                number += "9";
                            }
                            else
                            {
                                if (n == six)
                                {
                                    number += "6";
                                }
                                else
                                {
                                    if (n == two)
                                    {
                                        number += "2";
                                    }
                                    else
                                    {
                                        if (n == three)
                                        {
                                            number += "3";
                                        }
                                        else
                                        {
                                            if (n == five)
                                            {
                                                number += "5";
                                            }
                                            else
                                            {
                                                if (n == four)
                                                {
                                                    number += "4";
                                                }
                                                else
                                                {
                                                    if (n == seven)
                                                    {
                                                        number += "7";
                                                    }
                                                    else
                                                    {
                                                        if (n == one)
                                                        {
                                                            number += "1";
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                var finalNumber = Int32.Parse(number);
                counter += finalNumber;
            }

            return counter;
        }
    }
}
