namespace AppCuesTranslations
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
            this.selectFolderButton = new System.Windows.Forms.Button();
            this.filesListBox = new System.Windows.Forms.ListBox();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.addTranslationsButton = new System.Windows.Forms.Button();
            this.FeedbackLabel = new System.Windows.Forms.Label();
            this.overwriteCheckBox = new System.Windows.Forms.CheckBox();
            this.FolderLabel = new System.Windows.Forms.Label();
            this.logLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // selectFolderButton
            // 
            this.selectFolderButton.Location = new System.Drawing.Point(12, 24);
            this.selectFolderButton.Name = "selectFolderButton";
            this.selectFolderButton.Size = new System.Drawing.Size(286, 50);
            this.selectFolderButton.TabIndex = 0;
            this.selectFolderButton.Text = "Select AppCues folder with JSON files";
            this.selectFolderButton.UseVisualStyleBackColor = true;
            this.selectFolderButton.Click += new System.EventHandler(this.SelectFolderButton_Click);
            // 
            // filesListBox
            // 
            this.filesListBox.FormattingEnabled = true;
            this.filesListBox.ItemHeight = 15;
            this.filesListBox.Location = new System.Drawing.Point(12, 118);
            this.filesListBox.Name = "filesListBox";
            this.filesListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.filesListBox.Size = new System.Drawing.Size(361, 109);
            this.filesListBox.TabIndex = 1;
            this.filesListBox.Visible = false;
            // 
            // addTranslationsButton
            // 
            this.addTranslationsButton.Enabled = false;
            this.addTranslationsButton.Location = new System.Drawing.Point(12, 261);
            this.addTranslationsButton.Name = "addTranslationsButton";
            this.addTranslationsButton.Size = new System.Drawing.Size(361, 50);
            this.addTranslationsButton.TabIndex = 2;
            this.addTranslationsButton.Text = "Add translations to selected files";
            this.addTranslationsButton.UseVisualStyleBackColor = true;
            this.addTranslationsButton.Visible = false;
            this.addTranslationsButton.Click += new System.EventHandler(this.buttonGetTranslations_Click);
            // 
            // FeedbackLabel
            // 
            this.FeedbackLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.FeedbackLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.FeedbackLabel.Location = new System.Drawing.Point(0, 0);
            this.FeedbackLabel.Name = "FeedbackLabel";
            this.FeedbackLabel.Size = new System.Drawing.Size(1135, 21);
            this.FeedbackLabel.TabIndex = 3;
            this.FeedbackLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // overwriteCheckBox
            // 
            this.overwriteCheckBox.AutoSize = true;
            this.overwriteCheckBox.Location = new System.Drawing.Point(12, 233);
            this.overwriteCheckBox.Name = "overwriteCheckBox";
            this.overwriteCheckBox.Size = new System.Drawing.Size(217, 19);
            this.overwriteCheckBox.TabIndex = 4;
            this.overwriteCheckBox.Text = "Overwrite existing translation in files";
            this.overwriteCheckBox.UseVisualStyleBackColor = true;
            this.overwriteCheckBox.Visible = false;
            // 
            // FolderLabel
            // 
            this.FolderLabel.AutoSize = true;
            this.FolderLabel.Location = new System.Drawing.Point(12, 100);
            this.FolderLabel.Name = "FolderLabel";
            this.FolderLabel.Size = new System.Drawing.Size(341, 15);
            this.FolderLabel.TabIndex = 5;
            this.FolderLabel.Text = "Start by clicking the button to select a folder with AppCues files";
            this.FolderLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.FolderLabel.Visible = false;
            // 
            // logLabel
            // 
            this.logLabel.AutoSize = true;
            this.logLabel.Location = new System.Drawing.Point(394, 24);
            this.logLabel.Name = "logLabel";
            this.logLabel.Size = new System.Drawing.Size(0, 15);
            this.logLabel.TabIndex = 6;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1135, 450);
            this.Controls.Add(this.logLabel);
            this.Controls.Add(this.FolderLabel);
            this.Controls.Add(this.overwriteCheckBox);
            this.Controls.Add(this.FeedbackLabel);
            this.Controls.Add(this.addTranslationsButton);
            this.Controls.Add(this.filesListBox);
            this.Controls.Add(this.selectFolderButton);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        #endregion

        private Button selectFolderButton;
        private ListBox filesListBox;
        private FolderBrowserDialog folderBrowserDialog;
        private Button addTranslationsButton;
        private Label FeedbackLabel;
        private CheckBox overwriteCheckBox;
        private Label FolderLabel;
        private Label logLabel;
    }
}