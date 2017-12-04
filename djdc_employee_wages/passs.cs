using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

using djdc_employee_wages.a_sqlconn;
namespace djdc_employee_wages
{
    public partial class passs : a_qg_trol.qg_form
    {
        public passs()
        {
            InitializeComponent();
        }

        private void qg_button1_Click(object sender, EventArgs e)
        {
            string temp1 = qg_text1.Text.ToString().Trim();
            temp1 = Regex.Replace(temp1, @"\s", "").To_X_DBC();

            qg_read_text1.Text = temp1;
        }
    }
}
