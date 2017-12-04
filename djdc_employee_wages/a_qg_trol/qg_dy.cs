using System;
using System.Windows.Forms;
using djdc_employee_wages.a_sqlconn;
using System.Threading;

namespace djdc_employee_wages.a_qg_trol
{
    public partial class qg_dy : Button
    {
        public qg_dy()
        {
            
            InitializeComponent();
            Text = "打印预览";
        }
        private string _dy_title;
        public string dy_title
        {
            get { return _dy_title; }
            set { _dy_title = value; }
        }

        private string _dy_month;
        public string dy_month
        {
            get { return _dy_month; }
            set { _dy_month = value; }
        }


        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }

        //定义委托工厂
        public delegate void TestDelegate(DataGridView DGV, string dy_title, string dy_month);
        static void Test(DataGridView DGV, string dy_title, string dy_month)
        {
            Cs_Datatable.ExportExcel(DGV, dy_title, dy_month);
        }

        private void qg_dy_Click(object sender, EventArgs e)
        {
            try
            {
                //string dy_title = "我是标题";
                //string dy_month = "201612";

                Boolean findjytt = false;
                Control con_Form = this.Parent;
                foreach (Control trol in con_Form.Controls)
                {

                    if (trol is qg_grid || trol is qg_grid_tree)
                    {
                        findjytt = true;
                        //dt = (DataTable)(((qg_grid)trol).DataSource);
                        TestDelegate d = Test;

                        //other other = new other();
                        //string ly = "数据正在导出，请稍候……";
                        //other.winwait((qg_grid)trol, ly);
                        WaitFormService.Show();
                        WaitFormService.SetText("正在做导出Excel的准备,请稍候……");

                        d.BeginInvoke((DataGridView)trol, dy_title, dy_month, null, null);
                        //sqlconn.Cs_Datatable.ExportExcel((qg_grid)trol, dy_title, dy_month);

                        Thread.Sleep(3000);

                        WaitFormService.Close();
                        //other.winwait((qg_grid)trol, "");
                        break;
                    }
                   
                }
                if (!findjytt) { MessageBox.Show("没有在本表单查询到需要导出的数据表！"); }
            }
            catch (Exception ex)
            {
                ex.errormess();
            }
        }

//结束
    }
}
