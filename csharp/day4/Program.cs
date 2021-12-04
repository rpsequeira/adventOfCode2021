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
            Tuple<IEnumerable<int>, List<List<List<Tuple<int, bool>>>>> data = null;
            Utils.Utils.MeasureActionTime("Load", () =>
            {
                string input = args[0];
                Console.WriteLine($"Loading data...");
                data = LoadData(input);
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

        static Tuple<IEnumerable<int>, List<List<List<Tuple<int, bool>>>>> LoadData(string filename)
        {
            var raw = Utils.Utils.ReadAllLines(filename).ToArray();
            Console.WriteLine($"Length -> {raw.Count()}");

            var drawn = Array.ConvertAll(raw[0].Split(','), int.Parse);
            var cards = new List<List<List<Tuple<int, bool>>>>();


            for (int i = 2; i < raw.Count(); i += 6)
            {
                cards.Add(GetMatrix(raw.Take(new Range(i, i + 6))).ToList());
            }

            return new Tuple<IEnumerable<int>, List<List<List<Tuple<int, bool>>>>>(drawn, cards);
        }

        static int DoMagic(Tuple<IEnumerable<int>, List<List<List<Tuple<int, bool>>>>> input)
        {
            var bingo = false;
            List<List<Tuple<int, bool>>> bingoCard = null;
            int lastNumber = -1;
            foreach (var number in input.Item1)
            {
                for (int i = 0; i < input.Item2.Count(); i++)
                {
                    var card = input.Item2[i];
                    for (int j = 0; j < card.Count(); j++)
                    {
                        for (int k = 0; k < card[j].Count(); k++)
                        {
                            if (card[j][k].Item1 == number)
                            {
                                card[j][k] = new Tuple<int, bool>(card[j][k].Item1, true);
                                bingo = CheckCard(card, j, k);
                                if (bingo)
                                {
                                    bingoCard = card;
                                    lastNumber = number;
                                }
                                break;
                            }
                        }
                        if (bingo)
                        {
                            break;
                        }
                    }
                    if (bingo)
                    {
                        break;
                    }
                }
                if (bingo)
                {
                    break;
                }
            }

            return GetSum(bingoCard) * lastNumber;
        }

        static int DoMagic2(Tuple<IEnumerable<int>, List<List<List<Tuple<int, bool>>>>> input)
        {
            var bingo = false;
            List<List<Tuple<int, bool>>> bingoCard = null;
            int lastNumber = -1;
            List<int> numberOfBingos = new List<int>();
            foreach (var number in input.Item1)
            {
                for (int i = 0; i < input.Item2.Count(); i++)
                {
                    if (numberOfBingos.Contains(i))
                    {
                        continue;
                    }
                    var card = input.Item2[i];
                    for (int j = 0; j < card.Count(); j++)
                    {
                        for (int k = 0; k < card[j].Count(); k++)
                        {
                            if (card[j][k].Item1 == number)
                            {
                                card[j][k] = new Tuple<int, bool>(card[j][k].Item1, true);
                                bingo = CheckCard(card, j, k, i);
                                if (bingo && !numberOfBingos.Contains(i))
                                {
                                    numberOfBingos.Add(i);
                                }
                                if (numberOfBingos.Count() == input.Item2.Count())
                                {
                                    bingoCard = card;
                                    lastNumber = number;
                                    break;
                                }
                                else
                                {
                                    bingo = false;
                                }
                            }
                        }
                        if (bingo)
                        {
                            break;
                        }
                    }
                    if (bingo)
                    {
                        break;
                    }
                }
                if (bingo)
                {
                    break;
                }
            }

            return GetSum(bingoCard) * lastNumber;
        }

        private static IEnumerable<List<Tuple<int, bool>>> GetMatrix(IEnumerable<string> input)
        {
            const char separator = ' ';
            for (int i = 0; i < input.Count(); i++)
            {
                if (input.ToList()[i].Trim() == string.Empty)
                {
                    continue;
                }
                yield return input.ToList()[i].Split(separator).Where(s => s.Trim() != string.Empty).Select(s => new Tuple<int, bool>(Int32.Parse(s.Trim()), false)).ToList();
            }
        }

        private static bool CheckCard(List<List<Tuple<int, bool>>> card, int row, int col, int cardid = -1)
        {
            var bingo = true;
            for (int i = 0; i < card[row].Count(); i++)
            {
                if (!card[row][i].Item2)
                {
                    bingo = false;
                    break;
                }
            }
            if (bingo)
            {
                return bingo;
            }
            bingo = true;
            for (int i = 0; i < card.Count(); i++)
            {
                if (!card[i][col].Item2)
                {
                    bingo = false;
                    break;
                }
            }
            return bingo;
        }

        private static int GetSum(List<List<Tuple<int, bool>>> card)
        {
            int sum = 0;
            foreach (var row in card)
            {
                foreach (var col in row)
                {
                    if (!col.Item2)
                    {
                        sum += col.Item1;
                    }
                }
            }
            return sum;
        }

    }
}
