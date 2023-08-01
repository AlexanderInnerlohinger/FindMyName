using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ___Skribbl_Console___
{
    internal class DashedLineCommand : MarkingCommand
    {
        #region Properties

        public CartesianCoordinate Start { get; set; }

        public CartesianCoordinate End { get; set; }

        public uint Switches { get; set; }

        public double[] Data { get; set; }

        public double LaserPowerPercentage { get; set; }

        #endregion

        #region Constructors

        public DashedLineCommand()
        {
        }

        public DashedLineCommand(CartesianCoordinate start, CartesianCoordinate end, uint switches, double[] data, double power)
        {
            Start = start;
            End = end;
            LaserPowerPercentage = power;
            Switches = switches;
            Data = data;
        }

        #endregion

    }
}
