using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Math = System.Math;

namespace PlaygroundConsole
{
    class Octagon : IRegularPolygon
    {
        public int NumberOfSides { get; set; }
        public int SideLenght { get; set; }

        public Octagon(int lenght)
        {
            NumberOfSides = 8;
            SideLenght = lenght;
        }
        public double GetArea()
        {
            return SideLenght * SideLenght * (2 + 2 * Math.Sqrt(2));
        }

        public double GetPerimeter()
        {
            return NumberOfSides * SideLenght;
        }
    }
}
