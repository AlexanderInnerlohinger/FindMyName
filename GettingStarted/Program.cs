using System;

namespace GettingStarted
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            foreach (var s in args)
            {
                Console.WriteLine(s);
            }

            string[] interestingAnimals;
            interestingAnimals = new string[10];
            interestingAnimals[1] = "Monkey";

            Console.ReadLine();
        }

        static float FahrenheitToCelsius(float temperatureFahrenheit)
        {
            var temperatureCelsius = (temperatureFahrenheit - 32) * 1.8f;
            return temperatureCelsius;
        }
    }
}
