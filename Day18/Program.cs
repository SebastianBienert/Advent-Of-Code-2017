using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Day18
{
    class Generator
    {
        private Dictionary<string, long> registers = new Dictionary<string, long>();
        private HashSet<string> regHelper = new HashSet<string>();
        private List<string> data;
        public Queue<long> queue { get; set; }
        public Generator secGenerator { get; set; }
        public int ID { get; set; }

        public Generator(int id)
        {
           data = File.ReadAllLines("D:/data18.txt").ToList();
            var regex = new Regex(@"[a-z]");
 
            //INIT REGISTERS
            foreach (var line in data)
            {
                var splited = line.Split();
                if (regHelper.Add(splited[1]) && regex.IsMatch(splited[1]))
                {
                    registers.Add(splited[1], 0);
                }
                if (splited.Length > 2 && regHelper.Add(splited[2]) && regex.IsMatch(splited[2]))
                {
                    registers.Add(splited[2], 0);
                }
            }
            registers["p"] = id;
            queue = new Queue<long>();
            ID = id;
        }

        public int Start()
        {
            long lastPlayed = 0;
            int send = 0;
            for (int i = 0; i < data.Count; i++)
            {
                var line = data[i];
                var splited = line.Split();
                if (splited[0] == "set")
                {
                    long val;
                    val = registers.ContainsKey(splited[2]) ? registers[splited[2]] : Convert.ToInt64(splited[2]);
                    registers[splited[1]] = val;
                }
                else if (splited[0] == "add")
                {
                    long val;
                    val = registers.ContainsKey(splited[2]) ? registers[splited[2]] : Convert.ToInt64(splited[2]);
                    registers[splited[1]] += val;
                }
                else if (splited[0] == "mul")
                {
                    long val;
                    val = registers.ContainsKey(splited[2]) ? registers[splited[2]] : Convert.ToInt64(splited[2]);

                    registers[splited[1]] *= val;
                }
                else if (splited[0] == "mod")
                {
                    long val;
                    val = registers.ContainsKey(splited[2]) ? registers[splited[2]] : Convert.ToInt64(splited[2]);
                    registers[splited[1]] = registers[splited[1]] % val;
                }
                else if (splited[0] == "snd")
                {
                     long val;
                     val = registers.ContainsKey(splited[1]) ? registers[splited[1]] : Convert.ToInt64(splited[1]);
                    send++;
                    Console.WriteLine($"Send from {ID} to {secGenerator.ID}: {val}, nr {send}");
                    secGenerator.queue.Enqueue(val);
                    
                }
                else if (splited[0] == "rcv")
                {
                    while (this.queue.Count == 0)
                    {
                        
                    }
                    if (this.queue.Count > 0)
                    {
                        long temp = this.queue.Dequeue();
                        Console.WriteLine($"Received from {secGenerator.ID} to {ID}: {temp}");
                        registers[splited[1]] = temp;
                    }
  
                }
                else if (splited[0] == "jgz")
                {
                    long val;
                    val = registers.ContainsKey(splited[2]) ? registers[splited[2]] : Convert.ToInt64(splited[2]);

                    long reg;
                    reg = registers.ContainsKey(splited[1]) ? registers[splited[1]] : Convert.ToInt64(splited[1]);
                    if (reg > 0)
                    {
                        i += Convert.ToInt32(val);
                        i--;
                        continue;
                    }
                }
            }
            return send;
        }

    }


    class Program
    {
        static void Main(string[] args)
        {

            var data = File.ReadAllLines("D:/data18.txt").ToList();
            long recovered = 0;
            long lastPlayed = 0;
            Generator A = new Generator(0);
            Generator B = new Generator(1);
            A.secGenerator = B;
            B.secGenerator = A;
            int aResult = 0;
            int bResult = 0;
            Thread aThread = new Thread(() =>
            {
                aResult = A.Start();
                Console.WriteLine($"A: {aResult}");
            });

            Thread bThread = new Thread(() =>
            {
               bResult = B.Start();
                Console.WriteLine($"B: {bResult}");
            });

            aThread.Start();
            bThread.Start();


           
            Console.ReadKey();
        }
    }
}
