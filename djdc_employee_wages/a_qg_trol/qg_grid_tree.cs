using System;
using System.Drawing;
using System.Windows.Forms;

using System.Data;

using djdc_employee_wages.a_sqlconn;
using System.Linq;

namespace djdc_employee_wages.a_qg_trol
{
    public partial class qg_grid_tree : DataGridView
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
        public qg_grid_tree()
        {
            RowHeadersVisible = false;//左侧的选择列
            AutoGenerateColumns = false;
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

            AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;//列宽方式

            default_style = RowsDefaultCellStyle;//等于初始时的单元格样式

            AllowUserToResizeRows = false;//禁止修改行高

        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }

        private void qg_grid_tree_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            //判断是否可以编辑  
            if (dgv.Columns[e.ColumnIndex].Name != "选择")
            {
                //编辑不能  
                e.Cancel = true;
            }
        }

        private void qg_grid_tree_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
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
        //如果值为0，则单元格显示为空


        private void qg_grid_tree_DataSourceChanged(object sender, EventArgs e)
        {
            
            for (int i = 0; i < ColumnCount; i++)
            {

                if (
                    Columns[i].HeaderText == "选择"
                     || Columns[i].HeaderText == "序号"
                       || Columns[i].HeaderText == "票号"
                    )
                { Columns[i].Width = 40; }
                if (Columns[i].HeaderText == "ID")
                { Columns[i].Visible = false; }

                //禁止点击标题排序
                Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            set_column_data_name();

            //DataGridView dgv = (DataGridView)sender;
            DataTable dt = (DataTable)DataSource;
            DataView dataView = dt.DefaultView;
            dataView.RowFilter = "显示=1";
            //dt.Rows[0].Delete();
        }


        private void qg_grid_tree_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
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




        private void set_column_data_name()
        {
            foreach (DataGridViewColumn col in Columns)
            {
                if (col.DataPropertyName.IsNullOrEmpty())
                { col.DataPropertyName = col.Name; }
            }


        }


        private void qg_grid_tree_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            DataTable dt = (DataTable)dgv.DataSource;



            string id = Rows[e.RowIndex].Cells["ID"].Value.ToString();

            DataRow[] dtrow = dt.Select("ID='" + id + "'");
            int zk = dtrow[0]["展开"].ToString().ToInt();
            string bm1 = dtrow[0]["编码"].ToString();
            int bm_length = bm1.Length;

             DataRow[] dtrow_1 = dt.Select("上级ID='" + id + "'");
            //如果有下级的下级,这时如果只隐藏"上级ID=id"的记录,那么下级的下级就不会隐藏
            DataRow[] dtrow_1x = dt.Select("substring(编码,1," + bm_length.ToString() + ")='" + bm1.Trim() + "' and 编码 not in('" + bm1.Trim() + "')");
            //MessageBox.Show(dtrow_1x.Count().ToString());

            if (zk == 1)
            {
                dtrow[0]["展开"] = 0;
                dtrow[0]["部门名称"] = dtrow[0]["部门名称"].ToString().Replace("○","●" );
                for (int i = 0; i < dtrow_1x.Count(); i++) { dtrow_1x[i]["显示"] = 0; }

                //因为收回的时候将下级的下级也收回,所以需要将下级的展开恢复为0
                for (int i = 0; i < dtrow_1.Count(); i++)
                {
                    dtrow_1[i]["展开"] = 0;
                    dtrow_1[i]["部门名称"] = dtrow_1[i]["部门名称"].ToString().Replace("○", "●");
                }
            }
            else
            {
                dtrow[0]["展开"] = 1;
                dtrow[0]["部门名称"] = dtrow[0]["部门名称"].ToString().Replace( "●", "○");
                for (int i = 0; i < dtrow_1.Count(); i++) { dtrow_1[i]["显示"] = 1; }
            }
        }






        //结束
    }
}
