using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Threading;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SudokuSolver.SudoCalc;

namespace SudokuSolver
{
    public partial class FormMain : Form
    {
        SudokuPanel p;
        LabelButton buttonCalc;
        LabelButton buttonClr;
        LabelButton buttonLine;
        public FormMain()
        {
            InitializeComponent();
            p = new SudokuPanel();
            p.Location = new Point(0, 0);
            Controls.Add(p);
            KeyDown += new KeyEventHandler(Form_KeyDown);
            //PreviewKeyDown+= new PreviewKeyDownEventHandler(Form_KeyDown);
            KeyPress += new KeyPressEventHandler(Form_KeyPress);

            //buttonCalc

            buttonCalc = new LabelButton();
            buttonCalc.Name = "button2";
            buttonCalc.Size = new Size(64, 32);
            buttonCalc.Location = new Point(362, 315);
            buttonCalc.Text = "計算";
            Controls.Add(buttonCalc);
            buttonCalc.Click += buttonCalc_Click;

            //buttonClr

            buttonClr = new LabelButton();
            buttonClr.Name = "button3";
            buttonClr.Size = new Size(64, 32);
            buttonClr.Location = new Point(437, 315);
            buttonClr.Text = "清除";
            buttonClr.BorderColor = Color.Red;
            Controls.Add(buttonClr);
            buttonClr.Click += ButtonClr_Click;

            //buttonLine

            buttonLine = new LabelButton();
            buttonLine.Name = "button4";
            buttonLine.Size = new Size(132, 32);
            buttonLine.Location = new Point(370, 9);
            buttonLine.Text = "輸入單行格式";
            buttonLine.BorderColor = Color.Yellow;
            Controls.Add(buttonLine);
            buttonLine.Click += new EventHandler(buttonLine_Click);
        }

        private void buttonLine_Click(object sender, EventArgs e)
        {
            string tmp = GetOneLine();
           retry:
            if (InputBox("輸入單行格式", "輸入單行格式文字", ref tmp, p.Lock) == DialogResult.OK)
            {
                try 
                {
                	FillBack(tmp);
                } 
                catch (ArgumentException) 
                {
                	MessageBox.Show("請輸入0~9的數字","格式錯誤",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                	goto retry;
                }
            }
        }

        private void ButtonClr_Click(object sender, EventArgs e)
        {
            p.ClearNumber();
        }

        private void Form_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch (e.KeyChar)
            {
                case '0':
                case ' ':
                    p.ActiveBlock.Text = "";
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
                    p.ActiveBlock.Text = e.KeyChar.ToString();
                    break;
                default:
                    break;
            }
        }

        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Back:
                case Keys.Delete:
                    p.ActiveBlock.Text = "";
                    break;
                case Keys.Enter:
                    p.Next();
                    break;
                case Keys.Escape:
                    p.UnSelect();
                    break;
                case Keys.Left:
                    p.UserSelect(p.x - 1, p.y);
                    break;
                case Keys.Up:
                    p.UserSelect(p.x, p.y - 1);
                    break;
                case Keys.Right:
                    p.UserSelect(p.x + 1, p.y);
                    break;
                case Keys.Down:
                    p.UserSelect(p.x, p.y + 1);
                    break;
            }

        }

        private string GetOneLine()
        {
            string str = "";
            if(p.IsEmpty())return "";
            for (int y = 0; y < 9; y++)
            {
                for (int x = 0; x < 9; x++)
                {
                    switch (p[x,y].Text)
                    {
                        case "":
                            str += "0";
                            break;
                        default:
                            str += p[x,y].Text;
                            break;
                    }
                }
            }
            return str;
        }

        private void FillBack(string str)
        {
        	foreach (char c in str) //check
        	{
        		if ((int)(c - '0') > 9 || (int)(c - '0') < 0)
                {
                    throw new ArgumentException();
                }
        	}
            for (int y = 0; y < 9; y++)
            {
                for (int x = 0; x < 9; x++)
                {
                	if (str.Length <= y * 9 + x) 
                	{
                		return;
                	}
                    if (str[y * 9 + x] == '0')
                    {
                        p[x, y].Text = "";
                    }
                    else
                    {
                        p[x, y].Text = str[y * 9 + x].ToString();
                    }
                }
            }
        }

        private void buttonCalc_Click(object sender, EventArgs e)
        {
        	Calc();//TODO:BackgroundWorker
        }
        
        private void Calc()
        {
            string ds = GetOneLine();
            SudokuSolver.SudoCalc.Panel data = new SudokuSolver.SudoCalc.Panel(ds);
            Calculator.ExpelCandidate(ref data);
            //Calculator.Filler(ref data);
            while (Calculator.Filler(ref data))
            {
                Calculator.ExpelCandidate(ref data);
            }
            
            if(!data.IsFull())
            {
            	SudokuSolver.SudoCalc.Panel[] debug = Calculator.FindAnswer(data);
            	//data = Calculator.FindAnswer(data)[0];//TODO:FixMultiAns
            	data = debug[0];
            }
            
            for (int y = 0; y < 9; y++)
            {
                for (int x = 0; x < 9; x++)
                {
                    if (data[x, y].Number == SudoNum.Unknown)
                    {
                        p[x, y].Text = "";
                    }
                    else
                    {
                        p[x, y].Text = ((int)data[x, y].Number).ToString();
                        if(!data[x, y].Stable)p[x, y].ForeColor = Color.Red;
                        else p[x, y].ForeColor = Color.Black;
                    }
                }
            }
            p.Lock = true;
        }

        //https://www.dotblogs.com.tw/aquarius6913/2014/09/03/146444
        public static DialogResult InputBox(string title, string promptText, ref string value, bool rdonly = false)
        {
            Form form = new Form();
            Label label = new Label();
            TextBox textBox = new TextBox();
            Button buttonOk = new Button();
            Button buttonCancel = new Button();

            form.Text = title;
            label.Text = promptText;
            textBox.Text = value;
            textBox.ReadOnly = rdonly;

            buttonOk.Text = "確定";
            buttonCancel.Text = "取消";
            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;

            label.SetBounds(9, 20, 372, 13);
            textBox.SetBounds(12, 36, 572, 20);
            buttonOk.SetBounds(428, 72, 75, 23);
            buttonCancel.SetBounds(509, 72, 75, 23);

            label.AutoSize = true;
            textBox.Anchor = textBox.Anchor | AnchorStyles.Right;
            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            form.ClientSize = new Size(596, 107);
            form.Controls.AddRange(new Control[] { label, textBox, buttonOk, buttonCancel });
            form.ClientSize = new Size(Math.Max(500, label.Right + 10), form.ClientSize.Height);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;

            DialogResult dialogResult = form.ShowDialog();
            value = textBox.Text;
            return dialogResult;
        }
    }
}
