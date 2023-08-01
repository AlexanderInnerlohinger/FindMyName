namespace ___Skribbl_Forms___
{
    partial class Form1
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
            comboBox1 = new ComboBox();
            richTextBox1 = new RichTextBox();
            comboBox2 = new ComboBox();
            textBox1 = new TextBox();
            SuspendLayout();
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(287, 120);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(121, 23);
            comboBox1.TabIndex = 0;
            comboBox1.SelectedValueChanged += comboBox1_SelectedValueChanged;
            // 
            // richTextBox1
            // 
            richTextBox1.Location = new Point(186, 267);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(100, 96);
            richTextBox1.TabIndex = 1;
            richTextBox1.Text = "";
            // 
            // comboBox2
            // 
            comboBox2.FormattingEnabled = true;
            comboBox2.Location = new Point(287, 149);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new Size(121, 23);
            comboBox2.TabIndex = 2;
            comboBox2.SelectedValueChanged += comboBox2_SelectedValueChanged;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(455, 218);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(100, 23);
            textBox1.TabIndex = 3;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(textBox1);
            Controls.Add(comboBox2);
            Controls.Add(richTextBox1);
            Controls.Add(comboBox1);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox comboBox1;
        private RichTextBox richTextBox1;
        private ComboBox comboBox2;
        private TextBox textBox1;
    }
}