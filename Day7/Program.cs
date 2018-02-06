using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day7
{
    class Program
    {
        private static Dictionary<string, List<string>> myDic = new Dictionary<string, List<string>>();
        private static Dictionary<string, int> weights = new Dictionary<string, int>();
        private static Dictionary<string, int> weightsSum = new Dictionary<string, int>();
        static void Main(string[] args)
        {
            var data = File.ReadAllLines("D:/data7.txt");

            var parents = new List<string>();
            foreach (var line in data)
            {
                List<string> childs = new List<string>();
                var subsets = line.Split(' ');

                if (subsets.Length > 2)
                {
                    parents.Add(subsets[0]);
                    for (int i = 3; i < subsets.Length; i++)
                    {
                        childs.Add(subsets[i].TrimEnd(','));
                    }
                }

                myDic.Add(subsets[0], childs);
                weights.Add(subsets[0], Convert.ToInt32(subsets[1].Trim('(').TrimEnd(')')));
            }
            string root = "";

            foreach (var parent in parents)
            {
                if (!containsElement(parent))
                    root = parent;
            }
            Console.WriteLine(root);

 

                foreach (var child in myDic[root])
                {
                    weightsSum[child] = computeWeight(child);
                }




         //   PrintDic(weights);
            Console.WriteLine();
           // PrintDic(weightsSum);

            foreach (var item in weights)
            {
                if (!checkIfBalanced(item.Key))
                {
                    Console.WriteLine(item.Key);
                    foreach (var child in myDic[item.Key])
                    {
                        Console.WriteLine(child + " " +weightsSum[child]);
                    }
                }
            }


            Console.ReadKey();
        }

        private static bool checkIfBalanced(string index)
        {
            if (myDic[index].Count == 0)
                return true;

            int weight = weightsSum[ (myDic[index][0]) ];
            int temp;
            foreach (var child in myDic[index])
            {
                temp = weightsSum[child];
                if (weight != temp)
                   return false;
            }
            return true;
        }

        private static void PrintDic(Dictionary<string, int> dic)
        {
            foreach (var item in dic)
            {
                Console.WriteLine($"{item.Key}: {item.Value}");
            }
        } 

        private static int computeWeight(string index)
        {
            int sum = 0;
            if (myDic[index].Count == 0)
            {
                weightsSum[index] = weights[index];
                return weights[index];
            }


            foreach (var child in myDic[index])
            {
                sum += computeWeight(child);
            }
            sum += weights[index];
            weightsSum[index] = sum;
            return sum;
        }

        static bool containsElement(string element)
        {
            foreach (var item in myDic)
            {
                if (item.Value.Contains(element))
                    return true;
            }
            return false;
        }



    }
}
