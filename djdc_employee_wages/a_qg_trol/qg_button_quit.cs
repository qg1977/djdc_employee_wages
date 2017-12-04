using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace djdc_employee_wages.a_qg_trol
{
    public partial class qg_button_quit : Button
    {
        public qg_button_quit()
        {
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }

        private void qg_button_quit_Click(object sender, EventArgs e)
        {
            Control trol = this;
            do
            {
                trol = trol.Parent;
               
                if (trol is Form || trol is a_qg_trol.qg_form)
                {
                    ((Form)trol).Close();
                }
            }
            while (trol!=null);
            //((Form)Parent).Close();
        }
    }
}
