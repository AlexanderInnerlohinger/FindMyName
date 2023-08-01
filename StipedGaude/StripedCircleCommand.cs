using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ___Skribbl_Console___
{
    internal class StripedCircleCommand : MarkingCommand
    {
        #region Properties

        public CartesianCoordinate Center { get; set; }

        public double Radius { get; set; } = 10;

        public double StripeWidth { get; set; }

        public double FillDistance { get; set; }
        
        public double Pitch { get; set; }

        public double PreFeedSize { get; set; }

        public double PostFeedSize { get; set; }

        public bool MarkBorder { get; set; }
        
        public double LaserPowerPercentage { get; set; }

        public StripeFillingOptions FillingOption { get; set; }

        public double ScanSpeed { get; set; }

        #endregion

        #region Constructors

        public StripedCircleCommand()
        {
        }

        #endregion
    }
}
