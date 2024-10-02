namespace StreamCommunication
{
    partial class StreamCommunication
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            m_PictureBoxOriginal = new PictureBox();
            m_ButtonJustShow = new Button();
            m_ButtonConvert = new Button();
            m_ButtonShowImageToString = new Button();
            m_ButtonShowByte = new Button();
            m_ButtonConvertByteToPNG = new Button();
            m_ButtonShowPNG = new Button();
            m_ButtonWriteStream = new Button();
            m_ButtonReadStream = new Button();
            m_ButtonShowReadStream = new Button();
            m_ButtonShowReadStreamByte = new Button();
            m_ButtonConvertStreamToByte = new Button();
            ((System.ComponentModel.ISupportInitialize)m_PictureBoxOriginal).BeginInit();
            SuspendLayout();
            // 
            // m_PictureBoxOriginal
            // 
            m_PictureBoxOriginal.Location = new Point(12, 12);
            m_PictureBoxOriginal.Name = "m_PictureBoxOriginal";
            m_PictureBoxOriginal.Size = new Size(370, 322);
            m_PictureBoxOriginal.SizeMode = PictureBoxSizeMode.AutoSize;
            m_PictureBoxOriginal.TabIndex = 0;
            m_PictureBoxOriginal.TabStop = false;
            // 
            // m_ButtonJustShow
            // 
            m_ButtonJustShow.Location = new Point(388, 12);
            m_ButtonJustShow.Name = "m_ButtonJustShow";
            m_ButtonJustShow.Size = new Size(168, 57);
            m_ButtonJustShow.TabIndex = 0;
            m_ButtonJustShow.Text = "Just Show/Hide Picture";
            m_ButtonJustShow.UseVisualStyleBackColor = true;
            m_ButtonJustShow.Click += m_ButtonJustShow_Click;
            // 
            // m_ButtonConvert
            // 
            m_ButtonConvert.Location = new Point(388, 104);
            m_ButtonConvert.Name = "m_ButtonConvert";
            m_ButtonConvert.Size = new Size(168, 57);
            m_ButtonConvert.TabIndex = 1;
            m_ButtonConvert.Text = "Convert PNG to Byte[]";
            m_ButtonConvert.UseVisualStyleBackColor = true;
            m_ButtonConvert.Click += m_ButtonConvert_Click;
            // 
            // m_ButtonShowImageToString
            // 
            m_ButtonShowImageToString.Enabled = false;
            m_ButtonShowImageToString.Location = new Point(388, 196);
            m_ButtonShowImageToString.Name = "m_ButtonShowImageToString";
            m_ButtonShowImageToString.Size = new Size(168, 23);
            m_ButtonShowImageToString.TabIndex = 3;
            m_ButtonShowImageToString.Text = "Show Stringbuilder";
            m_ButtonShowImageToString.UseVisualStyleBackColor = true;
            m_ButtonShowImageToString.Click += m_ButtonShowImageToString_Click;
            // 
            // m_ButtonShowByte
            // 
            m_ButtonShowByte.Enabled = false;
            m_ButtonShowByte.Location = new Point(388, 167);
            m_ButtonShowByte.Name = "m_ButtonShowByte";
            m_ButtonShowByte.Size = new Size(168, 23);
            m_ButtonShowByte.TabIndex = 2;
            m_ButtonShowByte.Text = "Show Byte[]";
            m_ButtonShowByte.UseVisualStyleBackColor = true;
            m_ButtonShowByte.Click += m_ShowByte_Click;
            // 
            // m_ButtonConvertByteToPNG
            // 
            m_ButtonConvertByteToPNG.Enabled = false;
            m_ButtonConvertByteToPNG.Location = new Point(600, 254);
            m_ButtonConvertByteToPNG.Name = "m_ButtonConvertByteToPNG";
            m_ButtonConvertByteToPNG.Size = new Size(168, 57);
            m_ButtonConvertByteToPNG.TabIndex = 9;
            m_ButtonConvertByteToPNG.Text = "Convert Byte[] to PNG";
            m_ButtonConvertByteToPNG.UseVisualStyleBackColor = true;
            m_ButtonConvertByteToPNG.Click += m_ButtonConvertByteToPNG_Click;
            // 
            // m_ButtonShowPNG
            // 
            m_ButtonShowPNG.Enabled = false;
            m_ButtonShowPNG.Location = new Point(600, 314);
            m_ButtonShowPNG.Name = "m_ButtonShowPNG";
            m_ButtonShowPNG.Size = new Size(168, 23);
            m_ButtonShowPNG.TabIndex = 10;
            m_ButtonShowPNG.Text = "Show PNG";
            m_ButtonShowPNG.UseVisualStyleBackColor = true;
            m_ButtonShowPNG.Click += m_ButtonShowPNG_Click;
            // 
            // m_ButtonWriteStream
            // 
            m_ButtonWriteStream.Enabled = false;
            m_ButtonWriteStream.Location = new Point(388, 254);
            m_ButtonWriteStream.Name = "m_ButtonWriteStream";
            m_ButtonWriteStream.Size = new Size(168, 57);
            m_ButtonWriteStream.TabIndex = 4;
            m_ButtonWriteStream.Text = "Write Stream";
            m_ButtonWriteStream.UseVisualStyleBackColor = true;
            m_ButtonWriteStream.Click += m_ButtonWriteStream_Click;
            // 
            // m_ButtonReadStream
            // 
            m_ButtonReadStream.Enabled = false;
            m_ButtonReadStream.Location = new Point(600, 12);
            m_ButtonReadStream.Name = "m_ButtonReadStream";
            m_ButtonReadStream.Size = new Size(168, 57);
            m_ButtonReadStream.TabIndex = 5;
            m_ButtonReadStream.Text = "Read Stream";
            m_ButtonReadStream.UseVisualStyleBackColor = true;
            m_ButtonReadStream.Click += m_ButtonReadStream_Click;
            // 
            // m_ButtonShowReadStream
            // 
            m_ButtonShowReadStream.Enabled = false;
            m_ButtonShowReadStream.Location = new Point(600, 75);
            m_ButtonShowReadStream.Name = "m_ButtonShowReadStream";
            m_ButtonShowReadStream.Size = new Size(168, 23);
            m_ButtonShowReadStream.TabIndex = 6;
            m_ButtonShowReadStream.Text = "Show Read Stream as String";
            m_ButtonShowReadStream.UseVisualStyleBackColor = true;
            m_ButtonShowReadStream.Click += m_ButtonShowReadStream_Click;
            // 
            // m_ButtonShowReadStreamByte
            // 
            m_ButtonShowReadStreamByte.Enabled = false;
            m_ButtonShowReadStreamByte.Location = new Point(600, 167);
            m_ButtonShowReadStreamByte.Name = "m_ButtonShowReadStreamByte";
            m_ButtonShowReadStreamByte.Size = new Size(168, 23);
            m_ButtonShowReadStreamByte.TabIndex = 8;
            m_ButtonShowReadStreamByte.Text = "Show Read Stream Byte[]";
            m_ButtonShowReadStreamByte.UseVisualStyleBackColor = true;
            m_ButtonShowReadStreamByte.Click += m_ButtonShowReadStreamByte_Click;
            // 
            // m_ButtonConvertStreamToByte
            // 
            m_ButtonConvertStreamToByte.Enabled = false;
            m_ButtonConvertStreamToByte.Location = new Point(600, 104);
            m_ButtonConvertStreamToByte.Name = "m_ButtonConvertStreamToByte";
            m_ButtonConvertStreamToByte.Size = new Size(168, 57);
            m_ButtonConvertStreamToByte.TabIndex = 7;
            m_ButtonConvertStreamToByte.Text = "Convert Read Stream to Byte[]";
            m_ButtonConvertStreamToByte.UseVisualStyleBackColor = true;
            m_ButtonConvertStreamToByte.Click += m_ButtonConvertStreamToByte_Click;
            // 
            // StreamCommunication
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(777, 350);
            Controls.Add(m_ButtonConvertStreamToByte);
            Controls.Add(m_ButtonShowReadStreamByte);
            Controls.Add(m_ButtonShowReadStream);
            Controls.Add(m_ButtonReadStream);
            Controls.Add(m_ButtonWriteStream);
            Controls.Add(m_ButtonShowPNG);
            Controls.Add(m_ButtonConvertByteToPNG);
            Controls.Add(m_ButtonShowByte);
            Controls.Add(m_ButtonShowImageToString);
            Controls.Add(m_ButtonConvert);
            Controls.Add(m_ButtonJustShow);
            Controls.Add(m_PictureBoxOriginal);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "StreamCommunication";
            ShowIcon = false;
            Text = "StreamCommunication";
            ((System.ComponentModel.ISupportInitialize)m_PictureBoxOriginal).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox m_PictureBoxOriginal;
        private Button m_ButtonJustShow;
        private Button m_ButtonConvert;
        private Button m_ButtonShowImageToString;
        private Button m_ButtonShowByte;
        private Button m_ButtonConvertByteToPNG;
        private Button m_ButtonShowPNG;
        private Button m_ButtonWriteStream;
        private Button m_ButtonReadStream;
        private Button m_ButtonShowReadStream;
        private Button button1;
        private Button m_ButtonShowReadStreamByte;
        private Button m_ButtonConvertStreamToByte;
    }
}
