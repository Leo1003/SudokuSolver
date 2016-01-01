using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SudokuSolver
{
    /// <summary>
    /// 
    /// </summary>
    public class SudoBlock : Label
    {
        public static Image selpic = new Bitmap("Select.bmp");
        private int lx = 0;
        private int ly = 0;
        public SudoBlock() : base()
        {
            BackColor = Color.White;
            Font = new Font("微軟正黑體", 24F, FontStyle.Bold, GraphicsUnit.Point, 136);
            Name = "label";
            Margin = new Padding(0);
            Size = new Size(36, 36);
            TabIndex = 0;
            Text = "";
            TextAlign = ContentAlignment.MiddleCenter;
            UseMnemonic = false;
            Lock = false;
        }
        public int x
        {
            get
            {
                return lx;
            }
            set
            {
                if (value < 9)
                {
                    lx = value;
                }
            }
        }
        public int y
        {
            get
            {
                return ly;
            }
            set
            {
                if (value < 9)
                {
                    ly = value;
                }
            }
        }
        public void UnSelect()
        {
            Image = null;
        }
        public void UserSelect()
        {
            Image = selpic;
        }
        public bool Lock
        {
            get;
            set;
        }
        public new string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                if (Lock) return;
                base.Text = value;
            }
        }
    }
}
