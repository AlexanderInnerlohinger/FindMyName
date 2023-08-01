namespace FermatPrimzahltest
{
    internal class Program
    {
        static Random random = new Random();

        static void Main(string[] args)
        {
            Console.WriteLine("Bitte ungerade Zahl eintippen: ");
            int n = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine(IsPrime(n));
            Console.Read();
        }

        static bool IsPrime(int n)
        {
            // declare variables
            int exponent = n - 1;
            int s = 0;

            while (exponent % 2 == 0)
            {
                exponent /= 2;
                s += 1;
            }

            int a = random.Next(2, n - 1);
            double x = Math.Pow(a, exponent);
            x = x % n;
            if (x == 1 || x == n - 1)
                return true;

            while (s > 1)
            {
                x = Math.Pow(x, 2);
                x = x % n;

                if (x == 1)
                {
                    return false;
                }

                if (x == n - 1)
                {
                    return true;
                }
                s -= 1;
            }

            return false;
        }

    }
}