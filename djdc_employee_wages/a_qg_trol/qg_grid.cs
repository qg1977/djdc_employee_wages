using System;
using System.Drawing;
using System.Windows.Forms;


using djdc_employee_wages.a_sqlconn;

namespace djdc_employee_wages.a_qg_trol
{
    public partial class qg_grid : DataGridView
    {
        //初时的单元格样式
        DataGridViewCellStyle default_style;

        //是否添加“选择”列
        public bool xz_jytt
        {
            get; set;
        }
        //是否在窗体中自动平铺
        public bool auto_jytt
        { get; set; }

        public qg_grid()
        {
            RowHeadersVisible = false;//左侧的选择列
            AutoGenerateColumns = false;//禁止自动产生列
            AlternatingRowsDefaultCellStyle.BackColor = Color.LightCyan;//设置交换项的背景色为pin
            //RowHeadersVisible = false;//禁止grid左边的空格列显示
            //RowHeadersWidth = 30;
            //设置grid左边空格列不能改变宽度
            RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            //设置表头的格式（居中显示）
            ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            xz_jytt = false;
            auto_jytt = true;

            BackgroundColor = System.Drawing.ColorTranslator.FromHtml("#d3ebdb"); 

            InitializeComponent();
            RowTemplate.Height = 30;
            //单击单元格可选择一行
            SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            //AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;//列宽方式
            //AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;//列宽方式

            default_style = RowsDefaultCellStyle;//等于初始时的单元格样式
            AllowUserToResizeRows = false;//禁止修改行高
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }

        private void qg_grid_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            //判断是否可以编辑  
            if (dgv.Columns[e.ColumnIndex].Name != "选择")
            {
                //编辑不能  
                e.Cancel = true;
            }
        }

        //如果值为0，则单元格显示为空
        private void qg_grid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                DataGridView dgv = (DataGridView)sender;
                //判断本列的数据源是否存在 ，如果不存在就跳过本列
                if (dgv.Columns[e.ColumnIndex] != null && e.RowIndex > -1 && dgv.Columns[e.ColumnIndex].IsDataBound)
                {
                    if (
                        //如果该列的类型为普通的TextBoxColumn，避免为checkcolumn时设置值为空会报错
                        dgv.Columns[e.ColumnIndex].GetType() != null &&
                        dgv.Columns[e.ColumnIndex].GetType().ToString().IndexOf("TextBoxColumn") > -1
                        && Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null
                        && (
                            Rows[e.RowIndex].Cells[e.ColumnIndex].Value.GetType() == typeof(decimal)
                             || Rows[e.RowIndex].Cells[e.ColumnIndex].Value.GetType() == typeof(double)
                                || Rows[e.RowIndex].Cells[e.ColumnIndex].Value.GetType() == typeof(int)
                           )
                        )
                    {

                        if (Convert.ToInt32(Rows[e.RowIndex].Cells[e.ColumnIndex].Value) == 0)
                        {
                            e.Value = "";
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                ex.errormess();
            }
        }

        private void qg_grid_DataSourceChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < ColumnCount; i++)
            {

                if (
                    Columns[i].HeaderText == "选择"
                     || Columns[i].HeaderText == "序号"
                       || Columns[i].HeaderText == "票号"
                    )
                {Columns[i].Width = 40; }

            }
            set_column_data_name();
        }

        private void qg_grid_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (xz_jytt)
                {
                    DataGridView dgv = (DataGridView)sender;
                    //判断本列的数据源是否存在 ，如果不存在就跳过本列
                    if (e.RowIndex > -1 && dgv.Columns.Contains("选择"))
                    {
                        if (Rows[e.RowIndex].Cells["选择"].Value is DBNull)
                        {
                            Rows[e.RowIndex].Cells["选择"].Value = 1;
                        }
                        else
                        {
                            Rows[e.RowIndex].Cells["选择"].Value = Convert.ToInt32(!Convert.ToBoolean(Rows[e.RowIndex].Cells["选择"].Value));
                        }
                        if (Convert.ToBoolean(Rows[e.RowIndex].Cells["选择"].Value))
                        {
                            Rows[e.RowIndex].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#FFE4E1");
                            Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = ColorTranslator.FromHtml("#FFE4E1");
                            Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = Color.Black;
                        }
                        else
                        {
                            //Rows[e.RowIndex].DefaultCellStyle = DefaultCellStyle;
                            Rows[e.RowIndex].DefaultCellStyle.BackColor = DefaultCellStyle.BackColor;
                            if (e.RowIndex % 2 != 0)
                            {
                                Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightCyan;//设置交换项的背景色为pin
                            }

                            //Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = SystemColors.Highlight;
                            //Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = SystemColors.HighlightText;
                            Rows[e.RowIndex].DefaultCellStyle = default_style;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ex.errormess();
            }
        }

        ////设置选中行的背景色
        //public void xz_select_back_color()
        //{

        //    DataGridView dgv = (DataGridView)this;
        //    //判断本列的数据源是否存在 ，如果不存在就跳过本列
        //    if (dgv.Columns.Contains("选择"))
        //    {
        //        if (Rows.Count> 0)
        //        {
        //            int value_temp = 0;
        //            if (Rows[0].Cells["选择"].Value is DBNull)
        //            { value_temp = 1; }
        //            else
        //            {
        //                if (Convert.ToInt32(Rows[0].Cells["选择"].Value.ToString()) == 0)
        //                { value_temp = 1; }
        //            }
        //            for (int i = 0; i < Rows.Count; i++)
        //            {
        //                Rows[i].Cells["选择"].Value = value_temp;
        //            }
        //        }

        //        for (int i = 0; i < dgv.Rows.Count; i++)
        //        {
        //            if (Convert.ToBoolean(Rows[i].Cells["选择"].Value))
        //            {
        //                Rows[i].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#FFE4E1");
        //            }
        //            else
        //            {
        //                Rows[i].DefaultCellStyle.BackColor = DefaultCellStyle.BackColor;
        //                if (i % 2 != 0)
        //                {
        //                    Rows[i].DefaultCellStyle.BackColor = Color.LightCyan;//设置交换项的背景色为pin
        //                }
        //            }
        //        }
        //    }
        //}

        private void set_column_data_name()
        {
              foreach (DataGridViewColumn col in Columns)
            {
                if (col.DataPropertyName.IsNullOrEmpty())
                { col.DataPropertyName = col.Name; }
            }
        }

        private void qg_grid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //DataGridView dgv = (DataGridView)sender;
            //MessageBox.Show(Rows[e.RowIndex].Cells[e.ColumnIndex].Value.GetType().ToString());
        }

        //结束
    }
}
