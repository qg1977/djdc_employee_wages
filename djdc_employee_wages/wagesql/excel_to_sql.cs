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
using System.Text.RegularExpressions;

namespace djdc_employee_wages.wagesql
{

    public partial class excel_to_sql : a_qg_trol.qg_form
    {


        public string sql_month;//传递进来的工资月份
        public bool delete_old_jytt=false;//如果数据库有导入excel表月份的工资，是否删除,false表示不管以前的工资记录

        public excel_to_sql()
        {
            InitializeComponent();
        }

        private void qg_button1_Click(object sender, EventArgs e)
        {

            try
            {
                DataTable dt_all = a_sqlconn.dt_excel.GetDataFromExcelByCom();
                if (dt_all == null) { return; }


                WaitFormService.Show();
                WaitFormService.SetText("正在将工资表的数据内容格式统一化！");
                //将所有内容清除空格，全角转半角
                #region
                for (int i = 0; i < dt_all.Rows.Count; i++)
                {
                    for (int j = 0; j < dt_all.Columns.Count; j++)
                    {
                        //使用正则表达式.你能使用Regex.Replace方法, 它将所有匹配的替换为指定的字符
                        //.在这个例子中,使用正则表达式匹配符"\s",它将匹配任何空格包含在这个字符串里C#空格, tab字符, 换行符和新行(newline).
                        dt_all.Rows[i][j] = Regex.Replace(dt_all.Rows[i][j].ToString(), @"\s", "").To_X_DBC();
                        //最后再将全角转换为半角
                    }
                }
                #endregion
                //将所有内容清除空格，全角转半角
                WaitFormService.Close();


                DataColumn dc1 = new DataColumn("删除记号", Type.GetType("System.Int32"));
                dc1.DefaultValue = 0;
                dt_all.Columns.Add(dc1);

                #region 删除有“姓名”的行之前的行
                foreach (DataRow dr in dt_all.Rows)
                {

                    bool deletejytt = true;
                    for (int i = 0; i < dt_all.Columns.Count; i++)
                    {
                        if (dr[i].ToString() == "姓名")
                        { deletejytt = false; }
                    }
                    if (deletejytt)
                    { dr["删除记号"] = 1; }
                    else
                    { break; }

                }

                DataRow[] row_update = dt_all.Select("删除记号=1");
                for (int i = 0; i < row_update.Count(); i++)
                {
                    dt_all.Rows.Remove(row_update[i]);
                }
                dt_all.Columns.Remove("删除记号");

                if (dt_all.Rows.Count <= 0)
                {
                    MessageBox.Show("导入的工资表不正常，没有“姓名”列，无法导出工资信息！");
                    return;
                }
                #endregion  删除有“姓名”的行之前的行




                //将标头整理为有“姓名”行的正常标头，同时删除没有记录的column
                #region
                DataTable dt_temp = new DataTable("Datas");
                DataColumn dc = null;
                dc = dt_temp.Columns.Add("删除记录", Type.GetType("System.String"));


                DataRow dt_NO_1 = dt_all.Rows[0];

                //确保“工资条目”没有重复
                for (int i = 0; i < dt_all.Columns.Count; i++)
                {
                    string nametemp1 = dt_NO_1[i].ToString().Trim();
                    if (nametemp1.Trim() == "") { continue; }

                    for (int j = i + 1; j < dt_all.Columns.Count; j++)
                    {
                        string nametemp2 = dt_NO_1[j].ToString().Trim();

                        if (nametemp1.Trim() == nametemp2.Trim())
                        {
                            MessageBox.Show("有重复的工资条目：“" + nametemp1.Trim() + "”!\n\r 请先确保工资条目不重复！");
                            return;
                        }
                    }
                }
                //确保“工资条目”没有重复

                //如果姓名列正常，则将column的名称正常化，否则将column删除
                for (int i = 0; i < dt_all.Columns.Count; i++)
                {
                    if (dt_NO_1[i].ToString().Trim() != "")
                    {
                        dt_all.Columns[i].ColumnName = dt_NO_1[i].ToString().Trim();
                    }
                    else
                    {
                        DataRow dt_temp_newRow = dt_temp.NewRow();
                        dt_temp_newRow["删除记录"] = dt_all.Columns[i].ColumnName.ToString().Trim();
                        dt_temp.Rows.Add(dt_temp_newRow);
                    }
                }
                foreach (DataRow dr in dt_temp.Rows)
                {
                    string str = dr["删除记录"].ToString().Trim();
                    dt_all.Columns.Remove(str);
                }


                dt_all.Rows.RemoveAt(0);
                #endregion


                //如果工资表中的表头多次出现，则删除之
                #region
                dc1 = new DataColumn("删除记号", Type.GetType("System.Int32"));
                dc1.DefaultValue = 0;
                dt_all.Columns.Add(dc1);
                foreach (DataRow dr in dt_all.Rows)
                {
                    if (dr[0].ToString().Trim() == dt_all.Columns[0].ColumnName.ToString().Trim()
                         && dr[1].ToString().Trim() == dt_all.Columns[1].ColumnName.ToString().Trim()
                         )
                    { dr["删除记号"] = 1; }
                }
                row_update = dt_all.Select("删除记号=1");
                for (int i = 0; i < row_update.Count(); i++)
                {
                    dt_all.Rows.Remove(row_update[i]);
                }

                dt_all.Columns.Remove("删除记号");
                #endregion



                //删除“姓名”为空的行
                #region 
                row_update = dt_all.Select("姓名='' or (姓名 like '%合%' and 姓名 like '%计%')");
                for (int i = 0; i < row_update.Count(); i++)
                {
                    dt_all.Rows.Remove(row_update[i]);
                }
                #endregion


                //判断是否有重名的姓名 记录
                #region
                //DataView dv = dt_all.DefaultView;
                //if (dv.Count != dv.ToTable(true, "姓名").Rows.Count)
                //{
                //    MessageBox.Show("存在相同姓名的记录!");
                //    return;
                //}
                bool cfjytttemp1 = false;//是否有重复的姓名，false为没有重复
                string cfstringalltemp1 = "";
                int cfcounttemp1 = 0;
                var groupByResult = dt_all.Rows.Cast<DataRow>().GroupBy<DataRow, string>(dr => dr["姓名"].ToString());
                foreach (var rows in groupByResult)
                {
                    if (rows.Count() > 1)
                    {
                        //以“姓名”为筛选条件的数据存在多条
                        cfjytttemp1 = true;
                        DataTable dttemp = rows.CopyToDataTable<DataRow>();
                        cfstringalltemp1 = cfstringalltemp1 + "\n\r   " + dttemp.Rows[0]["姓名"].ToString();
                        cfcounttemp1 = cfcounttemp1 + 1;
                    }
                }
                if (cfjytttemp1)
                {
                    MessageBox.Show("存在   "+cfcounttemp1.ToString()+"    条相同姓名的记录!请确保没有重复姓名，然后再导出工资表！\n\r   " + cfstringalltemp1.Trim());
                    return;
                }
                #endregion
                //判断是否有重名的姓名 记录

                qg_grid1.DataSource = dt_all;
                qg_grid1.AutoGenerateColumns = true;
            }
            catch (Exception ex)
            {
                ex.errormess();
            }
        }

