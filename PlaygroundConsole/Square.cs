using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaygroundConsole
{
    class Square : ConcretePolygon
    {
        public Square(int lenght) : base(4, lenght) { }

        public override double GetArea()
        {
            return SideLenght * SideLenght;
        }
    }
}
