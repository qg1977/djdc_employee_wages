using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.IO;

using System.Runtime.InteropServices;

using System.Collections;//ArrayList的引用
using System.Reflection;//PropertyInfo的引用
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Data.OleDb;


namespace djdc_employee_wages.a_sqlconn
{
    //一些简单实用的小函数
    class Simple_all
    {

        /// 判断一个字符串是否为合法数字
        /// </summary>
        /// <param name="message">字符串</param>
        /// <returns></returns>
        public static bool isNumberic(string message)
        {
            try
            {
                Convert.ToInt32(message);
                return true;
            }
            catch
            {
                try
                {
                    Convert.ToDecimal(message);
                    return true;
                }
                catch
                { return false; }

                return false;
            }
        }
    }


    //关于文件编码方面的类
    class FileEncoding
    {
        /// <summary>
        /// 判断文件是否已经被打开
        /// </summary>
        /// <param name="lpPathName"></param>
        /// <param name="iReadWrite"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll")]
        private static extern IntPtr _lopen(string lpPathName, int iReadWrite);

        [DllImport("kernel32.dll")]
        private static extern bool CloseHandle(IntPtr hObject);

        private const int OF_READWRITE = 2;

        private const int OF_SHARE_DENY_NONE = 0x40;

        private static readonly IntPtr HFILE_ERROR = new IntPtr(-1);

        public static int FileIsOpen(string fileFullName)
        {
            if (!File.Exists(fileFullName))
            {
                return -1;
            }

            IntPtr handle = _lopen(fileFullName, OF_READWRITE | OF_SHARE_DENY_NONE);

            if (handle == HFILE_ERROR)
            {
                return 1;
            }

            CloseHandle(handle);

            return 0;
        }


        /// <summary>
        /// 给定文件的路径，读取文件的二进制数据，判断文件的编码类型
        /// </summary>
        /// <param name="FILE_NAME">文件路径</param>
        /// <returns>文件的编码类型</returns>
        public static System.Text.Encoding GetType(string FILE_NAME)
        {
            FileStream fs = new FileStream(FILE_NAME, FileMode.Open, FileAccess.Read);
            Encoding r = GetType(fs);
            fs.Close();
            return r;
        }


        /// <summary>
        /// 通过给定的文件流，判断文件的编码类型
        /// </summary>
        /// <param name="fs">文件流</param>
        /// <returns>文件的编码类型</returns>
        public static System.Text.Encoding GetType(FileStream fs)
        {
            byte[] Unicode = new byte[] { 0xFF, 0xFE, 0x41 };
            byte[] UnicodeBIG = new byte[] { 0xFE, 0xFF, 0x00 };
            byte[] UTF8 = new byte[] { 0xEF, 0xBB, 0xBF }; //带BOM
            Encoding reVal = Encoding.Default;


            BinaryReader r = new BinaryReader(fs, System.Text.Encoding.Default);
            int i;
            int.TryParse(fs.Length.ToString(), out i);
            byte[] ss = r.ReadBytes(i);
            if (IsUTF8Bytes(ss) || (ss[0] == 0xEF && ss[1] == 0xBB && ss[2] == 0xBF))
            {
                reVal = Encoding.UTF8;
            }
            else if (ss[0] == 0xFE && ss[1] == 0xFF && ss[2] == 0x00)
            {
                reVal = Encoding.BigEndianUnicode;
            }
            else if (ss[0] == 0xFF && ss[1] == 0xFE && ss[2] == 0x41)
            {
                reVal = Encoding.Unicode;
            }
            r.Close();
            return reVal;


        }


        /// <summary>
        /// 判断是否是不带 BOM 的 UTF8 格式
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private static bool IsUTF8Bytes(byte[] data)
        {
            int charByteCounter = 1; //计算当前正分析的字符应还有的字节数
            byte curByte; //当前分析的字节.
            for (int i = 0; i < data.Length; i++)
            {
                curByte = data[i];
                if (charByteCounter == 1)
                {
                    if (curByte >= 0x80)
                    {
                        //判断当前
                        while (((curByte <<= 1) & 0x80) != 0)
                        {
                            charByteCounter++;
                        }
                        //标记位首位若为非0 则至少以2个1开始 如:110XXXXX...........1111110X 
                        if (charByteCounter == 1 || charByteCounter > 6)
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    //若是UTF-8 此时第一位必须为1
                    if ((curByte & 0xC0) != 0x80)
                    {
                        return false;
                    }
                    charByteCounter--;
                }
            }
            if (charByteCounter > 1)
            {
                throw new Exception("非预期的byte格式");
            }
            return true;
        }
        //结束
    }

