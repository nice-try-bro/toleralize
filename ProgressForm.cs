using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace Toleralize2011_0
{
    public partial class ProgressForm : Form
    {
        Thread thread;
        public ProgressForm(Thread runningThread)
        {
            InitializeComponent();
            thread = runningThread;
        }

        private void ProgressForm_Load(object sender, EventArgs e)
        {
        }

        private void workProcessControlTimer_Tick(object sender, EventArgs e)
        {
            if (thread.IsAlive)
            {
                if (calculationsProgressBar.Value < 100)
                    calculationsProgressBar.Value++;
                else
                    calculationsProgressBar.Value = 0;
            }
            else
            {
                calculationsProgressBar.Value = 0;
                workProcessControlTimer.Stop();
            }
        }
    }
}
