using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SudokuSolver
{
    class LabelButton : Label
    {
        private Color RawColor = Color.DodgerBlue;
        private Color LightColor;
        private Color DeepColor;
        private Color BColor = Color.DodgerBlue;
        private const int LRDColor = 50;
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
            LightColor = Light();
            DeepColor = Deep();
            MouseEnter += LabelButton_MouseEnter;
            MouseLeave += LabelButton_MouseLeave;
            MouseDown += LabelButton_MouseDown;
            MouseUp += LabelButton_MouseUp;
        }

        private void LabelButton_MouseUp(object sender, MouseEventArgs e)
        {
            BColor = RawColor;
            DrawBorder();
        }

        private void LabelButton_MouseDown(object sender, MouseEventArgs e)
        {
            BColor = Deep();
            DrawBorder();
        }

        private Color Deep()
        {
            int r = RawColor.R - LRDColor;
            int g = RawColor.G - LRDColor;
            int b = RawColor.B - LRDColor;
            if (r < 0)
            {
                r = 0;
            }
            if (g < 0)
            {
                g = 0;
            }
            if (b < 0)
            {
                b = 0;
            }
            return Color.FromArgb(r, g, b);
        }

        private Color Light()
        {
            int r = RawColor.R + LRDColor;
            int g = RawColor.G + LRDColor;
            int b = RawColor.B + LRDColor;
            if (r > 255)
            {
                r = 255;
            }
            if (g > 255)
            {
                g = 255;
            }
            if (b > 255)
            {
                b = 255;
            }
            return Color.FromArgb(r, g, b);
        }

        private void LabelButton_MouseLeave(object sender, EventArgs e)
        {
            BColor = RawColor;
            DrawBorder();
        }

        private void LabelButton_MouseEnter(object sender, EventArgs e)
        {
            BColor = LightColor;
            DrawBorder();
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
                return RawColor;
            }
            set
            {
                RawColor = value;
                BColor = value;
                LightColor = Light();
                DeepColor = Deep();
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
