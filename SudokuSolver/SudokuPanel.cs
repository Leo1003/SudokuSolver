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
        private int LastSelectx = -1;
        private int LastSelecty = -1;
        private SudoBlock[,] labels = new SudoBlock[9, 9];
        private bool _lock = false;
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
        public bool IsEmpty()
        {
            foreach (SudoBlock item in labels)
            {
                if (item.Text != "")
                {
                    return false;
                }
            }
            return true;
        }
        public bool IsFull()
        {
            foreach (SudoBlock item in labels)
            {
                if (item.Text == "")
                {
                    return false;
                }
            }
            return true;
        }
        public SudoBlock ActiveBlock
        {
            get
            {
                if (LastSelectx == -1) return null;
                return labels[LastSelectx, LastSelecty];
            }
            set
            {
                if (LastSelectx == -1) return;
                if (Lock) return;
                labels[LastSelectx, LastSelecty] = value;
            }
        }
        public bool Next()
        {
            if (LastSelectx == -1)
            {
                UserSelect(0, 0);
            }
            else if (LastSelectx == 8 && LastSelecty == 8)
            {
                return false;
            }
            else
            {
                UserSelect((LastSelectx + 1) % 9, LastSelecty + (LastSelectx + 1) / 9);
            }
            return true;
        }
        public void UserSelect(int x, int y)
        {
            if (x > 8 || y > 8 || x < 0 || y < 0)
            {
                return;
            }
            if (LastSelectx != -1)
            {
                labels[LastSelectx, LastSelecty].UnSelect();
            }
            labels[x, y].UserSelect();
            LastSelectx = x;
            LastSelecty = y;
        }
        public void UnSelect()
        {
            if (LastSelectx != -1)
            {
                labels[LastSelectx, LastSelecty].UnSelect();
            }
            LastSelectx = -1;
            LastSelecty = -1;
        }
        public void ClearNumber()
        {
            UnSelect();
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    Lock = false;
                    labels[j, i].Text = "";
                    labels[j, i].ForeColor = Color.Black;
                }
            }
        }
        private void CreateLabel(int x, int y)
        {
            SudoBlock label = new SudoBlock();
            label.Location = new Point(36 * x + (x / 3 + 1) * 3 + (x + 1) * 2, 36 * y + (y / 3 + 1) * 3 + (y + 1) * 2);
            label.x = x;
            label.y = y;
            label.Click += new EventHandler(label_Click);
            label.ForeColor = Color.Black;
            labels[x, y] = label;
            Controls.Add(label);
        }
        private void label_Click(object sender, EventArgs e)
        {
            SudoBlock obj = (SudoBlock)sender;
            if (LastSelectx != -1)
            {
                labels[LastSelectx, LastSelecty].UnSelect();
            }
            obj.UserSelect();
            LastSelectx = obj.x;
            LastSelecty = obj.y;
        }
        public SudoBlock this[int x, int y]
        {
            get
            {
                return labels[x, y];
            }
            set
            {
                if (Lock) return;
                labels[x, y] = value;
            }
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
        public int x
        {
            get
            {
                return LastSelectx;
            }
        }
        public int y
        {
            get
            {
                return LastSelecty;
            }
        }
        public bool Lock
        {
            get
            {
                return _lock;
            }
            set
            {
                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        labels[j, i].Lock = value;
                    }
                }
                _lock = value;
            }
        }
    }
}
