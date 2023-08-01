namespace ___Skribbl_Console___;

public class RectangleCommand
{
    public enum RectangleFillingOptions
    {
        Fill,
        HoneyComb
    }

    public CartesianCoordinate Start { get; set; }
    
    public CartesianCoordinate End { get; set; }

    public double FillDistanceY { get; set; }

    public double LaserPowerPercentage { get; set; }

    public double PreFeedSize { get; set; }

    public double PostFeedSize { get; set; }
    
    public double Pitch { get; set; }

    public RectangleFillingOptions FillingOption { get; set; }
}