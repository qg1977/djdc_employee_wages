using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using djdc_employee_wages.a_sqlconn;
using static djdc_employee_wages.a_GlobalClass.con_sql;

namespace djdc_employee_wages.wagesql
{
    public partial class permoneyallbm : a_qg_trol.qg_form
    {
        public permoneyallbm()
        {
            InitializeComponent();


            try
            {
                string sqlstring = "select distinct months 月份 from allmax order by 月份 desc";
                DataTable dt_mon = return_select(sqlstring);

                monaaabegin.DataSource = dt_mon;
                monaaabegin.DisplayMember = "月份";

                DataTable dt_monbbb = dt_mon.Copy();
                monaaaend.DataSource = dt_monbbb;
                monaaaend.DisplayMember = "月份";


                sqlstring = "select ID,部门名称 from z_fcname where 删除=0";
                DataTable dt_bmaaa = return_select(sqlstring);

                bmaaa.DataSource = dt_bmaaa;
                bmaaa.DisplayMember = "部门名称";
                bmaaa.ValueMember = "ID";
            }
            catch (Exception ex)
            {
                ex.errormess();
            }
        }

        private void qg_button_small1_Click(object sender, EventArgs e)
        {
            string pro3begin = monaaabegin.Text.ToString();
            string pro3end = monaaaend.Text.ToString();
            if (pro3begin.Trim() != "" && pro3end.Trim() != "")
            {
                if (pro3begin.ToInt() > pro3end.ToInt())
                {
                    string temp1 = pro3begin;
                    pro3begin = pro3end;
                    pro3end = temp1;
                    //MessageBox.Show("开始月份不能大于结束月份！");
                    //return;
                }
            }
            string bmid = "";
            if (!string.IsNullOrEmpty(bmaaa.Text))
            {
                bmid = bmaaa.SelectedValue.ToString();
            }

            permoney_one permoney = new permoney_one();
            DataTable dt_permoney_bm = permoney.permoneyonebm(pro3begin, pro3end, bmid, "");

            qg_grid_tree1.DataSource = dt_permoney_bm;
            qg_grid_tree1.AutoGenerateColumns = true;

            foreach (DataGridViewColumn col in qg_grid_tree1.Columns)
            {
                if (col.Name=="部门ID"
                    || col.Name == "员工ID"
                    || col.Name == "排序"
                    || col.Name == "ID"
                    || col.Name == "上级ID"
                    || col.Name == "显示"
                    || col.Name == "展开"
                    || col.Name == "编码")
                { col.Visible = false; }
            }
        }

        private void permoneyallbm_Load(object sender, EventArgs e)
        {
            qg_grid_tree1.Top = 90;
        }
    }
}
