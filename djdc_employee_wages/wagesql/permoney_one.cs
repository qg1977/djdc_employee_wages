using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using static djdc_employee_wages.a_GlobalClass.con_sql;
using djdc_employee_wages.a_sqlconn;

namespace djdc_employee_wages.wagesql
{
    class permoney_one
    {

        //返回员工的工资表
        public DataTable permoneyone(string pro3begin, string pro3end,string bmidall,string peridall)
        {

            //IniFile ini = new IniFile("F://xx.ini");
            //ini.IniWriteValue(pro3begin+"  "+pro3end, "x1x", "y2y");

            DataTable dt_permoney = new DataTable();
            DataTable dt_permoney_2 = new DataTable();
            try
            {
                
                string sqlstring;
                DataTable dt_temp;

                sqlstring = "select distinct 条目ID,条目名称=isnull((select 条目名称 from subject_name where ID=s.条目ID),'')"
                     + ",类型,排序 from Subject_mon s where ID>0";
                if (pro3begin.Trim() != "")
                { sqlstring = sqlstring + " and 月份 >='" + pro3begin + "'"; }
                if (pro3end.Trim() != "")
                { sqlstring = sqlstring + " and 月份<='" + pro3end + "'"; }
                sqlstring=sqlstring+" order by 排序";
                dt_temp = return_select(sqlstring);

                DataColumn dc1;
                for (int i = 0; i < dt_temp.Rows.Count; i++)
                {
                    string jectnametemp1 = dt_temp.Rows[i]["条目名称"].ToString();
                    int ject_type = dt_temp.Rows[i]["类型"].ToString().ToInt();

                    if (ject_type == 0)
                    { dc1 = new DataColumn(jectnametemp1, Type.GetType("System.Decimal")); }
                    else
                    { dc1 = new DataColumn(jectnametemp1, Type.GetType("System.String")); }

                    dt_permoney.Columns.Add(dc1);


                }
                dc1 = new DataColumn("月份", Type.GetType("System.String"));
                dt_permoney.Columns.Add(dc1);


                sqlstring = "select 部门ID,部门名称,员工ID,姓名,条目ID,条目名称,月份,类型"
                    + ",金额,内容=(case when 类型=1 then (select 文字内容 from permoney_lr where ID=temp.金额) else '' end)"
                    + " from ("
                    + "select 部门ID,部门名称=isnull((select 部门名称 from z_fcname where ID=p.部门ID),'')"
                   + ",员工ID,姓名=isnull((select 姓名 from person where ID=p.员工ID),'')"
                     + ",条目ID,条目名称=isnull((select 条目名称 from Subject_name where ID=p.条目ID),'')"
                       + ",类型=isnull((select 类型 from subject_mon where 月份=p.月份 and 条目ID=p.条目ID),1),金额"
                          + ",月份,排序 from permoney p where ID>0";
                if (pro3begin.Trim() != "")
                { sqlstring = sqlstring + " and 月份 >='" + pro3begin + "'"; }
                 if (pro3end.Trim() != "")
                { sqlstring = sqlstring + " and 月份<='" + pro3end + "'"; }
                if (bmidall.Trim() != "")
                { sqlstring = sqlstring + " and 部门ID in (" + bmidall.Trim() + ")"; }
                if (peridall.Trim()!="")
                { sqlstring = sqlstring + " and 员工ID in (" + peridall.Trim() + ")"; }
                sqlstring = sqlstring + ") temp order by 排序";
                dt_temp = return_select(sqlstring);


                if (dt_temp.Rows.Count <= 0) { return null; }
                DataRow datarow ;
                for (int i=0;i<dt_temp.Rows.Count;i++)
                {
                    string jectnametemp1 = dt_temp.Rows[i]["条目名称"].ToString();
                    string bmidtempid = dt_temp.Rows[i]["部门ID"].ToString();
                    string bmnametemp1 = dt_temp.Rows[i]["部门名称"].ToString();
                    string peridtmepid = dt_temp.Rows[i]["员工ID"].ToString();
                    string pernametemp1 = dt_temp.Rows[i]["姓名"].ToString();
                    int lxtemp1 = dt_temp.Rows[i]["类型"].ToString().ToInt();
                    Decimal moneytemp1 = dt_temp.Rows[i]["金额"].ToString().ToDecimal();
                    string lrtemp1 = dt_temp.Rows[i]["内容"].ToString();
                    string pro3temp1 = dt_temp.Rows[i]["月份"].ToString();

                    datarow = dt_permoney.NewRow();

                    datarow["部门ID"] = bmidtempid;
                    datarow["部门名称"] = bmnametemp1;
                    datarow["员工ID"] = peridtmepid;
                    datarow["姓名"] = pernametemp1;
                    datarow["月份"] = pro3temp1;

                    if (lxtemp1 == 0)
                    { datarow[jectnametemp1] = moneytemp1; }
                    else
                    { datarow[jectnametemp1] = lrtemp1; }

                    dt_permoney.Rows.Add(datarow);
                }


                //相当于select 员工ID,月份,sum(工种) 工种,sum(基本工资) group by 员工ID,月份
                #region
                DataTable dtResult = dt_permoney.Clone();
                DataTable dtName = dt_permoney.DefaultView.ToTable(true,  "部门ID","部门名称","员工ID","姓名","月份");
                for (int i = 0; i < dtName.Rows.Count; i++)
                {
                    DataRow[] rows = dt_permoney.Select("员工ID='" + dtName.Rows[i][2] + "' and 月份='" + dtName.Rows[i][4] + "'");

                    //temp用来存储筛选出来的数据
                    DataTable temp = dtResult.Clone();
                    foreach (DataRow row in rows)
                    {
                        temp.Rows.Add(row.ItemArray);
                    }

                    DataRow dr = dtResult.NewRow();
                    dr["部门ID"] = dtName.Rows[i][0].ToString();
                    dr["部门名称"] = dtName.Rows[i][1].ToString();
                    dr["员工ID"] = dtName.Rows[i][2].ToString();
                    dr["姓名"] = dtName.Rows[i][3].ToString();
                    dr["月份"] = dtName.Rows[i][4].ToString();

                    for (int j = 0; j < temp.Columns.Count; j++)
                    {
                        string columnnametemp1 = temp.Columns[j].ColumnName.ToString().Trim();



                        if (columnnametemp1!="部门ID"
                            && columnnametemp1!="部门名称"
                            && columnnametemp1 != "员工ID"
                            && columnnametemp1 != "姓名"
                            && columnnametemp1 != "月份")
                        {

                            dr[columnnametemp1] = temp.Compute("max("+ columnnametemp1.Trim()+ ")", "");
                        }
                    }
                    //dr["工种"] = temp.Compute("max(工种)", "");
                    dtResult.Rows.Add(dr);
                }
                //for (int ii = 0; ii < dtResult.Columns.Count; ii++)
                //{                     
                //    IniFile ini = new IniFile("F://xx.ini");
                //    ini.IniWriteValue(dtResult.Columns[ii].ColumnName.ToString(), "x1x", "y2y");
                //}
                dtResult.Columns["部门ID"].SetOrdinal(0);
                dtResult.Columns["员工ID"].SetOrdinal(1);
                dtResult.Columns["月份"].SetOrdinal(3);
                #endregion
                //相当于select 员工ID,月份,sum(工种) 工种,sum(基本工资) group by 员工ID,月

                dt_permoney_2 = dtResult.Copy();

                //排序
                DataView dv = dt_permoney_2.DefaultView;
                dv.Sort = "员工ID,月份";
                dt_permoney = dv.ToTable();
                //排序
            }
            catch (Exception ex)
            {
                ex.errormess();
            }



            return dt_permoney;
        }



