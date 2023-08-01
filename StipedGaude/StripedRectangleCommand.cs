using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ___Skribbl_Console___
{
    public enum StripeFillingOptions
    {
        Fill,
        HoneyComb
    }

    internal class StripedRectangleCommand
    {
        #region Properties StirpedRectangle

        public CartesianCoordinate Start { get; set; }

        public CartesianCoordinate End { get; set; } = new CartesianCoordinate(10, 10, 10);

        public double StripeWidth { get; set; }

        public double FillDistance { get; set; }

        public bool MarkBorder { get; set; }

        public double PreFeedSize { get; set; }

        public double PostFeedSize { get; set; }

        public StripeFillingOptions FillingOption { get; set; }

        #endregion

        #region Properties MarkingCommand

        public string Naming { get; set; }

        public Guid Guid { get; internal set; }

        public double LaserPowerPercentage { get; set; }

        public double MarkingCount { get; set; } = 1;

        public double ScanSpeed { get; set; }

        #endregion

        #region Constructor

        public StripedRectangleCommand()
        {
        }

        #endregion
    }
}
