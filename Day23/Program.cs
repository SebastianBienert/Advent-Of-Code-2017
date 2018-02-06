using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day23
{
    class Program
    {
        public static Dictionary<char, int> _registers = new Dictionary<char, int>()
        {
            {'a',1},
            {'b',0},
            {'c',0},
            {'d',0},
            {'e',0},
            {'f',0},
            {'g',0},
            {'h',0},
        };

        public static void secondPart()
        {
            int a = 1;
            int b = 106500;
            int c = 123500;
            int d = 2;
            int e = 2;
            int f = 1;
            int g = d;
            int h = 0;


            int smallLoop = 106500;
            int midLoop = 106500;
            int bigLoop = 0;

            do
            {
                f = 1;
                d = 2;
                do
                {
                    e = 2;
                    do
                    {
                        if ((d * e) - b == 0)
                        {
                            f = 0;
                        }
                        if (d * e > b)
                            break;
                        e++;
                    } while ( (e - b) != 0);
                    d++;
                } while ( (d - b) != 0);

                if (f == 0)
                    h++;

                if (b - c == 0)
                    break;

                b += 17;
                Console.WriteLine(h);
            } while (true);
            Console.WriteLine(h);
        }
       
        static void Main(string[] args)
        {

            secondPart();

            Console.ReadKey();






            var instructions = File.ReadAllLines($"D:/data23.txt").Select(line => line.Split(' '))
                .Select(
                    l =>
                        new KeyValuePair<string, KeyValuePair<string, string>>(l[0],
                            new KeyValuePair<string, string>(l[1], l[2]))).ToList();
  

           // printRegisters();
            int mulIterator = 0;
            int secCounter = 0;
            bool turnOn = false;
            const int STOP = 851900;
            int helpCounter = 0;
            for (int i = 0; i < instructions.Count; i++)
            {
                var dstRegister = instructions[i].Value.Key;
                var srcValue = instructions[i].Value.Value;
                int value = 0;

                if (_registers['e'] > 106498 && _registers['d'] > 30)
                    Console.WriteLine($"{instructions[i].Key} {instructions[i].Value.Key} {instructions[i].Value.Value}");

                if (srcValue.Length < 2 && Char.IsLetter(Convert.ToChar(srcValue)))
                    value = _registers[Convert.ToChar(srcValue)];
                else
                    value = Convert.ToInt32(srcValue);

                if (instructions[i].Key == "set")
                {
                    _registers[Convert.ToChar(dstRegister)] = value;
                }
                else if (instructions[i].Key == "sub")
                {
                    _registers[Convert.ToChar(dstRegister)] -= value;
                }
                else if (instructions[i].Key == "mul")
                {
                    _registers[Convert.ToChar(dstRegister)] *= value;
                    mulIterator++;
                }
                else if (instructions[i].Key == "jnz")
                {
                    if (dstRegister.Length > 1 || !Char.IsLetter(Convert.ToChar(dstRegister)))
                    {
                        if (Convert.ToInt32(dstRegister) != 0)
                        {
                            i += value;
                            i--;
                        }
                    }
                    else if (_registers[Convert.ToChar(dstRegister)] != 0)
                    {
                        i += value;
                        i--;
                    }
                }

                if (_registers['e'] > 106498 && _registers['d'] > 30)
                {
                    turnOn = true;


                }
                if (turnOn)
                {
                    printRegisters();
                    Console.WriteLine("\n");
                    helpCounter++;
                }
                    

                if (helpCounter > 400)
                    break;

            }

            Console.WriteLine(mulIterator);
            Console.ReadKey();

        }

        public static void printRegisters()
        {
            foreach (var reg in _registers)
            {
                Console.WriteLine($"{reg.Key}: {reg.Value}");
            }
        }
    }
}
