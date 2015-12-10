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
    	SudokuPanel p;
        LabelButton button2;
        LabelButton button3;
        public FormMain()
        {
            InitializeComponent();
            p = new SudokuPanel();
            p.Location = new Point(0, 0);
            Controls.Add(p);
            KeyDown+= new KeyEventHandler(Form_KeyDown);
            //PreviewKeyDown+= new PreviewKeyDownEventHandler(Form_KeyDown);
            KeyPress+= new KeyPressEventHandler(Form_KeyPress);

            //button2

            button2 = new LabelButton();
            button2.Name = "button2";
            button2.Size = new Size(64, 32);
            button2.Location = new Point(362, 315);
            button2.Text = "計算";
            Controls.Add(button2);

            //button3

            button3 = new LabelButton();
            button3.Name = "button3";
            button3.Size = new Size(64, 32);
            button3.Location = new Point(437, 315);
            button3.Text = "清除";
            button3.BorderColor = Color.Red;
            Controls.Add(button3);
            button3.Click += Button3_Click;
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            p.ClearNumber();
        }

        private void Form_KeyPress(object sender, KeyPressEventArgs e)
        {
        	switch (e.KeyChar) {
        		case '0':
        		case ' ':
        			p.ActiveBlock().Text="";
        			break;
        		case '1':
        		case '2':
				case '3':
				case '4':
				case '5':
				case '6':
				case '7':
				case '8':
				case '9':        			
        			p.ActiveBlock().Text=e.KeyChar.ToString();
        			break;
        		default:
        			break;
        	}
        }
        //private void Form_KeyDown(object sender, PreviewKeyDownEventArgs e)
        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
        	switch (e.KeyCode) {
        		case Keys.Back:
        		case Keys.Delete:
        			p.ActiveBlock().Text="";
        			break;
        		case Keys.Enter:
        			//TODO:WaitTheSetting
        			break;
        		case Keys.Escape:
        			p.UnSelect();
        			break;
        		case Keys.Left:
        			p.UserSelect(p.x-1,p.y);
        			break;
        		case Keys.Up:
        			p.UserSelect(p.x,p.y-1);
        			break;
        		case Keys.Right:
        			p.UserSelect(p.x+1,p.y);
        			break;
        		case Keys.Down:
        			p.UserSelect(p.x,p.y+1);
        			break;
        	}
            
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            p.ClearNumber();
            p.Focus();
        }
    }
}
