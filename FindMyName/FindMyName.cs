// ReSharper disable LocalizableElement
using System.Diagnostics;
using System.Text.Json;

namespace FindMyName
{
    public partial class FindMyName : Form
    {
        #region Events

        private delegate void StringEventHandler(string message);
        private static event StringEventHandler? StringEvent;

        #endregion

        #region Constructors

        public FindMyName()
        {
            InitializeComponent();
            StringEvent += HandleStringEvent;
        }

        #endregion

        #region Overrides

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            string savedNamesJson = Environment.CurrentDirectory + "\\names.json";

            string fileContents = File.ReadAllText(savedNamesJson);

            string[] deserializedString = JsonSerializer.Deserialize<string[]>(fileContents) ?? Array.Empty<string>();

            if (deserializedString.Any())
            {
                // Clear existing names and add loaded ones from file
                listBox.Items.Clear();

                foreach (string name in deserializedString)
                    listBox.Items.Add(name);
            }

        }

        #endregion

        #region Methods

        private void AddName(string name)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));

            if (name == string.Empty)
            {
                OnStringEvent("Input string was empty.");
                return;
            }

            listBox.Items.Add(name);
        }

        private List<string?> FindMatches(CheckedListBox.CheckedItemCollection? selectedNamesOne, CheckedListBox.CheckedItemCollection? selectedNamesTwo)
        {
            return (from object name in selectedNamesOne
                    where selectedNamesTwo.Contains(name)
                    select name.ToString()).ToList();
        }

        private void ShowMatchingNames(List<string?> matchingNames, string message)
        {
            Result resultForm = new Result(matchingNames, message);
            resultForm.ShowDialog();
        }

        #endregion

        #region Event Handlers

        private void buttonAddName_Click(object sender, EventArgs e)
        {
            AddName(textBoxAddName.Text);
            textBoxAddName.Clear();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            string path = "names.json";
            var jsonString = JsonSerializer.Serialize(listBox.Items);

            File.WriteAllText(path, jsonString);

            OnStringEvent($"Names saved to {path}");
        }

        private void buttonLoad_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();

                openFileDialog.Filter = "Text Files (*.txt)|*.txt|JSON Files (*.json)|*.json|All Files (*.*)|*.*";
                openFileDialog.Title = "Select a Text or JSON File";
                openFileDialog.InitialDirectory = Environment.CurrentDirectory;

                // Set the default selection to JSON files
                openFileDialog.FilterIndex = 2;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string selectedFilePath = openFileDialog.FileName;
                    string fileContents = File.ReadAllText(selectedFilePath);

                    string[] deserializedString = JsonSerializer.Deserialize<string[]>(fileContents) ?? Array.Empty<string>();

                    if (deserializedString.Any())
                    {
                        // Clear existing names and add loaded ones from file
                        listBox.Items.Clear();

                        foreach (string name in deserializedString)
                            listBox.Items.Add(name);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new FileLoadException("Loading of file failed.", ex);
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            var selectedItem = listBox.SelectedItem as string ?? string.Empty;

            if (selectedItem == string.Empty)
                OnStringEvent("No item selected.");

            listBox.Items.Remove(selectedItem);
        }

        private void buttonCompare_Click(object sender, EventArgs e)
        {
            CheckedListBox.CheckedItemCollection? selectedNamesOne = null;
            CheckedListBox.CheckedItemCollection? selectedFavoritesOne = null;
            CheckedListBox.CheckedItemCollection? selectedNamesTwo = null;
            CheckedListBox.CheckedItemCollection? selectedFavoritesTwo = null;

            CompareForm compareFormOne = new CompareForm(listBox);
            compareFormOne.Text = "Parent One, select the names you like";

            if (compareFormOne.ShowDialog() == DialogResult.OK)
            {
                selectedNamesOne = compareFormOne.CheckedItemCollection;
            }

            CompareForm compareFormFavoriteOne = new CompareForm(selectedNamesOne);
            compareFormFavoriteOne.Text = "Select SUPER-Likes";
            compareFormFavoriteOne.BackColor = Color.DeepPink;
            if (compareFormFavoriteOne.ShowDialog() == DialogResult.OK)
            {
                selectedFavoritesOne = compareFormFavoriteOne.CheckedItemCollection;
            }

            CompareForm compareFormTwo = new CompareForm(listBox);
            compareFormTwo.Text = "Parent Two, select the names you like";
            if (compareFormTwo.ShowDialog() == DialogResult.OK)
            {
                selectedNamesTwo = compareFormTwo.CheckedItemCollection;
            }

            CompareForm compareFormFavoriteTwo = new CompareForm(selectedNamesTwo);
            compareFormFavoriteTwo.Text = "Select SUPER-Likes";
            compareFormFavoriteTwo.BackColor = Color.DeepPink;
            if (compareFormFavoriteTwo.ShowDialog() == DialogResult.OK)
            {
                selectedFavoritesTwo = compareFormFavoriteTwo.CheckedItemCollection;
            }

            if (selectedNamesOne == null || selectedNamesTwo == null)
            {
                OnStringEvent("Selection not valid.");
                return;
            }

            List<string?> matchingNames = FindMatches(selectedNamesOne, selectedNamesTwo);
            List<string?> matchingFavorites = FindMatches(selectedFavoritesOne, selectedFavoritesTwo);

            if (matchingNames.Count == 0)
            {
                MessageBox.Show("No matching names found :(");
                return;
            }

            ShowMatchingNames(matchingNames, message: "The matching names are");
            ShowMatchingNames(matchingFavorites, message: "Matching SUPER-Likes");
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            Close();
        }


        private void HandleStringEvent(string message)
        {
            Task.Factory.StartNew(() =>
            {
                statusStrip.Items[0].Text = message;
                statusStrip.BackColor = Color.BlanchedAlmond;

                Stopwatch sw = Stopwatch.StartNew();
                while (sw.ElapsedMilliseconds < 2000)
                {
                    Thread.Sleep(50);
                }

                sw.Stop();
                statusStrip.Items[0].Text = string.Empty;
                statusStrip.ResetBackColor();
            });
        }

        private void OnStringEvent(string message)
        {
            StringEvent?.Invoke(message);
        }

        private void textBoxAddName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.Equals((char)Keys.Enter))
            {
                buttonAddName_Click(this, EventArgs.Empty);
                e.Handled = true;
            }
        }

        #endregion
    }
}