namespace Toleralize2011_0
{
    partial class DetansForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.denominatorDetanValueTextBox = new System.Windows.Forms.TextBox();
            this.denominatorDetanFormuleTextBox = new System.Windows.Forms.RichTextBox();
            this.elementsDataGridView = new System.Windows.Forms.DataGridView();
            this.CounterColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SelectedElements = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RemovedElements = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SubtendedElements = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label2 = new System.Windows.Forms.Label();
            this.numeratorDetanValueTextBox = new System.Windows.Forms.TextBox();
            this.numeratorDetanFormuleTextBox = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.elementsDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // denominatorDetanValueTextBox
            // 
            this.denominatorDetanValueTextBox.Location = new System.Drawing.Point(412, 444);
            this.denominatorDetanValueTextBox.Name = "denominatorDetanValueTextBox";
            this.denominatorDetanValueTextBox.ReadOnly = true;
            this.denominatorDetanValueTextBox.Size = new System.Drawing.Size(370, 20);
            this.denominatorDetanValueTextBox.TabIndex = 4;
            // 
            // denominatorDetanFormuleTextBox
            // 
            this.denominatorDetanFormuleTextBox.Location = new System.Drawing.Point(412, 307);
            this.denominatorDetanFormuleTextBox.Name = "denominatorDetanFormuleTextBox";
            this.denominatorDetanFormuleTextBox.ReadOnly = true;
            this.denominatorDetanFormuleTextBox.Size = new System.Drawing.Size(370, 131);
            this.denominatorDetanFormuleTextBox.TabIndex = 3;
            this.denominatorDetanFormuleTextBox.Text = "";
            // 
            // elementsDataGridView
            // 
            this.elementsDataGridView.AllowUserToAddRows = false;
            this.elementsDataGridView.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.elementsDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.elementsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.elementsDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CounterColumn,
            this.SelectedElements,
            this.RemovedElements,
            this.SubtendedElements});
            this.elementsDataGridView.Location = new System.Drawing.Point(12, 12);
            this.elementsDataGridView.MultiSelect = false;
            this.elementsDataGridView.Name = "elementsDataGridView";
            this.elementsDataGridView.ReadOnly = true;
            this.elementsDataGridView.RowHeadersVisible = false;
            this.elementsDataGridView.Size = new System.Drawing.Size(770, 263);
            this.elementsDataGridView.TabIndex = 0;
            this.elementsDataGridView.SelectionChanged += new System.EventHandler(this.elementsDataGridView_SelectionChanged);
            // 
            // CounterColumn
            // 
            this.CounterColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.CounterColumn.DefaultCellStyle = dataGridViewCellStyle2;
            this.CounterColumn.Frozen = true;
            this.CounterColumn.HeaderText = "#";
            this.CounterColumn.Name = "CounterColumn";
            this.CounterColumn.ReadOnly = true;
            this.CounterColumn.Width = 40;
            // 
            // SelectedElements
            // 
            this.SelectedElements.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.SelectedElements.DefaultCellStyle = dataGridViewCellStyle3;
            this.SelectedElements.Frozen = true;
            this.SelectedElements.HeaderText = "Элементы";
            this.SelectedElements.Name = "SelectedElements";
            this.SelectedElements.ReadOnly = true;
            // 
            // RemovedElements
            // 
            this.RemovedElements.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.RemovedElements.DefaultCellStyle = dataGridViewCellStyle4;
            this.RemovedElements.HeaderText = "Удаленные";
            this.RemovedElements.MinimumWidth = 2;
            this.RemovedElements.Name = "RemovedElements";
            this.RemovedElements.ReadOnly = true;
            this.RemovedElements.Width = 108;
            // 
            // SubtendedElements
            // 
            this.SubtendedElements.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.SubtendedElements.DefaultCellStyle = dataGridViewCellStyle5;
            this.SubtendedElements.HeaderText = "Стянутые";
            this.SubtendedElements.Name = "SubtendedElements";
            this.SubtendedElements.ReadOnly = true;
            this.SubtendedElements.Width = 96;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(409, 278);
            this.label2.Name = "label2";
            this.label2.Padding = new System.Windows.Forms.Padding(10, 10, 0, 0);
            this.label2.Size = new System.Drawing.Size(253, 26);
            this.label2.TabIndex = 13;
            this.label2.Text = "Определитель знаменателя (D)";
            // 
            // numeratorDetanValueTextBox
            // 
            this.numeratorDetanValueTextBox.Location = new System.Drawing.Point(12, 444);
            this.numeratorDetanValueTextBox.Name = "numeratorDetanValueTextBox";
            this.numeratorDetanValueTextBox.ReadOnly = true;
            this.numeratorDetanValueTextBox.Size = new System.Drawing.Size(370, 20);
            this.numeratorDetanValueTextBox.TabIndex = 2;
            // 
            // numeratorDetanFormuleTextBox
            // 
            this.numeratorDetanFormuleTextBox.Location = new System.Drawing.Point(12, 307);
            this.numeratorDetanFormuleTextBox.Name = "numeratorDetanFormuleTextBox";
            this.numeratorDetanFormuleTextBox.ReadOnly = true;
            this.numeratorDetanFormuleTextBox.Size = new System.Drawing.Size(370, 131);
            this.numeratorDetanFormuleTextBox.TabIndex = 1;
            this.numeratorDetanFormuleTextBox.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(12, 278);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(10, 10, 0, 0);
            this.label1.Size = new System.Drawing.Size(233, 26);
            this.label1.TabIndex = 8;
            this.label1.Text = "Определитель числителя (N)";
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridViewTextBoxColumn1.HeaderText = "Элементы (∞)";
            this.dataGridViewTextBoxColumn1.MinimumWidth = 2;
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // DetansForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(794, 471);
            this.Controls.Add(this.denominatorDetanValueTextBox);
            this.Controls.Add(this.denominatorDetanFormuleTextBox);
            this.Controls.Add(this.elementsDataGridView);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.numeratorDetanValueTextBox);
            this.Controls.Add(this.numeratorDetanFormuleTextBox);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MinimumSize = new System.Drawing.Size(406, 372);
            this.Name = "DetansForm";
            this.Text = "Определители";
            this.Load += new System.EventHandler(this.DetansForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.elementsDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox denominatorDetanValueTextBox;
        private System.Windows.Forms.RichTextBox denominatorDetanFormuleTextBox;
        private System.Windows.Forms.DataGridView elementsDataGridView;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox numeratorDetanValueTextBox;
        private System.Windows.Forms.RichTextBox numeratorDetanFormuleTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn CounterColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn SelectedElements;
        private System.Windows.Forms.DataGridViewTextBoxColumn RemovedElements;
        private System.Windows.Forms.DataGridViewTextBoxColumn SubtendedElements;

    }
}