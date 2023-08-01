namespace FindMyName
{
    partial class FindMyName
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
            buttonAddName = new Button();
            listBox = new ListBox();
            textBoxAddName = new TextBox();
            buttonSave = new Button();
            buttonLoad = new Button();
            buttonDelete = new Button();
            statusStrip = new StatusStrip();
            toolStripStatusLabel1 = new ToolStripStatusLabel();
            buttonCompare = new Button();
            buttonExit = new Button();
            statusStrip.SuspendLayout();
            SuspendLayout();
            // 
            // buttonAddName
            // 
            buttonAddName.Location = new Point(12, 12);
            buttonAddName.Name = "buttonAddName";
            buttonAddName.Size = new Size(51, 23);
            buttonAddName.TabIndex = 0;
            buttonAddName.Text = "Add";
            buttonAddName.UseVisualStyleBackColor = true;
            buttonAddName.Click += buttonAddName_Click;
            // 
            // listBox
            // 
            listBox.FormattingEnabled = true;
            listBox.ItemHeight = 15;
            listBox.Location = new Point(12, 41);
            listBox.Name = "listBox";
            listBox.Size = new Size(271, 139);
            listBox.TabIndex = 6;
            // 
            // textBoxAddName
            // 
            textBoxAddName.Location = new Point(69, 12);
            textBoxAddName.Name = "textBoxAddName";
            textBoxAddName.Size = new Size(133, 23);
            textBoxAddName.TabIndex = 1;
            textBoxAddName.KeyPress += textBoxAddName_KeyPress;
            // 
            // buttonSave
            // 
            buttonSave.Location = new Point(289, 70);
            buttonSave.Name = "buttonSave";
            buttonSave.Size = new Size(97, 23);
            buttonSave.TabIndex = 4;
            buttonSave.Text = "Save Names";
            buttonSave.UseVisualStyleBackColor = true;
            buttonSave.Click += buttonSave_Click;
            // 
            // buttonLoad
            // 
            buttonLoad.Location = new Point(289, 41);
            buttonLoad.Name = "buttonLoad";
            buttonLoad.Size = new Size(97, 23);
            buttonLoad.TabIndex = 3;
            buttonLoad.Text = "Load Names";
            buttonLoad.UseVisualStyleBackColor = true;
            buttonLoad.Click += buttonLoad_Click;
            // 
            // buttonDelete
            // 
            buttonDelete.Location = new Point(289, 128);
            buttonDelete.Name = "buttonDelete";
            buttonDelete.Size = new Size(97, 23);
            buttonDelete.TabIndex = 5;
            buttonDelete.Text = "Delete Name";
            buttonDelete.UseVisualStyleBackColor = true;
            buttonDelete.Click += buttonDelete_Click;
            // 
            // statusStrip
            // 
            statusStrip.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel1 });
            statusStrip.Location = new Point(0, 196);
            statusStrip.Name = "statusStrip";
            statusStrip.Size = new Size(394, 22);
            statusStrip.TabIndex = 7;
            statusStrip.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Size = new Size(0, 17);
            // 
            // buttonCompare
            // 
            buttonCompare.BackColor = Color.IndianRed;
            buttonCompare.ForeColor = SystemColors.WindowText;
            buttonCompare.Location = new Point(208, 12);
            buttonCompare.Name = "buttonCompare";
            buttonCompare.Size = new Size(75, 23);
            buttonCompare.TabIndex = 2;
            buttonCompare.Text = "Compare";
            buttonCompare.UseVisualStyleBackColor = false;
            buttonCompare.Click += buttonCompare_Click;
            // 
            // buttonExit
            // 
            buttonExit.BackColor = SystemColors.ControlDark;
            buttonExit.Location = new Point(289, 157);
            buttonExit.Name = "buttonExit";
            buttonExit.Size = new Size(97, 23);
            buttonExit.TabIndex = 8;
            buttonExit.Text = "Exit";
            buttonExit.UseVisualStyleBackColor = false;
            buttonExit.Click += buttonExit_Click;
            // 
            // FindMyName
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(394, 218);
            Controls.Add(buttonExit);
            Controls.Add(buttonCompare);
            Controls.Add(statusStrip);
            Controls.Add(buttonDelete);
            Controls.Add(buttonLoad);
            Controls.Add(buttonSave);
            Controls.Add(textBoxAddName);
            Controls.Add(listBox);
            Controls.Add(buttonAddName);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FindMyName";
            Text = "FindMyName";
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button buttonAddName;
        private ListBox listBox;
        private TextBox textBoxAddName;
        private Button buttonSave;
        private Button buttonLoad;
        private Button buttonDelete;
        private StatusStrip statusStrip;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private Button buttonCompare;
        private Button buttonExit;
    }
}