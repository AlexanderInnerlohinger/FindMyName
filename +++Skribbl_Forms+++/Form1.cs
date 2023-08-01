using System.Diagnostics;

namespace ___Skribbl_Forms___
{
    public partial class Form1 : Form
    {
        List<string> m_remainingParameters = new List<string>();
        private string previousSelectedItem1;
        private string previousSelectedItem2;

        private string Power = "Power";
        private string ZDistance = "ZDistance";
        private string Pitch = "Pitch";
        private string FillDistance = "FillDistance";

        public Form1()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            m_remainingParameters.Add(Power);
            m_remainingParameters.Add(ZDistance);
            m_remainingParameters.Add(Pitch);
            m_remainingParameters.Add(FillDistance);

            foreach (string mRemainingParameter in m_remainingParameters)
            {
                comboBox1.Items.Add(mRemainingParameter);
                comboBox2.Items.Add(mRemainingParameter);
            }

            //previousSelectedItem1 = comboBox1.SelectedItem?.ToString();
            //previousSelectedItem2 = comboBox2.SelectedItem?.ToString();

            foreach (string mRemainingParameter in m_remainingParameters)
            {
                richTextBox1.Text += mRemainingParameter;
                richTextBox1.Text += "\n";
            }

            Debug.Assert(false);
        }


        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            string selectedItem = comboBox1.SelectedItem?.ToString();

            string previouslySelctedItem = previousSelectedItem1;

            if (previouslySelctedItem != null && previouslySelctedItem != comboBox2.SelectedItem)
                m_remainingParameters.Add(previouslySelctedItem);

            m_remainingParameters.Remove(selectedItem);

            UpdateGUI();

            previousSelectedItem1 = selectedItem;

            //if (previouslySelctedItem != null)
            //    comboBox2.Items.Add(previouslySelctedItem);

            //comboBox2.Items.Remove(selectedItem);




            //comboBox1.DataSource = m_remainingParameters;
        }

        private void comboBox2_SelectedValueChanged(object sender, EventArgs e)
        {
            string selectedItem = comboBox2.SelectedItem?.ToString();

            string previouslySelctedItem = previousSelectedItem2;

            if (previouslySelctedItem != null && previouslySelctedItem != comboBox1.SelectedItem)
                m_remainingParameters.Add(previouslySelctedItem);

            m_remainingParameters.Remove(selectedItem);

            UpdateGUI();

            previousSelectedItem2 = selectedItem;

            //if (previouslySelctedItem != null)
            //    comboBox1.Items.Add(previouslySelctedItem);

            //comboBox1.Items.Remove(selectedItem);


            //comboBox2.DataSource = m_remainingParameters;
        }

        private void UpdateGUI()
        {
            richTextBox1.Clear();
            m_remainingParameters.Sort();
            foreach (string mRemainingParameter in m_remainingParameters)
            {
                richTextBox1.Text += mRemainingParameter;
                richTextBox1.Text += "\n";
            }
        }
    }
}