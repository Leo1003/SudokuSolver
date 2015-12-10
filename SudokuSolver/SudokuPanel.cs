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
    	private int LastSelectx=-1;
    	private int LastSelecty=-1;
        private SudoBlock[,] labels = new SudoBlock[9, 9];
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
        public SudoBlock ActiveBlock()
        {
        	return labels[LastSelectx,LastSelecty];
        }
        public void UserSelect(int x, int y)
        {
        	if (x>8||y>8||x<0||y<0) 
        	{
        		return;
        	}
        	if(LastSelectx!=-1)
        	{
        		labels[LastSelectx,LastSelecty].UnSelect();
        	}
        	labels[x,y].UserSelect();
        	LastSelectx=x;
        	LastSelecty=y;
        }
        public void UnSelect()
        {
        	if(LastSelectx!=-1)
        	{
        		labels[LastSelectx,LastSelecty].UnSelect();
        	}
        	LastSelectx=-1;
        	LastSelecty=-1;
        }
        public void ClearNumber()
        {
            UnSelect();
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    labels[j, i].Text = "";
                }
            }
        }
        private void CreateLabel(int x, int y)
        {
            SudoBlock label = new SudoBlock();
			label.Location = new Point(36 * x + (x / 3 + 1) * 3 + (x + 1) * 2, 36 * y + (y / 3 + 1) * 3 + (y + 1) * 2);
            label.x=x;
            label.y=y;
            label.Click+= new EventHandler(label_Click);
            labels[x, y] = label;
            Controls.Add(label);
        }
        private void label_Click(object sender,EventArgs e)
        {
        	SudoBlock obj = (SudoBlock)sender;
        	if(LastSelectx!=-1)
        	{
        		labels[LastSelectx,LastSelecty].UnSelect();
        	}
        	obj.UserSelect();
        	LastSelectx=obj.x;
        	LastSelecty=obj.y;
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
    }
}
