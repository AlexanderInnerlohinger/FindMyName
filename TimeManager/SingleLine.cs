namespace TimeManager
{
    public class SingleLine
    {
        #region Properties

        public string Tag { get; set; }
        public string Text { get; set; }
        public TimeOnly Beguz { get; set; }
        public TimeOnly Enduz { get; set; }
        public double Erf { get; set; }
        public double Sollz { get; set; }
        public double Saldo { get; set; }
        public string TAZPL { get; set; }

        #endregion

        #region Constructors

        public SingleLine(string tempTag, string tempText, string tempBeguz, string tempEnduz, string tempErf, string tempSollz, string tempSaldo, string tempTAZPL)
        {
            Tag = tempTag;
            Text = tempText;
            Beguz = TimeOnly.Parse(tempBeguz);
            Enduz = TimeOnly.Parse(tempEnduz);
            Erf = double.Parse(tempErf);
            Sollz = string.IsNullOrEmpty(tempSollz) ? double.NaN : double.Parse(tempSollz);
            Saldo = string.IsNullOrEmpty(tempSaldo) ? double.NaN : double.Parse(tempSaldo);
            TAZPL = tempTAZPL;
        }

        #endregion
    }
}
