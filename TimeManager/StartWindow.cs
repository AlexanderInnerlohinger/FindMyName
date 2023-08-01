namespace TimeManager
{
    public partial class Form1 : Form
    {
        private string m_filePath = string.Empty;
        private string m_fileContent = string.Empty;
        private List<string> m_inputList = new();
        private InputParser m_inputParser = new();

        public Form1()
        {
            InitializeComponent();
        }

        private void search_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();

            DialogResult result = dialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                //Get the path of specified file
                m_filePath = dialog.FileName;

                //Read the content of the file
                var fileStream = dialog.OpenFile();

                using (StreamReader reader = new StreamReader(fileStream))
                {
                    m_fileContent = reader.ReadToEnd();
                }

                int counter = 0;
                // Read the file and save it line by line.
                foreach (string line in File.ReadLines(m_filePath))
                {
                    m_inputList.Add(line);
                    counter++;
                }

                display.Text = m_fileContent;
            }
        }

        private void proceed_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(display.Text))
            {
                throw new ArgumentException("File must not be null or empty.");
            }

            try
            {
                m_inputParser.RawTextList = m_inputList;
                m_inputParser.ParseText();
                m_inputParser.GenerateWorkdays();

                //Open up new control
                PreviewParsed control = new PreviewParsed(m_inputParser);
                control.Dock = DockStyle.Fill;
                Controls.Add(control);

                control.Show();
                control.BringToFront();
            }
            catch (Exception exception)
            {
                throw new ArgumentException("Something went wrong with the parsing of the input.", exception);
            }
        }
    }
}