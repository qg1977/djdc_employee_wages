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
    public partial class wage_month : a_qg_trol.qg_form
    {
        public wage_month()
        {
            InitializeComponent();
        }
        private void qg_text_spinner1_Enter(object sender, EventArgs e)
        {
            qg_check1.Checked = false;
            qg_check1.Visible = false;
        }

        private void qg_button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (!out_jytt(true))
                { return; }
                string months = qg_text_spinner1.Text.ToString();
                string sqlstring = "select count(1) un from permoney where 月份='" + months.Trim() + "'";
                DataTable dt = return_select(sqlstring);
                if (dt.Rows[0]["un"].ToString().ToInt() > 0 && qg_check1.Checked == false)
                {
                    DialogResult err = MessageBox.Show("后台数据库中已经存在" + months.Trim() + "月份的工资信息!\n\r"
                             + "如果不删除原先工资信息,再次导入" + months.Trim() + "月份的工资信息，会同时存在两个批次的工资信息,\n\r是否确定？",
                                "提示", MessageBoxButtons.OKCancel);
                    if (err == DialogResult.Cancel)
                    {
                        return;
                    }
                }


                excel_to_sql lForm1 = (excel_to_sql)this.Owner;//把Form2的父窗口指针赋给lForm1
                lForm1.sql_month = months;//将月份传递给父窗口
                lForm1.delete_old_jytt = qg_check1.Checked;//是否删除当月已经有后台数据库中的工资信息

                this.DialogResult = DialogResult.OK;//表示点击 的是确定按钮

                this.Close();
            }
            catch (Exception ex)
            {
                ex.errormess();
            }
        }



        private void qg_text_spinner1_KeyUp(object sender, KeyEventArgs e)
        {
            check_visible();
        }

        private void qg_text_spinner1_Leave(object sender, EventArgs e)
        {
            check_visible();

         }

        private void check_visible()
        {
            try
            {
                if (!out_jytt(false)) { return; }


                string months = qg_text_spinner1.Text.ToString();

                string sqlstring = "select count(1) un from permoney where 月份='" + months.Trim() + "'";
                DataTable dt = return_select(sqlstring);

                if (dt.Rows[0]["un"].ToString().ToInt() > 0)
                {
                    qg_check1.Visible = true;
                    qg_check1.Text = "数据库中已经存在" + months.Trim() + "月份的工资，是否删除原数据，再重新导入新数据？";
                    qg_check1.Checked = true;
                }
                else
                {
                    qg_check1.Visible = false;
                    qg_check1.Checked = false;
                }
            }
            catch (Exception ex)
            {
                ex.errormess();
            }

        }


        //判断输入的月份格式是否正确
        private bool out_jytt(bool mess)
        {
            bool jytt = true;

            string months = qg_text_spinner1.Text.ToString();
        
            if (months.Trim().Length != 6)
            {
                if (mess)
                { MessageBox.Show("请输入正确的月份格式：比如201701(表示2017年1月)"); }

                jytt = false;
                return jytt;
            }

            return jytt;
        }





        //结束
    }
}
