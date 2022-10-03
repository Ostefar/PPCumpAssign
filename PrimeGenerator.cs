using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace primeGenerator
{
    public class PrimeGenerator
    {
        public static List<long> GetPrimesSequential(long first, long last)
        {
            var sw = Stopwatch.StartNew();

            List<long> primes = new List<long>();

            Console.WriteLine($"The Prime Numbers between {first} and {last} are: ");
            for (long i = first; i <= last; i++)
            {
                int counter = 0;
                for (int j = 2; j <= i / 2; j++)
                {
                    if (i % j == 0)
                    {
                        counter++;
                        break;
                    }
                }

                if (counter == 0 && i != 1)
                {
                    primes.Add(i);
                }
            }
            foreach (object prime in primes)
            {
                Console.Write(prime + " ");
            }
            sw.Stop();
            Console.WriteLine("");
            Console.WriteLine("Time = {0:f3} sec.", sw.ElapsedMilliseconds / 1000d);
            return primes;
        }

        public static List<long> GetPrimesParallel(long first, long last) 
        {
            List<long> primes = new List<long>();

            return primes;
        }

    }
}
