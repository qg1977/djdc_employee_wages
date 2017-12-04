using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using static djdc_employee_wages.a_GlobalClass.con_sql;

namespace djdc_employee_wages
{
    public partial class sign : a_qg_trol.qg_form
    {
        public sign()
        {
            InitializeComponent();
        }

        private void qg_button1_Click(object sender, EventArgs e)
        {
            if (text_user.Text.ToString().Trim().Length<=0 || text_pass.Text.ToString().Trim().Length<=0)
            { MessageBox.Show("用户名或密码不能为空！");return; }

            string sqlstring = "select ID,用户名,密码,角色ID from p_passpass where 删除=0 and "
                   +" 用户名=ltrim(rtrim('" + text_user.Text.ToString().Trim() + "')) and 密码=ltrim(rtrim('" + text_pass.Text.ToString().Trim() + "'))";
            //MessageBox.Show(sqlstring);
            DataTable dt = return_select(sqlstring);

            if (dt.Rows.Count <= 0) { MessageBox.Show("用户名或密码不正确！");return; }

            begin_class.allczyid = dt.Rows[0]["ID"].ToString();
            begin_class.allczyname = dt.Rows[0]["用户名"].ToString();
            begin_class.allroleid = dt.Rows[0]["角色ID"].ToString();

            MainForm form = new MainForm();
            form.Show();
            this.Hide();
        }
    }
}
