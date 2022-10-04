using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace primeGenerator
{
    public class PrimeGenerator
    {
        static readonly object theLock = new object();
        static long index = -1;
        public static List<long> GetPrimesSequential(long first, long last)
        {
            var sw = Stopwatch.StartNew();

            List<long> primes = new List<long>();

            Console.WriteLine("Sequential");
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
            // remove commentation for printing primes
            /*foreach (int prime in primes)
            {
                Console.Write(prime + " ");
            }*/
            sw.Stop();
            Console.WriteLine("");
            Console.WriteLine("Time = {0:f3} sec.", sw.ElapsedMilliseconds / 1000d);
            Console.WriteLine("----------------------------------------------------");
            return primes;
        }

        public static List<int> GetPrimesSequentialLINQ(int first, int last)
        {
            var sw = Stopwatch.StartNew();

            IEnumerable<int> range = Enumerable.Range(first, last - first);

            Console.WriteLine("Sequential LINQ");
            //Console.WriteLine($"The Prime Numbers between {first} and {last} are: ");

            List<int> primes = (from n in range
                         let w = (int)Math.Sqrt(n)
                         where Enumerable.Range(2, w).All((i) => n % i > 0)
                         select n).ToList();

            // remove commentation for printing primes
            /*foreach (int prime in primes)
            {
                Console.Write(prime + " ");
            }*/
            sw.Stop();
            Console.WriteLine("");
            Console.WriteLine("Time = {0:f3} sec.", sw.ElapsedMilliseconds / 1000d);
            Console.WriteLine("----------------------------------------------------");
            return primes.ToList();

        }
        public static List<int> GetPrimesParallelLINQ(int first, int last) 
            {
            var sw = Stopwatch.StartNew();

            Console.WriteLine("Parallel LINQ");
            //Console.WriteLine($"The Prime Numbers between {first} and {last} are: ");

            IEnumerable<int> range = Enumerable.Range(first, last);

            List<int> primes = (from n in range.AsParallel()
                          let w = (int)Math.Sqrt(n)
                          where Enumerable.Range(2, w).All((i) => n % i > 0)
                          select n).ToList();

            primes.Sort();

            // remove commentation for printing primes
            /* foreach (int prime in primes)
             {
                 Console.Write(prime + " ");
             }*/
            sw.Stop();
            Console.WriteLine("");
            Console.WriteLine("Time = {0:f3} sec.", sw.ElapsedMilliseconds / 1000d);
            Console.WriteLine("----------------------------------------------------");
            return primes.ToList();
        }

    }
}
