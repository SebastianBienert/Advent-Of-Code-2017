using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfCode
{
    class Program
    {
        private static int size = 325489;
        private static int xMiddle;
        private static int yMiddle;
        static void Main(string[] args)
        {
            var arrSize = Convert.ToInt32(Math.Ceiling(Math.Sqrt(size)));

            var arr = new int[arrSize,arrSize];
            //printArray(arr);
            if (arrSize % 2 == 1)
            {
                xMiddle = arrSize / 2;
                yMiddle = (arrSize / 2);
            }
            else
            {
                xMiddle = (arrSize / 2) ;
                yMiddle = (arrSize / 2) - 1 ;
            }


           /* arr[xMiddle - 2, yMiddle + 1] = 3;
            arr[xMiddle - 2, yMiddle - 1] = 2;
            arr[xMiddle - 2, yMiddle] = 1;
            arr[xMiddle, yMiddle + 1] = 3;
            arr[xMiddle, yMiddle - 1] = 2;
            arr[xMiddle, yMiddle] = 1;
            arr[xMiddle - 1, yMiddle - 1] = 8;
            arr[xMiddle - 1, yMiddle + 1] = 8;*/

            //printArray(arr);
            // Console.WriteLine(getSumOfNehigbhours(arr,xMiddle - 1,yMiddle));
            arr[xMiddle, yMiddle] = 1;
            fillSecond(arr);
           // printArray(arr);



            Console.ReadKey();

        }

        private static void printArray(int [,] array)
        {
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    Console.Write("{0,4}", array[i,j]);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
            Console.WriteLine();
        }

        private static void fillSecond(int[,] array)
        {
            int x = xMiddle;
            int y = yMiddle;
            int jmp = 1;
            int size = array.Length;
            for (int i = 1; i < array.Length;)
            {
                if (i == 1)
                {
                    array[x, y] = 1;
                    i++;
                }

                //W PRAWO
                for (int k = 0; k < jmp; k++)
                {
                    y++;
                    array[x, y] = getSumOfNehigbhours(array, x, y);
                    if (array[x, y] > size)
                    {
                        Console.WriteLine($"ZNALZLEM : {array[x,y]}");
                        return;
                    }
                    i++;
                    if (i == size)
                        return;
                }
                // printArray(array);
                // W GORE
                for (int k = 0; k < jmp; k++)
                {
                    x--;
                    array[x, y] = getSumOfNehigbhours(array, x, y);
                    if (array[x, y] > size)
                    {
                        Console.WriteLine($"ZNALZLEM : {array[x, y]}");
                        return;
                    }
                    i++;
                    if (i == size)
                        return;
                }
                jmp += 1;
                // printArray(array);
                //W LEWO
                for (int k = 0; k < jmp; k++)
                {
                    y--;
                    array[x, y] = getSumOfNehigbhours(array, x, y);
                    if (array[x, y] > size)
                    {
                        Console.WriteLine($"ZNALZLEM : {array[x, y]}");
                        return;
                    }

                    i++;
                    if (i == size)
                        return;
                }
                // printArray(array);
                //W DOL
                for (int k = 0; k < jmp; k++)
                {
                    x++;
                    array[x, y] = getSumOfNehigbhours(array, x, y);
                    if (array[x, y] > size)
                    {
                        Console.WriteLine($"ZNALZLEM : {array[x, y]}");
                        return;
                    }
                    i++;
                    if (i == size)
                        return;
                }
                jmp += 1;
                //printArray(array);
            }
        }

        private static void fillArray(int[,] array)
        {
            int x = xMiddle;
            int y = yMiddle;
            int jmp = 1;
            int size = array.Length;
            for (int i = 1; i < array.Length;)
            {
                if (i == 1)
                {
                    array[x, y] = i;
                    i++;
                }

                //W PRAWO
                for (int k = 0; k < jmp; k++)
                {
                    y++;
                    array[x, y] = i;

                    i++;
                    if (i == size)
                        return;
                }
               // printArray(array);
                // W GORE
                for (int k = 0; k < jmp; k++)
                {
                    x--;
                    array[x, y] = i;
                    i++;
                    if (i == size)
                        return;
                }
                jmp += 1;
               // printArray(array);
                //W LEWO
                for (int k = 0; k < jmp; k++)
                {
                    y--;
                    array[x, y] = i;

                    i++;
                    if (i == size)
                        return;
                }
               // printArray(array);
                //W DOL
                for (int k = 0; k < jmp; k++)
                {
                    x++;
                    array[x, y] = i;

                    i++;
                    if (i == size)
                        return;
                }
                jmp += 1;
                //printArray(array);
            }
        }

        private static int getSumOfNehigbhours(int [,] array, int x, int y)
        {
            int sum = 0;
            int xSize = array.GetLength(0);
            int ySize = array.GetLength(1);

                //DOLNY LEWY
                if(y - 1 >= 0 && x + 1 < xSize)
                    sum += array[x + 1, y - 1];
                //DOLNY SRODEK
                if (x + 1 < xSize)
                     sum += array[x + 1, y];
                //DOLNNY PRAWY
                if(x + 1 < xSize && y + 1 < ySize)
                     sum += array[x + 1, y + 1];

                //GORNY LEWY
                if (x - 1 >= 0 && y - 1 >= 0)
                    sum += array[x - 1, y - 1];
                //GORNY SRODEK
                if(x - 1 >= 0)
                    sum += array[x - 1, y];
                //GORNY PRAWY
                if(x - 1 >= 0 && y + 1 < ySize)
                    sum += array[x - 1, y + 1];

                //LEWY
                if (y - 1 >= 0)
                {
                     sum += array[x, y - 1];
                }
                 //PRAWY
                if (y + 1 < ySize)
                {
                    sum += array[x, y + 1];
                }

            return sum;
        }

    }
}
