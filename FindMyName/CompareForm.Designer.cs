namespace FindMyName
{
    partial class CompareForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            checkedListBox = new CheckedListBox();
            buttonAccept = new Button();
            SuspendLayout();
            // 
            // checkedListBox
            // 
            checkedListBox.CheckOnClick = true;
            checkedListBox.FormattingEnabled = true;
            checkedListBox.Location = new Point(12, 12);
            checkedListBox.Name = "checkedListBox";
            checkedListBox.Size = new Size(294, 166);
            checkedListBox.TabIndex = 0;
            // 
            // buttonAccept
            // 
            buttonAccept.Location = new Point(12, 184);
            buttonAccept.Name = "buttonAccept";
            buttonAccept.Size = new Size(294, 23);
            buttonAccept.TabIndex = 1;
            buttonAccept.Text = "Accept";
            buttonAccept.UseVisualStyleBackColor = true;
            buttonAccept.Click += buttonAccept_Click;
            // 
            // CompareForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(315, 214);
            ControlBox = false;
            Controls.Add(buttonAccept);
            Controls.Add(checkedListBox);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "CompareForm";
            ShowIcon = false;
            ShowInTaskbar = false;
            Text = "Compare Names";
            TopMost = true;
            ResumeLayout(false);
        }

        #endregion

        private CheckedListBox checkedListBox;
        private Button buttonAccept;
    }
}