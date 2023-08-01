using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaygroundConsole
{
    class ConcretePolygon
    {
        public int NumberOfSides { get; set; }
        public int SideLenght { get; set; }
        
        public ConcretePolygon(int sides, int lenght)
        {
            NumberOfSides = sides;
            SideLenght = lenght;
        }

        public double GetPerimeter()
        {
            return NumberOfSides * SideLenght;
        }

        public virtual double GetArea()
        {
            throw new NotImplementedException();
        }
    }
}
