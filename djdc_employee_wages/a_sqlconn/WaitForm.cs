using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace djdc_employee_wages
{
    public partial class WaitForm : Form
    {
        public WaitForm()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            SetText("正在执行，请耐心等待....");
            this.SuspendLayout();
            panel1.Top = 0;
            panel1.Left = 0;
            auto();
            //// 
            //// label1
            ////
            //this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            //this.label1.Font = new System.Drawing.Font("Microsoft YaHei", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            //this.label1.Location = new System.Drawing.Point(0, 0);
            //this.label1.Name = "label1";
            //this.label1.Size = new System.Drawing.Size(386, 60);
            //this.label1.TabIndex = 0;
            //this.label1.Text = "正 在 处 理 请 稍 候 ";
            //this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //// 
            //// FrmWait
            ////
            //this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            //this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            //this.ClientSize = new System.Drawing.Size(386, 60);
            //this.ControlBox = false;
            //this.Controls.Add(this.label1);
            //this.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            //this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            //this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            //this.Name = "FrmWait";
            //this.ShowIcon = false;
            //this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;//屏幕居中
            FormBorderStyle = FormBorderStyle.None;//不显示标题栏
            this.ResumeLayout(false);

        }

        private void auto()
        {
            Width = panel1.Width;
            Height = panel1.Height;
            
        }
        private delegate void SetTextHandler(string text);
        public void SetText(string text)
        {
            if (this.label1.InvokeRequired)
            {
                this.Invoke(new SetTextHandler(SetText), text);
            }
            else
            {
                this.label1.Text = text;
            }
            int len = this.label1.Text.ToString().Trim().Length;
            if (len < 5) { len = 10; }
            panel1.Width = len * 15;

            int gd = this.label1.Size.Height;
            panel1.Height = gd + 20;
            auto();
        }
    }
}