        private void qg_button2_Click(object sender, EventArgs e)
        {

            try
            {
                if (qg_grid1.DataSource == null)
                {
                    MessageBox.Show("尚未从excel表中导入工资信息！");
                    return;
                }

                DataTable dt_all1 = (DataTable)qg_grid1.DataSource;
                DataTable dt_all_copy = dt_all1.Copy();

                if (dt_all_copy.Rows.Count <= 0)
                {
                    MessageBox.Show("本次工资信息记录数为0，无法导入！");
                    return;
                }

                DialogResult err = MessageBox.Show("确认将本次工资信息提交至后台数据库吗？", "提示", MessageBoxButtons.OKCancel);
                if (err == DialogResult.Cancel)
                {
                    return;
                }

                wage_month form_mon = new wage_month();
                form_mon.Owner = this;//重要的一步，主要是使Form2的Owner指针指向Form1
                form_mon.ShowDialog();

                //如果“选择月份”窗体 点击 的不是确定键，则退出
                if (form_mon.DialogResult != DialogResult.OK)
                { return; }


                string sqlstring;
                DataTable dt_temp;
                WaitFormService.Show();

                //如果需要删除当月已经在后台数据库中的工资信息
                if (delete_old_jytt)
                {
                    sqlstring = "delete from Subject_mon where 月份='" + sql_month.Trim() + "'";
                    sqlstring = sqlstring + "\n\r" + "delete from permoney where 月份='" + sql_month + "'";
                    insert_update_delete(sqlstring);
                }


                //保存月份
                sqlstring = "select ID from allmax where months=ltrim(rtrim('" + sql_month.Trim() + "'))";
                dt_temp = return_select(sqlstring);
                if (dt_temp.Rows.Count <= 0)
                {
                    sqlstring = "insert into allmax(months) values (ltrim(rtrim('" + sql_month.Trim() + "')))";
                    insert_update_delete(sqlstring);
                }
                //保存月份



                //保存部门信息
                #region
                //如果“部门名称”为空，则取上一行的“部门名称”
                string bmname = dt_all_copy.Rows[0]["部门名称"].ToString();
                foreach (DataRow dr in dt_all_copy.Rows)
                {
                    WaitFormService.SetText("正在整理部门名称！\n\r       " + bmname.Trim());

                    if (dr["部门名称"].ToString().Trim() == "")
                    { dr["部门名称"] = bmname; }

                    bmname = dr["部门名称"].ToString().Trim();
                }

                DataColumn dc1 = new DataColumn("部门ID", Type.GetType("System.Int64"));
                dc1.DefaultValue = 0;
                dt_all_copy.Columns.Add(dc1);

                DataView dataView = dt_all_copy.DefaultView;
                DataTable dt_Distinct = dataView.ToTable(true, "部门名称");//注：其中ToTable（）的第一个参数为是否distinct
                for (int i = 0; i < dt_Distinct.Rows.Count; i++)
                {
                    string bmnametemp1 = dt_Distinct.Rows[i]["部门名称"].ToString().Trim();

                    sqlstring = "select ID from z_fcname where 部门名称=ltrim(rtrim('" + bmnametemp1.Trim() + "')) and 删除=0";
                    dt_temp = return_select(sqlstring);
                    if (dt_temp.Rows.Count <= 0)
                    {
                        sqlstring = "insert into z_fcname(部门名称) values (ltrim(rtrim('" + bmnametemp1.Trim() + "')))";
                        insert_update_delete(sqlstring);

                        sqlstring = "select ID from z_fcname where 部门名称=ltrim(rtrim('" + bmnametemp1.Trim() + "')) and 删除=0";
                        dt_temp = return_select(sqlstring);
                    }
                    string bmid = dt_temp.Rows[0]["ID"].ToString();

                    DataRow[] row_update = dt_all_copy.Select("部门名称='" + bmnametemp1.Trim() + "'");
                    for (int ii = 0; ii < row_update.Count(); ii++)
                    {
                        row_update[ii]["部门ID"] = bmid.ToInt();
                    }
                }
                #endregion
                //保存部门信息

                //保存员工姓名至person
                #region
                dc1 = new DataColumn("员工ID", Type.GetType("System.Int64"));
                dc1.DefaultValue = 0;
                dt_all_copy.Columns.Add(dc1);

                foreach (DataRow dr in dt_all_copy.Rows)
                {

                    string pernametemp1 = dr["姓名"].ToString().Trim();
                    string namepytemp1 = MyPinYin.GetFirst(pernametemp1);// ChineseToPinYin.ToPinYin_one(pernametemp1);

                    WaitFormService.SetText("正在整理员工姓名信息！\n\r" + pernametemp1.Trim());

                    sqlstring = "select ID from person where 姓名=ltrim(rtrim('" + pernametemp1.Trim() + "'))";
                    dt_temp = return_select(sqlstring);
                    if (dt_temp.Rows.Count <= 0)
                    {
                        sqlstring = "insert into person(姓名,拼音) values (ltrim(rtrim('" + pernametemp1.Trim() + "')),ltrim(rtrim('"+ namepytemp1.Trim()+"')))";
                        insert_update_delete(sqlstring);

                        sqlstring = "select ID from person where 姓名=ltrim(rtrim('" + pernametemp1.Trim() + "'))";
                        dt_temp = return_select(sqlstring);
                    }
                    string perid = dt_temp.Rows[0]["ID"].ToString();

                    DataRow[] row_update = dt_all_copy.Select("姓名='" + pernametemp1.Trim() + "'");
                    for (int ii = 0; ii < row_update.Count(); ii++)
                    {
                        row_update[ii]["员工ID"] = perid.ToInt();
                    }
                }
                #endregion
                //保存员工姓名至person

                //保存工资条目信息
                #region
                for (int i = 0; i < dt_all_copy.Columns.Count; i++)
                {
                    string column_name = dt_all_copy.Columns[i].ColumnName.ToString();

                    WaitFormService.SetText("正在处理工资条目！\n\r" + column_name.Trim());


                    //MessageBox.Show(column_name.Trim() + "   类型：" + dt_all_copy.Columns[i].DataType.ToString());
                    sqlstring = "select ID from Subject_name where 条目名称=ltrim(rtrim('" + column_name.Trim() + "')) and 删除=0";
                    dt_temp = return_select(sqlstring);
                    if (dt_temp.Rows.Count <= 0)
                    {
                        sqlstring = "insert into subject_name(条目名称) values (ltrim(rtrim('" + column_name.Trim() + "')))";
                        insert_update_delete(sqlstring);

                        sqlstring = "select ID from Subject_name where 条目名称=ltrim(rtrim('" + column_name.Trim() + "')) and 删除=0";
                        dt_temp = return_select(sqlstring);
                    }

                    string jectid = dt_temp.Rows[0]["ID"].ToString();

                    //判断column的列datatype,只分为string和decimal
                    object test = dt_all_copy.Compute("max(" + column_name.Trim() + ")", "");
                    int column_type = 1;//为0表示是数字，为1表示是string
                    if (test.ToString().IsInt() || test.ToString().IsDecimal())
                    {
                        column_type = 0;
                    }
                    //判断column的列datatype,只分为string和decimal

                    sqlstring = "select ID from subject_mon where 月份='" + sql_month.Trim() + "' and 条目ID=" + jectid.Trim();
                    dt_temp = return_select(sqlstring);
                    if (dt_temp.Rows.Count <= 0)
                    {
                        sqlstring = "insert into subject_mon(月份,条目ID,排序,类型) values ('" + sql_month.Trim() + "'," + jectid.Trim() + "," + i.ToString() + "," + column_type + ")";
                        insert_update_delete(sqlstring);
                    }
                }
                #endregion
                //保存工资条目信息


                //将工资信息拷贝至后台数据库
                #region
                for (int i = 0; i < dt_all_copy.Rows.Count; i++)
                {
                    string pernametemp1 = dt_all_copy.Rows[i]["姓名"].ToString();
                    string bmidtempid = dt_all_copy.Rows[i]["部门ID"].ToString();
                    string peridtempid = dt_all_copy.Rows[i]["员工ID"].ToString();

                    WaitFormService.SetText("正在拷贝第" + i.ToString() + "条工资记录/共" + dt_all_copy.Rows.Count.ToString() + "条记录！\n\r" + "      员工姓名：" + pernametemp1.ToString());

                    for (int j = 0; j < dt_all_copy.Columns.Count; j++)
                    {
                        string columnnametemp1 = dt_all_copy.Columns[j].ToString();
                        if (columnnametemp1.Trim() == "姓名"
                            || columnnametemp1.Trim() == "部门名称"
                            || columnnametemp1.Trim() == "部门ID"
                            || columnnametemp1.Trim() == "员工ID"
                            ) { continue; }

                        string dt_all_copy_one_z = dt_all_copy.Rows[i][j].ToString().Trim();

                        if (dt_all_copy_one_z.Trim() == "" || (dt_all_copy_one_z.IsDecimal() && dt_all_copy_one_z.ToDecimal() == 0)) { continue; }

                        sqlstring = "select ID from Subject_name where 条目名称=ltrim(rtrim('" + columnnametemp1.Trim() + "')) and 删除=0";
                        dt_temp = return_select(sqlstring);
                        if (dt_temp.Rows.Count <= 0)
                        {
                            MessageBox.Show("没有在后台数据库中查询到：" + columnnametemp1.Trim() + "的条目记录，请重新导入工资信息，或与系统管理员联系！");
                            return;
                        }
                        string jectidtemp1 = dt_temp.Rows[0]["ID"].ToString();

                        sqlstring = "select * from Subject_mon where 月份='" + sql_month.Trim() + "' and 条目ID=" + jectidtemp1.Trim();
                        dt_temp = return_select(sqlstring);
                        if (dt_temp.Rows.Count <= 0)
                        {
                            MessageBox.Show("没有在后台数据库中查询到：" + sql_month.Trim() + "月份的" + columnnametemp1.Trim() + "的条目记录，请重新导入工资信息，或与系统管理员联系！");
                            return;
                        }

                        int ject_typetemp1 = dt_temp.Rows[0]["类型"].ToString().ToInt();

                        //如果单元格里是string,就用permoney_lr中的ID代替单元格里的内容
                        if (ject_typetemp1 == 1)
                        {
                            sqlstring = "select ID from permoney_lr where 文字内容=ltrim(rtrim('" + dt_all_copy_one_z.Trim() + "'))";
                            dt_temp = return_select(sqlstring);

                            if (dt_temp.Rows.Count <= 0)
                            {
                                sqlstring = "insert into permoney_lr(文字内容) values (ltrim(rtrim('" + dt_all_copy_one_z.Trim() + "')))";
                                insert_update_delete(sqlstring);

                                sqlstring = "select ID from permoney_lr where 文字内容=ltrim(rtrim('" + dt_all_copy_one_z.Trim() + "'))";
                                dt_temp = return_select(sqlstring);
                            }
                            dt_all_copy_one_z = dt_temp.Rows[0]["ID"].ToString();
                        }
                        //MessageBox.Show(pernametemp1+"   "+columnnametemp1 + "  " + ject_typetemp1.ToString()+"    "+ dt_all_copy_one_z);

                        sqlstring = "insert into permoney(部门ID,员工ID,条目ID,金额,月份,排序) values ('" + bmidtempid + "','" + peridtempid + "','" + jectidtemp1 + "'," + dt_all_copy_one_z.ToDecimal().ToString() + ",'" + sql_month.Trim() + "',"+i.ToString()+")";
                        insert_update_delete(sqlstring);

                    }
                }
                #endregion
                //将工资信息拷贝至后台数据库



                WaitFormService.Close();
                MessageBox.Show(sql_month.Trim() + "月份的员工工资已经导入至后台数据库！");
            }
            catch (Exception ex)
            {
                ex.errormess();
            }
        }

        private void qg_label1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(((Label)sender).Text);
        }


        //结束
    }
}
