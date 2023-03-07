namespace Sandboxa
{
    partial class UI
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
            this.button1 = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.exePath = new System.Windows.Forms.TextBox();
            this.exeName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.permList = new System.Windows.Forms.CheckedListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(314, 316);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(123, 35);
            this.button1.TabIndex = 0;
            this.button1.Text = "Execute";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFIle";
            // 
            // exePath
            // 
            this.exePath.CharacterCasing = System.Windows.Forms.CharacterCasing.Lower;
            this.exePath.Location = new System.Drawing.Point(82, 106);
            this.exePath.Name = "exePath";
            this.exePath.Size = new System.Drawing.Size(355, 20);
            this.exePath.TabIndex = 2;
            this.exePath.TextChanged += new System.EventHandler(this.exePath_TextChanged);
            // 
            // exeName
            // 
            this.exeName.Location = new System.Drawing.Point(518, 106);
            this.exeName.Name = "exeName";
            this.exeName.Size = new System.Drawing.Size(121, 20);
            this.exeName.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(82, 87);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "/path to executable";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(517, 87);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(105, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "/name of executable";
            // 
            // permList
            // 
            this.permList.FormattingEnabled = true;
            this.permList.Items.AddRange(new object[] {
            "Execution Access",
            "File Access",
            "Network Access",
            "User Interface Access",
            "File Dialog Access"});
            this.permList.Location = new System.Drawing.Point(85, 198);
            this.permList.Name = "permList";
            this.permList.Size = new System.Drawing.Size(120, 94);
            this.permList.TabIndex = 6;
            this.permList.SelectedIndexChanged += new System.EventHandler(this.permList_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(82, 183);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "/permissions";
            // 
            // UI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.permList);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.exeName);
            this.Controls.Add(this.exePath);
            this.Controls.Add(this.button1);
            this.Name = "UI";
            this.Text = "Sandboxa";
            this.Load += new System.EventHandler(this.UI_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TextBox exePath;
        private System.Windows.Forms.TextBox exeName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckedListBox permList;
        private System.Windows.Forms.Label label3;
    }
}