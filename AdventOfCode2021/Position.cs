namespace AdventOfCode2021
{
    class Position
    {
        public int HorizontalPosition { get; set; }
        public int VertikalPosition { get; set; }

        public int Aim { get; set; }

        public Position()
        {
            HorizontalPosition = 0;
            VertikalPosition = 0;
            Aim = 0;
        }

        public void AddHorizontal(int horizontalMovement)
        {
            HorizontalPosition += horizontalMovement;
            VertikalPosition += horizontalMovement * Aim;
        }

        public void AddVertikal(int downMovement)
        {
            Aim += downMovement;
        }

        public void SubstractVertikal(int upMovement)
        {
            Aim -= upMovement;
        }

        public int GetResult()
        {
            return HorizontalPosition * VertikalPosition;
        }
    }
}
