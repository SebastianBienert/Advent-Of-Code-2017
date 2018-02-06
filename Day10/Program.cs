using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day10
{
    class Program
    {
        static void Main(string[] args)
        {

            List<string> data = new List<string>() { "" };
            List<int> additional = new List<int>() {17, 31, 73, 47, 23};
            List<int> seq = new List<int>();
            foreach (var word in data)
            {
                byte[] asciiBytes = Encoding.ASCII.GetBytes(word);
                foreach (var asciiByte in asciiBytes)
                {
                    seq.Add(asciiByte);
                }
            }
          //  seq = new List<int>() {3,4,1,5};
            seq.AddRange(additional);

            

            List<int> list = new List<int>();
            for(int i = 0; i < 256; i++)
                list.Add(i);

            int currentPos = 0;
            int skipSize = 0;
            int seqIndex = 0;

            for (int i = 0; i < 64; i++)
            {
                seqIndex = 0;
                do
                {
                    if ((currentPos + seq[seqIndex]) < list.Count)
                        list.Reverse(currentPos, seq[seqIndex]);
                    else
                    {
                        customReverse(list, currentPos, seq[seqIndex]);
                    }
                    currentPos += seq[seqIndex] + skipSize;
                    currentPos %= list.Count;
                    skipSize++;
                    seqIndex++;
                } while (seqIndex < seq.Count);
            }
            
            List<int> denseHash = new List<int>();
      
            for (int i = 0; i < 16; i++)
            {
                int output = list[i * 16];
                for (int j = (i * 16) + 1; j < (i * 16) + 16; j++)
                {
                    output ^= list[j];
                }
                denseHash.Add(output);
            }
            string result = String.Empty;
            for (int i = 0; i < 16; i++)
            {
                string hexValue = denseHash[i].ToString("X");
                if (hexValue.Length == 1)
                    hexValue = "0" + hexValue;
                result += hexValue;
            }

            Console.WriteLine(result);
            Console.ReadKey();

        }
        private static void customReverse(List<int> list, int pos, int count)
        {
            var newList = new List<int>();
            int counter = count;
            for (int i = pos; count > 0; i++)
            {
                newList.Add(list[i % list.Count]);
                count--;
            }

            newList.Reverse();
            int j = 0;
            for (int i = pos; counter > 0; i++)
            {
                list[i % list.Count] = newList[j];
                counter--;
                j++;
            }

        }
        private static void printList(List<int> list)
        {
            foreach (var it in list)
            {
                Console.Write($"{it} ");
            }
            Console.WriteLine();
        }


    }
}
