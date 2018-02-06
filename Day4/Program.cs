using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Day4
{
    class Program
    {
        static void Main(string[] args)
        {
          var data = File.ReadAllLines("D:/dane4.txt").Select(line => line.Split().Where(x => !String.IsNullOrWhiteSpace(x))).ToArray();
            int good = 0;
            int bad = 0;
            foreach (var line in data)
            {
                if (LineAngagram(line.ToArray()))
                {
                    bad++;
                }
                else
                {
                    good++;
                }

                
            }
            Console.WriteLine($"Zle: {bad}, dobre: {good}");
            Console.ReadKey();
        }

        public static bool LineAngagram(string[] line)
        {
            for (int i = 0; i < line.Count(); i++)
            {
                for (int j = i + 1; j < line.Count(); j++)
                {
                    var res = IsAnagram(line[i], line[j]);
                    if (res)
                    {
                        return true;
                    }
                }

            }
            return false;
        }

        public static bool IsAnagram(string s1, string s2)
        {
            if (string.IsNullOrEmpty(s1) || string.IsNullOrEmpty(s2))
                return false;
            if (s1.Length != s2.Length)
                return false;

            foreach (char c in s2)
            {
                int ix = s1.IndexOf(c);
                if (ix >= 0)
                    s1 = s1.Remove(ix, 1);
                else
                    return false;
            }

            return string.IsNullOrEmpty(s1);
        }
    }
}
