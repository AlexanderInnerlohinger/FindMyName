namespace TimeManager
{
    public partial class DisplayDetails : UserControl
    {
        private readonly InputParser m_inputParser;
        private TimeCalculator m_timeCalculator;

        public DisplayDetails(InputParser m_inputParser)
        {
            InitializeComponent();

            this.m_inputParser = m_inputParser;
            CalculateValues();
        }

        private void CalculateValues()
        {
            m_timeCalculator = new TimeCalculator();

            if (m_inputParser == null || m_inputParser.Workdays == null)
                throw new ArgumentNullException(nameof(m_inputParser));

            foreach (List<SingleLine> workday in m_inputParser.Workdays)
            {

            }
        }
    }
}
