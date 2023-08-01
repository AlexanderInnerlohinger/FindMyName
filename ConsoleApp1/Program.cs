using System;
using System.Collections.Generic;
using System.Threading;

namespace ConsoleApp1
{
    class Program
    {
        private static List<Entity> entityList = new List<Entity>();
        private static Random rng = new Random();

        static void Main(string[] args)
        {
            // Generation
            GeneratePlayer();
            GenerateEnemy();

            //ExampleAttack();
            while (true)
            {
                AwaitInput();
            }
            Console.ReadLine();
        }

        private static void AwaitInput()
        {
            var input = Console.ReadKey();
            Console.Clear();

            switch (input.Key)
            {
                case ConsoleKey.A:
                    Console.WriteLine("Performing attack!");
                    break;
                case ConsoleKey.M:
                    Console.WriteLine("Performing move!");
                    break;
                default:
                    Console.WriteLine($"{input.Key} is an invalid input!");
                    break;
            }
        }

        private static void ExampleAttack()
        {
            bool combatOngoing = true;
            while (combatOngoing)
            {
                double damageDealt = Attack(entityList[0], entityList[1]);
                Console.WriteLine($"{entityList[0].Name}({entityList[0].Weapon.WeaponType}) hit {entityList[1].Name}({entityList[1].Armor.ArmorType}) for {damageDealt} Damage.");

                Shuffle(entityList);

                combatOngoing = !CheckForDeletion();
            }

            Console.WriteLine("Combat ended.");
        }

        private static bool CheckForDeletion()
        {
            foreach (Entity entity in entityList)
            {
                if (entity.Health < 1)
                {
                    entityList.Remove(entity);
                    return true;
                }
            }

            return false;
        }

        private static double Attack(Entity attacker, Entity defender)
        {
            double randomizedDamage = RollDamage(attacker.Weapon);
            double damageDealt = Math.Round(Math.Max(randomizedDamage - defender.Armor.ArmorAmount, 0), 2);
            defender.Health -= damageDealt;
            return damageDealt;
        }

        private static double RollDamage(Weapon attackerWeapon)
        {
            Random ran = new Random();
            double randomized = ran.Next(80,120);
            return attackerWeapon.DamageAmount * (randomized / 100);
        }

        private static void GenerateEnemy()
        {
            Entity enemy = new Enemy();
            enemy.Name = "Enemy";
            enemy.Health = 100;

            enemy.Armor = new Armor("None", Armor.ArmorTypeEnum.None, 0);
            enemy.Weapon = new Weapon("Fists", Weapon.WeaponTypeEnum.Unarmed, 5);

            entityList.Add(enemy);
        }

        private static void GeneratePlayer()
        {
            Entity player = new Player();
            player.Name = "Player";
            player.Health = 100;

            player.Armor = new Armor("Cloth Armor",Armor.ArmorTypeEnum.Light, 5);
            player.Weapon = new Weapon("Short Sword", Weapon.WeaponTypeEnum.Sharp, 15);

            entityList.Add(player);
        }

        private static void Shuffle<T>(IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        private static void GetExperience()
        {

        }
    }
}