        public DataTable permoneyonebm(string pro3begin, string pro3end, string bmidall, string peridall)
        {
            DataTable dt_permoney_bm = null;
            try
            {
                DataTable dt_permoney = permoneyone(pro3begin, pro3end, bmidall, peridall);

                DataColumn dc1 = new DataColumn("排序", Type.GetType("System.Int32"));
                dc1.DefaultValue = 2;
                dt_permoney.Columns.Add(dc1);

                dc1 = new DataColumn("ID", Type.GetType("System.Int64"));
                dt_permoney.Columns.Add(dc1);

                dc1 = new DataColumn("上级ID", Type.GetType("System.Int64"));
                dt_permoney.Columns.Add(dc1);

                dc1 = new DataColumn("编码", Type.GetType("System.String"));
                dt_permoney.Columns.Add(dc1);

                dc1 = new DataColumn("展开", Type.GetType("System.Int32"));
                dc1.DefaultValue = 0;
                dt_permoney.Columns.Add(dc1);

                dc1 = new DataColumn("显示", Type.GetType("System.Int32"));
                dc1.DefaultValue = 0;
                dt_permoney.Columns.Add(dc1);

                //相当于select 员工ID,月份,sum(工种) 工种,sum(基本工资) group by 员工ID,月份
                #region
                DataTable dtResult = dt_permoney.Clone();
                DataTable dtName = dt_permoney.DefaultView.ToTable(true, "部门ID", "部门名称");
                for (int i = 0; i < dtName.Rows.Count; i++)
                {
                    string bmidtempid = dtName.Rows[i][0].ToString().Trim();

                    DataRow drtemp1 = dt_permoney.NewRow();
                    drtemp1["部门ID"] = bmidtempid;
                    drtemp1["排序"] = 3;
                    drtemp1["员工ID"] = 0;
                    dt_permoney.Rows.Add(drtemp1);

                    DataRow[] rows = dt_permoney.Select("部门ID='" + bmidtempid + "'");

                    //temp用来存储筛选出来的数据
                    DataTable temp = dtResult.Clone();
                    foreach (DataRow row in rows)
                    {
                        temp.Rows.Add(row.ItemArray);

                        //顺便将员工列的“上级ID”和“编码”定下来
                        row["上级ID"] = bmidtempid;
                        row["编码"] = bmidtempid.Trim() + "-" + row["员工ID"].ToString().Trim();
                        //顺便将员工列的“上级ID”和“编码”定下来
                    }


                    DataRow dr = dtResult.NewRow();
                    dr["ID"] = dtName.Rows[i][0].ToString();
                    dr["编码"] = dtName.Rows[i][0].ToString();
                    dr["部门ID"] = dtName.Rows[i][0].ToString();
                    dr["部门名称"] = "● " + dtName.Rows[i][1].ToString();
                    dr["排序"] = 1;
                    dr["展开"] = 0;
                    dr["显示"] = 1;

                    for (int j = 0; j < temp.Columns.Count; j++)
                    {
                        string columnnametemp1 = temp.Columns[j].ColumnName.ToString().Trim();



                        if (columnnametemp1 != "部门ID"
                            && columnnametemp1 != "部门名称"
                            && columnnametemp1 != "员工ID"
                            && columnnametemp1 != "姓名"
                            && columnnametemp1 != "月份"
                            && columnnametemp1 != "上级ID"
                            && columnnametemp1 != "编码"
                            && (temp.Columns[j].DataType.FullName == "System.Decimal")
                            )
                        {

                            dr[columnnametemp1] = temp.Compute("sum(" + columnnametemp1.Trim() + ")", "");
                        }
                    }
                    //dr["工种"] = temp.Compute("max(工种)", "");
                    dtResult.Rows.Add(dr);


                }
                #endregion
                //相当于select 员工ID,月份,sum(工种) 工种,sum(基本工资) group by 员工ID,月份

                //员工工资列的“部门名称”设为“”
                for (int i = 0; i < dt_permoney.Rows.Count; i++)
                {
                    dt_permoney.Rows[i]["部门名称"] = "";  //添加数据行
                }
                //员工工资列的“部门名称”设为“”

                //将部门合计的金额列加入到总表中
                for (int i = 0; i < dtResult.Rows.Count; i++)
                {
                    dt_permoney.Rows.Add(dtResult.Rows[i].ItemArray);  //添加数据行
                }
                //将部门合计的金额列加入到总表中

                //排序
                DataView dv = dt_permoney.DefaultView;
                dv.Sort = "部门ID,排序,月份";
                dt_permoney_bm = dv.ToTable();
                //排序
            }
            catch (Exception ex)
            {
                ex.errormess();
            }


            return dt_permoney_bm;
        }
        //结束
    }
}
