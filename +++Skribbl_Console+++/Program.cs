using System;
using System.Collections.Generic;
using System.Linq;

namespace ___Skribbl_Console___
{
    internal class Program
    {
        private static Dictionary<double, string> m_DictMeasuredValues;


        static void Main(string[] args)
        {
            List<string> newList = new List<string>();

            for (int i = 0; i < 10; i++)
            {
                newList.Add($"item {i}");
            }

            List<string> preList = new List<string>();
            preList.Add("First item");

            List<string> postList = new List<string>();
            postList.Add("Last item");
            
            newList.RemoveRange(newList.Count-2, 2);
            
            newList.InsertRange(0, preList);
            newList.InsertRange(newList.Count, postList);
            
            Console.Read();
        }
    }

}