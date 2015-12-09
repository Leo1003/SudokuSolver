using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SudokuSolver
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
            SudokuPanel p = new SudokuPanel();
            p.Location = new Point(0, 0);
            Controls.Add(p);
        }

        private void FormMain_Load(object sender, EventArgs e)
        {

        }
    }
}
