using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day24
{
    public class MyComparer : IEqualityComparer<List<KeyValuePair<int, int>>>
    {
        private KPComparer _kpComparer = new KPComparer();

        public bool Equals(List<KeyValuePair<int, int>> x, List<KeyValuePair<int, int>> y)
        {
            if (x.Count != y.Count)
                return false;

            for (int i = 0; i < x.Count; i++)
            {
                if (!keyPairComparer(x[i], y[i]))
                    return false;
            }

            return true;
        }

        private bool keyPairComparer(KeyValuePair<int, int> x, KeyValuePair<int, int> y)
        {
            return _kpComparer.Equals(x, y);
        }

        public int GetHashCode(List<KeyValuePair<int, int>> obj)
        {
            unchecked
            {
                int hash = 19;
                foreach (var foo in obj)
                {
                    hash = hash * 31 + foo.GetHashCode();
                }
                return hash;
            }
        }
    }

    public class KPComparer : IEqualityComparer<KeyValuePair<int, int>>
    {
        public bool Equals(KeyValuePair<int, int> x, KeyValuePair<int, int> y)
        {
            if (x.Value == y.Value && x.Key == y.Key)
                return true;
            else if (x.Value == y.Key && x.Key == y.Value)
                return true;

            return false;
        }

        public int GetHashCode(KeyValuePair<int, int> obj)
        {
            return 31 * obj.Value + 31 * obj.Key;
        }
    }

    class Program
    {
        public static MyComparer _comparer = new MyComparer();
        public static KPComparer _keyComparer = new KPComparer();

        static void Main(string[] args)
        {
            var bridges = File.ReadAllLines($"D:/data24.txt")
                .Select(
                    line =>
                        new KeyValuePair<int, int>(Convert.ToInt32(line.Split('/')[0]),
                            Convert.ToInt32(line.Split('/')[1]))).ToList();

            var startBrdiges = bridges.Where(x => x.Key == 0).ToList();
            var stack = new Stack<KeyValuePair<int, int>>();
            var path = new List<KeyValuePair<int, int>>();
            var allPaths = new HashSet<List<KeyValuePair<int, int>>>(_comparer);
            bool zeroNeigh = true;


            int bestPathValue = 0;
            List<KeyValuePair<int, int>> bestPath = null;
            for (int i = 0; i < startBrdiges.Count(); i++)
            {
                stack = new Stack<KeyValuePair<int, int>>();
                path = new List<KeyValuePair<int, int>>();

                var current = startBrdiges[i];
                path.Add(current);

                var value = calculatePathsValue(path);
                if (value > bestPathValue)
                {
                    bestPathValue = value;
                    bestPath = new List<KeyValuePair<int, int>>(path);
                }
                allPaths.Add(new List<KeyValuePair<int, int>>(path));
                do
                {
                    zeroNeigh = true;
                    foreach (var bridge in bridges.Except(path, _keyComparer))
                    {
                        if (current.Value == bridge.Key && !(current.Value == bridge.Key && current.Key == bridge.Value))
                        {
                            var newPath = new List<KeyValuePair<int, int>>(path);
                            newPath.Add(bridge);
                            if (allPaths.Contains(newPath))
                            {
                                continue;
                            }
                            stack.Push(bridge);
                            value = calculatePathsValue(newPath);
                            if (value > bestPathValue)
                            {
                                bestPathValue = value;
                                bestPath = new List<KeyValuePair<int, int>>(newPath);
                            }
                            zeroNeigh = false;
                        }
                        else if (current.Value == bridge.Value &&
                                 !(current.Value == bridge.Key && current.Key == bridge.Value))
                        {
                            var newBridge = new KeyValuePair<int, int>(bridge.Value, bridge.Key);
                            var newPath = new List<KeyValuePair<int, int>>(path);
                            newPath.Add(newBridge);
                            if (allPaths.Contains(newPath))
                            {
                                continue;
                            }
                            stack.Push(newBridge);
                            value = calculatePathsValue(newPath);
                            if (value > bestPathValue)
                            {
                                bestPathValue = value;
                                bestPath = new List<KeyValuePair<int, int>>(newPath);
                            }
                            zeroNeigh = false;
                        }
                    }
                    if (zeroNeigh)
                    {
                        path.RemoveAt(path.Count - 1);
                        if (path.Count == 0)
                        {
                            break;
                        }
                        current = path[path.Count - 1];
                        continue;
                    }

                    if (stack.Count == 0)
                        break;

                    current = stack.Pop();
                    path.Add(current);
                    allPaths.Add(new List<KeyValuePair<int, int>>(path));

                } while (true);
            }
            Console.WriteLine();
            printPath(bestPath);
            Console.WriteLine($"BEST PATH: {bestPathValue}");
            partTwo(allPaths);
            Console.ReadKey();


        }


        public static void partTwo(HashSet<List<KeyValuePair<int, int>>> all )
        {
            var maxLength = all.Select(list => list.Count).Max();
            int max = 0;
            List<KeyValuePair<int, int>> strongest = null;
            foreach (var path in all.Where(list => list.Count == maxLength))
            {
                var val = calculatePathsValue(path);
                if (val > max)
                {
                    max = val;
                    strongest = path;
                }

            }
            Console.WriteLine($"{strongest} \n {max}");

        }

        public static int calculatePathsValue(List<KeyValuePair<int, int>> path)
        {
            int sum = 0;
            foreach (var pair in path)
            {
                sum += pair.Value + pair.Key;
            }
            return sum;
        }

        public static void printPath(List<KeyValuePair<int, int>> pth)
        {
            foreach (var brd in pth)
            {
                Console.Write($"{brd.Key} / {brd.Value} ->");
            }
            Console.WriteLine();
        }

        public static int BridgeValue(KeyValuePair<int, int> bridge)
        {
            return bridge.Value + bridge.Key;
        }


        public static int Search(List<KeyValuePair<int, int>> e, int cur = 0, int strength = 0)
        {
            return e.Where(x => x.Key == cur || x.Value == cur).
                Select(x =>
                {
                    var newDic = new List<KeyValuePair<int, int>>(e);
                    newDic.Remove(x);
                    return Search(newDic, x.Key == cur ? x.Value : x.Key, strength + x.Key + x.Value);
                })
                .Concat(Enumerable.Repeat(strength, 1)).Max();
    }
}
}

