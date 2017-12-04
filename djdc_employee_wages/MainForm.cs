using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


using djdc_employee_wages.a_sqlconn;
using static djdc_employee_wages.a_GlobalClass.con_sql;


namespace djdc_employee_wages
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
 

            InitializeComponent();
            //skinEngine1.SkinFile = "skins/GlassOrange.ssk";
            toolStripStatusLabel1.Text = "";//准备放当前时间
            mdi_menu.LayoutStyle = ToolStripLayoutStyle.Flow;//菜单宽度不够，可多层
            Text = "丹江口水力发电厂员工工资系统";

            
        }

        //执行一些Begin


        private void timer1_Tick(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = DateTime.Now.ToString() + "     ";
        }



        #region 遍历菜单，查找对应的菜单，并返回 
        private ToolStripMenuItem selectMenu(string top_ID)
        {
            ToolStripMenuItem topItem = null;
            try
            {
                

                foreach (ToolStripMenuItem item in mdi_menu.Items)
                {

                    if (item.Tag == null)
                    { continue; }
                    if (item.Tag.ToString() == top_ID)
                    {
                        topItem = item;
                    }
                    if (topItem == null)
                    {
                        itemtemp = null;
                        topItem = EnumerateMenu(item, top_ID);
                        if (itemtemp != null)
                        { break; }
                    }
                }
                
            }
            catch (Exception ex)
            {
                ex.errormess();
            }
            return topItem;
        }

        private ToolStripMenuItem itemtemp = null;
        private ToolStripMenuItem EnumerateMenu(ToolStripMenuItem item, string top_ID)
        {
            try
            {
                foreach (ToolStripMenuItem subItem in item.DropDownItems)
                {
                    if (subItem.Tag.ToString() == top_ID)
                    {
                        itemtemp = subItem;
                        return subItem;
                    }
                    EnumerateMenu(subItem, top_ID);
                }
                
            }
            catch (Exception ex)
            {
                ex.errormess();
            }
            return itemtemp;
        }
        #endregion 遍历菜单，查找对应的菜单，并返回 




        private void add_menu()
        {
            try
            {
                #region 将原生菜单的全部菜单保存，待生成后将原先的菜单附在最后
                List<ToolStripMenuItem> itemtemp = new List<ToolStripMenuItem>();
                foreach (ToolStripMenuItem item in mdi_menu.Items)
                {
                    itemtemp.Add(item);
                }
                #endregion


                string sqlstring = "select ID,上级ID,名称,表单,级数,有下级  from z_menu_view where 删除=0 order by 级数";
                DataTable dt_menu = return_select(sqlstring);

                string top_ID = "0";
                ToolStripMenuItem subItem = null;

                foreach (DataRow dr in dt_menu.Rows)
                {
                    if (dr["上级ID"].ToString() == "0")
                    {
                        AddContextMenu(dr["ID"].ToString().Trim(), mdi_menu.Items, new EventHandler(MenuClicked));
                        continue;
                    }



                    subItem = selectMenu(dr["上级ID"].ToString());
                    if (subItem != null)
                    {
                        if (dr["有下级"].ToString() == "0")
                        {
                            AddContextMenu(dr["ID"].ToString().Trim(), subItem.DropDownItems, new EventHandler(MenuClicked));
                        }
                        else
                        {
                            AddContextMenu(dr["ID"].ToString().Trim(), subItem.DropDownItems, new EventHandler(MenuClicked));
                        }
                    }
                    //如果没有查找到上级菜单，则直接放到顶级菜单中
                    else
                    {
                        if (dr["有下级"].ToString() == "0")
                        {
                            AddContextMenu(dr["ID"].ToString().Trim(), mdi_menu.Items, new EventHandler(MenuClicked));
                        }
                        else
                        {
                            AddContextMenu(dr["ID"].ToString().Trim(), mdi_menu.Items, new EventHandler(MenuClicked));
                        }
                    }

                    top_ID = dr["上级ID"].ToString();

                }




                foreach (ToolStripMenuItem item in itemtemp)
                {
                    mdi_menu.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                ex.errormess();
            }
        }


        #region 添加子菜单
        /// <summary>
        /// 添加子菜单
        /// </summary>
        /// <param name="text">要显示的文字，如果为 - 则显示为分割线</param>
        /// <param name="cms">要添加到的子菜单集合</param>
        /// <param name="callback">点击时触发的事件</param>
        /// <returns>生成的子菜单，如果为分隔条则返回null</returns>

        ToolStripMenuItem AddContextMenu(string menu_id,ToolStripItemCollection cms, EventHandler callback)
        {
            try
            {
                string sqlstring = "select ID,名称,上级ID,isnull(表单,'') 表单 from z_menu_view where ID=" + menu_id;
                DataTable dt = return_select(sqlstring);

                //菜单间的分隔线
                if (dt.Rows.Count <= 0)
                {
                    ToolStripSeparator tsp = new ToolStripSeparator();
                    cms.Add(tsp);
                    return null;
                }

                string menu_top_id = dt.Rows[0]["上级ID"].ToString().Trim();
                string menu_name = dt.Rows[0]["名称"].ToString().Trim();
                string form_name = dt.Rows[0]["表单"].ToString().Trim();

                if (menu_top_id == "0")
                {
                    menu_name = "【" + menu_name.Trim() + "】";
                }
                //

                if (!string.IsNullOrEmpty(menu_name))
                {
                    ToolStripMenuItem tsmi = new ToolStripMenuItem(menu_name);
                    tsmi.Tag = menu_id;
                    if (!string.IsNullOrEmpty(form_name) && callback != null)
                    {
                        tsmi.Click += callback;
                    }
                    cms.Add(tsmi);

                    tsmi.ToolTipText = menu_id;
                    return tsmi;
                }
               
            }
            catch (Exception ex)
            {
                ex.errormess();
            }
            return null;
        }

        void MenuClicked(object sender, EventArgs e)
        {
            try
            {
                string menu_id = (sender as ToolStripMenuItem).Tag.ToString();

                string sqlstring = "select ID,isnull(表单,'') 表单,最大化 from z_menu_view where ID=" + menu_id;
                DataTable dt = return_select(sqlstring);

                string max_jytt = dt.Rows[0]["最大化"].ToString().Trim();
                string form_name = dt.Rows[0]["表单"].ToString().Trim();

                string form_name_add = "djdc_employee_wages." + form_name;


                int index =other.HaveOpened(this, form_name);
                if (index == -1)
                {
                    ObjectHandle t = null;
                    try
                    {
                        t = Activator.CreateInstance("djdc_employee_wages", form_name_add);
                        
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("没有查询到本表单!\n表单名称："+form_name_add);
                    }

                    Form f = (Form)t.Unwrap();
                    f.MdiParent = this;
                    //窗体居中
                    f.WindowState = FormWindowState.Normal;
                    //f.ControlBox = false;//取消窗体的标题栏
                    if (max_jytt == "1")
                    {
                        f.Dock = DockStyle.Fill;
                    }else
                    {
                        f.StartPosition = FormStartPosition.CenterScreen;//窗体居中
                    }
                    
                    f.Show();
                    mdi_menu.Enabled = false;//当打开新窗口时，将mdi窗体的菜单设为不可用，关闭新窗口时再恢复为可用        
                                               
                }
                else
                {
                    this.MdiChildren[index].Show();
                }



            }
            catch (Exception ex)
            {
                ex.errormess();
            }
            //catch (Exception ex)
            //{
            //    //fileSaved = false;  
            //    MessageBox.Show("没有找到对应的表单！请与系统管理员联系！\n" + (sender as ToolStripMenuItem).ToolTipText.ToString()+ ex.Message);
            //}
        }
        #endregion 添加子菜单


        private void 更改密码_Click(object sender, EventArgs e)
        {
            try
            {
                int index = other.HaveOpened(this, "passs");
                if (index == -1)
                {
                    Form passs = new passs();
                    passs.MdiParent = this;
                    //passs.WindowState = FormWindowState.Maximized;
                    passs.StartPosition = FormStartPosition.CenterScreen;//窗体居中
                    passs.Show();
                }
                else
                {
                    this.MdiChildren[index].Show();
                }
            }
            catch (Exception ex)
            {
                ex.errormess();
            }
        }


        private void 退出系统_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
            //Application.Exit();
            Close();
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            auto_bg_img();
        }
        private void auto_bg_img()
        {
            this.BackgroundImage = Image.FromFile(@"ware/bj/bj001.jpg");
            this.BackgroundImageLayout = ImageLayout.Stretch;//设置背景图片自动适应

            //待用，看能否设置窗体宽高
            a_GlobalClass.other_static.mdi_max_width = ClientSize.Width;
            a_GlobalClass.other_static.mdi_max_height = ClientSize.Height - mdi_menu.Height - statusStrip1.Height;
            a_GlobalClass.other_static.mdi_top_zl = (Height - ClientRectangle.Height) + mdi_menu.Height;


        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            try {
                WaitFormService.Show();
                WaitFormService.SetText("正在初始化系统数据,请稍候…………");
                begin.auto();
                add_menu();
                auto_bg_img();
                WaitFormService.Close();

            }
            catch (Exception ex)
            {
                ex.errormess();
                System.Environment.Exit(0);
            }

        }



        //结束
    }
}
