using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day17
{
    class Program
    {
        static void Main(string[] args)
        {
            int numberOfSteps = 343;

            List<int> list = new List<int>(50000000);
            list.Add(0);
            int currentPos = 0;
            int pos = 0;
           /* for (int i = 0; i < 2017; i++)
            {
                //list.Add(0);
                if(i != 0)   
                     pos = (currentPos + numberOfSteps) % (i + 1);
                list.Insert(pos + 1,i+1);
                currentPos = pos + 1;
                //printList(list);
                if (i % 1000000 == 0)
                {
                    Console.WriteLine(i);
                }
            }
            printList(list);*/
            int zero = 0;
            int value = 0;
            for (int i = 0; i < 50000000; i++)
            {
                //list.Add(0);
                if (i != 0)
                    pos = (currentPos + numberOfSteps) % (i + 1);

              /*  if (pos <= zero)
                {
                    zero++;
                }*/
                if (pos == zero)
                {
                    value = i + 1;
                }
               // list.Insert(pos + 1, i + 1);
                currentPos = pos + 1;
                //printList(list);
                if (i % 1000000 == 0)
                {
                    Console.WriteLine(i);
                }
            }

            int index = list.IndexOf(0);
            Console.WriteLine(value);
            Console.ReadKey();
        }

        static void printList(List<int> list)
        {
            foreach (var item in list)
            {
                Console.Write($"{item} ");
            }
            Console.WriteLine();
        }
    }
}
