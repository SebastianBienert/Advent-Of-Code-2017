using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day6
{
    class Program
    {
        static void Main(string[] args)
        {
            var data =
                File.ReadAllText("D:/data6.txt")
                    .Split()
                    .Where(x => !String.IsNullOrWhiteSpace(x))
                    .Select(i => Convert.ToInt32(i))
                    .ToList();

            int maxIndex;
            int maxValue;
            string currentSeq;
            int counter = 0;
            HashSet<string> memory = new HashSet<string>();

            do
            {
                maxIndex = findIndexOfMax(data);
                maxValue = data[maxIndex];
                data[maxIndex] -= maxValue;
                for (int i = 1; i <= maxValue; i++)
                {
                    data[(maxIndex + i) % data.Count]++;
                }
                currentSeq = createString(data);
                Console.WriteLine(currentSeq);
                counter++;


            } while (memory.Add(currentSeq));

            var last = currentSeq;
            data =
                File.ReadAllText("D:/data6.txt")
                    .Split()
                    .Where(x => !String.IsNullOrWhiteSpace(x))
                    .Select(i => Convert.ToInt32(i))
                    .ToList();


            memory = new HashSet<string>();
            memory.Add(last);
            int newCounter = 0;
            do
            {
                maxIndex = findIndexOfMax(data);
                maxValue = data[maxIndex];
                data[maxIndex] -= maxValue;
                for (int i = 1; i <= maxValue; i++)
                {
                    data[(maxIndex + i) % data.Count]++;
                }
                currentSeq = createString(data);
                newCounter++;


            } while (memory.Add(currentSeq));


            Console.WriteLine(counter - newCounter);
            Console.ReadKey();
        }

        static int findIndexOfMax(List<int> array)
        {
            int max = Int32.MinValue;
            int result = 0;
            for (int i = 0; i < array.Count; i++)
            {
                if (array[i] > max)
                {
                    result = i;
                    max = array[i];
                }
            }

            return result;
        }

        static string createString(List<int> list)
        {
            string result = String.Empty;

            foreach (var it in list)
            {
                result += it;
            }
            return result;
        }
    }
}
