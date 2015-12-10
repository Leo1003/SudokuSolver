using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SudokuSolver
{
    class LabelButton:Label
    {
        private Color BColor = Color.DodgerBlue;
        private int BWidth = 3;
        public LabelButton()
        {
            InitializeComponent();
            DrawBorder();
        }
        private void InitializeComponent()
        {
            Font = new Font("微軟正黑體", 12F, FontStyle.Regular, GraphicsUnit.Point, 136);
            Margin = new Padding(0);
            Text = "Button";
            TextAlign = ContentAlignment.MiddleCenter;
        }
        private void DrawBorder()
        {
            Bitmap img = new Bitmap(Size.Width, Size.Height);
            Graphics g = Graphics.FromImage(img);
            g.FillRectangle(new SolidBrush(BColor), 0, 0, Size.Width, Size.Height);
            g.FillRectangle(new SolidBrush(BackColor), 0 + BWidth, 0 + BWidth, Size.Width - 2 * BWidth, Size.Height - 2 * BWidth);
            Image = img;
        }
        public Color BorderColor
        {
            get
            {
                return BColor;
            }
            set
            {
                BColor = value;
                DrawBorder();
            }
        }
        public int BorderWidth
        {
            get
            {
                return BWidth;
            }
            set
            {
                BWidth = value;
                DrawBorder();
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
                base.Size = value;
                DrawBorder();
            }
        }
        public new Color BackColor
        {
            get
            {
                return base.BackColor;
            }
            set
            {
                base.BackColor = value;
                DrawBorder();
            }
        }
    }
}
