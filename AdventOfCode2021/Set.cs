namespace AdventOfCode2021
{
    public class Set
    {
        private int FirstEntry { get; set; }
        private int SecondEntry { get; set; }
        private int ThirdEntry { get; set; }

        public Set(int firstEntry, int secondEntry, int thirdEntry)
        {
            FirstEntry = firstEntry;
            SecondEntry = secondEntry;
            ThirdEntry = thirdEntry;
        }

        public int Sum()
        {
            return FirstEntry + SecondEntry + ThirdEntry;
        }
    }
}
