using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace djdc_employee_wages.a_sqlconn
{
    class other
    {
        //public static ToolTip toolTip ;

        //public void winwait(Control trol, string ly,int times=3000)
        //{

        //    ToolTip toolTip = new ToolTip();
        //    toolTip.IsBalloon = true;// 不显示为气泡弹窗，气泡的箭头会乱跑
        //    toolTip.ToolTipIcon = ToolTipIcon.Info;
        //    toolTip.SetToolTip(trol, "");
        //    toolTip.ShowAlways = true;// 总是显示
        //    toolTip.UseAnimation = true;
        //    toolTip.UseFading = true;

        //    toolTip.Hide(trol);
        //    toolTip.Show(ly, trol, trol.Width / 2, trol.Height / 2, times);
        //}



        /// <summary>
        /// 功能名称:查看MDI子窗体是否已经被打开
        /// 输入参数:MdiFather,Form,需要判断的父窗体对象
        ///          MdiChild,string,需要判断的子窗体控件名
        /// 返回结果:-1为没有被打开,正数为子窗体集的数组下标
        /// </summary>
        public static int HaveOpened(Form frmMdiFather, string strMdiChild)
        {
            int bReturn = -1;
            for (int i = 0; i < frmMdiFather.MdiChildren.Length; i++)
            {
                //MessageBox.Show(frmMdiFather.MdiChildren[i].Name.ToString()+"   "+strMdiChild.ToString());
                if (frmMdiFather.MdiChildren[i].Name == strMdiChild)
                {
                   
                    frmMdiFather.MdiChildren[i].BringToFront();
                    bReturn = i;
                    break;
                }
            }
            return bReturn;
        }





        //结束
    }
}
