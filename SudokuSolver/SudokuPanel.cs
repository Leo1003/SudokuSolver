using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SudokuSolver
{
    class SudokuPanel : Panel
    {
        private Label[,] labels = new Label[9, 9];
        internal SudokuPanel() : base()
        {
            base.Size = new Size(356, 356);
            BackColor = Color.Black;
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    CreateLabel(j, i);
                }
            }
        }
        private void CreateLabel(int x, int y)
        {
            Label label = new Label();
            label.BackColor = Color.White;
            label.Font = new Font("微軟正黑體", 24F, FontStyle.Bold, GraphicsUnit.Point, 136);
            label.Location = new Point(36 * x + (x / 3 + 1) * 3 + (x + 1) * 2, 36 * y + (y / 3 + 1) * 3 + (y + 1) * 2);
            label.Name = "label";
            label.Margin = new Padding(0);
            label.Size = new Size(36, 36);
            label.TabIndex = 0;
            label.Text = "";//debug
            label.TextAlign = ContentAlignment.MiddleCenter;
            label.UseMnemonic = false;
            labels[x, y] = label;
            Controls.Add(label);
        }
        public new Size Size
        {
            get
            {
                return base.Size;
            }
            set
            {
            }
        }
    }
}
