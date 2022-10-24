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
        private static readonly ConcurrentQueue<int> primes = new ConcurrentQueue<int>();

        public static List<long> GetPrimesSequential(long first, long last)
        {
            var sw = Stopwatch.StartNew();

            List<long> result = new List<long>();

            Console.WriteLine("Sequential");

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
                    result.Add(i);
                }
            }
            // remove commentation for printing primes
            /*foreach (int prime in primes)
            {
                Console.Write(prime + " ");
            }*/
            sw.Stop();
            Console.WriteLine("");
            Console.WriteLine("Number of primes added to the list " + result.Count);
            Console.WriteLine("Time = {0:f3} sec.", sw.ElapsedMilliseconds / 1000d);
            Console.WriteLine("----------------------------------------------------");
            return result;
        }

        private static bool IsPrime(int number)
        {
            if (number < 2) return false;
            if (number == 2 || number == 3) return true;
            if (number % 2 == 0 || number % 3 == 0) return false;
            for (int i = 5; i * i <= number; i += 6)
            {
                if (number % i == 0 || number % (i + 2) == 0)
                    return false;
            }
            return true;
        }

        public static List<int> GetPrimesSequentialLINQ(int first, int last)
        {
            Console.WriteLine("Sequential LINQ");

            var sw = Stopwatch.StartNew();

            IEnumerable<int> range = Enumerable.Range(first, (last - first));

            List<int> result = range.Where(x => IsPrime(x)).Select(x => x).ToList();

            // remove commentation for printing primes
            /*foreach (int results in result)
            {
                Console.Write(primes + " ");
            }*/

            sw.Stop();
            Console.WriteLine("");
            Console.WriteLine("Number of primes added to the list " + result.Count);
            Console.WriteLine("Time = {0:f3} sec.", sw.ElapsedMilliseconds / 1000d);
            Console.WriteLine("----------------------------------------------------");
            return result.ToList();

        }
        public static List<int> GetPrimesParallelLINQ(int first, int last)
        {
            Console.WriteLine("Parallel LINQ");
            var sw = Stopwatch.StartNew();

            IEnumerable<int> range = Enumerable.Range(first, (last - first));

            List<int> result = range.AsParallel().Where(x => IsPrime(x)).Select(x => x).OrderBy(x => x).ToList();

            // remove commentation for printing primes
            // primes3.Sort();
            /*foreach (int results in result)
             {
                 Console.Write(results + " ");
             }*/

            sw.Stop();
            Console.WriteLine("");
            Console.WriteLine("Number of primes added to the list " + result.Count);
            Console.WriteLine("Time = {0:f3} sec.", sw.ElapsedMilliseconds / 1000d);
            Console.WriteLine("----------------------------------------------------");
            return result.ToList();

        }

   
        private static void IsPrimeThread(int start, int range)
        {
            var isPrime = true;
            var end = start + range;
                for (var i = start; i < end; i++)
                {
                if (i < 2) isPrime = false;
                if (i == 2 || i == 3) isPrime = true;
                if (i % 2 == 0 || i % 3 == 0) isPrime = false;
                for (int j = 5; j * j <= i; j += 6)
                    {
                        if (i % j == 0 || i % (j + 2) == 0)
                        {
                            isPrime = false;
                            break;
                        }
                    }
                    if (isPrime)
                    {
                        primes.Enqueue(i);
                    }
                    isPrime = true;
                }
        }


        public static List<int> GetPrimesParallelThread(int first, int last)
        {
            Console.WriteLine("Parallel threading");

            var sw = Stopwatch.StartNew();

            var threadCount = Environment.ProcessorCount;
            var threads = new Thread[threadCount];
            var range = (last - first) / threadCount;
            var start = first;

            for (var i = 0; i < threadCount - 1; i++)
            {
                var startl = start;
                threads[i] = new Thread(() => IsPrimeThread(startl, range));
                start += range;
                threads[i].Start();
            }
            threads[threadCount - 1] = new Thread(() => IsPrimeThread(start, range + (last - first) % threadCount));
            threads[threadCount - 1].Start();

            for (var i = 0; i < threadCount; i++)
                threads[i].Join();

            // Move from primes concurrentqueue to list, to be able to clear primes to avoid double filling
            List<int> result = primes.ToList();

            primes.Clear();

            /*
            foreach (int results in result)
            {
                Console.Write(results + " ");
            }
            */

            sw.Stop();
            Console.WriteLine("");
            Console.WriteLine("Number of primes added to the list " + result.Count);
            Console.WriteLine("Time = {0:f3} sec.", sw.ElapsedMilliseconds / 1000d);
            Console.WriteLine("----------------------------------------------------");
           
            return result.ToList();

        }
    }
}
