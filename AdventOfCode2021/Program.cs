using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace AdventOfCode2021
{
    class Program
    {
        private static List<string> m_inputList;
        private static string outputString;
        private static string path;

        static void Main(string[] args)
        {
            Console.WriteLine("Enter day of advent calender and hit enter.");
            int userInput = Convert.ToInt16(Console.ReadLine());

            switch (userInput)
            {
                case 1:
                    path = "day1.txt";
                    //path = "day1_training.txt";
                    m_inputList = Reader.Default.ReadInput(path);
                    outputString = SetIncrease();
                    break;
                case 2:
                    path = "day2.txt";
                    //path = "day2_training.txt";
                    m_inputList = Reader.Default.ReadInput(path);
                    outputString = Pathing();
                    break;
                case 3:
                    //path = "day3.txt";
                    path = "day3_training.txt";
                    m_inputList = Reader.Default.ReadInput(path);
                    outputString = CalculatePowerConsumption();
                    break;
                default:
                    break;
            }
            
            Writer.Default.WriteOutput(outputString, path);
        }

        private static string CalculatePowerConsumption()
        {
            string[] diagnosticReport = m_inputList.ToArray();
            string gammaRate = "";

            // Main loop for finding the gammaRate list
            for (int i = 0; i < diagnosticReport[0].Length; i++)
            {
                int[] entryColumn = new int[diagnosticReport.Length];
                int counterZero = 0;
                int counterOne = 0;

                for (int j = 0; j < entryColumn.Length; j++)
                {
                    entryColumn[j] = diagnosticReport[j][i] - '0';
                }

                foreach (int state in entryColumn)
                {
                    if (state == 0)
                        counterZero++;
                    else if (state == 1)
                        counterOne++;
                    else
                        throw new ArgumentException("Unknown state detected!", nameof(Exception));
                }

                if (counterZero > counterOne)
                    gammaRate += '0';
                else
                    gammaRate += '1';
            }

            string epsilonRate = "";
            foreach (char element in gammaRate)
            {
                if (element == '0')
                    epsilonRate += '1';
                else
                    epsilonRate += '0';
            }

            int integerValuesGammaRate = Convert.ToInt16(gammaRate, 2);
            int integerValuesEpsilonRate = Convert.ToInt16(epsilonRate, 2);

            return (integerValuesGammaRate * integerValuesEpsilonRate).ToString();
        }

        private static string Pathing()
        {
            Position position = new Position();

            foreach (string row in m_inputList)
            {
                string[] command = row.Split(" ");

                switch (command[0])
                {
                    case "forward":
                        position.AddHorizontal(Convert.ToInt16(command[1]));
                        break;
                    case "down":
                        position.AddVertikal(Convert.ToInt16(command[1]));
                        break;
                    case "up":
                        position.SubstractVertikal(Convert.ToInt16(command[1]));
                        break;
                    default:
                        throw new ArgumentException("Command not found!", nameof(Exception));
                }
            }

            return position.GetResult().ToString();
        }

        private static string SetIncrease()
        {
            int currentIndex = 0;
            int numberOfRows = m_inputList.Count;
            bool enoughMeasurements = true;
            int hasIncreased = 0;

            while (enoughMeasurements)
            {
                Set firstSet = new Set(Convert.ToInt16(m_inputList[currentIndex]),
                    Convert.ToInt16(m_inputList[currentIndex + 1]), Convert.ToInt16(m_inputList[currentIndex + 2]));
                Set secondSet = new Set(Convert.ToInt16(m_inputList[currentIndex + 1]),
                    Convert.ToInt16(m_inputList[currentIndex + 2]), Convert.ToInt16(m_inputList[currentIndex + 3]));


                if (secondSet.Sum() > firstSet.Sum())
                    hasIncreased++;

                currentIndex++;

                if (numberOfRows - currentIndex <= 3)
                    enoughMeasurements = false;
            }

            return hasIncreased.ToString();
        }
    }
}
