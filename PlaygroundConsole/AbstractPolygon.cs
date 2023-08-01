using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaygroundConsole
{
    public abstract class AbstractPolygon
    {
        public int NumberOfSides { get; set; }
        public int SideLenght { get; set; }

        public AbstractPolygon(int sides, int lenght)
        {
            NumberOfSides = sides;
            SideLenght = lenght;
        }

        public double GetPerimeter()
        {
            return NumberOfSides * SideLenght;
        }

        public abstract double GetArea();
    }
}
