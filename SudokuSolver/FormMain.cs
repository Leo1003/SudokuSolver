using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
        LabelButton button2;
        LabelButton button3;
        LabelButton button4;
        string datastr = "";
        public FormMain()
        {
            InitializeComponent();
            p = new SudokuPanel();
            p.Location = new Point(0, 0);
            Controls.Add(p);
            KeyDown += new KeyEventHandler(Form_KeyDown);
            //PreviewKeyDown+= new PreviewKeyDownEventHandler(Form_KeyDown);
            KeyPress += new KeyPressEventHandler(Form_KeyPress);

            //button2

            button2 = new LabelButton();
            button2.Name = "button2";
            button2.Size = new Size(64, 32);
            button2.Location = new Point(362, 315);
            button2.Text = "計算";
            Controls.Add(button2);
            button2.Click += button2_Click;

            //button3

            button3 = new LabelButton();
            button3.Name = "button3";
            button3.Size = new Size(64, 32);
            button3.Location = new Point(437, 315);
            button3.Text = "清除";
            button3.BorderColor = Color.Red;
            Controls.Add(button3);
            button3.Click += Button3_Click;

            //button4

            button4 = new LabelButton();
            button4.Name = "button4";
            button4.Size = new Size(132, 32);
            button4.Location = new Point(370, 9);
            button4.Text = "輸入單行格式";
            button4.BorderColor = Color.Yellow;
            Controls.Add(button4);
            button4.Click += new EventHandler(button4_Click);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            datastr = GetOneLine();
            string tmp = datastr;
            if (InputBox("輸入單行格式", "輸入單行格式文字", ref tmp) == DialogResult.OK)
            {
                datastr = tmp;
                FillBack(datastr);
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            p.ClearNumber();
        }

        private void Form_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch (e.KeyChar)
            {
                case '0':
                case ' ':
                    p.ActiveBlock().Text = "";
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
                    p.ActiveBlock().Text = e.KeyChar.ToString();
                    break;
                default:
                    break;
            }
        }
        //private void Form_KeyDown(object sender, PreviewKeyDownEventArgs e)
        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Back:
                case Keys.Delete:
                    p.ActiveBlock().Text = "";
                    break;
                case Keys.Enter:
                    //TODO:WaitTheSetting
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
            for (int y = 0; y < 9; y++)
            {
                for (int x = 0; x < 9; x++)
                {
                    p.UserSelect(x, y);
                    switch (p.ActiveBlock().Text)
                    {
                        case "":
                            str += "0";
                            break;
                        default:
                            str += p.ActiveBlock().Text;
                            break;
                    }
                }
            }
            return str;
        }

        private void FillBack(string str)
        {
            for (int y = 0; y < 9; y++)
            {
                for (int x = 0; x < 9; x++)
                {
                    if (str[y * 9 + x] == '0')
                    {
                        p[x, y].Text = "";
                    }
                    else
                    {
                        if ((int)(str[y * 9 + x] - '0') > 9 || (int)(str[y * 9 + x] - '0') < 0)
                        {
                            throw new ArgumentException();
                        }
                        p[x, y].Text = str[y * 9 + x].ToString();
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int locx = p.x;
            int locy = p.y;
            string ds = datastr;
            if (ds == "")
            {
                ds = GetOneLine();
            }
            //ds="040250008030409170000081200006000720000604000012000300003810000064902010700045090";
            SudokuSolver.SudoCalc.Panel data = new SudokuSolver.SudoCalc.Panel(ds);
            Calculator.ExpelCandidate(ref data);
            //Calculator.Filler(ref data);
            while (Calculator.Filler(ref data))
            {
                Calculator.ExpelCandidate(ref data);
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
                    }
                }
            }
            p.UserSelect(locx, locy);
        }

        //https://www.dotblogs.com.tw/aquarius6913/2014/09/03/146444
        public static DialogResult InputBox(string title, string promptText, ref string value)
        {
            Form form = new Form();
            Label label = new Label();
            TextBox textBox = new TextBox();
            Button buttonOk = new Button();
            Button buttonCancel = new Button();

            form.Text = title;
            label.Text = promptText;
            textBox.Text = value;

            buttonOk.Text = "OK";
            buttonCancel.Text = "Cancel";
            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;

            label.SetBounds(9, 20, 372, 13);
            textBox.SetBounds(12, 36, 372, 20);
            buttonOk.SetBounds(228, 72, 75, 23);
            buttonCancel.SetBounds(309, 72, 75, 23);

            label.AutoSize = true;
            textBox.Anchor = textBox.Anchor | AnchorStyles.Right;
            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            form.ClientSize = new Size(396, 107);
            form.Controls.AddRange(new Control[] { label, textBox, buttonOk, buttonCancel });
            form.ClientSize = new Size(Math.Max(300, label.Right + 10), form.ClientSize.Height);
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
