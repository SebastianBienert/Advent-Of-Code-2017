using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day22
{
    public static class MyExtensions
    {
        public static void Popuplate<T>(this T[,] source, T value)
        {
            for (int i = 0; i < source.GetLength(0); i++)
            {
                for (int j = 0; j < source.GetLength(1); j++)
                {
                    source[i, j] = value;
                }
            }
        }
    }

    public enum Direction
    {
        LEFT,
        RIGHT,
        UP,
        DOWN,
        REVERSE
    }

    class Program
    {
        public  const int SIZE = 10001;
        public static string[,] _array = new string[SIZE, SIZE];
        public static int middleX = (SIZE - 1 )/ 2;
        public static int middleY = (SIZE - 1 )/ 2;
        static void Main(string[] args)
        {
            _array.Popuplate(".");
            var data = File.ReadAllLines($"D:/data22.txt").ToList();
            var dataXSize = data[0].Length;
            var dataYSize = data.Count;
            //POPULATE GRID
           // printGrid();
            for (int i = middleX - dataXSize / 2, counterX = 0; counterX < dataXSize; counterX++, i++)
            {
                for (int j = middleY - dataYSize / 2, counterY = 0; counterY < dataYSize; counterY++, j++)
                {
                    _array[i, j] = data[counterX][counterY].ToString();
                }
            }
            //Console.WriteLine("\n\n");
            //printGrid();

            var currentX = middleX;
            var currentY = middleY;
            Direction direction = Direction.UP;
            int bursts = 0;
            int infections = 0;
            do
            {
                if (_array[currentX, currentY] == "#")
                {
                    direction = TransformDirection(direction, Direction.RIGHT);
                    _array[currentX, currentY] = "f";
                }
                else if (_array[currentX, currentY] == "f")
                {
                    direction = TransformDirection(direction, Direction.REVERSE);
                    _array[currentX, currentY] = ".";

                }
                else if(_array[currentX, currentY] == ".")
                {
                    direction = TransformDirection(direction, Direction.LEFT);
                    _array[currentX, currentY] = "w";

                }
                else if (_array[currentX, currentY] == "w")
                {
                    _array[currentX, currentY] = "#";
                    infections++;
                }


                switch (direction)
                {
                    case Direction.RIGHT:
                    {
                        currentY++;
                        break;
                    }
                    case Direction.LEFT:
                    {
                        currentY--;
                            break;
                    }
                    case Direction.UP:
                    {
                        currentX--;
                            break;
                    }
                    case Direction.DOWN:
                    {
                            currentX++;
                            break;
                    }
                }

                bursts++;
            } while (bursts < 10000000);

            Console.WriteLine($"\n {direction} {infections}\n");
           // printGrid();

            Console.ReadKey();

        }

        public static Direction TransformDirection(Direction source, Direction move)
        {
            var array = new Direction[4] {Direction.UP, Direction.RIGHT, Direction.DOWN, Direction.LEFT};
            int srcIndex = Array.FindIndex(array, x => x == source);
            if (move == Direction.RIGHT)
                return array[(srcIndex + 1) % 4];
            else if(move == Direction.LEFT)
                return array[(srcIndex - 1 + 4) % 4];
            else
            {
                return array[(srcIndex + 2) % 4];
            }

        }

        public static void printGrid()
        {
            for (int i = 0; i < _array.GetLength(0); i++)
            {
                for (int j = 0; j < _array.GetLength(1); j++)
                {
                    Console.Write(_array[i,j]);
                }
                Console.WriteLine();
            }
        }
    }

        

    }

