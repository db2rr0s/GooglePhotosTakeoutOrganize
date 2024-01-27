namespace GooglePhotosTakeoutOrganize
{
    partial class FrmMain
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
            label1 = new Label();
            tbxFolderPath = new TextBox();
            button1 = new Button();
            folderBrowserDialog1 = new FolderBrowserDialog();
            button2 = new Button();
            lblStatus = new Label();
            label3 = new Label();
            tbxLog = new TextBox();
            label2 = new Label();
            button3 = new Button();
            button4 = new Button();
            button5 = new Button();
            button6 = new Button();
            button7 = new Button();
            label4 = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(350, 20);
            label1.TabIndex = 0;
            label1.Text = "Folder where you unzipped google takeout zip files";
            // 
            // tbxFolderPath
            // 
            tbxFolderPath.Location = new Point(12, 32);
            tbxFolderPath.Name = "tbxFolderPath";
            tbxFolderPath.Size = new Size(457, 27);
            tbxFolderPath.TabIndex = 1;
            // 
            // button1
            // 
            button1.Location = new Point(475, 32);
            button1.Name = "button1";
            button1.Size = new Size(75, 27);
            button1.TabIndex = 2;
            button1.Text = "Search";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(12, 65);
            button2.Name = "button2";
            button2.Size = new Size(79, 27);
            button2.TabIndex = 3;
            button2.Text = "Validate*";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblStatus.ForeColor = Color.FromArgb(192, 0, 0);
            lblStatus.Location = new Point(70, 375);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(21, 20);
            lblStatus.TabIndex = 4;
            lblStatus.Text = "...";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 375);
            label3.Name = "label3";
            label3.Size = new Size(52, 20);
            label3.TabIndex = 5;
            label3.Text = "Status:";
            // 
            // tbxLog
            // 
            tbxLog.Location = new Point(12, 136);
            tbxLog.Multiline = true;
            tbxLog.Name = "tbxLog";
            tbxLog.ScrollBars = ScrollBars.Vertical;
            tbxLog.Size = new Size(818, 236);
            tbxLog.TabIndex = 6;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 113);
            label2.Name = "label2";
            label2.Size = new Size(34, 20);
            label2.TabIndex = 7;
            label2.Text = "Log";
            // 
            // button3
            // 
            button3.Location = new Point(97, 65);
            button3.Name = "button3";
            button3.Size = new Size(57, 27);
            button3.TabIndex = 8;
            button3.Text = "Scan*";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // button4
            // 
            button4.Location = new Point(160, 65);
            button4.Name = "button4";
            button4.Size = new Size(153, 27);
            button4.TabIndex = 9;
            button4.Text = "Scan for live photos";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // button5
            // 
            button5.Location = new Point(571, 65);
            button5.Name = "button5";
            button5.Size = new Size(150, 27);
            button5.TabIndex = 10;
            button5.Text = "Scan for reorganize";
            button5.UseVisualStyleBackColor = true;
            button5.Click += button5_Click;
            // 
            // button6
            // 
            button6.Location = new Point(319, 65);
            button6.Name = "button6";
            button6.Size = new Size(246, 27);
            button6.TabIndex = 11;
            button6.Text = "Remove video part of live photos";
            button6.UseVisualStyleBackColor = true;
            button6.Click += button6_Click;
            // 
            // button7
            // 
            button7.Location = new Point(727, 65);
            button7.Name = "button7";
            button7.Size = new Size(103, 27);
            button7.TabIndex = 12;
            button7.Text = "Reorganize";
            button7.UseVisualStyleBackColor = true;
            button7.Click += button7_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 9F);
            label4.Location = new Point(192, 113);
            label4.Name = "label4";
            label4.Size = new Size(638, 15);
            label4.TabIndex = 13;
            label4.Text = "There are no confirmation dialog on actions. Think before click. Keep your files backed up before complete and validate.";
            // 
            // FrmMain
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(842, 404);
            Controls.Add(label4);
            Controls.Add(button7);
            Controls.Add(button6);
            Controls.Add(button5);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(label2);
            Controls.Add(tbxLog);
            Controls.Add(label3);
            Controls.Add(lblStatus);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(tbxFolderPath);
            Controls.Add(label1);
            Font = new Font("Segoe UI", 11F);
            Margin = new Padding(3, 4, 3, 4);
            Name = "FrmMain";
            Text = "GooglePhotosTakeoutOrganize";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox tbxFolderPath;
        private Button button1;
        private FolderBrowserDialog folderBrowserDialog1;
        private Button button2;
        private Label lblStatus;
        private Label label3;
        private TextBox tbxLog;
        private Label label2;
        private Button button3;
        private Button button4;
        private Button button5;
        private Button button6;
        private Button button7;
        private Label label4;
    }
}
