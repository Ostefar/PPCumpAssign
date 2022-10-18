using primeGenerator;

namespace PPCumpAssign
{
    class Program
    {
        // INFO: at this moment, the printing of the primes is commented out in PrimeGenerator.class, in each of the functions.
        // This is done on purpose, to easier see the test results instead of millions of numbers. Remove the commentation, if you want too see the primes
        // Instead I've added a counter, so you can see that the same amount of primes is getting added in the different functions

        static void Main(string[] args)
        {
            bool isRunning = true;
            Console.WriteLine("Welcome to the prime generator");
            do
            {
                Console.WriteLine("Enter range start");
                var first = Console.ReadLine();
                Console.WriteLine("Enter range end");
                var last = Console.ReadLine();
                Console.WriteLine("----------------------------------------------------");

                RunOrder(Int32.Parse(first!), Int32.Parse(last!));

            } while (isRunning == true);
        }

        static void RunOrder(int first, int last)
        {
            //If you value your spare time, please do not remove comments from the next line
            //PrimeGenerator.GetPrimesSequential(first,last); // be aware, this will take a long time with big intervals (approx 250 seconds for 1-1000000)
            PrimeGenerator.GetPrimesSequentialLINQ(first, last);
            PrimeGenerator.GetPrimesParallelLINQ(first, last);
            PrimeGenerator.GetPrimesParallelThread(first, last);
        }

    }
}