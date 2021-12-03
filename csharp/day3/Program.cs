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
            Console.WriteLine("Day 3 of AoC");
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
            var raw = Utils.Utils.ReadAllLines(filename);
            return raw;
        }

        static int DoMagic(IEnumerable<string> input)
        {        
            var counter = new List<int>();
            foreach (var item in input)
            {
                for (int i = 0; i < item.Length;i++)                        
                {
                    if(counter.Count() <= i){
                        counter.Add(0);
                    }
                    if(item[i] == '0'){
                        counter[i]++;
                    }
                }
            }

            var gamma = "";            
            foreach (var item in counter)
            {
                if(item > (input.Count()/2)){
                    gamma += "0";
                }
                else
                {
                    gamma += "1";
                }
            }

            var gammaInt = Convert.ToInt32(gamma, 2);
            var epsilon = Convert.ToString( ~gammaInt, 2)[^gamma.Length..];
            var epsilonInt = Convert.ToInt32(epsilon, 2);
            
            return gammaInt * epsilonInt;
        }

        static int DoMagic2(IEnumerable<string> input)
        {
            var list = input.ToList();
            var index = 0;

            while(list.Count() > 1)
            {
                var counter = 0;
                foreach (var item in list)
                {
                    if(item[index] == '0'){
                        counter++;
                    }
                }
                if(counter > (list.Count()/2)){
                    list = list.Where(item => item[index] == '0').ToList();
                }
                else
                {
                     list = list.Where(item => item[index] == '1').ToList();
                }
                index++;
            }
            var oxygen =  Convert.ToInt32(list.Single(),2);

            list = input.ToList();
            index = 0;

            while(list.Count() > 1)
            {
                var counter = 0;
                foreach (var item in list)
                {
                    if(item[index] == '0'){
                        counter++;
                    }
                }
                if(counter <= (list.Count()/2))
                {
                    list = list.Where(item => item[index] == '0').ToList();
                }
                else
                {
                     list = list.Where(item => item[index] == '1').ToList();
                }
                index++;
            }
            var co =  Convert.ToInt32(list.Single(),2);

            return oxygen * co;

        }

    }
}
