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
        public static List<long> GetPrimesSequential(long first, long last)
        {
            var sw = Stopwatch.StartNew();

            List<long> result = new List<long>();

            Console.WriteLine("Sequential");
            //Console.WriteLine($"The Prime Numbers between {first} and {last} are: ");
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

        private static bool IsPrime(long number)
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
            var sw = Stopwatch.StartNew();

            IEnumerable<int> range = Enumerable.Range(first, last - first);

            Console.WriteLine("Sequential LINQ");
            //Console.WriteLine($"The Prime Numbers between {first} and {last} are: ");

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
            var sw = Stopwatch.StartNew();

            Console.WriteLine("Parallel LINQ");
            //Console.WriteLine($"The Prime Numbers between {first} and {last} are: ");

            IEnumerable<int> range = Enumerable.Range(first, last - first);

            List<int> result = range.AsParallel().Where(x => IsPrime(x)).Select(x => x).OrderBy(x => x).ToList()

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

            //maybe create another version using threading, showing the steps beneath plinq
        }

    }
}
