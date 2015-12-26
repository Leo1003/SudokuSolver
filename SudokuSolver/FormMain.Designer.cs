namespace SudokuSolver
{
    partial class FormMain
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
        	this.panel1 = new System.Windows.Forms.Panel();
        	this.button1 = new System.Windows.Forms.Label();
        	this.SuspendLayout();
        	// 
        	// panel1
        	// 
        	this.panel1.BackColor = System.Drawing.SystemColors.ActiveCaption;
        	this.panel1.Location = new System.Drawing.Point(0, 0);
        	this.panel1.Name = "panel1";
        	this.panel1.Size = new System.Drawing.Size(356, 356);
        	this.panel1.TabIndex = 2;
        	this.panel1.Visible = false;
        	// 
        	// button1
        	// 
        	this.button1.BackColor = System.Drawing.SystemColors.ControlDark;
        	this.button1.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
        	this.button1.Location = new System.Drawing.Point(370, 9);
        	this.button1.Margin = new System.Windows.Forms.Padding(0);
        	this.button1.Name = "button1";
        	this.button1.Size = new System.Drawing.Size(131, 32);
        	this.button1.TabIndex = 3;
        	this.button1.Text = "量尺";
        	this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        	this.button1.Visible = false;
        	// 
        	// FormMain
        	// 
        	this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
        	this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        	this.BackColor = System.Drawing.SystemColors.ControlLightLight;
        	this.ClientSize = new System.Drawing.Size(510, 366);
        	this.Controls.Add(this.button1);
        	this.Controls.Add(this.panel1);
        	this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
        	this.KeyPreview = true;
        	this.MaximizeBox = false;
        	this.MinimumSize = new System.Drawing.Size(372, 394);
        	this.Name = "FormMain";
        	this.Text = "SudokuSolver";
        	this.ResumeLayout(false);
        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label button1;
    }
}

