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
    public partial class permoneyall : a_qg_trol.qg_form
    {
        public permoneyall()
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


                sqlstring = "select ID,ltrim(rtrim(姓名)) 姓名,拼音 from person where 删除=0";
                DataTable dt_peraaa = return_select(sqlstring);

                peraaa.DataSource = dt_peraaa;
                peraaa.DisplayMember = "姓名";
                peraaa.ValueMember = "ID";
            }
            catch (Exception ex)
            {
                ex.errormess();
            }
        }

        private void qg_button1_Click(object sender, EventArgs e)
        {
           
        }

        private void qg_button_small1_Click(object sender, EventArgs e)
        {
            try
            {
                //if (monaaabegin.Text.IsNullOrEmpty())
                //{
                //    MessageBox.Show("请选择月份");
                //    return;
                //}
                string pro3begin = monaaabegin.Text.ToString();
                string pro3end = monaaaend.Text.ToString();
                if (pro3begin.Trim()!="" && pro3end.Trim()!="")
                {
                    if (pro3begin.ToInt()>pro3end.ToInt())
                    {
                        string temp1 = pro3begin;
                        pro3begin = pro3end;
                        pro3end = temp1;
                        //MessageBox.Show("开始月份不能大于结束月份！");
                        //return;
                    }
                }
                string perid = "";
                if (!string.IsNullOrEmpty(peraaa.Text))
                {
                    perid = peraaa.SelectedValue.ToString();
                }

                permoney_one permoney = new permoney_one();
                DataTable dt = permoney.permoneyone(pro3begin, pro3end, "",perid);
                if (dt == null || dt.Rows.Count <= 0)
                {
                    MessageBox.Show("没有找到相应的工资记录！");
                    return;
                }

                qg_grid1.DataSource = dt;
                qg_grid1.AutoGenerateColumns = true;
            }
            catch (Exception ex)
            {
                ex.errormess();
            }
        }

        private void permoneyall_Load(object sender, EventArgs e)
        {
            qg_grid1.Top = 70;

        }


        //结束
    }
}
