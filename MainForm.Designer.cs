namespace Toleralize2011_0
{
    partial class mainForm
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
            this.openCirFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.calculateButton = new System.Windows.Forms.Button();
            this.schemeDataGrid = new System.Windows.Forms.DataGridView();
            this.nameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.typeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.knot1Column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.knot2Column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.knot3Column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.knot4Column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.valueColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toleranceColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.targetFormuleComboBox = new System.Windows.Forms.ComboBox();
            this.resultsDataGrid = new System.Windows.Forms.DataGridView();
            this.elementsColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.formuleColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.valueForPositiveColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.valueForNegativeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.errorTextBox = new System.Windows.Forms.TextBox();
            this.errorLabel = new System.Windows.Forms.Label();
            this.mainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.файлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.открытьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.вывестиРезультатВФайлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.выходToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.операцииToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.посчитатьОпределителиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.formuleNameLabel = new System.Windows.Forms.Label();
            this.resultInfoGroupBox = new System.Windows.Forms.GroupBox();
            this.valueForNegativeTextBox = new System.Windows.Forms.TextBox();
            this.valueForPositiveTextBox = new System.Windows.Forms.TextBox();
            this.valueForNegativeLabel = new System.Windows.Forms.Label();
            this.valueForPositiveLabel = new System.Windows.Forms.Label();
            this.formuleLabel = new System.Windows.Forms.Label();
            this.formuleTextBox = new System.Windows.Forms.RichTextBox();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.calculationsBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.progressStatusStrip = new System.Windows.Forms.StatusStrip();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.calculationsProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.schemeDataGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.resultsDataGrid)).BeginInit();
            this.mainMenuStrip.SuspendLayout();
            this.resultInfoGroupBox.SuspendLayout();
            this.progressStatusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // openCirFileDialog
            // 
            this.openCirFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.openCirFileDialog_FileOk);
            // 
            // calculateButton
            // 
            this.calculateButton.Enabled = false;
            this.calculateButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.calculateButton.Location = new System.Drawing.Point(450, 278);
            this.calculateButton.Name = "calculateButton";
            this.calculateButton.Size = new System.Drawing.Size(101, 23);
            this.calculateButton.TabIndex = 1;
            this.calculateButton.Text = "Рассчитать";
            this.calculateButton.UseVisualStyleBackColor = true;
            this.calculateButton.Click += new System.EventHandler(this.button2_Click);
            // 
            // schemeDataGrid
            // 
            this.schemeDataGrid.AllowUserToAddRows = false;
            this.schemeDataGrid.AllowUserToDeleteRows = false;
            this.schemeDataGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.schemeDataGrid.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.schemeDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.schemeDataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.nameColumn,
            this.typeColumn,
            this.knot1Column,
            this.knot2Column,
            this.knot3Column,
            this.knot4Column,
            this.valueColumn,
            this.toleranceColumn});
            this.schemeDataGrid.Enabled = false;
            this.schemeDataGrid.Location = new System.Drawing.Point(12, 27);
            this.schemeDataGrid.MaximumSize = new System.Drawing.Size(768, 235);
            this.schemeDataGrid.Name = "schemeDataGrid";
            this.schemeDataGrid.RowHeadersVisible = false;
            this.schemeDataGrid.RowHeadersWidth = 30;
            this.schemeDataGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.schemeDataGrid.ShowEditingIcon = false;
            this.schemeDataGrid.Size = new System.Drawing.Size(768, 235);
            this.schemeDataGrid.TabIndex = 2;
            this.schemeDataGrid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.schemeDataGrid_CellContentClick);
            this.schemeDataGrid.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.schemeDataGrid_CellValueChanged);
            this.schemeDataGrid.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.schemeDataGrid_ColumnHeaderMouseClick);
            this.schemeDataGrid.CurrentCellDirtyStateChanged += new System.EventHandler(this.schemeDataGrid_CurrentCellDirtyStateChanged);
            this.schemeDataGrid.SelectionChanged += new System.EventHandler(this.schemeDataGrid_SelectionChanged);
            // 
            // nameColumn
            // 
            this.nameColumn.HeaderText = "Название";
            this.nameColumn.Name = "nameColumn";
            this.nameColumn.ReadOnly = true;
            this.nameColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.nameColumn.Width = 82;
            // 
            // typeColumn
            // 
            this.typeColumn.HeaderText = "Тип";
            this.typeColumn.Name = "typeColumn";
            this.typeColumn.ReadOnly = true;
            this.typeColumn.Width = 51;
            // 
            // knot1Column
            // 
            this.knot1Column.HeaderText = "Узел 1";
            this.knot1Column.Name = "knot1Column";
            this.knot1Column.ReadOnly = true;
            this.knot1Column.Width = 67;
            // 
            // knot2Column
            // 
            this.knot2Column.HeaderText = "Узел 2";
            this.knot2Column.Name = "knot2Column";
            this.knot2Column.ReadOnly = true;
            this.knot2Column.Width = 67;
            // 
            // knot3Column
            // 
            this.knot3Column.HeaderText = "Узел 3";
            this.knot3Column.Name = "knot3Column";
            this.knot3Column.ReadOnly = true;
            this.knot3Column.Width = 67;
            // 
            // knot4Column
            // 
            this.knot4Column.HeaderText = "Узел 4";
            this.knot4Column.Name = "knot4Column";
            this.knot4Column.ReadOnly = true;
            this.knot4Column.Width = 67;
            // 
            // valueColumn
            // 
            this.valueColumn.HeaderText = "Значение";
            this.valueColumn.Name = "valueColumn";
            this.valueColumn.ReadOnly = true;
            this.valueColumn.Width = 80;
            // 
            // toleranceColumn
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Yellow;
            this.toleranceColumn.DefaultCellStyle = dataGridViewCellStyle1;
            this.toleranceColumn.HeaderText = "Допуск";
            this.toleranceColumn.Name = "toleranceColumn";
            this.toleranceColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.toleranceColumn.Width = 70;
            // 
            // targetFormuleComboBox
            // 
            this.targetFormuleComboBox.Enabled = false;
            this.targetFormuleComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.targetFormuleComboBox.FormattingEnabled = true;
            this.targetFormuleComboBox.Items.AddRange(new object[] {
            "Допуски для выбранных элементов",
            "Допуски для выбранных элементов (+ для отрицательной погрешности)",
            "Погрешность для выбранных элементов",
            "Погрешность для выбранных элементов (+для отрицательных допусков)",
            "Погрешности для каждого отдельного элемента",
            "Погрешности для каждого отдельного элемента (+для отрицательных допусков)",
            "Все погрешности",
            "Все погрешности (+для отрицательных допусков)",
            "Дробная ССФ"});
            this.targetFormuleComboBox.Location = new System.Drawing.Point(12, 278);
            this.targetFormuleComboBox.Name = "targetFormuleComboBox";
            this.targetFormuleComboBox.Size = new System.Drawing.Size(432, 21);
            this.targetFormuleComboBox.TabIndex = 4;
            this.targetFormuleComboBox.SelectedValueChanged += new System.EventHandler(this.targetFormuleComboBox_SelectedValueChanged);
            this.targetFormuleComboBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.targetFormuleComboBox_KeyPress);
            // 
            // resultsDataGrid
            // 
            this.resultsDataGrid.AllowUserToAddRows = false;
            this.resultsDataGrid.AllowUserToDeleteRows = false;
            this.resultsDataGrid.AllowUserToResizeColumns = false;
            this.resultsDataGrid.AllowUserToResizeRows = false;
            this.resultsDataGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.resultsDataGrid.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.resultsDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.resultsDataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.elementsColumn,
            this.formuleColumn,
            this.valueForPositiveColumn,
            this.valueForNegativeColumn});
            this.resultsDataGrid.Enabled = false;
            this.resultsDataGrid.Location = new System.Drawing.Point(16, 330);
            this.resultsDataGrid.MultiSelect = false;
            this.resultsDataGrid.Name = "resultsDataGrid";
            this.resultsDataGrid.ReadOnly = true;
            this.resultsDataGrid.RowHeadersVisible = false;
            this.resultsDataGrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.resultsDataGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.resultsDataGrid.Size = new System.Drawing.Size(392, 231);
            this.resultsDataGrid.TabIndex = 5;
            this.resultsDataGrid.Visible = false;
            this.resultsDataGrid.SelectionChanged += new System.EventHandler(this.resultsDataGrid_SelectionChanged);
            // 
            // elementsColumn
            // 
            this.elementsColumn.HeaderText = "Элементы";
            this.elementsColumn.Name = "elementsColumn";
            this.elementsColumn.ReadOnly = true;
            this.elementsColumn.Visible = false;
            this.elementsColumn.Width = 84;
            // 
            // formuleColumn
            // 
            this.formuleColumn.HeaderText = "Формула";
            this.formuleColumn.Name = "formuleColumn";
            this.formuleColumn.ReadOnly = true;
            this.formuleColumn.Visible = false;
            this.formuleColumn.Width = 80;
            // 
            // valueForPositiveColumn
            // 
            this.valueForPositiveColumn.HeaderText = "Значение (+)";
            this.valueForPositiveColumn.Name = "valueForPositiveColumn";
            this.valueForPositiveColumn.ReadOnly = true;
            this.valueForPositiveColumn.Visible = false;
            this.valueForPositiveColumn.Width = 95;
            // 
            // valueForNegativeColumn
            // 
            this.valueForNegativeColumn.HeaderText = "Значение (-)";
            this.valueForNegativeColumn.Name = "valueForNegativeColumn";
            this.valueForNegativeColumn.ReadOnly = true;
            this.valueForNegativeColumn.Visible = false;
            this.valueForNegativeColumn.Width = 92;
            // 
            // errorTextBox
            // 
            this.errorTextBox.Location = new System.Drawing.Point(684, 278);
            this.errorTextBox.Name = "errorTextBox";
            this.errorTextBox.Size = new System.Drawing.Size(96, 20);
            this.errorTextBox.TabIndex = 6;
            this.errorTextBox.Visible = false;
            this.errorTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.errorTextBox_KeyPress);
            // 
            // errorLabel
            // 
            this.errorLabel.AutoSize = true;
            this.errorLabel.Location = new System.Drawing.Point(557, 283);
            this.errorLabel.Name = "errorLabel";
            this.errorLabel.Size = new System.Drawing.Size(121, 13);
            this.errorLabel.TabIndex = 7;
            this.errorLabel.Text = "Введите погрешность:";
            this.errorLabel.Visible = false;
            // 
            // mainMenuStrip
            // 
            this.mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файлToolStripMenuItem,
            this.операцииToolStripMenuItem});
            this.mainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.mainMenuStrip.Name = "mainMenuStrip";
            this.mainMenuStrip.Size = new System.Drawing.Size(792, 24);
            this.mainMenuStrip.TabIndex = 8;
            this.mainMenuStrip.Text = "menuStrip1";
            // 
            // файлToolStripMenuItem
            // 
            this.файлToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.открытьToolStripMenuItem,
            this.вывестиРезультатВФайлToolStripMenuItem,
            this.выходToolStripMenuItem});
            this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            this.файлToolStripMenuItem.Size = new System.Drawing.Size(45, 20);
            this.файлToolStripMenuItem.Text = "Файл";
            // 
            // открытьToolStripMenuItem
            // 
            this.открытьToolStripMenuItem.Name = "открытьToolStripMenuItem";
            this.открытьToolStripMenuItem.Size = new System.Drawing.Size(234, 22);
            this.открытьToolStripMenuItem.Text = "Открыть...";
            this.открытьToolStripMenuItem.Click += new System.EventHandler(this.открытьToolStripMenuItem_Click);
            // 
            // вывестиРезультатВФайлToolStripMenuItem
            // 
            this.вывестиРезультатВФайлToolStripMenuItem.Name = "вывестиРезультатВФайлToolStripMenuItem";
            this.вывестиРезультатВФайлToolStripMenuItem.Size = new System.Drawing.Size(234, 22);
            this.вывестиРезультатВФайлToolStripMenuItem.Text = "Вывести результат в файл...";
            this.вывестиРезультатВФайлToolStripMenuItem.Visible = false;
            this.вывестиРезультатВФайлToolStripMenuItem.Click += new System.EventHandler(this.вывестиРезультатВФайлToolStripMenuItem_Click);
            // 
            // выходToolStripMenuItem
            // 
            this.выходToolStripMenuItem.Name = "выходToolStripMenuItem";
            this.выходToolStripMenuItem.Size = new System.Drawing.Size(234, 22);
            this.выходToolStripMenuItem.Text = "Выход";
            this.выходToolStripMenuItem.Click += new System.EventHandler(this.выходToolStripMenuItem_Click);
            // 
            // операцииToolStripMenuItem
            // 
            this.операцииToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.посчитатьОпределителиToolStripMenuItem});
            this.операцииToolStripMenuItem.Name = "операцииToolStripMenuItem";
            this.операцииToolStripMenuItem.Size = new System.Drawing.Size(75, 20);
            this.операцииToolStripMenuItem.Text = "Вычиления";
            // 
            // посчитатьОпределителиToolStripMenuItem
            // 
            this.посчитатьОпределителиToolStripMenuItem.Enabled = false;
            this.посчитатьОпределителиToolStripMenuItem.Name = "посчитатьОпределителиToolStripMenuItem";
            this.посчитатьОпределителиToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.посчитатьОпределителиToolStripMenuItem.Text = "Определители";
            this.посчитатьОпределителиToolStripMenuItem.Click += new System.EventHandler(this.посчитатьОпределителиToolStripMenuItem_Click);
            // 
            // formuleNameLabel
            // 
            this.formuleNameLabel.AutoSize = true;
            this.formuleNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.formuleNameLabel.Location = new System.Drawing.Point(12, 307);
            this.formuleNameLabel.Name = "formuleNameLabel";
            this.formuleNameLabel.Size = new System.Drawing.Size(0, 20);
            this.formuleNameLabel.TabIndex = 19;
            // 
            // resultInfoGroupBox
            // 
            this.resultInfoGroupBox.Controls.Add(this.valueForNegativeTextBox);
            this.resultInfoGroupBox.Controls.Add(this.valueForPositiveTextBox);
            this.resultInfoGroupBox.Controls.Add(this.valueForNegativeLabel);
            this.resultInfoGroupBox.Controls.Add(this.valueForPositiveLabel);
            this.resultInfoGroupBox.Controls.Add(this.formuleLabel);
            this.resultInfoGroupBox.Controls.Add(this.formuleTextBox);
            this.resultInfoGroupBox.Enabled = false;
            this.resultInfoGroupBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.resultInfoGroupBox.Location = new System.Drawing.Point(414, 330);
            this.resultInfoGroupBox.Name = "resultInfoGroupBox";
            this.resultInfoGroupBox.Size = new System.Drawing.Size(366, 231);
            this.resultInfoGroupBox.TabIndex = 20;
            this.resultInfoGroupBox.TabStop = false;
            this.resultInfoGroupBox.Visible = false;
            // 
            // valueForNegativeTextBox
            // 
            this.valueForNegativeTextBox.Location = new System.Drawing.Point(9, 202);
            this.valueForNegativeTextBox.Name = "valueForNegativeTextBox";
            this.valueForNegativeTextBox.ReadOnly = true;
            this.valueForNegativeTextBox.Size = new System.Drawing.Size(351, 23);
            this.valueForNegativeTextBox.TabIndex = 26;
            this.valueForNegativeTextBox.Visible = false;
            // 
            // valueForPositiveTextBox
            // 
            this.valueForPositiveTextBox.Location = new System.Drawing.Point(6, 156);
            this.valueForPositiveTextBox.Name = "valueForPositiveTextBox";
            this.valueForPositiveTextBox.ReadOnly = true;
            this.valueForPositiveTextBox.Size = new System.Drawing.Size(354, 23);
            this.valueForPositiveTextBox.TabIndex = 25;
            // 
            // valueForNegativeLabel
            // 
            this.valueForNegativeLabel.AutoEllipsis = true;
            this.valueForNegativeLabel.AutoSize = true;
            this.valueForNegativeLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.valueForNegativeLabel.Location = new System.Drawing.Point(6, 182);
            this.valueForNegativeLabel.Name = "valueForNegativeLabel";
            this.valueForNegativeLabel.Size = new System.Drawing.Size(112, 17);
            this.valueForNegativeLabel.TabIndex = 23;
            this.valueForNegativeLabel.Text = "Значение (-), %";
            this.valueForNegativeLabel.Visible = false;
            // 
            // valueForPositiveLabel
            // 
            this.valueForPositiveLabel.AutoEllipsis = true;
            this.valueForPositiveLabel.AutoSize = true;
            this.valueForPositiveLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.valueForPositiveLabel.Location = new System.Drawing.Point(6, 136);
            this.valueForPositiveLabel.Name = "valueForPositiveLabel";
            this.valueForPositiveLabel.Size = new System.Drawing.Size(93, 17);
            this.valueForPositiveLabel.TabIndex = 22;
            this.valueForPositiveLabel.Text = "Значение, %";
            // 
            // formuleLabel
            // 
            this.formuleLabel.AutoSize = true;
            this.formuleLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.formuleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.formuleLabel.Location = new System.Drawing.Point(6, 19);
            this.formuleLabel.Name = "formuleLabel";
            this.formuleLabel.Size = new System.Drawing.Size(73, 17);
            this.formuleLabel.TabIndex = 21;
            this.formuleLabel.Text = "Формула:";
            this.formuleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // formuleTextBox
            // 
            this.formuleTextBox.Location = new System.Drawing.Point(6, 40);
            this.formuleTextBox.Name = "formuleTextBox";
            this.formuleTextBox.ReadOnly = true;
            this.formuleTextBox.Size = new System.Drawing.Size(354, 93);
            this.formuleTextBox.TabIndex = 20;
            this.formuleTextBox.Text = "";
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Название";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn1.Width = 82;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Тип";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 51;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "Узел 1";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Width = 67;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "Узел 2";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Width = 67;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.HeaderText = "Узел 3";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.Width = 67;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.HeaderText = "Узел 4";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            this.dataGridViewTextBoxColumn6.Width = 67;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.HeaderText = "Значение";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.ReadOnly = true;
            this.dataGridViewTextBoxColumn7.Width = 80;
            // 
            // dataGridViewTextBoxColumn8
            // 
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Yellow;
            this.dataGridViewTextBoxColumn8.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewTextBoxColumn8.HeaderText = "Допуск";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn8.Width = 70;
            // 
            // dataGridViewTextBoxColumn9
            // 
            this.dataGridViewTextBoxColumn9.HeaderText = "Элементы";
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            this.dataGridViewTextBoxColumn9.ReadOnly = true;
            this.dataGridViewTextBoxColumn9.Visible = false;
            this.dataGridViewTextBoxColumn9.Width = 84;
            // 
            // dataGridViewTextBoxColumn10
            // 
            this.dataGridViewTextBoxColumn10.HeaderText = "Column1";
            this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
            this.dataGridViewTextBoxColumn10.ReadOnly = true;
            this.dataGridViewTextBoxColumn10.Visible = false;
            this.dataGridViewTextBoxColumn10.Width = 465;
            // 
            // dataGridViewTextBoxColumn11
            // 
            this.dataGridViewTextBoxColumn11.HeaderText = "Column1";
            this.dataGridViewTextBoxColumn11.Name = "dataGridViewTextBoxColumn11";
            this.dataGridViewTextBoxColumn11.ReadOnly = true;
            this.dataGridViewTextBoxColumn11.Visible = false;
            this.dataGridViewTextBoxColumn11.Width = 135;
            // 
            // dataGridViewTextBoxColumn12
            // 
            this.dataGridViewTextBoxColumn12.HeaderText = "Column1";
            this.dataGridViewTextBoxColumn12.Name = "dataGridViewTextBoxColumn12";
            this.dataGridViewTextBoxColumn12.ReadOnly = true;
            this.dataGridViewTextBoxColumn12.Visible = false;
            this.dataGridViewTextBoxColumn12.Width = 135;
            // 
            // calculationsBackgroundWorker
            // 
            this.calculationsBackgroundWorker.WorkerReportsProgress = true;
            this.calculationsBackgroundWorker.WorkerSupportsCancellation = true;
            this.calculationsBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.calculationsBackgroundWorker_DoWork);
            this.calculationsBackgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.calculationsBackgroundWorker_ProgressChanged);
            this.calculationsBackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.calculationsBackgroundWorker_RunWorkerCompleted);
            // 
            // progressStatusStrip
            // 
            this.progressStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel,
            this.calculationsProgressBar});
            this.progressStatusStrip.Location = new System.Drawing.Point(0, 564);
            this.progressStatusStrip.Name = "progressStatusStrip";
            this.progressStatusStrip.Size = new System.Drawing.Size(792, 22);
            this.progressStatusStrip.TabIndex = 21;
            this.progressStatusStrip.Text = "statusStrip1";
            // 
            // statusLabel
            // 
            this.statusLabel.AutoSize = false;
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(200, 17);
            this.statusLabel.Text = "Ожидание";
            // 
            // calculationsProgressBar
            // 
            this.calculationsProgressBar.Name = "calculationsProgressBar";
            this.calculationsProgressBar.Size = new System.Drawing.Size(200, 16);
            this.calculationsProgressBar.Step = 1;
            this.calculationsProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.calculationsProgressBar.Visible = false;
            // 
            // mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 586);
            this.Controls.Add(this.progressStatusStrip);
            this.Controls.Add(this.resultInfoGroupBox);
            this.Controls.Add(this.formuleNameLabel);
            this.Controls.Add(this.errorLabel);
            this.Controls.Add(this.errorTextBox);
            this.Controls.Add(this.resultsDataGrid);
            this.Controls.Add(this.targetFormuleComboBox);
            this.Controls.Add(this.schemeDataGrid);
            this.Controls.Add(this.calculateButton);
            this.Controls.Add(this.mainMenuStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MainMenuStrip = this.mainMenuStrip;
            this.Name = "mainForm";
            this.Text = "Toleralize";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.mainForm_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.schemeDataGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.resultsDataGrid)).EndInit();
            this.mainMenuStrip.ResumeLayout(false);
            this.mainMenuStrip.PerformLayout();
            this.resultInfoGroupBox.ResumeLayout(false);
            this.resultInfoGroupBox.PerformLayout();
            this.progressStatusStrip.ResumeLayout(false);
            this.progressStatusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openCirFileDialog;
        private System.Windows.Forms.Button calculateButton;
        private System.Windows.Forms.DataGridView schemeDataGrid;
        private System.Windows.Forms.ComboBox targetFormuleComboBox;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private System.Windows.Forms.DataGridView resultsDataGrid;
        private System.Windows.Forms.TextBox errorTextBox;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn11;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn12;
        private System.Windows.Forms.Label errorLabel;
        private System.Windows.Forms.MenuStrip mainMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem файлToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem открытьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem выходToolStripMenuItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn typeColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn knot1Column;
        private System.Windows.Forms.DataGridViewTextBoxColumn knot2Column;
        private System.Windows.Forms.DataGridViewTextBoxColumn knot3Column;
        private System.Windows.Forms.DataGridViewTextBoxColumn knot4Column;
        private System.Windows.Forms.DataGridViewTextBoxColumn valueColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn toleranceColumn;
        private System.Windows.Forms.Label formuleNameLabel;
        private System.Windows.Forms.GroupBox resultInfoGroupBox;
        private System.Windows.Forms.TextBox valueForNegativeTextBox;
        private System.Windows.Forms.TextBox valueForPositiveTextBox;
        private System.Windows.Forms.Label valueForNegativeLabel;
        private System.Windows.Forms.Label valueForPositiveLabel;
        private System.Windows.Forms.Label formuleLabel;
        private System.Windows.Forms.RichTextBox formuleTextBox;
        private System.Windows.Forms.DataGridViewTextBoxColumn elementsColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn formuleColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn valueForPositiveColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn valueForNegativeColumn;
        private System.Windows.Forms.ToolStripMenuItem вывестиРезультатВФайлToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem операцииToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem посчитатьОпределителиToolStripMenuItem;
        private System.ComponentModel.BackgroundWorker calculationsBackgroundWorker;
        private System.Windows.Forms.StatusStrip progressStatusStrip;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;
        private System.Windows.Forms.ToolStripProgressBar calculationsProgressBar;
    }
}