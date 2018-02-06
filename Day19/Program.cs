using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Day19
{
    class Program
    {
        static void Main(string[] args)
        {
            var data = File.ReadAllLines("D:/data19.txt").Select(line => line.ToList()).ToList();
            int xMax = data[0].Count;
            int yMax = data.Count;
            var letters = new List<char>();

            int x;
            int y;

            Regex letter = new Regex("[A-Z]");

            //START
            x = data[0].IndexOf('|');
            y = 0;
            string direction = "DOWN";
            int counter = 1;
            char currentSign = data[x][y];
            do
            {
                if (direction == "DOWN")
                    y++;
                else if (direction == "UP")
                    y--;
                else if (direction == "LEFT")
                    x--;
                else if (direction == "RIGHT")
                    x++;

                currentSign = data[y][x];
                if (currentSign == ' ')
                {
                    break;
                }


                if (letter.IsMatch(currentSign.ToString()))
                {
                    letters.Add(currentSign);
                }
                else if (currentSign == '+')
                {
                    if (direction == "DOWN" || direction == "UP")
                    {
                        if (data[y][x + 1] != ' ')
                            direction = "RIGHT";
                        else
                            direction = "LEFT";
                    }
                    else
                    {
                        if ( y + 1 < data.Count && data[y + 1][x] != ' ')
                            direction = "DOWN";
                        else
                            direction = "UP";
                    }
                }
                counter++;
            } while (true);

            printList(letters);
            Console.WriteLine(counter);
            Console.ReadKey();

        }

        static void printList(List<char> list)
        {
            foreach (var item in list)
            {
                Console.Write($"{item}");
            }
            Console.WriteLine();
        }
    }
}