    //将list与datatable之间互相转换的类
    class List_con_Datatable
    {
        //将List转换为datatable
        #region
        public static DataTable ListToDataTable<T>(IEnumerable<T> collection)
        {
            var props = typeof(T).GetProperties();
            var dt = new DataTable();
            dt.Columns.AddRange(props.Select(p => new DataColumn(p.Name, p.PropertyType)).ToArray());
            if (collection.Count() > 0)
            {
                for (int i = 0; i < collection.Count(); i++)
                {
                    ArrayList tempList = new ArrayList();
                    foreach (PropertyInfo pi in props)
                    {
                        object obj = pi.GetValue(collection.ElementAt(i), null);
                        tempList.Add(obj);
                    }
                    object[] array = tempList.ToArray();
                    dt.LoadDataRow(array, true);
                }
            }
            return dt;
        }
        #endregion

    }

    /// <summary>
    /// INI文件的操作类
    /// </summary>
    /// 下面看一下具体实例化IniFile类的操作：
    ////path为ini文件的物理路径
    //IniFile ini = new IniFile(path);
    ////读取ini文件的所有段落名
    //byte[] allSection = ini.IniReadValues(null, null);

    //通过如下方式转换byte[]类型为string[]数组类型
    //string[] sectionList;
    //ASCIIEncoding ascii = new ASCIIEncoding();
    ////获取自定义设置section中的所有key，byte[]类型
    //sectionByte = ini.IniReadValues("personal", null);
    ////编码所有key的string类型
    //sections = ascii.GetString(sectionByte);
    ////获取key的数组
    //sectionList = sections.Split(new char[1]{'\0'});

    ////读取ini文件personal段落的所有键名，返回byte[]类型
    //byte[] sectionByte = ini.IniReadValues("personal", null);

    ////读取ini文件evideo段落的MODEL键值
    //model = ini.IniReadValue("evideo", "MODEL");

    ////将值eth0写入ini文件evideo段落的DEVICE键
    //ini.IniWriteValue("evideo", "DEVICE", "eth0");
    //即：
    //[evideo]
    //DEVICE = eth0

    ////删除ini文件下personal段落下的所有键
    //ini.IniWriteValue("personal", null, null);

    ////删除ini文件下所有段落
    //ini.IniWriteValue(null, null, null);
    public class IniFile
    {
        public string Path;

        public IniFile(string path)
        {
            this.Path = path;
        }

        #region 声明读写INI文件的API函数 
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string defVal, StringBuilder retVal, int size, string filePath);

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string defVal, Byte[] retVal, int size, string filePath);
        #endregion

        /// <summary>
        /// 写INI文件
        /// </summary>
        /// <param name="section">段落</param>
        /// <param name="key">键</param>
        /// <param name="iValue">值</param>
        public void IniWriteValue(string section, string key, string iValue)
        {
            WritePrivateProfileString(section, key, iValue, this.Path);
        }

        /// <summary>
        /// 读取INI文件
        /// </summary>
        /// <param name="section">段落</param>
        /// <param name="key">键</param>
        /// <returns>返回的键值</returns>
        public string IniReadValue(string section, string key)
        {
            StringBuilder temp = new StringBuilder(255);

            int i = GetPrivateProfileString(section, key, "", temp, 255, this.Path);
            return temp.ToString();
        }

