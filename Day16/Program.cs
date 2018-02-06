using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Day16
{
    class Program
    {
        static void Main(string[] args)
        {
            var data = File.ReadAllText("D:/data16.txt").Split(',').ToList();
            Regex exchange = new Regex(@"x\d+(\/)\d+$");
            Regex spin = new Regex(@"s\d+$");
            Regex partner = new Regex(@"p[a-p]\/[a-p]$");

            List<char> start = "abcdefghijklmnop".ToList();

            HashSet<string> set = new HashSet<string>();

            for (int i = 0; i < 1000000000; i++)
            {
                foreach (var wordt in data)
                {
                    var word = wordt.Trim();
                    if (spin.IsMatch(word))
                    {
                        //  Console.WriteLine("SPIN");
                        int index = Convert.ToInt32(word.Remove(0, 1));
                        var end = start.GetRange(start.Count - index, index);
                        start.RemoveRange(start.Count - index, index);
                        start.InsertRange(0, end);
                    }
                    else if (exchange.IsMatch(word))
                    {
                        //  Console.WriteLine("EXCHANGE");
                        var s = word.Remove(0, 1);
                        var numbers = s.Split('/').Select(x => Convert.ToInt32(x)).ToList();
                        char temp = start[numbers[0]];
                        start[numbers[0]] = start[numbers[1]];
                        start[numbers[1]] = temp;

                    }
                    else if (partner.IsMatch(word))
                    {
                        // Console.WriteLine("PARTNER");
                        var chars = word.Remove(0, 1).Split('/').Select(x => Convert.ToChar(x)).ToList();
                        int first = start.IndexOf(chars[0]);
                        int second = start.IndexOf(chars[1]);
                        char temp = start[first];
                        start[first] = start[second];
                        start[second] = temp;
                    }
                }

                string result = new string(start.ToArray());
                if (i >= 39 && i <=41)
                {
                    Console.WriteLine(start.ToArray());
                }
                if (!set.Add(result))
                {
                    Console.WriteLine(1000000000 % i);
                    break;
                }
            }
            
            Console.WriteLine(start.ToArray());
            Console.ReadKey();
        }
    }
}
