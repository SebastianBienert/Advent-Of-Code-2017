using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Day15
{
    class Generator
    {
        private int factor;
        private long previousValue;
        private int multipleFactor;
        public Generator(int fac, int start, int mul)
        {
            factor = fac;
            previousValue = start;
            multipleFactor = mul;
        }

        public long getNextValue()
        {
            long nextValue = (previousValue * factor) % 2147483647;
            if (nextValue % multipleFactor != 0)
            {
                previousValue = nextValue;
                 return getNextValue();
            }
            previousValue = nextValue;
            return nextValue;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Generator A = new Generator(16807, 516, 4);
            Generator B = new Generator(48271, 190, 8);
            int mask = 0xFFFF;
            Console.WriteLine("A: ");
            int counter = 0;
            for (int i = 0; i < 5000000; i++)
            {
               // Console.WriteLine(A.getNextValue());
               // Console.WriteLine(B.getNextValue());
                long a = mask & A.getNextValue();
                long b = mask & B.getNextValue();
                if (a == b)
                {
                    counter++;
                }
            
            }
            Console.WriteLine(counter);
            Console.ReadKey();
        }
    }
}
