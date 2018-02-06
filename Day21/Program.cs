using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day21
{

    class Grid
    {
        public int[,] Data { get; set; }
        public int Size { get; set; }
        public string Pattern { get; set; }

        public Grid(string pattern)
        {
           var splited =  pattern.Split('/');
            Size = splited[0].Length;
            Data = new int[Size,Size];
            Pattern = pattern;

            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    if (splited[i][j] == '.')
                    {
                        Data[i, j] = 0;
                    }
                    else
                    {
                        Data[i, j] = 1;
                    }
                }
            }
        }

        public int TurnnedOnNumber()
        {
            int ret = 0;
            for (int i = 0; i < Size; i++)
            {
                for(int j = 0; j < Size; j++)
                    if (Data[i, j] == 1)
                        ret++;
            }
            return ret;
        }

        public Grid(int[,] data)
        {
            Data = data;
            Size = data.GetLength(0);
            StringBuilder builder = new StringBuilder(12);

            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    if (Data[i, j] == 0)
                    {
                        builder.Append(".");
                    }
                    else
                    {
                        builder.Append("#");
                    }
                }
                if(i < Size - 1)
                    builder.Append("/");
            }
            Pattern = builder.ToString();
        }

        public override string ToString()
        {
            var sp = Pattern.Split('/');
            StringBuilder builder = new StringBuilder();
            foreach (var line in sp)
            {
                builder.AppendLine(line);
            }
            return builder.ToString();
        }

        private static int[,] rotated90(int [,] data)
        {
            int n = data.GetLength(0);
            int[,] ret = new int[n, n];

            for (int i = 0; i < n; ++i)
            {
                for (int j = 0; j < n; ++j)
                {
                    ret[i, j] = data[n - j - 1, i];
                }
            }
            return ret;
        }

        public override int GetHashCode()
        {
            return Size.GetHashCode();
        }

        private static int[,] flippedVertically(int[,] data)
        {
            int n = data.GetLength(0);
            int[,] ret = new int[n, n];

            for (int i = 0; i < n; ++i)
            {
                for (int j = 0; j < n; ++j)
                {
                    ret[i, j] = data[n - i - 1, j];
                }
            }
            return ret;
        }

        private static int[,] flippedHorizontally(int[,] data)
        {
            int n = data.GetLength(0);
            int[,] ret = new int[n, n];

            for (int i = 0; i < n; ++i)
            {
                for (int j = 0; j < n; ++j)
                {
                    ret[i, j] = data[i, n - 1 - j];
                }
            }
            return ret;
        }


        public override bool Equals(object obj)
        {
            Grid grid = obj as Grid;

            var rotated90Var = rotated90(this.Data);
            var rotated180Var = rotated90(rotated90Var);
            var rotated270Var = rotated90(rotated180Var);

            if (
                   grid.Data.ContentEquals(Data)
                || grid.Data.ContentEquals(flippedHorizontally(Data))
                || grid.Data.ContentEquals(rotated90Var)
                || grid.Data.ContentEquals(flippedHorizontally(rotated90Var))
                || grid.Data.ContentEquals(rotated180Var)
                || grid.Data.ContentEquals(flippedHorizontally(rotated180Var))
                || grid.Data.ContentEquals(rotated270Var)
                || grid.Data.ContentEquals(flippedHorizontally(rotated270Var))
                )
                return true;


            return false;

        }

    }

    public static class MyExtensions
    {
        public static bool ContentEquals<T>(this T[,] arr, T[,] other) where T : IComparable
        {
            if (arr.GetLength(0) != other.GetLength(0) ||
                arr.GetLength(1) != other.GetLength(1))
                return false;
            for (int i = 0; i < arr.GetLength(0); i++)
                for (int j = 0; j < arr.GetLength(1); j++)
                    if (arr[i, j].CompareTo(other[i, j]) != 0)
                        return false;
            return true;
        }
    }

    class Program
    {


        private static Dictionary<Grid, Grid> _dic = new Dictionary<Grid, Grid>();

        static void Main(string[] args)
        {
            var start = ".#./..#/###";
            var data =
                File.ReadAllLines("D:/data21.txt")
                    .Select(line => line.Split().Where(word => word != "=>").ToList()).ToList();
            foreach (var splited in data)
            {
                var from = new Grid(splited[0]);
                var to = new Grid(splited[1]);
                _dic.Add(from,to);
            }

            var current = start;

            Grid next = new Grid(start);
            Console.WriteLine(next);
            for (int it = 0; it < 18; it++)
            {
                var grid = new Grid(current); 
                if (grid.Size % 2 == 0)
                {
                   next = div2Grid(grid);
                }
                else if (grid.Size % 3 == 0)
                {
                    next = div3Grid(grid);
                }

                current = next.Pattern;
               // Console.WriteLine(next);
                Console.WriteLine($"{it + 1}: {next.TurnnedOnNumber()}");
            }
           

            Console.ReadKey();
        }

        public static Grid transformAndMergeListOfGrids3D(Grid[,] array, int size)
        {
            var transformed = new Grid[size, size];
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                    transformed[i, j] = _dic[array[i, j]];
            }
            string finalPattern = String.Empty;

            var finalData = new int[size * 4, size * 4];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    finalData[i * 4, j * 4] = transformed[i, j].Data[0, 0];
                    finalData[i  * 4, j  * 4 + 1] = transformed[i, j].Data[0, 1];
                    finalData[i  * 4, j  * 4 + 2] = transformed[i, j].Data[0, 2];
                    finalData[i * 4, j * 4 + 3] = transformed[i, j].Data[0, 3];

                    finalData[i  * 4 + 1, j  * 4] = transformed[i, j].Data[1, 0];
                    finalData[i  * 4 + 1, j  * 4 + 1] = transformed[i, j].Data[1, 1];
                    finalData[i  * 4 + 1, j  * 4 + 2] = transformed[i, j].Data[1, 2];
                    finalData[i * 4 + 1, j * 4 + 3] = transformed[i, j].Data[1, 3];

                    finalData[i  * 4 + 2, j  * 4] = transformed[i, j].Data[2, 0];
                    finalData[i  * 4 + 2, j  * 4 + 1] = transformed[i, j].Data[2, 1];
                    finalData[i  * 4 + 2, j * 4 + 2] = transformed[i, j].Data[2, 2];
                    finalData[i * 4 + 2, j * 4 + 3] = transformed[i, j].Data[2, 3];

                    finalData[i * 4 + 3, j * 4] = transformed[i, j].Data[3, 0];
                    finalData[i * 4 + 3, j * 4 + 1] = transformed[i, j].Data[3, 1];
                    finalData[i * 4 + 3, j * 4 + 2] = transformed[i, j].Data[3, 2];
                    finalData[i * 4 + 3, j * 4 + 3] = transformed[i, j].Data[3, 3];
                }
            }

          //  Console.WriteLine(new Grid(finalData));
          //  Console.WriteLine(new Grid(finalData));
            return new Grid(finalData);

        }

        public static Grid div3Grid(Grid input)
        {
            var listOfNewGrids = new Grid[input.Size / 3, input.Size / 3];
            int numOfSmallGrids = input.Size;
            if (input.Size == 30000000000000)
            {
                return _dic[input];
            }
            else
            {
                string newPattern = String.Empty;
                for (int i = 0; i < numOfSmallGrids / 3; i++)
                {
                    for (int j = 0; j < numOfSmallGrids / 3; j++)
                    {
                        newPattern += translateToSign(input.Data[i * 3, j * 3]);
                        newPattern += translateToSign(input.Data[i * 3, j * 3 + 1]);
                        newPattern += translateToSign(input.Data[i * 3, j * 3 + 2]);
                        newPattern += "/";
                        newPattern += translateToSign(input.Data[i * 3 + 1, j * 3]);
                        newPattern += translateToSign(input.Data[i * 3 + 1, j * 3 + 1]);
                        newPattern += translateToSign(input.Data[i * 3 + 1, j * 3 + 2]);
                        newPattern += "/";
                        newPattern += translateToSign(input.Data[i * 3 + 2, j * 3]);
                        newPattern += translateToSign(input.Data[i * 3 + 2, j * 3 + 1]);
                        newPattern += translateToSign(input.Data[i * 3 + 2, j * 3 + 2]);

                        listOfNewGrids[i, j] = new Grid(newPattern);
                       // Console.WriteLine(new Grid(newPattern));
                        newPattern = String.Empty;
                    }

                }
            }

            return transformAndMergeListOfGrids3D(listOfNewGrids, listOfNewGrids.GetLength(0));
        }

        public static Grid div2Grid(Grid input)
        {
            var listOfNewGrids = new Grid[input.Size / 2, input.Size / 2];
            int numOfSmallGrids = input.Size;
            if (input.Size == 200000000000)
            {
                return _dic[input];
            }
            else
            {
                string newPattern = String.Empty;
                for (int i = 0; i < numOfSmallGrids / 2; i++)
                {
                    for (int j = 0; j < numOfSmallGrids / 2; j++)
                    {
                        newPattern += translateToSign(input.Data[i * 2, j * 2]);
                        newPattern += translateToSign(input.Data[i * 2, j * 2 + 1]);
                        newPattern += "/";
                        newPattern += translateToSign(input.Data[i * 2 + 1, j * 2]);
                        newPattern += translateToSign(input.Data[i * 2 + 1, j* 2 + 1]);

                        listOfNewGrids[i, j] = new Grid(newPattern);
                       // Console.WriteLine(new Grid(newPattern));
                        newPattern = String.Empty;
                    }

                }
            }

            return transformAndMergeListOfGrids(listOfNewGrids, listOfNewGrids.GetLength(0));
        }

        public static Grid transformAndMergeListOfGrids(Grid[,] array, int size )
        {
            var transformed = new Grid[size, size];
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                    transformed[i, j] = _dic[array[i, j]];
            }
            string finalPattern = String.Empty;

            var finalData = new int[size * 3, size * 3];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    finalData[i * 3, j * 3] = transformed[i, j].Data[0, 0];
                    finalData[i * 3, j * 3 + 1] = transformed[i, j].Data[0, 1];
                    finalData[i * 3, j * 3 + 2] = transformed[i, j].Data[0, 2];

                    finalData[i * 3 + 1, j * 3] = transformed[i, j].Data[1, 0];
                    finalData[i * 3 + 1, j * 3 + 1] = transformed[i, j].Data[1, 1];
                    finalData[i *3 + 1, j * 3 + 2] = transformed[i, j].Data[1, 2];

                    finalData[i * 3 + 2, j * 3] = transformed[i, j].Data[2, 0];
                    finalData[i *3 + 2, j *3 + 1] = transformed[i, j].Data[2, 1];
                    finalData[i * 3 + 2, j*3 + 2] = transformed[i, j].Data[2, 2];
                }
            }

        //    Console.WriteLine(new Grid(finalData));
            return new Grid(finalData);

        }

        public static string translateToSign(int x)
        {
            if (x == 1)
                return "#";

            return ".";
        }

    }
}
