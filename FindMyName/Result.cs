namespace FindMyName
{
    public partial class Result : Form
    {
        private List<string> matchingNames;
        private string message;

        public Result(List<string> matchingNames, string message)
        {
            InitializeComponent();

            this.matchingNames = matchingNames;
            this.message = message;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            listBox1.Items.AddRange(matchingNames.ToArray());
            label1.Text = message;
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
