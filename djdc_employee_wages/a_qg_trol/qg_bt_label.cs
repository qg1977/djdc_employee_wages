using System.Drawing;
using System.Windows.Forms;

namespace djdc_employee_wages.a_qg_trol
{
    public partial class qg_bt_label : Label
    {
        public qg_bt_label()
        {
            Font = new Font("宋体", 20F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(134)));
            ForeColor = Color.LimeGreen;
            BackColor = Color.Transparent;//背景透明
            InitializeComponent();

        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }




    }
}