        /// <summary>
        /// 读取INI文件
        /// </summary>
        /// <param name="Section">段，格式[]</param>
        /// <param name="Key">键</param>
        /// <returns>返回byte类型的section组或键值组</returns>
        public byte[] IniReadValues(string section, string key)
        {
            byte[] temp = new byte[255];

            int i = GetPrivateProfileString(section, key, "", temp, 255, this.Path);
            return temp;
        }
    }


    public class dt_excel
    {
        public static DataTable GetDataFromExcelByCom(bool hasTitle = false)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Excel(*.xlsx)|*.xlsx|Excel(*.xls)|*.xls";
            openFile.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            openFile.Multiselect = false;
            if (openFile.ShowDialog() == DialogResult.Cancel) return null;
            var excelFilePath = openFile.FileName;

            Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Sheets sheets;
            object oMissiong = System.Reflection.Missing.Value;
            Microsoft.Office.Interop.Excel.Workbook workbook = null;
            DataTable dt = new DataTable();

            try
            {
                WaitFormService.Show();
                

                if (app == null) return null;
                workbook = app.Workbooks.Open(excelFilePath, oMissiong, oMissiong, oMissiong, oMissiong, oMissiong,
                    oMissiong, oMissiong, oMissiong, oMissiong, oMissiong, oMissiong, oMissiong, oMissiong, oMissiong);
                sheets = workbook.Worksheets;

                //将数据读入到DataTable中
                Microsoft.Office.Interop.Excel.Worksheet worksheet = (Microsoft.Office.Interop.Excel.Worksheet)sheets.get_Item(1);//读取第一张表  
                if (worksheet == null) return null;

                int iRowCount = worksheet.UsedRange.Rows.Count;
                int iColCount = worksheet.UsedRange.Columns.Count;
                //生成列头
                for (int i = 0; i < iColCount; i++)
                {
                    var name = "column" + i;
                    if (hasTitle)
                    {
                        var txt = ((Microsoft.Office.Interop.Excel.Range)worksheet.Cells[1, i + 1]).Text.ToString();
                        if (!string.IsNullOrWhiteSpace(txt)) name = txt;
                    }
                    while (dt.Columns.Contains(name)) name = name + "_1";//重复行名称会报错。
                    dt.Columns.Add(new DataColumn(name, typeof(string)));
                }
                //生成行数据
                Microsoft.Office.Interop.Excel.Range range;
                int rowIdx = hasTitle ? 2 : 1;
                for (int iRow = rowIdx; iRow <= iRowCount; iRow++)
                {

                    WaitFormService.SetText("正在录入第"+iRow.ToString()+"条的数据/共"+iRowCount.ToString()+"条记录！");

                    DataRow dr = dt.NewRow();
                    for (int iCol = 1; iCol <= iColCount; iCol++)
                    {
                        range = (Microsoft.Office.Interop.Excel.Range)worksheet.Cells[iRow, iCol];
                        dr[iCol - 1] = (range.Value2 == null) ? "" : range.Text.ToString();
                    }
                    dt.Rows.Add(dr);
                }
                WaitFormService.Close();

                return dt;
            }
            catch { return null; }
            finally
            {
                workbook.Close(false, oMissiong, oMissiong);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
                workbook = null;
                app.Workbooks.Close();
                app.Quit();
                System.Runtime.InteropServices.Marshal.ReleaseComObject(app);
                app = null;
            }
        }



        #region
        //表datagridview控件中的数据导出到excel
        //public static void ExportExcel(DataGridView myDGV, string dy_title, string dy_month)
        //{

        //    if (myDGV.Rows.Count > 0)
        //    {

        //        //string saveFileName = "1.xls";
        //        //bool fileSaved = false;  
        //        //SaveFileDialog saveDialog = new SaveFileDialog();
        //        //saveDialog.DefaultExt = "xls";
        //        //saveDialog.Filter = "Excel文件|*.xls";
        //        ////saveDialog.FileName = fileName;
        //        //saveDialog.ShowDialog();
        //        //saveFileName = saveDialog.FileName;
        //        //if (saveFileName.IndexOf(":") < 0) return; //被点了取消   
        //        Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
        //        if (xlApp == null)
        //        {
        //            MessageBox.Show("无法创建Excel对象，可能您的机子未安装Excel");
        //            return;
        //        }

        //        Double zz_width_all = 0.00;//页面的横向长度
        //        bool zz_Orientation_bool=false;//打印机设置的纸张是横向还是坚向,默认为坚向
        //        string zz_pagename = "A4";//打印机选择的纸张类型
        //        try {
        //            //调出页面设置对话框
        //            PageSetupDialog pd = new PageSetupDialog();
        //            PrintDocument printDocument = new System.Drawing.Printing.PrintDocument();
        //            pd.Document = printDocument;
        //            pd.ShowDialog();


        //            zz_Orientation_bool = pd.PageSettings.Landscape;
        //            if (zz_Orientation_bool)//如果是横向
        //            { zz_width_all = pd.PageSettings.PaperSize.Height; }
        //            else { zz_width_all = pd.PageSettings.PaperSize.Width; }

        //            zz_pagename=pd.PageSettings.PaperSize.PaperName;
        //            //调出页面设置对话框
        //        }
        //        catch (Exception ex)
        //        {
        //            //fileSaved = false;  
        //            MessageBox.Show("页面属性对话框调用失败，请重试或与系统管理员联系！\n" + ex.Message);
        //        }

        //        for (int i = 0; i < myDGV.ColumnCount; i++)
        //        {
        //            if (myDGV.Columns[i].Visible == false 
        //                  || myDGV.Columns[i].Width < 1 
        //                    || myDGV.Columns[i].GetType().ToString().IndexOf("ButtonColumn") > -1//如果是按钮、单选按钮、图像则不纳入打印
        //                      || myDGV.Columns[i].GetType().ToString().IndexOf("CheckBoxColumn") >-1
        //                        || myDGV.Columns[i].GetType().ToString().IndexOf("ImageColumn") > -1
        //               )
        //            { myDGV.Columns[i].Tag = "N"; }
        //            else { myDGV.Columns[i].Tag = "Y"; }
        //        }

        //        int DVG_width_all = 0;//表格列的宽度之和
        //        DataTable dt_temp = (DataTable)myDGV.DataSource;
        //        DataTable srcDataTable=new DataTable();
        //        for (int i = 0; i < myDGV.ColumnCount; i++)
        //        {
        //            if (myDGV.Columns[i].Tag.ToString() == "Y")
        //            {
        //                //newdt.Columns.Add(dc.ColumnName, dc.DataType);
        //                string DVG_column_name = myDGV.Columns[i].HeaderText;
        //                string data_column_name = myDGV.Columns[i].DataPropertyName;
        //                int DVG_column_width = myDGV.Columns[i].Width;
        //                //srcDataTable.Columns.Add();
        //                srcDataTable.Columns.Add(data_column_name, dt_temp.Columns[data_column_name].DataType);
        //                //因为导出的表可以列头和数据表中的标题不一致，所以columnname保存表标题，caption保存表格的标题
        //                srcDataTable.Columns[data_column_name].Caption = DVG_column_name;
        //                //将表格中每一列的宽度保存到表的某个属性中
        //                DVG_width_all = DVG_width_all + DVG_column_width;
        //                srcDataTable.Columns[data_column_name].Namespace = DVG_column_width.ToString();                     
        //            }
        //        }

        //        //将传入的原始的表dtBefore中每一行中的每个数据复制给dr2这新行，再加入到新表dt中  
        //        DataRow dr2 = null;
        //        foreach (DataRow row in dt_temp.Rows)
        //        {
        //            dr2 = srcDataTable.NewRow();
        //            for (int i=0;i<srcDataTable.Columns.Count;i++)
        //            {
        //                dr2[i] = row[srcDataTable.Columns[i].ColumnName];
        //            }

        //            srcDataTable.Rows.Add(dr2);
        //        }


        //        try
        //        {

        //            Microsoft.Office.Interop.Excel.Workbooks workbooks = xlApp.Workbooks;
        //            Microsoft.Office.Interop.Excel.Workbook workbook = workbooks.Add(Microsoft.Office.Interop.Excel.XlWBATemplate.xlWBATWorksheet);
        //            Microsoft.Office.Interop.Excel.Worksheet worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Worksheets[1];//取得sheet1  
        //             //让后台执行设置为不可见，为true的话会看到打开一个Excel，然后数据在往里写  
        //            xlApp.Visible = true;

        //            int row_begin = 1;
        //            //文档的标题
        //            worksheet.Cells[row_begin, 1] = dy_title;
        //            row_begin = row_begin + 1;
        //            //月份
        //            worksheet.Cells[row_begin, 1] = dy_month;
        //            row_begin = row_begin + 1;


        //            //写入表格标题  
        //            for (int i = 0; i < srcDataTable.Columns.Count; i++)
        //            {
        //                worksheet.Cells[row_begin, i + 1] = srcDataTable.Columns[i].Caption;
        //            }


        //                //写入数值  
        //                for (int i = 0; i < srcDataTable.Columns.Count; i++)
        //                {
        //                    //if (myDGV.Columns[i].Tag.ToString().Trim() == "Y")
        //                    //{
        //                        for (int j = 0; j < srcDataTable.Rows.Count; j++)
        //                        {
        //                            worksheet.Cells[j + 1 + row_begin, i + 1] = srcDataTable.Rows[j][i];
        //                        }
        //                        System.Windows.Forms.Application.DoEvents();
        //                    //}
        //                }


        //        //获得当前插入值的所有Excel表格范围
        //            string startCol = "A";
        //            int iCnt = (srcDataTable.Columns.Count / 26);
        //            string endColSignal = (iCnt == 0 ? "" : ((char)('A' + (iCnt - 1))).ToString());
        //            string endCol = endColSignal + ((char)('A' + srcDataTable.Columns.Count - iCnt * 26 - 1)).ToString();
        //            Microsoft.Office.Interop.Excel.Range excelRange = worksheet.get_Range(startCol + (row_begin).ToString(), endCol + (srcDataTable.Rows.Count+(row_begin-1) - iCnt * 26 + 1).ToString());
        //            //获得当前插入值的所有Excel表格范围
        //            excelRange.Borders.LineStyle = 1;
        //            excelRange.Borders.get_Item(Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeTop).LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
        //            //文章标题和月份的表格范围
        //            excelRange = worksheet.get_Range(startCol + "1", endCol + "1");
        //            excelRange.MergeCells = true;
        //            excelRange.Font.Size = 20;
        //            excelRange.Font.Bold = true;
        //            excelRange = worksheet.get_Range(startCol + "2", endCol + "2");
        //            excelRange.MergeCells = true;


        //            excelRange = worksheet.get_Range(startCol + "1", endCol + "2");
        //            excelRange.HorizontalAlignment = 3;

        //            worksheet.PageSetup.PrintTitleRows = "$1:$3";

        //            //excel的纸张方向，是横排还是坚排
        //            if (zz_Orientation_bool)
        //            {
        //                worksheet.PageSetup.Orientation = Microsoft.Office.Interop.Excel.XlPageOrientation.xlLandscape;
        //            }
        //            else { worksheet.PageSetup.Orientation = Microsoft.Office.Interop.Excel.XlPageOrientation.xlPortrait; }
        //            //excel的纸张方向，是横排还是坚排

        //            //excel的纸张大小，取前面页面属性对话框中的纸张大小
        //            Type sizetype = typeof(Microsoft.Office.Interop.Excel.XlPaperSize);
        //            bool select_zz = false;
        //            int zz_pagename_excel = 0;
        //            foreach (int myCode in Enum.GetValues(sizetype))
        //             {
        //                // TODO:遍历操作  
        //                if (Enum.GetName(sizetype, myCode).IndexOf(zz_pagename)>-1)//获取名称
        //                {
        //                    //MessageBox.Show("名称:"+Enum.GetName(sizetype, myCode)+"\n"+"值："+myCode.ToString());
        //                    zz_pagename_excel =Convert.ToInt32(myCode.ToString());
        //                    select_zz = true;//代表找到了前面打印设置页面选中的纸张大小类型
        //                    break;
        //                }
        //            }
        //            if (select_zz)
        //            {
        //                worksheet.PageSetup.PaperSize = (Microsoft.Office.Interop.Excel.XlPaperSize)zz_pagename_excel;
        //            }
        //            //excel的纸张大小，取前面页面属性对话框中的纸张大小


        //            zz_width_all = zz_width_all / 10;//将打印机的张纸宽度修改为ecel的合适宽度
        //            for (int i = 0; i <srcDataTable.Columns.Count; i++)
        //            {
        //                iCnt = ((i+1) / 26);
        //                endColSignal = (iCnt == 0 ? "" : ((char)('A' + (iCnt - 1))).ToString());
        //                endCol = endColSignal + ((char)('A' + (i+1) - iCnt * 26 - 1)).ToString();

        //                //上面将datagridview每列的宽保存在了临时表srcDataTable每一列的属性Namespace中
        //                int col_width = Convert.ToInt32(srcDataTable.Columns[i].Namespace);
        //                ((Microsoft.Office.Interop.Excel.Range)worksheet.Columns[endCol.Trim()+":"+endCol.Trim(), System.Type.Missing]).ColumnWidth
        //                      = zz_width_all* col_width/DVG_width_all;
        //            }
        //            //打印预览
        //            workbook.PrintPreview();

        //            //直接打印
        //            //workbook.PrintOutEx(1, 1, 1, Missing.Value, Missing.Value, Missing.Value, Missing.Value,
        //            //         Missing.Value, Missing.Value);
        //        }
        //        catch (Exception ex)
        //        {
        //            //fileSaved = false;  
        //            MessageBox.Show("导出被中断，请重新导出！\n" + ex.Message);
        //        }





        //        //if (saveFileName != "")
        //        //{
        //        //    try
        //        //    {
        //        //        //workbook.Saved = true;
        //        //        //workbook.SaveCopyAs(saveFileName);
        //        //        //fileSaved = true;  
        //        //    }
        //        //    catch (Exception ex)
        //        //    {
        //        //        //fileSaved = false;  
        //        //        MessageBox.Show("导出文件时出错,文件可能正被打开！\n" + ex.Message);
        //        //    }

        //        //}
        //        //else  
        //        //{  
        //        //    fileSaved = false;  
        //        //}  
        //        //xlApp.Quit();
        //        //GC.Collect();//强行销毁   
        //        // if (fileSaved && System.IO.File.Exists(saveFileName)) System.Diagnostics.Process.Start(saveFileName); //打开EXCEL  
        //        //MessageBox.Show(fileName + "的简明资料保存成功", "提示", MessageBoxButtons.OK);
        //    }
        //    else
        //    {
        //        MessageBox.Show("报表为空,无表格需要导出", "提示", MessageBoxButtons.OK);
        //    }

        //}
        #endregion

        //把数据表dt的内容导出到Excel文件中  
        public static void OutDataToExcel(System.Data.DataTable srcDataTable, string excelFilePath)
        {
            Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
            object missing = System.Reflection.Missing.Value;

            //导出到execl   
            try
            {
                if (xlApp == null)
                {
                    MessageBox.Show("无法创建Excel对象，可能您的电脑未安装Excel!");
                    return;
                }

                Microsoft.Office.Interop.Excel.Workbooks xlBooks = xlApp.Workbooks;
                Microsoft.Office.Interop.Excel.Workbook xlBook = xlBooks.Add(Microsoft.Office.Interop.Excel.XlWBATemplate.xlWBATWorksheet);
                Microsoft.Office.Interop.Excel.Worksheet xlSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlBook.Worksheets[1];

                //让后台执行设置为不可见，为true的话会看到打开一个Excel，然后数据在往里写  
                xlApp.Visible = true;

                object[,] objData = new object[srcDataTable.Rows.Count + 1, srcDataTable.Columns.Count];
                //首先将数据写入到一个二维数组中  
                for (int i = 0; i < srcDataTable.Columns.Count; i++)
                {
                    objData[0, i] = srcDataTable.Columns[i].ColumnName;
                }
                if (srcDataTable.Rows.Count > 0)
                {
                    for (int i = 0; i < srcDataTable.Rows.Count; i++)
                    {
                        for (int j = 0; j < srcDataTable.Columns.Count; j++)
                        {
                            objData[i + 1, j] = srcDataTable.Rows[i][j];
                        }
                    }
                }

                string startCol = "A";
                int iCnt = (srcDataTable.Columns.Count / 26);
                string endColSignal = (iCnt == 0 ? "" : ((char)('A' + (iCnt - 1))).ToString());
                string endCol = endColSignal + ((char)('A' + srcDataTable.Columns.Count - iCnt * 26 - 1)).ToString();
                Microsoft.Office.Interop.Excel.Range range = xlSheet.get_Range(startCol + "1", endCol + (srcDataTable.Rows.Count - iCnt * 26 + 1).ToString());

                range.Value = objData; //给Exccel中的Range整体赋值  
                range.EntireColumn.AutoFit(); //设定Excel列宽度自适应  
                xlSheet.get_Range(startCol + "1", endCol + "1").Font.Bold = 1;//Excel文件列名 字体设定为Bold  

                //设置禁止弹出保存和覆盖的询问提示框  
                xlApp.DisplayAlerts = false;
                xlApp.AlertBeforeOverwriting = false;

                if (xlSheet != null)
                {
                    xlSheet.SaveAs(excelFilePath, missing, missing, missing, missing, missing, missing, missing, missing, missing);
                    //xlApp.Quit();  
                    //KillProcess(xlApp);
                }
            }
            catch (Exception ex)
            {
                //KillProcess(xlApp);
                throw ex;
            }
        }

    }


    
    public class time_jytt
    {
        #region 判断日期     
        /// <summary>
        /// 判断日期的全部格式（yyyy-MM-dd HH:mm:ss） 
        /// </summary>
        /// <param name="dateStr">输入日期的字符串</param>
        /// <returns></returns>
        public bool isDateTimeLong(string dateStr)
        {
            return Regex.IsMatch(dateStr, @"^(((((1[6-9]|[2-9]\d)\d{2})-(0?[13578]|1[02])-(0?[1-9]|[12]\d|3[01]))|(((1[6-9]|[2-9]\d)\d{2})-(0?[13456789]|1[012])-(0?[1-9]|[12]\d|30))|(((1[6-9]|[2-9]\d)\d{2})-0?2-(0?[1-9]|1\d|2[0-8]))|(((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))-0?2-29-)) (20|21|22|23|[0-1]?\d):[0-5]?\d:[0-5]?\d)$");

        }

        /// <summary>
        /// 判断日期的全部格式（不带秒）（yyyy-MM-dd HH:mm）
        /// </summary>
        /// <param name="dateStr">输入日期的字符串</param>
        /// <returns></returns>
        public bool isDateTimeShort(string dateStr)
        {
            return Regex.IsMatch(dateStr, @"^(((((1[6-9]|[2-9]\d)\d{2})-(0?[13578]|1[02])-(0?[1-9]|[12]\d|3[01]))|(((1[6-9]|[2-9]\d)\d{2})-(0?[13456789]|1[012])-(0?[1-9]|[12]\d|30))|(((1[6-9]|[2-9]\d)\d{2})-0?2-(0?[1-9]|1\d|2[0-8]))|(((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))-0?2-29-)) (20|21|22|23|[0-1]?\d):[0-5]?\d)$");
        }

        /// <summary>
        /// 判断日期的日期部分格式（yyyy-MM-dd）
        /// </summary>
        /// <param name="dateStr">输入的日期的日期部分字符串</param>
        /// <returns>bool</returns>
        public bool isDateShort(string dateStr)
        {
            return Regex.IsMatch(dateStr, @"^((((1[6-9]|[2-9]\d)\d{2})-(0?[13578]|1[02])-(0?[1-9]|[12]\d|3[01]))|(((1[6-9]|[2-9]\d)\d{2})-(0?[13456789]|1[012])-(0?[1-9]|[12]\d|30))|(((1[6-9]|[2-9]\d)\d{2})-0?2-(0?[1-9]|1\d|2[0-9]))|(((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))-0?2-29-))$");

        }

        /// <summary>
        /// 判断日期的时间部分格式，带秒（HH:mm:ss）  
        /// </summary>
        /// <param name="dateStr">输入日期的时间部分字符串</param>
        /// <returns>bool</returns>
        public bool isTimeLong(string dateStr)
        {
            return Regex.IsMatch(dateStr, @"^((20|21|22|23|[0-1]?\d):[0-5]?\d:[0-5]?\d)$");
        }

        /// <summary>
        /// 判断日期的时间部分格式，没有秒（HH:mm） 
        /// </summary>
        /// <param name="dateStr">输入日期的时间部分字符串</param>
        /// <returns>bool</returns>
        public bool isTimeShort(string dateStr)
        {
            return Regex.IsMatch(dateStr, @"^((20|21|22|23|[0-1]?\d):[0-5]?\d)$");
        }
        #endregion 判断日期 
    }



    //结束
}
