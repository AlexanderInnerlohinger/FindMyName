using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ___Skribbl_Console___
{
    public class CartesianCoordinate
    {
        #region Fields

        public static readonly CartesianCoordinate Origin = new CartesianCoordinate(0, 0, 0);

        private double m_x;
        private double m_y;
        private double m_z;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the X component of the coordinate.
        /// </summary>
        public double X
        {
            get { return m_x; }
            set { m_x = value; }
        }

        /// <summary>
        /// Gets or sets the Y component of the coordinate.
        /// </summary>
        public double Y
        {
            get { return m_y; }
            set { m_y = value; }
        }

        /// <summary>
        /// Gets or sets the Z component of the coordinate.
        /// </summary>
        public double Z
        {
            get { return m_z; }
            set { m_z = value; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="x">The X component of the coordinate.</param>
        /// <param name="y">The Y component of the coordinate.</param>
        /// <param name="z">The Z component of the coordinate.</param>
        public CartesianCoordinate(double x, double y, double z)
        {
            m_x = x;
            m_y = y;
            m_z = z;
        }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="components">An array containing the X, Y and Z component.</param>
        /// <exception cref="ArgumentNullException">components</exception>
        /// <exception cref="ArgumentException">At least three elements are required.</exception>
        public CartesianCoordinate(double[] components)
        {
            if (components == null)
                throw new ArgumentNullException("components");

            if (components.Length < 3)
                throw new ArgumentException(@"At least three elements are required.", "components");

            m_x = components[0];
            m_y = components[1];
            m_z = components[2];
        }

        #endregion

        #region Methods

        public static double GetDistanceTo(CartesianCoordinate from, CartesianCoordinate to)
        {
            var A = from;
            var B = to;

            return Math.Sqrt(Math.Pow(A.X - B.X, 2.0) + Math.Pow(A.Y - B.Y, 2.0));
        }

        #endregion

        #region Arithmetic Operators

        public static CartesianCoordinate operator +(CartesianCoordinate a, CartesianCoordinate b)
        {
            return new CartesianCoordinate(a.m_x + b.m_x, a.m_y + b.m_y, a.m_z + b.m_z);
        }

        #endregion
    }
}
