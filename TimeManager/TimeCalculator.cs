namespace TimeManager
{
    public class TimeCalculator
    {
        #region Fields

        private readonly TimeSpan Lunchtime = new TimeSpan(0, 30, 0);
        private readonly List<TimeSpan> m_timeDeltas;

        #endregion

        #region Properties

        public TimeSpan WorkingtimeAbs { get; set; }

        public TimeSpan WorkingtimeExclusiveBreak { get; set; }

        public TimeSpan SmokingtimeAbs { get; set; }

        public TimeSpan BreaksLunchtime { get; set; }

        public TimeSpan ÜberschüssigeZeit { get; set; }

        public TimeSpan ReichzeitNominell { get; set; }

        public TimeSpan StundenGeleistet { get; set; }

        public TimeSpan Überstunden { get; set; }

        public double Stundenleistung { get; set; }

        #endregion
    }
}
