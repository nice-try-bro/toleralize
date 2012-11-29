using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Toleralize2011_0
{
    public partial class StartForm : Form
    {
        ToolTip errorToolTip = new ToolTip();

        public StartForm()
        {
            InitializeComponent();
        }

        private void searchFileButton_Click(object sender, EventArgs e)
        {
            openCirFileDialog.ShowDialog();
        }

        private void openCirFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            fileNameTextBox.Text = openCirFileDialog.FileName;
        }

        private void startMainFormButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(fileNameTextBox.Text))
            {
                errorToolTip.Show("Введите имя файла", startMainFormButton, 1000);
                return;
            }
            if (!File.Exists(fileNameTextBox.Text))
            {
                errorToolTip.Show("Указанный файл не доступен", startMainFormButton, 1000);
                return;
            }
            this.Visible = false;
            mainForm form = new mainForm(fileNameTextBox.Text);
            //form.loadSchemeFromFile();
            form.ShowDialog();
        }
    }
}