using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;
using djdc_employee_wages.a_sqlconn;

namespace djdc_employee_wages.a_qg_trol
{

    public partial class qg_combobox : ComboBox
    {
        private bool flag = true;

        private ArrayList m_list = new ArrayList();

        public qg_combobox()
        {
            //this.SuspendLayout();
            //BackColor = Color.White; //当textBox1失去焦点时，背景色恢复为White(白色)
            //this.ResumeLayout(false);
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }

        private void qg_combobox_TextUpdate(object sender, EventArgs e)
        {
            try
            {
                if (DataSource == null)
                { MessageBox.Show("没有绑定相应的数据!"); return; }

                DataTable dt = (DataTable)DataSource;
                int columnall = dt.Columns.Count;
                bool jytt_by = false;
                for (int i = 0; i < columnall; i++)
                {
                    if (dt.Columns[i].ColumnName.ToString().Trim() == "拼音")
                    { jytt_by = true; }
                }
                if (!jytt_by)
                { MessageBox.Show("数据源中不包含“拼音列”，无法拼音查询!"); return; }

                Tag = Text.ToString();
                dt.DefaultView.RowFilter = " 拼音 like '%" + Tag.ToString().Trim() + "%'";
                if (dt.DefaultView.Count <= 0)
                {
                    ToolTip toolTip = new ToolTip();// 气泡弹窗
                    toolTip.IsBalloon = false;// 不显示为气泡弹窗，气泡的箭头会乱跑
                    toolTip.SetToolTip(this, "");
                    toolTip.ShowAlways = true;// 总是显示
                    toolTip.UseAnimation = true;
                    toolTip.UseFading = true;
                    toolTip.Hide(this);
                    toolTip.Show("该拼音没有查询到记录，请重新查询！", this, 0, -22, 900);
                    Tag = Tag.ToString().Remove(Tag.ToString().Length - 1);
                    dt.DefaultView.RowFilter = " 拼音 like '%" + Tag.ToString().Trim() + "%'";
                }
                SelectedIndex = -1;
                Text = Tag.ToString();
                SelectionStart = Text.Length;
            }                            
           catch (Exception ex)
            {
                ex.errormess();
            }
        }

        private void qg_combobox_Enter(object sender, EventArgs e)
        {
            flag = false;
            ImeMode = ImeMode.Disable;//获得焦点时,设置为英文
            BackColor = Color.LightCyan; //当textBox1获得焦点时，背景色变为LightCyan（淡蓝绿色）
        }

        private void qg_combobox_Leave(object sender, EventArgs e)
        {
            ImeMode = ImeMode.NoControl;//失去焦点时，恢复原先输入法
            BackColor = Color.White; //当textBox1失去焦点时，背景色恢复为White(白色)
        }

        private void qg_combobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (flag)
            {
                SelectedIndex = -1;
            }
        }



        private void qg_combobox_DisplayMemberChanged(object sender, EventArgs e)
        {
            if (flag)
            {
                SelectedIndex = -1;
            }
        }


    }
}
