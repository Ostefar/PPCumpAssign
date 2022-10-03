using primeGenerator;

namespace PPCumpAssign
{
    class Program
    {
        static void Main(string[] args)

        {
            RunOrder();
        }

        static void RunOrder()
        {
            PrimeGenerator.GetPrimesSequential(1000000,1000100);
            PrimeGenerator.GetPrimesParallel(100,500);
        }

    }
}