using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day5
{
    class Program
    {
        static void Main(string[] args)
        {
            var data = File.ReadAllLines("D:/data5.txt").Select(line => Convert.ToInt32(line)).ToArray();

            int counter = 0;
            int index = 0;
            int newindex = 0;
            do
            {
                newindex = index + data[index];
                if (data[index] >= 3)
                    data[index]--;
                else
                     data[index]++;


                index = newindex;
                counter++;

            } while (index < data.Length);

            Console.WriteLine(counter);
            Console.ReadKey();
        }
    }
}
