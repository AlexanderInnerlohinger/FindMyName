using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ___Skribbl_Console___
{
    internal class Line
    {
        #region Fields

        public static readonly Line None = new Line(new CartesianCoordinate(0, 0, 0), new CartesianCoordinate(0, 0, 0));

        public CartesianCoordinate P0;
        public CartesianCoordinate P1;

        #endregion

        #region Construtors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="p0">The coordinate of the first point on the line.</param>
        /// <param name="p1">The coordinate of the second point on the line.</param>
        public Line(CartesianCoordinate p0, CartesianCoordinate p1)
        {
            P0 = p0;
            P1 = p1;
        }

        public Line(Line other)
        {
            this.P0 = new CartesianCoordinate(other.P0.X, other.P0.Y, other.P0.Z);
            this.P1 = new CartesianCoordinate(other.P1.X, other.P1.Y, other.P1.Z);
        }

        #endregion
    }
}
