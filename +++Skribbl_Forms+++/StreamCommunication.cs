using System.Drawing.Imaging;
using System.IO;
using System.Resources;
using System.Runtime.InteropServices.ComTypes;
using System.Text;

namespace StreamCommunication
{
    public partial class StreamCommunication : Form
    {
        #region Constants

        private const string PicturePath = "C:\\Users\\A.Innerlohinger\\source\\repos\\Playground\\+++Skribbl_Forms+++\\images.png";

        #endregion

        #region Fields

        private byte[] m_ImageData;

        private string m_HeaderAndImage = "";

        private Stream m_Stream;

        private string m_ReadString = "";

        private byte[] m_InputStream;

        private Image m_ReadImage;

        private List<Button> m_Buttons = new();

        #endregion

        #region Constructors

        public StreamCommunication()
        {
            InitializeComponent();

            m_Stream = new MemoryStream();

            foreach (Control control in Controls)
                if (control is Button button)
                    m_Buttons.Add(button);

            m_Buttons = m_Buttons.OrderBy(b => b.TabIndex).ToList();
        }

        #endregion

        #region Event Handling

        private void m_ButtonJustShow_Click(object sender, EventArgs e)
        {
            m_PictureBoxOriginal.Image = m_PictureBoxOriginal.Image == null ? Image.FromFile(PicturePath) : null;
        }

        private void m_ButtonConvert_Click(object sender, EventArgs e)
        {
            var stringBuilder = new StringBuilder();

            m_ImageData = ImageToByteArray(Image.FromFile(PicturePath));
            int imageSize = m_ImageData.Length;
            string header = $"{imageSize}\r\n";
            string imageDataString = Convert.ToBase64String(m_ImageData);

            stringBuilder.Append(header);
            stringBuilder.Append(imageDataString);

            m_HeaderAndImage = stringBuilder.ToString();

            EnableShowButtons(5);
        }

        private void m_ShowByte_Click(object sender, EventArgs e)
        {
            StringBuilder stringBuilder = new StringBuilder();

            foreach (byte b in m_ImageData)
            {
                stringBuilder.Append(b);
                stringBuilder.Append("\n\r");
            }

            MessageBox.Show(stringBuilder.ToString());
        }

        private void m_ButtonShowImageToString_Click(object sender, EventArgs e)
        {
            MessageBox.Show(m_HeaderAndImage);
        }

        private void m_ButtonWriteStream_Click(object sender, EventArgs e)
        {
            using (StreamWriter writer = new StreamWriter(m_Stream, leaveOpen: true))
            {
                writer.Write(m_HeaderAndImage);
                writer.Flush();
                m_Stream.Position = 0;
            }

            EnableShowButtons(6);
        }

        private void m_ButtonReadStream_Click(object sender, EventArgs e)
        {
            m_Stream.Position = 0;

            using (StreamReader reader = new StreamReader(m_Stream, leaveOpen: true))
            {
                m_ReadString = reader.ReadToEnd();
            }

            EnableShowButtons(8);
        }

        private void m_ButtonShowReadStream_Click(object sender, EventArgs e)
        {
            MessageBox.Show(m_ReadString);
        }

        private void m_ButtonConvertStreamToByte_Click(object sender, EventArgs e)
        {
            int indexSizeEnd = m_ReadString.IndexOf("\r\n", StringComparison.Ordinal);
            var truncatedCollection = m_ReadString.Remove(0, indexSizeEnd + 2);

            m_InputStream = Convert.FromBase64String(truncatedCollection);

            EnableShowButtons(10);
        }

        private void m_ButtonShowReadStreamByte_Click(object sender, EventArgs e)
        {
            StringBuilder stringBuilder = new StringBuilder();

            foreach (byte b in m_InputStream)
            {
                stringBuilder.Append(b);
                stringBuilder.Append("\n\r");
            }

            MessageBox.Show(stringBuilder.ToString());
        }

        private void m_ButtonConvertByteToPNG_Click(object sender, EventArgs e)
        {
            Stream stream = new MemoryStream(m_InputStream);
            stream.Position = 0;

            m_ReadImage = Image.FromStream(stream);

            EnableShowButtons(11);
        }

        private void m_ButtonShowPNG_Click(object sender, EventArgs e)
        {
            m_PictureBoxOriginal.Image = null;
            m_PictureBoxOriginal.Image = m_ReadImage;
        }

        #endregion

        #region Methods

        private byte[] ImageToByteArray(Image image)
        {
            using MemoryStream ms = new MemoryStream();
            image.Save(ms, ImageFormat.Png);
            return ms.ToArray();
        }

        private void EnableShowButtons(int buttonIndexToEnable)
        {
            for (int i = 0; i < buttonIndexToEnable; i++)
                m_Buttons[i].Enabled = true;
        }

        #endregion
    }
}
