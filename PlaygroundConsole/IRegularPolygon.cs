namespace PlaygroundConsole
{
    public interface IRegularPolygon
    {
        public int NumberOfSides { get; set; }
        public int SideLenght { get; set; }

        double GetPerimeter();
        double GetArea();
    }
}