using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day25
{
    enum Direction
    {
        LEFT,
        RIGHT
    }

    class Settings
    {
        public int WriteValue { get; set; }
        public Direction Move { get; set; }
        public string ContinueWith { get; set; }
    }



    class State
    {
        public string Name { get; set; }
        public Settings ZeroSetting { get; set; }
        public Settings OneSetting { get; set; }
        public State(Settings zero, Settings one, string name)
        {
            Name = name;
            ZeroSetting = zero;
            OneSetting = one;
        }


        public KeyValuePair<string, int> Move(int[] tape, int index)
        {
            Settings currSettings = null;
            if (tape[index] == 0)
                currSettings = ZeroSetting;
            else
                currSettings = OneSetting;

            tape[index] = currSettings.WriteValue;
            if (currSettings.Move == Direction.RIGHT)
                index++;
            else
                index--;

            return new KeyValuePair<string, int>(currSettings.ContinueWith, index);

        }

        
    }


    public static class MyExtensions
    {
        public static int sumResult(this int[] src)
        {
            int sum = 0;
            for (int i = 0; i < src.Length; i++)
                sum += src[i];

            return sum;
        }
    }

    class Program
    {
        private static int SIZE = 100000001;
        private static List<State> _states = new List<State>();
        static void Main(string[] args)
        {
            var tape = new int[SIZE];
            var middle = SIZE / 2;
            
            _states.AddRange(ReadFromFile("D:/data25.txt"));

            var currentPos = middle;
            var currentState = _states.Where(x => x.Name == "A").FirstOrDefault();
            for (int i = 0; i < 12667664; i++)
            {
                var move = currentState.Move(tape, currentPos);
                currentState = _states.Where(x => x.Name == move.Key).FirstOrDefault();
                currentPos = move.Value;
              //  printTape(tape, currentPos);
            }

            Console.WriteLine(tape.sumResult());

            Console.ReadKey();
        }

        public static List<State> ReadFromFile(string path)
        {
            var data = File.ReadAllLines(path).ToList().SkipWhile(line => !line.Contains("In state")).ToList();
            var list = new List<State>();
            for (int i = 0; i < 6; i++)
            {
                var temp = data.Take(9).ToList();
                data = data.Skip(4).SkipWhile(line => !line.Contains("In state")).ToList();
                State newState = CreateState(temp);
                list.Add(newState);

            }
            return list;
        }


        public static State CreateState(List<string> data)
        {
            var stateName = data[0].Split(' ').Last().TrimEnd(':');
            var zeroSettings = data.SkipWhile(line => !line.Contains("If the current")).Skip(1).Take(3).ToList();
            data.RemoveRange(0,4);
            var oneSettings = data.SkipWhile(line => !line.Contains("If the current")).Skip(1).Take(3).ToList();


            var zeroSettingObj = CreateSettings(zeroSettings);
            var oneSettingOjb = CreateSettings(oneSettings);

            return new State(zeroSettingObj, oneSettingOjb, stateName);
        }

        public static Settings CreateSettings(List<string> data)
        {
            var writeValue = Convert.ToInt32(data[0].Split(' ').Last().TrimEnd('.'));
            var dir = data[1].Split(' ').Last().TrimEnd('.');
            var continueWith = data[2].Split(' ').Last().TrimEnd('.');

            return new Settings()
            {
                ContinueWith = continueWith,
                WriteValue = writeValue,
                Move = dir == "right" ? Direction.RIGHT : Direction.LEFT
            };


        }


        public static void printTape(int[] tp, int curIndx)
        {
            for(int i = 0; i < tp.Length; i++)
            { 
                if(i == curIndx)
                    Console.Write($"[{tp[i]}] ");
                else
                    Console.Write(tp[i] + " ");
            }
            Console.WriteLine();
        }
    }
}
