using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day13
{
    class Program
    {
        private static Dictionary<int, int> data;
        private static Dictionary<int, int> incrementer;
        static void Main(string[] args)
        {
           data =
                File.ReadAllLines("D:/data13.txt")
                    .Select(line => line.Split(':').Select(x => x.Trim()).ToArray())
                    .ToDictionary(array => Convert.ToInt32(array[0]), array=> Convert.ToInt32(array[1]));

            incrementer = data.ToDictionary(x => x.Key, x=> 1);

            var state = data.ToDictionary(x => x.Key, x => 0);
            var max = data.Keys.Max();
            var result = 0;
            int sum = 0;


            for (int delay = 0; delay < Int32.MaxValue; delay++)
            {
                sum = 0;
                foreach (var item in data)
                {
                    if ((delay + item.Key) % (item.Value* 2  - 2) == 0)
                    {
                        sum = 1;
                        break;
                    }
                }
                if (sum == 0)
                {
                    Console.WriteLine(delay);
                    break;
                }
            }


           /* for (int delay = 0; delay < Int32.MaxValue; delay++)
            {
                for (int i = 0; i < delay; i++)
                {
                    state = updateState(state);
                    continue;
                }
                for (int path = 0; path <= max; path++)
                {
                    // printDict(state);
                    // Console.WriteLine();
                    if (!state.ContainsKey(path))
                    {
                        state = updateState(state);
                        continue;
                    }


                    if (state.ContainsKey(path) && state[path] == 0)
                    {
                        sum += (path + 1) * data[path];
                        state = updateState(state);
                        continue;
                    }
                    state = updateState(state);
                }
                if (sum == 0)
                {
                    result = delay;
                    break;
                }

                else
                {
                    sum = 0;
                    state = data.ToDictionary(x => x.Key, x => 0);
                    incrementer = data.ToDictionary(x => x.Key, x => 1);
                }

            }*/



            

            Console.WriteLine(result);
            Console.ReadKey();

        }

        private static Dictionary<int, int>  updateState(Dictionary<int, int> dic)
        {
            var result = dic.ToDictionary(t => t.Key, t =>
            {
                var value = t.Value + incrementer[t.Key];
                if (value == data[t.Key] - 1)
                        incrementer[t.Key] *= -1;
                    if (value == 0)
                        incrementer[t.Key] *= -1;

                return value;


            });
            return result;
        }

        private static void printDict(Dictionary<int, int> dic)
        {
            foreach (var item in dic)
            {
                Console.WriteLine($"{item.Key}: {item.Value}");
            }
        }
    }
}
