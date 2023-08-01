using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaygroundConsole
{
    class Triangle : AbstractPolygon
    {
        public Triangle(int lenght) : base(3, lenght) { }

        public override double GetArea()
        {
            return SideLenght * SideLenght * Math.Sqrt(3) / 4;
        }
    }
}
