using System;

namespace WorkingWithNulls
{
    class Program
    {
        static void Main(string[] args)
        {
            PlayerCharakter sarah = new PlayerCharakter(new DiamondSkinDefence())
            {
                Name = "Sarah"
            };

            PlayerCharakter amrit = new PlayerCharakter(SpecialDefence.Null)
            {
                Name = "Amrit"
            };

            PlayerCharakter alex = new PlayerCharakter(SpecialDefence.Null)
            {
                Name = "Alex"
            };

            sarah.Hit(10);
            amrit.Hit(10);
            alex.Hit(10);

            Console.ReadLine();
        }
    }
}
