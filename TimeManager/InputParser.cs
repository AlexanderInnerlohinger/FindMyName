using System.Text.RegularExpressions;

namespace TimeManager
{
    public class InputParser
    {
        #region Fields

        // ReSharper disable once StringLiteralTypo
        private readonly string[] m_keywords = { "Abrechnungsperiode", "-" };
        private readonly List<SingleLine> m_singleLines = new();

        private static readonly string m_tagPattern = @"(?<Tag>\d{2})";
        private static readonly string m_textPattern = @"(?<Text>\S+(?:\s?\S+)?)";
        private static readonly string m_beguzPattern = @"(?<Beguz>\d{2}:\d{2})";
        private static readonly string m_enduzPattern = @"(?<Enduz>\d{2}:\d{2})";
        private static readonly string m_erfPattern = @"(?<erf>\d,\d{2})";
        private static readonly string m_sollzPattern = @"(?<Sollz>\d,\d{2})";
        private static readonly string m_saldoPattern = @"(?<Saldo>\d,\d{2})";
        private static readonly string m_tazplPattern = @"(?<TAZPL>\S+)";

        private readonly Regex m_validRegex = new($@"{m_tagPattern}\s*{m_textPattern}?\s*{m_beguzPattern}\s*{m_enduzPattern}\s*{m_erfPattern}\s*{m_sollzPattern}?\s*{m_saldoPattern}?\s*{m_tazplPattern}?\s*", RegexOptions.IgnoreCase);

        #endregion

        #region Properties

        public List<string>? RawTextList { get; set; }

        public int BillingPeriode { get; internal set; }

        public List<List<SingleLine>>? Workdays { get; set; }

        public List<string> InvalidLines { get; set; }

        public TimeCalculator TimeCalculator { get; set; }

        #endregion

        #region Constructor

        public InputParser()
        {
            Workdays = new List<List<SingleLine>>();
            InvalidLines = new List<string>();
            TimeCalculator = new TimeCalculator();
        }

        #endregion

        #region Methods

        public void ParseText()
        {
            bool timeDataStarted = false;
            int flipcounter = 0;

            if (RawTextList != null)
            {
                foreach (string line in RawTextList)
                {
                    //Flip bool for time section of the text
                    if (line.StartsWith(m_keywords[1], StringComparison.InvariantCultureIgnoreCase))
                    {
                        timeDataStarted = !timeDataStarted;
                        flipcounter++;

                        if (flipcounter > 2)
                            break;

                        continue;
                    }

                    if (line.StartsWith(m_keywords[0], StringComparison.InvariantCultureIgnoreCase))
                    {
                        int indexPeriode = line.IndexOfAny("0123456789".ToCharArray());
                        BillingPeriode = int.Parse(line.Substring(indexPeriode, 6));
                    }

                    //Check if the line is empty, thus a spacing between different days
                    if (line.Length <= 0)
                    {
                        continue;
                    }

                    if (timeDataStarted)
                    {
                        if (!AddLineToList(line))
                            InvalidLines.Add(line);
                    }
                }
            }
        }

        private bool AddLineToList(string line)
        {
            //Check if it is a line with valid times, eg. no vacation
            MatchCollection matches = m_validRegex.Matches(line);
            if (matches.Count > 0 && matches[0].Success)
            {
                string tempTag = matches[0].Groups["Tag"].Value;
                string tempText = matches[0].Groups["Text"].Value;

                string tempBeguz = matches[0].Groups["Beguz"].Value;
                string tempEnduz = matches[0].Groups["Enduz"].Value;
                string tempErf = matches[0].Groups["erf"].Value;
                string tempSollz = matches[0].Groups["Sollz"].Value;
                string tempSaldo = matches[0].Groups["Saldo"].Value;
                string tempTAZPL = matches[0].Groups["TAZPL"].Value;

                m_singleLines.Add(new SingleLine(tempTag, tempText, tempBeguz, tempEnduz, tempErf, tempSollz, tempSaldo, tempTAZPL));

                return true;
            }

            return false;
        }

        public void GenerateWorkdays()
        {
            string tempTag = m_singleLines[0].Tag;
            int index = 0;

            List<List<SingleLine>> workdaySets = new List<List<SingleLine>>();

            while (m_singleLines.Count > 0)
            {
                List<SingleLine> currentWorkday = new List<SingleLine>();

                foreach (SingleLine singleLine in m_singleLines)
                {
                    if (tempTag == singleLine.Tag)
                        currentWorkday.Add(singleLine);

                    else
                    {
                        tempTag = singleLine.Tag;
                        break;
                    }
                }

                workdaySets.Add(currentWorkday);

                m_singleLines.RemoveRange(0, workdaySets[index].Count);
                index++;
            }
            Workdays = workdaySets;
        }

        #endregion
    }
}
