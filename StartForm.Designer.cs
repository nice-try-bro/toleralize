namespace Toleralize2011_0
{
    partial class StartForm
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
            this.fileNameTextBox = new System.Windows.Forms.TextBox();
            this.startMainFormButton = new System.Windows.Forms.Button();
            this.fileNameLabel = new System.Windows.Forms.Label();
            this.searchFileButton = new System.Windows.Forms.Button();
            this.openCirFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // fileNameTextBox
            // 
            this.fileNameTextBox.Location = new System.Drawing.Point(12, 25);
            this.fileNameTextBox.Name = "fileNameTextBox";
            this.fileNameTextBox.Size = new System.Drawing.Size(222, 20);
            this.fileNameTextBox.TabIndex = 0;
            // 
            // startMainFormButton
            // 
            this.startMainFormButton.Location = new System.Drawing.Point(240, 54);
            this.startMainFormButton.Name = "startMainFormButton";
            this.startMainFormButton.Size = new System.Drawing.Size(86, 23);
            this.startMainFormButton.TabIndex = 1;
            this.startMainFormButton.Text = "Продолжить";
            this.startMainFormButton.UseVisualStyleBackColor = true;
            this.startMainFormButton.Click += new System.EventHandler(this.startMainFormButton_Click);
            // 
            // fileNameLabel
            // 
            this.fileNameLabel.AutoSize = true;
            this.fileNameLabel.Location = new System.Drawing.Point(9, 9);
            this.fileNameLabel.Name = "fileNameLabel";
            this.fileNameLabel.Size = new System.Drawing.Size(110, 13);
            this.fileNameLabel.TabIndex = 2;
            this.fileNameLabel.Text = "Введите имя файла:";
            // 
            // searchFileButton
            // 
            this.searchFileButton.Location = new System.Drawing.Point(240, 25);
            this.searchFileButton.Name = "searchFileButton";
            this.searchFileButton.Size = new System.Drawing.Size(86, 23);
            this.searchFileButton.TabIndex = 3;
            this.searchFileButton.Text = "Обзор...";
            this.searchFileButton.UseVisualStyleBackColor = true;
            this.searchFileButton.Click += new System.EventHandler(this.searchFileButton_Click);
            // 
            // openCirFileDialog
            // 
            this.openCirFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.openCirFileDialog_FileOk);
            // 
            // StartForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(336, 87);
            this.Controls.Add(this.searchFileButton);
            this.Controls.Add(this.fileNameLabel);
            this.Controls.Add(this.startMainFormButton);
            this.Controls.Add(this.fileNameTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "StartForm";
            this.Text = "Новая схема";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox fileNameTextBox;
        private System.Windows.Forms.Button startMainFormButton;
        private System.Windows.Forms.Label fileNameLabel;
        private System.Windows.Forms.Button searchFileButton;
        private System.Windows.Forms.OpenFileDialog openCirFileDialog;
    }
}