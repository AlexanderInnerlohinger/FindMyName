namespace TimeManager
{
    public partial class PreviewParsed : UserControl
    {
        private readonly InputParser m_inputParser;
        public PreviewParsed(InputParser inputParser)
        {
            InitializeComponent();
            m_inputParser = inputParser;

            ListViewSetUp();
        }

        private void ListViewSetUp()
        {
            listView1.Items.Clear();

            #region StyleDefintion

            // Set the view to show details.
            listView1.View = View.Details;
            // Forbid the user to edit item text.
            listView1.LabelEdit = false;
            // Allow the user to rearrange columns.
            listView1.AllowColumnReorder = true;
            // Select the item and subitems when selection is made.
            listView1.FullRowSelect = true;
            // Display grid lines.
            listView1.GridLines = true;

            #endregion

            #region PopulateList

            var alternatingColor = Color.White;

            for (int i = 0; i < m_inputParser.Workdays.Count; i++)
            {
                for (int j = 0; j < m_inputParser.Workdays[i].Count; j++)
                {
                    ListViewItem newItem = new ListViewItem(i.ToString());
                    newItem.BackColor = alternatingColor;

                    newItem.SubItems.Add(m_inputParser.Workdays[i][j].Tag);
                    newItem.SubItems.Add(m_inputParser.Workdays[i][j].Text);
                    newItem.SubItems.Add(m_inputParser.Workdays[i][j].Beguz.ToString());
                    newItem.SubItems.Add(m_inputParser.Workdays[i][j].Enduz.ToString());
                    newItem.SubItems.Add(m_inputParser.Workdays[i][j].Erf.ToString());
                    newItem.SubItems.Add(m_inputParser.Workdays[i][j].Sollz.ToString());
                    newItem.SubItems.Add(m_inputParser.Workdays[i][j].Saldo.ToString());
                    newItem.SubItems.Add(m_inputParser.Workdays[i][j].TAZPL);

                    listView1.Items.Add(newItem);
                }

                if (alternatingColor == Color.White)
                    alternatingColor = Color.LightGray;

                else
                    alternatingColor = Color.White;
            }

            #endregion

            // Create columns for the items and subitems.
            listView1.Columns.Add("Arbeitstag", -2);
            listView1.Columns.Add("Tag", -2);
            listView1.Columns.Add("Text", -2);
            listView1.Columns.Add("Beguz", -2);
            listView1.Columns.Add("Enduz", -2);
            listView1.Columns.Add("erf", -2);
            listView1.Columns.Add("Sollz", -2);
            listView1.Columns.Add("Saldo", -2);
            listView1.Columns.Add("TAZPL", -2);
        }

        private void proceed_Click(object sender, EventArgs e)
        {
            //Open up new control
            DisplayDetails control = new DisplayDetails(m_inputParser);
            control.Dock = DockStyle.Fill;
            Controls.Add(control);

            control.Show();
            control.BringToFront();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // TODO: implement a much cleaner option of viewing the lines
            string invalidLines = null;

            foreach (string line in m_inputParser.InvalidLines)
            {
                invalidLines += line + "\n";
            }

            MessageBox.Show(invalidLines);
        }
    }
}
