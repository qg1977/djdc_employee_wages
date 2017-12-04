using System;
using System.Drawing;
using System.Windows.Forms;

namespace djdc_employee_wages.a_qg_trol
{
    public partial class qg_text : TextBox
    {
        public qg_text()
        {
           
            this.SuspendLayout();
            ImeMode = System.Windows.Forms.ImeMode.Hangul;//默认为半角
   
            this.ResumeLayout(false);
            InitializeComponent();
        }



        private void qg_text_Enter(object sender, EventArgs e)
        {
            BackColor = Color.LightCyan; //当textBox1获得焦点时，背景色变为LightCyan（淡蓝绿色）
        }

        private void qg_text_Leave(object sender, EventArgs e)
        {
            BackColor = Color.White; //当textBox1失去焦点时，背景色恢复为White(白色)
        }

        private void qg_text_EnabledChanged(object sender, EventArgs e)
        {
            if (Enabled == false)
            {
                Font = new Font(this.Font, FontStyle.Bold);//加粗
                TextAlign = HorizontalAlignment.Right;
                //Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
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
