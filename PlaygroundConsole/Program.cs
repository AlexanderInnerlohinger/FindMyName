using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Permissions;
using System.Threading.Channels;

namespace PlaygroundConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var square = new Square(5);
            DisplayPolygon("Square", square);

            var triangle = new Triangle(5);
            DisplayPolygon("Triangle", triangle);

            var octagon = new Octagon(5);
            DisplayPolygon("Octagon", octagon);

            Console.ReadLine();
        }

        private static void DisplayPolygon(string polygonType, dynamic polygon)
        {
            Console.WriteLine($"For the polygon {polygonType} with {polygon.NumberOfSides} sides and a lenght of {polygon.SideLenght} the perimeter is {polygon.GetPerimeter()} and the area is {Math.Round(polygon.GetArea(),2)}.");
        }
    }
}
