using System.Drawing;
using System.Windows.Forms;

namespace djdc_employee_wages.a_qg_trol
{
    public partial class qg_button : Button
    {
        public qg_button()
        {
            InitializeComponent();

        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }


        private void qg_button_EnabledChanged(object sender, System.EventArgs e)
        {
            if (Enabled == false)
            {
                this.BackColor = System.Drawing.Color.FromArgb(255, 255, 220);//背景色为浅黄色
                this.Font = new Font(this.Font, FontStyle.Bold);//加粗
            }
            else
            {
                BackColor = System.Drawing.SystemColors.Control;//恢复默认背景颜色
                Font = new Font(this.Font, FontStyle.Regular);//恢复为普通文本
            }
        }

      

    }
}
