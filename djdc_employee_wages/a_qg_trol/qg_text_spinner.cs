using djdc_employee_wages.a_sqlconn;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace djdc_employee_wages.a_qg_trol
{
    public partial class qg_text_spinner : TextBox
    {

        public qg_text_spinner()
        {
            InitializeComponent();
            this.SuspendLayout();
            //BackColor = System.Drawing.Color.FromArgb(255, 255, 170);//背景色为浅黄色
            Font = new Font(this.Font, FontStyle.Bold);//加粗

            Tag = true;

            this.ResumeLayout(false);
           
        }

        //protected override void OnPaint(PaintEventArgs pe)
        //{
        //    base.OnPaint(pe);
        //}

        private void qg_text_spinner_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                //判断按键是不是要输入的类型。
                if (((int)e.KeyChar < 48 || (int)e.KeyChar > 57) && (int)e.KeyChar != 8 && (int)e.KeyChar != 46 && e.KeyChar != 45)
                    e.Handled = true;

                //输入为负号时，只能输入一次且只能输入一次
                if (e.KeyChar == 45 && (((TextBox)sender).SelectionStart != 0 || ((TextBox)sender).Text.IndexOf("-") >= 0))
                    e.Handled = true;

                //输入为小数点时，只能输入一次且只能输入一次
                if (e.KeyChar == 46 && ((TextBox)sender).Text.IndexOf(".") >= 0)
                    e.Handled = true;
            }
            catch (Exception ex)
            {
                ex.errormess();
            }

        }

        private void qg_text_spinner_Enter(object sender, EventArgs e)
        {
            ImeMode = ImeMode.Disable;//获得焦点时,设置为英文
            BackColor = Color.LightCyan; //当textBox1获得焦点时，背景色变为LightCyan（淡蓝绿色）
        }

        private void qg_text_spinner_Leave(object sender, EventArgs e)
        {
            ImeMode = ImeMode.NoControl;//
            BackColor = Color.White; //当textBox1失去焦点时，背景色恢复为White(白色)
            if (!Simple_all.isNumberic(this.Text.ToString()))
            { this.Text = "0"; }
        }

        private void qg_text_spinner_MouseUp(object sender, MouseEventArgs e)
        {
            //如果鼠标左键操作并且标记存在，则执行全选             
            if (e.Button == MouseButtons.Left && (bool)Tag == true)
            {
                SelectAll();
            }

            //取消全选标记              
            //Tag = false;
        }


        private void qg_text_spinner_EnabledChanged(object sender, EventArgs e)
        {
            if (Enabled == false)
            {
                Font = new Font(this.Font, FontStyle.Bold);//加粗
                TextAlign = HorizontalAlignment.Right;
            }
        }

        #region 通过重绘 设置控件enabled时的背景颜色
        // 当控件不可用的时候字体的颜色
        private Color _EnabledFontColor = Color.FromArgb(255, 89, 89);
        //背景颜色
        private Color _EnabledBackColor = Color.FromArgb(255, 255, 200);


        // 重写OnEnabledChanged  
        protected override void OnEnabledChanged(EventArgs e)
        {
            if (!Enabled)
            {
                SetStyle(ControlStyles.UserPaint, true);
            }
            else
            {
                SetStyle(ControlStyles.UserPaint, false);
            }
            base.OnEnabledChanged(e);
        }

        // 重写OnPaint 
        protected override void OnPaint(PaintEventArgs pe)
        {


            base.OnPaint(pe);
            if (!Enabled)
            {
                //描绘背景
                //pe.Graphics.FillRectangle(new SolidBrush(SystemColors.Window),
                pe.Graphics.FillRectangle(new SolidBrush(_EnabledBackColor),
                pe.ClipRectangle);
                //文字描画   
                System.Drawing.Pen bText = new Pen(_EnabledFontColor);
                StringFormat strFormat = new StringFormat();

                //设置文字的位置
                if (this.TextAlign == HorizontalAlignment.Left)//左边
                {
                    strFormat.Alignment = StringAlignment.Near;
                }
                else if (this.TextAlign == HorizontalAlignment.Right)
                {
                    strFormat.Alignment = StringAlignment.Far;
                }
                Graphics g = this.CreateGraphics();
                g.DrawString(this.Text, this.Font, bText.Brush,
                    new RectangleF(this.ClientRectangle.X, this.ClientRectangle.Y+2, this.ClientRectangle.Width, this.ClientRectangle.Height),
                    strFormat);
            }
        }
        #endregion


    }
}
