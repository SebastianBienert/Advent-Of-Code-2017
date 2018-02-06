using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Day12
{
    class Program
    {
        private static Dictionary<int, List<int>> dic = new Dictionary<int, List<int>>();
        private static HashSet<int> set;
        private static HashSet<HashSet<int>> allSets;

        static void Main(string[] args)
        {
            var data = File.ReadAllLines("D:/data12.txt").ToList();
            int n = data.Count;
            bool[] visited = new bool[n];
            int j = 0;
            foreach (var line in data)
            {
                var parsed =
                    line.Split(' ').Select(word => word.TrimEnd(',')).Where(x => Regex.IsMatch(x, @"^\d+$")).Select(s => Convert.ToInt32(s)).ToList();
                var list = new List<int>();
                for (int i = 1; i < parsed.Count; i++)
                {
                    list.Add(parsed[i]);
                }
                dic.Add(parsed[0], list);
                visited[j] = false;
                j++;
            }



            allSets = new HashSet<HashSet<int>>();
            for (int i = 0; i < n; i++)
            {
                set = new HashSet<int>();
                recSol(dic[i]);
                addIfNewSet(set);
            }


           // printList(set);

            Console.WriteLine(allSets.Count);
            Console.ReadKey();

        }

        private static void addIfNewSet(HashSet<int> paramSet)
        {
            foreach (var set in allSets)
            {
                if (set.Overlaps(paramSet))
                    return;
            }
            allSets.Add(paramSet);
        }

        private static void recSol(List<int> list)
        {
            foreach (var it in list)
            {
                if(set.Add(it) )
                    recSol(dic[it]);
            }
        }

        private static void printDict(Dictionary<int, List<int>> dic)
        {
            foreach (var item in dic)
            {
                Console.Write($"{item.Key}: ");
                foreach (var it in item.Value)
                {
                    Console.Write($"{it}, ");
                }
                Console.WriteLine();
            }
        }

        private static void printList(HashSet<int> set)
        {
            Console.Write("List: ");
            foreach (var item in set)
            {
                Console.Write($"{item} ");
            }
            Console.WriteLine();
        }

        private static void addToSet(List<int> list, HashSet<int> set)
        {
            foreach (var item in list)
            {
                set.Add(item);
            }
        }

    }
}
