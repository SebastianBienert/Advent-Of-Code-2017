using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day14
{
    class Program
    {
        static void Main(string[] args)
        {
            string key = "amgozmfv-";
            int ones = 0;
            int sum = 0;
            List<string> binaries = new List<string>();
            for (int i = 0; i < 128; i++)
            {
                string newKey = key + i;
                string hashed = hash(newKey);
                string binForm = convertToBinary(hashed);
                binaries.Add(binForm);
                ones = countOnes(binForm);
                sum += ones;
            }

            int[,] array = new int[128,128];
            int regionNumber = 1;

            int temp = 0;
            for (int i = 0; i < 128; i++)
            {
                for (int j = 0; j < 128; j++)
                {
                    if(binaries[i][j] == '0')
                        continue;

                    if (array[i, j] != 0)
                        continue;

                   array[i, j] = regionNumber;
                   temp = fillNeighbours(binaries,array,i,j,regionNumber);
                   // if(temp != 0)
                        regionNumber++;

                }
            }


            printArray(array);
            printToFile(array);
            Console.WriteLine(regionNumber - 1);
            Console.ReadKey();
        }

        static void printArray(int[,] arra)
        {
            for (int i = 0; i < arra.GetLength(0); i++)
            {
                for (int j = 0; j < arra.GetLength(1); j++)
                {
                    Console.Write(arra[i, j] + " ");
                }
                Console.WriteLine();
            }
        }

        static void printToFile(int[,] arra)
        {
            using (var wrirter = new StreamWriter("D:/result.txt"))
            {
                for (int i = 0; i < arra.GetLength(0); i++)
                {
                    for (int j = 0; j < arra.GetLength(1); j++)
                    {
                        wrirter.Write(arra[i, j] + " ");
                    }
                    wrirter.WriteLine();
                }
            }
        }

        static int fillNeighbours(List<string> data, int[,] array, int x, int y, int value)
        {
            int sum = 0;
            int xSize = 128;
            int ySize = 128;


            
            //DOLNY SRODEK
            if (x + 1 < xSize)
            {
                if (data[x + 1][y] == '1' && array[x + 1,y] == 0)
                {
                    array[x + 1, y] = value;
                    sum++;
                   sum += fillNeighbours(data,array,x+1,y,value);
                }
            }
            //GORNY SRODEk
            if (x - 1 >= 0)
            {
                if (data[x - 1][y] == '1' && array[x - 1, y] == 0)
                {
                    array[x - 1, y] = value;
                    sum++;
                    sum += fillNeighbours(data, array, x - 1, y, value);
                }
            }



            //LEWY
            if (y - 1 >= 0)
            {
                if (data[x][y - 1] == '1' && array[x, y - 1] == 0)
                {
                    array[x, y - 1] = value;
                    sum++;
                    sum += fillNeighbours(data, array, x, y - 1, value);
                }
            }
            //PRAWY
            if (y + 1 < ySize)
            {
                if (data[x][y + 1] == '1' && array[x, y + 1] == 0)
                {
                    array[x, y + 1] = value;
                    sum++;
                    sum += fillNeighbours(data, array, x, y + 1, value);
                }
            }
            return sum;

        }

        static int getNeigbhoursRegion(List<string> data, int[,] array, int x, int y)
        {
            int sum = 0;
            int xSize = 127;
            int ySize = 127;


            if (data[x][y] != '1')
            {
                return -1;
            }


           /* //DOLNY LEWY
            if (y - 1 >= 0 && x + 1 < xSize)
            {
                if (data[x + 1][y - 1] == '1' && array[x + 1,y - 1] != 0)
                {
                    return array[x + 1, y - 1];
                }
            }*/

            //DOLNY SRODEK
            if (x + 1 < xSize)
            {
                if (data[x + 1][y] == '1' && array[x + 1,y] != 0)
                {
                    return array[x + 1, y];
                }
            }

           /* //DOLNNY PRAWY
            if (x + 1 < xSize && y + 1 < ySize)
            {
                if (data[x + 1][y + 1] == '1' && array[x + 1, y + 1] != 0)
                {
                    return array[x + 1, y + 1];
                }
            }*/


           /* //GORNY LEWY
            if (x - 1 >= 0 && y - 1 >= 0)
            {
                if (data[x - 1][y - 1] == '1' && array[x - 1, y -1] != 0)
                {
                    return array[x - 1, y -1];
                }
            }*/
            //GORNY SRODEK
            if (x - 1 >= 0)
            {
                if (data[x - 1][y] == '1' && array[x - 1, y] != 0)
                {
                    return array[x - 1, y];
                }
            }
          /*  //GORNY PRAWY

            if (x - 1 >= 0 && y + 1 < ySize)
            {
                if (data[x - 1][y + 1] == '1' && array[x - 1, y + 1] != 0)
                {
                    return array[x - 1, y + 1];
                }
            }*/


            //LEWY
            if (y - 1 >= 0)
            {
                if (data[x][y - 1] == '1' && array[x, y - 1] != 0)
                {
                    return array[x, y - 1];
                }
            }
            //PRAWY
            if (y + 1 < ySize)
            {
                if (data[x][y + 1] == '1' && array[x, y + 1] != 0)
                {
                    return array[x, y + 1];
                }
            }

            return -1;
        }

        static string convertToBinary(string input)
        {
            string result = "";
            foreach (var ch in input)
            {
                if(ch <= '9' && ch >= '0')
                    result += Convert.ToString( (ch -'0' ), 2).PadLeft(4,'0');
                else
                    result += Convert.ToString((ch - 'A' + 10), 2);
            }
            return result;
        }

        static int countOnes(string input)
        {
            int sum = 0;
            foreach (var ch in input)
            {
                if (ch == '1')
                    sum++;
            }
            return sum;
        }

        static string hash(string param)
        {

            List<string> data = new List<string>();
            data.Add(param);
            List<int> additional = new List<int>() { 17, 31, 73, 47, 23 };
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
            for (int i = 0; i < 256; i++)
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

            return result;
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
