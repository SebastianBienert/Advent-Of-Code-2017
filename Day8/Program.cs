using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day8
{
    class Program
    {
        private static Dictionary<string,int> registers = new Dictionary<string, int>();
        static void Main(string[] args)
        {
            var set = new HashSet<string>();
            var instructions = File.ReadAllLines("D:/data8.txt");

            //ADD ALL REGISTERS
            foreach (var item in instructions)
            {
                var lineSplit = item.Split(' ');
                var firstRegName = lineSplit[0];
                var secondRegName = lineSplit[4];

                if(set.Add(firstRegName))
                    registers.Add(firstRegName, 0);

                if(set.Add(secondRegName))
                    registers.Add(secondRegName, 0);
            }
            printDict(registers);


            int max = Int32.MinValue;
            
            foreach (var instr in instructions)
            {
                var lineSplit = instr.Split(' ');
                var condition = lineSplit[5];
                var secondRegName = lineSplit[4];
                var conditionValue = Convert.ToInt32(lineSplit[6]);
                bool conditionResult = false;

                if (condition == ">")
                {
                    if (registers[secondRegName] > (conditionValue))
                        conditionResult = true;
                }
                else if (condition == "<")
                {
                    if (registers[secondRegName] < (conditionValue))
                        conditionResult = true;
                }
                else if (condition == ">=")
                {
                    if (registers[secondRegName] >= (conditionValue))
                        conditionResult = true;
                }
                else if (condition == "<=")
                {
                    if (registers[secondRegName] <= (conditionValue))
                        conditionResult = true;
                }
                else if (condition == "==")
                {
                    if (registers[secondRegName] == (conditionValue))
                        conditionResult = true;
                }
                else if (condition == "!=")
                {
                    if (registers[secondRegName] != (conditionValue))
                        conditionResult = true;
                }
                else
                {
                    Console.WriteLine("ERROR");
                }


                if (conditionResult == true)
                {
                    if (lineSplit[1] == "inc")
                    {
                        registers[lineSplit[0]] += Convert.ToInt32(lineSplit[2]);
                    }
                    else
                    {
                        registers[lineSplit[0]] -= Convert.ToInt32(lineSplit[2]);
                    }
                }

                if (getHighestValue(registers) > max)
                {
                    max = getHighestValue(registers);
                }
            }

            printDict(registers);

            Console.WriteLine($"MaxValue:{max}");
            Console.ReadKey();

        }

        private static int getHighestValue(Dictionary<string, int> dic)
        {
            int max = Int32.MinValue;
            KeyValuePair<string, int> maxPair = new KeyValuePair<string, int>();
            foreach (var keyValuePair in registers)
            {
                if (keyValuePair.Value > max)
                {
                    maxPair = keyValuePair;
                    max = keyValuePair.Value;
                }
            }

            return max;
        } 

        private static void printDict(Dictionary<string, int> dic)
        {
            foreach (var item in dic)
            {
                Console.WriteLine($"{item.Key}: {item.Value}");
            }
        }
    }
}
