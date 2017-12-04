using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing.Printing;
using System.Reflection;
using System.Windows.Forms;

namespace djdc_employee_wages.a_sqlconn
{
    class Cs_Datatable
    {

        /// 将泛类型集合List类转换成DataTable   
        /// </summary>   
        /// <param name="list">泛类型集合</param>   
        /// <returns></returns>   
        public static DataTable ToDataTable<T>(List<T> entitys)
        {

            //检查实体集合不能为空   
            if (entitys == null || entitys.Count < 1)
            {
                throw new Exception("需转换的集合为空");
            }

            //取出第一个实体的所有Propertie   
            Type entityType = entitys[0].GetType();
            PropertyInfo[] entityProperties = entityType.GetProperties();
            
            //生成DataTable的structure   
            //生产代码中，应将生成的DataTable结构Cache起来，此处略   
            DataTable dt = new DataTable();
            for (int i = 0; i < entityProperties.Length; i++)
            {
                dt.Columns.Add(entityProperties[i].Name, entityProperties[i].PropertyType);  
                //dt.Columns.Add(entityProperties[i].Name);
            }

            //将所有entity添加到DataTable中   
            foreach (object entity in entitys)
            {
                //检查所有的的实体都为同一类型   
                if (entity.GetType() != entityType)
                {
                    throw new Exception("要转换的集合元素类型不一致");
                }
                object[] entityValues = new object[entityProperties.Length];
                for (int i = 0; i < entityProperties.Length; i++)
                {
                    entityValues[i] = entityProperties[i].GetValue(entity, null);

                }
                dt.Rows.Add(entityValues);
            }
            return dt;
        }

        /// <summary>  
        /// 将DataRow[]转换为DataTable
        /// </summary>  
        /// <param >DataTable</param>  
        /// <returns></returns>  
        public static DataTable Row_To_Table(DataRow[] rows)
        {
            if (rows == null || rows.Length == 0) return null;
            DataTable tmp = rows[0].Table.Clone(); // 复制DataRow的表结构
            foreach (DataRow row in rows)
            {

                tmp.ImportRow(row); // 将DataRow添加到DataTable中
            }
            return tmp;
        }



        /// <summary>  
        /// 在DataTable中添加一序号列，编号从1依次递增  
        /// </summary>  
        /// <param >DataTable</param>  
        /// <returns></returns>  
        public static DataTable AddSeriNumToDataTable(DataTable dt)
        {
            //需要返回的值  
            DataTable dtNew;
            if (dt.Columns.IndexOf("序号") >= 0)
            {
                dtNew = dt;
            }
            else //添加一序号列,并且在第一列  
            {
                dtNew = dt;//暂时不用下面的自动增加“序号”列，如果要用的话删除本行代码

                return dtNew;
                //int rowLength = dt.Rows.Count;
                //int colLength = dt.Columns.Count;
                //DataRow[] newRows = new DataRow[rowLength];

                //dtNew = new DataTable();
                ////在第一列添加“序号”列  
                //dtNew.Columns.Add("序号");
                //for (int i = 0; i < colLength; i++)
                //{
                //    dtNew.Columns.Add(dt.Columns[i].ColumnName);
                //    //复制dt中的数据  
                //    for (int j = 0; j < rowLength; j++)
                //    {
                //        if (newRows[j] == null)
                //            newRows[j] = dtNew.NewRow();
                //        //将其他数据填充到第二列之后，因为第一列为新增的序号列  
                //        newRows[j][i + 1] = dt.Rows[j][i];
                //    }
                //}
                //foreach (DataRow row in newRows)
                //{
                //    dtNew.Rows.Add(row);
                //}

            }
            //对序号列填充，从1递增  
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dtNew.Rows[i]["序号"] = i + 1;
            }

            return dtNew;

        }

