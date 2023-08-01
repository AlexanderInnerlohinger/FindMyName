namespace FindMyName
{
    public partial class CompareForm : Form
    {
        private ListBox listbox;

        public CheckedListBox.CheckedItemCollection? CheckedItemCollection { get; set; }

        public CompareForm(ListBox listbox)
        {
            InitializeComponent();

            this.listbox = listbox;
        }

        public CompareForm(CheckedListBox.CheckedItemCollection? selectedNames)
        {
            InitializeComponent();

            this.listbox = new ListBox();

            foreach (object selectedName in selectedNames)
                listbox.Items.Add(selectedName);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            checkedListBox.Items.Clear();
            checkedListBox.Items.AddRange(listbox.Items);
        }

        private void buttonAccept_Click(object sender, EventArgs e)
        {
            CheckedItemCollection = checkedListBox.CheckedItems;
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
