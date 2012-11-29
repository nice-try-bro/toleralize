namespace Toleralize2011_0
{
    partial class ProgressForm
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
            this.components = new System.ComponentModel.Container();
            this.calculationsProgressBar = new System.Windows.Forms.ProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.formulesCountLabel = new System.Windows.Forms.Label();
            this.stopButton = new System.Windows.Forms.Button();
            this.workProcessControlTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // calculationsProgressBar
            // 
            this.calculationsProgressBar.Location = new System.Drawing.Point(12, 12);
            this.calculationsProgressBar.Name = "calculationsProgressBar";
            this.calculationsProgressBar.Size = new System.Drawing.Size(310, 23);
            this.calculationsProgressBar.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Формулы:";
            // 
            // formulesCountLabel
            // 
            this.formulesCountLabel.AutoSize = true;
            this.formulesCountLabel.Location = new System.Drawing.Point(78, 56);
            this.formulesCountLabel.Name = "formulesCountLabel";
            this.formulesCountLabel.Size = new System.Drawing.Size(35, 13);
            this.formulesCountLabel.TabIndex = 2;
            this.formulesCountLabel.Text = "label2";
            // 
            // stopButton
            // 
            this.stopButton.Location = new System.Drawing.Point(247, 51);
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(75, 23);
            this.stopButton.TabIndex = 3;
            this.stopButton.Text = "Стоп";
            this.stopButton.UseVisualStyleBackColor = true;
            // 
            // workProcessControlTimer
            // 
            this.workProcessControlTimer.Tick += new System.EventHandler(this.workProcessControlTimer_Tick);
            // 
            // ProgressForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(334, 95);
            this.Controls.Add(this.stopButton);
            this.Controls.Add(this.formulesCountLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.calculationsProgressBar);
            this.Name = "ProgressForm";
            this.Text = "Вычисления...";
            this.Load += new System.EventHandler(this.ProgressForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar calculationsProgressBar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label formulesCountLabel;
        private System.Windows.Forms.Button stopButton;
        private System.Windows.Forms.Timer workProcessControlTimer;
    }
}