        #region
        //表datagridview控件中的数据导出到excel
        public static void ExportExcel(DataGridView myDGV, string dy_title, string dy_month)
        {
            //other other = new other();
            //other.winwait((Form)myDGV.Parent);

            if (myDGV.Rows.Count > 0)
            {               
                Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
                if (xlApp == null)
                {
                    MessageBox.Show("无法创建Excel对象，可能您的机子未安装Excel");
                    return;
                }

                Double zz_width_all = 0.00;//页面的横向长度
                bool zz_Orientation_bool = false;//打印机设置的纸张是横向还是坚向,默认为坚向
                string zz_pagename = "A4";//打印机选择的纸张类型
                try
                {
                    //调出页面设置对话框
                    PageSetupDialog pd = new PageSetupDialog();
                    PrintDocument printDocument = new System.Drawing.Printing.PrintDocument();
                    pd.Document = printDocument;
                    pd.ShowDialog();


                    zz_Orientation_bool = pd.PageSettings.Landscape;
                    if (zz_Orientation_bool)//如果是横向
                    { zz_width_all = pd.PageSettings.PaperSize.Height; }
                    else { zz_width_all = pd.PageSettings.PaperSize.Width; }

                    zz_pagename = pd.PageSettings.PaperSize.PaperName;
                    //调出页面设置对话框
                }
                catch (Exception ex)
                {
                    //fileSaved = false;  
                    MessageBox.Show("页面属性对话框调用失败，请重试或与系统管理员联系！\n" + ex.Message);
                }

                for (int i = 0; i < myDGV.ColumnCount; i++)
                {
                    if (myDGV.Columns[i].Visible == false
                          || myDGV.Columns[i].Width < 1
                            || myDGV.Columns[i].GetType().ToString().IndexOf("ButtonColumn") > -1//如果是按钮、单选按钮、图像则不纳入打印
                              || myDGV.Columns[i].GetType().ToString().IndexOf("CheckBoxColumn") > -1
                                || myDGV.Columns[i].GetType().ToString().IndexOf("ImageColumn") > -1
                       )
                    { myDGV.Columns[i].Tag = "N"; }
                    else { myDGV.Columns[i].Tag = "Y"; }
                }

                int DVG_width_all = 0;//表格列的宽度之和
                DataTable dt_temp = ((DataTable)myDGV.DataSource).DefaultView.ToTable();
                DataTable srcDataTable = new DataTable();
                for (int i = 0; i < myDGV.ColumnCount; i++)
                {
                    if (myDGV.Columns[i].Tag.ToString() == "Y")
                    {
                        //newdt.Columns.Add(dc.ColumnName, dc.DataType);
                        string DVG_column_name = myDGV.Columns[i].HeaderText;
                        string data_column_name = myDGV.Columns[i].DataPropertyName;
                        int DVG_column_width = myDGV.Columns[i].Width;
                        //srcDataTable.Columns.Add();
                        srcDataTable.Columns.Add(data_column_name, dt_temp.Columns[data_column_name].DataType);
                        //因为导出的表可以列头和数据表中的标题不一致，所以columnname保存表标题，caption保存表格的标题
                        srcDataTable.Columns[data_column_name].Caption = DVG_column_name;
                        //将表格中每一列的宽度保存到表的某个属性中
                        DVG_width_all = DVG_width_all + DVG_column_width;
                        srcDataTable.Columns[data_column_name].Namespace = DVG_column_width.ToString();
                    }
                }

                //将传入的原始的表dtBefore中每一行中的每个数据复制给dr2这新行，再加入到新表dt中  
                DataRow dr2 = null;
                foreach (DataRow row in dt_temp.Rows)
                {
                    dr2 = srcDataTable.NewRow();
                    for (int i = 0; i < srcDataTable.Columns.Count; i++)
                    {
                        dr2[i] = row[srcDataTable.Columns[i].ColumnName];
                    }

                    srcDataTable.Rows.Add(dr2);
                }


                try
                {

                    Microsoft.Office.Interop.Excel.Workbooks workbooks = xlApp.Workbooks;
                    Microsoft.Office.Interop.Excel.Workbook workbook = workbooks.Add(Microsoft.Office.Interop.Excel.XlWBATemplate.xlWBATWorksheet);
                    Microsoft.Office.Interop.Excel.Worksheet worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Worksheets[1];//取得sheet1  
                                                                                                                                          //让后台执行设置为不可见，为true的话会看到打开一个Excel，然后数据在往里写  
                    xlApp.Visible = true;

                    int row_begin = 1;
                    //文档的标题
                    worksheet.Cells[row_begin, 1] = dy_title;
                    row_begin = row_begin + 1;
                    //月份
                    worksheet.Cells[row_begin, 1] = dy_month;
                    row_begin = row_begin + 1;


                    //写入表格标题  
                    for (int i = 0; i < srcDataTable.Columns.Count; i++)
                    {
                        worksheet.Cells[row_begin, i + 1] = srcDataTable.Columns[i].Caption;
                    }


                    //写入数值  
                    for (int i = 0; i < srcDataTable.Columns.Count; i++)
                    {
                        //if (myDGV.Columns[i].Tag.ToString().Trim() == "Y")
                        //{
                        for (int j = 0; j < srcDataTable.Rows.Count; j++)
                        {
                            worksheet.Cells[j + 1 + row_begin, i + 1] = srcDataTable.Rows[j][i];
                        }
                        System.Windows.Forms.Application.DoEvents();
                        //}
                    }


                    //获得当前插入值的所有Excel表格范围
                    string startCol = "A";
                    int iCnt = (srcDataTable.Columns.Count / 26);
                    string endColSignal = (iCnt == 0 ? "" : ((char)('A' + (iCnt - 1))).ToString());
                    string endCol = endColSignal + ((char)('A' + srcDataTable.Columns.Count - iCnt * 26 - 1)).ToString();
                    Microsoft.Office.Interop.Excel.Range excelRange = worksheet.get_Range(startCol + (row_begin).ToString(), endCol + (srcDataTable.Rows.Count + (row_begin - 1) - iCnt * 26 + 1).ToString());
                    //获得当前插入值的所有Excel表格范围
                    excelRange.Borders.LineStyle = 1;
                    excelRange.Borders.get_Item(Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeTop).LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                    //文章标题和月份的表格范围
                    excelRange = worksheet.get_Range(startCol + "1", endCol + "1");
                    excelRange.MergeCells = true;
                    excelRange.Font.Size = 20;
                    excelRange.Font.Bold = true;
                    excelRange = worksheet.get_Range(startCol + "2", endCol + "2");
                    excelRange.MergeCells = true;


                    excelRange = worksheet.get_Range(startCol + "1", endCol + "2");
                    excelRange.HorizontalAlignment = 3;

                    worksheet.PageSetup.PrintTitleRows = "$1:$3";

                    //excel的纸张方向，是横排还是坚排
                    if (zz_Orientation_bool)
                    {
                        worksheet.PageSetup.Orientation = Microsoft.Office.Interop.Excel.XlPageOrientation.xlLandscape;
                    }
                    else { worksheet.PageSetup.Orientation = Microsoft.Office.Interop.Excel.XlPageOrientation.xlPortrait; }
                    //excel的纸张方向，是横排还是坚排

                    //excel的纸张大小，取前面页面属性对话框中的纸张大小
                    Type sizetype = typeof(Microsoft.Office.Interop.Excel.XlPaperSize);
                    bool select_zz = false;
                    int zz_pagename_excel = 0;
                    foreach (int myCode in Enum.GetValues(sizetype))
                    {
                        // TODO:遍历操作  
                        if (Enum.GetName(sizetype, myCode).IndexOf(zz_pagename) > -1)//获取名称
                        {
                            //MessageBox.Show("名称:"+Enum.GetName(sizetype, myCode)+"\n"+"值："+myCode.ToString());
                            zz_pagename_excel = Convert.ToInt32(myCode.ToString());
                            select_zz = true;//代表找到了前面打印设置页面选中的纸张大小类型
                            break;
                        }
                    }
                    if (select_zz)
                    {
                        worksheet.PageSetup.PaperSize = (Microsoft.Office.Interop.Excel.XlPaperSize)zz_pagename_excel;
                    }
                    //excel的纸张大小，取前面页面属性对话框中的纸张大小


                    zz_width_all = zz_width_all / 10;//将打印机的张纸宽度修改为ecel的合适宽度
                    for (int i = 0; i < srcDataTable.Columns.Count; i++)
                    {
                        iCnt = ((i + 1) / 26);
                        endColSignal = (iCnt == 0 ? "" : ((char)('A' + (iCnt - 1))).ToString());
                        endCol = endColSignal + ((char)('A' + (i + 1) - iCnt * 26 - 1)).ToString();

                        //上面将datagridview每列的宽保存在了临时表srcDataTable每一列的属性Namespace中
                        int col_width = Convert.ToInt32(srcDataTable.Columns[i].Namespace);
                        ((Microsoft.Office.Interop.Excel.Range)worksheet.Columns[endCol.Trim() + ":" + endCol.Trim(), System.Type.Missing]).ColumnWidth
                              = zz_width_all * col_width / DVG_width_all;
                    }
                    //打印预览
                    workbook.PrintPreview();

                    //直接打印
                    //workbook.PrintOutEx(1, 1, 1, Missing.Value, Missing.Value, Missing.Value, Missing.Value,
                    //         Missing.Value, Missing.Value);
                }
                catch (Exception ex)
                {
                    //fileSaved = false;  
                    MessageBox.Show("导出被中断，请重新导出！\n" + ex.Message);
                }





                //if (saveFileName != "")
                //{
                //    try
                //    {
                //        //workbook.Saved = true;
                //        //workbook.SaveCopyAs(saveFileName);
                //        //fileSaved = true;  
                //    }
                //    catch (Exception ex)
                //    {
                //        //fileSaved = false;  
                //        MessageBox.Show("导出文件时出错,文件可能正被打开！\n" + ex.Message);
                //    }

                //}
                //else  
                //{  
                //    fileSaved = false;  
                //}  
                //xlApp.Quit();
                //GC.Collect();//强行销毁   
                // if (fileSaved && System.IO.File.Exists(saveFileName)) System.Diagnostics.Process.Start(saveFileName); //打开EXCEL  
                //MessageBox.Show(fileName + "的简明资料保存成功", "提示", MessageBoxButtons.OK);
            }
            else
            {
                MessageBox.Show("报表为空,无表格需要导出", "提示", MessageBoxButtons.OK);
            }

        }
        #endregion





        //结束
    }


}
