using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Forms;

namespace djdc_employee_wages.a_sqlconn
{
    public static class qg_common
    {
        //字符串是否为空或null
        #region
        public static bool IsNullOrEmpty(this string s)
        {
            return string.IsNullOrEmpty(s);
        }
        public static bool NotIsNullOrEmpty(this string s)
        {
            return !string.IsNullOrEmpty(s);
        }
        #endregion


        //判断是否数字
        #region
        public static bool IsInt(this string s)
        {
            string cc = "";
            bool L_jytt = false;
            foreach (char c in s)
            {
                if (c != '0') { L_jytt = true; }
                if (L_jytt) { cc = cc.Trim() + c.ToString().Trim(); }
            }
            if (cc.Trim() == "") { cc = "0"; }
            s = cc;
            int i;
            return int.TryParse(s, out i);
        }
        //        //string s = "615";
        //int i = 0;
        //    if (s.IsInt()) i = s.ToInt();
        //
        //转换数字,不为数字是为0
        public static int ToInt(this string s)
        {
            //return int.Parse(s);
            int id;
            //int.TryParse(s, out id);//这里当转换失败时返回的id为0
            if (s.IndexOf('.') > 0)
            {
                
                s = s.Substring(0, s.IndexOf('.'));
            }
            bool j_jytt = false;
            if (s.IndexOf('-') > 0)
            {
                j_jytt = true;
                s = s.Replace("-", "");
            }
            if (j_jytt) { s = "-" + s.Trim(); }

            int.TryParse(s, out id);
            //id = Convert.ToInt32(s);
            return id;
        }
        #endregion


        //判断是否长数字
        #region
        public static bool IsLong(this string s)
        {
            string cc = "";
            bool L_jytt = false;
            foreach (char c in s)
            {
                if (c != '0') { L_jytt = true; }
                if (L_jytt) { cc = cc.Trim() + c.ToString().Trim(); }
            }
            if (cc.Trim() == "") { cc = "0"; }
            s = cc;
            long i;
            return long.TryParse(s, out i);
        }
        //        //string s = "615";
        //int i = 0;
        //    if (s.IsInt()) i = s.ToInt();
        //
        //转换数字,不为数字是为0
        public static long ToLong(this string s)
        {
            //return int.Parse(s);
            long id;
            if (s.IndexOf('.') > 0)
            {
                s = s.Substring(0, s.IndexOf('.'));
            }
            bool j_jytt = false;
            if (s.IndexOf('-')>0)
            {
                j_jytt = true;
                s = s.Replace("-", "");
            }
            if (j_jytt) { s = "-" + s.Trim(); }
            long.TryParse(s, out id);//这里当转换失败时返回的id为0
   
            return id;
        }
        #endregion

        //将字符串截取成整数
        #region
        //public static long StrJQLong(this string s)
        //{
        //    long i = 0;

        //    如果是小数
        //    if (s.IndexOf('.') > 0)
        //    {
        //        s = s.Substring(0, s.IndexOf('.'));
        //    }

        //    string cc = "";

        //    bool L_jytt = false;
        //    foreach (char c in s)
        //    {
        //        if (c != '0') { L_jytt = true; }
        //        if (L_jytt) { cc = cc.Trim() + c.ToString().Trim(); }
        //    }

        //    如果字符串是“000000000.00”之类
        //    if (cc.Trim() == "") { cc = "0"; }

        //    if (!cc.IsDouble() && !cc.IsLong() && !cc.IsInt())
        //    { throw new Exception(s.Trim() + "不是一个正常的数字！"); }

        //    i = cc.ToLong();

        //    return i;
        //}
        #endregion

        //判断是否为小数
        #region
        public static bool IsDouble(this string s)
        {
            string cc = "";
            bool L_jytt = false;
            foreach (char c in s)
            {
                if (c != '0') { L_jytt = true; }
                if (L_jytt) { cc = cc.Trim() + c.ToString().Trim(); }
            }
            if (cc.Trim() == "") { cc = "0"; }
            s = cc;
            double i;
            return double.TryParse(s, out i);
        }
        //转换为小数，不为数字为0
        public static double ToDouble(this string s)
        {
            double id;

            bool j_jytt = false;
            if (s.IndexOf('-') > 0)
            {
                j_jytt = true;
                s = s.Replace("-", "");
            }
            if (j_jytt) { s = "-" + s.Trim(); }
       
            double.TryParse(s, out id);
            return id;
        }
        #endregion

        //判断是否为小数
        #region
        public static bool IsDecimal(this string s)
        {
            string cc = "";
            bool L_jytt = false;
            foreach (char c in s)
            {
                if (c != '0') { L_jytt = true; }
                if (L_jytt) { cc = cc.Trim() + c.ToString().Trim(); }
            }
            if (cc.Trim() == "") { cc = "0"; }
            s = cc;
            Decimal i;
            return Decimal.TryParse(s, out i);
        }
        //转换为小数，不为数字为0
        public static Decimal ToDecimal(this string s)
        {
            Decimal id;

            bool j_jytt = false;
            if (s.IndexOf('-') > 0)
            {
                j_jytt = true;
                s = s.Replace("-", "");
            }
            if (j_jytt) { s = "-" + s.Trim(); }

            Decimal.TryParse(s, out id);
            return id;
        }
        #endregion
        //判断是否日期格式
        #region
        public static bool IsDate(this string s)
        {
            DateTime i;
            return DateTime.TryParse(s, out i);
        }


        /// <summary>
        /// 此时间是否在此范围内 -1:小于开始时间 0:在开始与结束时间范围内 1:已超出结束时间
        /// </summary>
        /// <param name="t"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public static int IsRange(this DateTime t, DateTime startTime, DateTime endTime)
        {
            if (((startTime - t).TotalSeconds > 0))
            {
                return -1;
            }

            if (((endTime - t).TotalSeconds < 0))
            {
                return 1;
            }

            return 0;
        }
        //time.IsRange(t1, t2);//判断时间time是否在t1到t2的范围内
        #endregion



        //当linq生成的var转换为datatable时，需要先判断var是为空集
        #region
        public static bool linqIsEmpty<T>(this IEnumerable<T> source)
        {
            return !source.Any();
        }
        public static bool linqIsNotEmpty<T>(this IEnumerable<T> source)
        {
            return source.Any();
        }
        //当linq生成的var转换为datatable时，需要先判断var是为空集

        //直接将linq生成的var转换为datatable,如果var为空的话，返回的是dt_old的结构（不含数据）
        public static DataTable LinqToTable<T>(this IEnumerable<T> source,DataTable dt_old) where T:DataRow
        {
            DataTable dt = dt_old.Clone();

            if (source.linqIsNotEmpty())
            {
                dt = source.CopyToDataTable<DataRow>();
            }
            return dt;
        }
        #endregion



        #region 全角转换半角以及半角转换为全角  
        ///转全角的函数(SBC case)  
        ///全角空格为12288，半角空格为32  
        ///其他字符半角(33-126)与全角(65281-65374)的对应关系是：均相差65248  
        public static string To_D_SBC(this string input)
        {
            // 半角转全角：  
            char[] array = input.ToCharArray();
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == 32)
                {
                    array[i] = (char)12288;
                    continue;
                }
                if (array[i] < 127)
                {
                    array[i] = (char)(array[i] + 65248);
                }
            }
            return new string(array);
        }
        
        ///转半角的函数(DBC case)  
        ///全角空格为12288，半角空格为32  
        ///其他字符半角(33-126)与全角(65281-65374)的对应关系是：均相差65248//   
        public static string To_X_DBC(this string input)
        {
            char[] array = input.ToCharArray();
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == 12288)
                {
                    array[i] = (char)32;
                    continue;
                }
                if (array[i] > 65280 && array[i] < 65375)
                {
                    array[i] = (char)(array[i] - 65248);
                }
            }
            return new string(array);
        }
        #endregion



        //catch的ex的错误弹出框
        #region
        public static void errormess(this Exception ex)
        {
            int i = ex.StackTrace.IndexOf("行号");
            string s = ex.StackTrace.Substring(i + 3);
            i = s.IndexOf(' ');
            if (i != -1)
            {
                s = s.Substring(0, i);
            }
            string stacktrace = ex.StackTrace;//获得详细的错误位置
            string errpoint = stacktrace.Substring(stacktrace.IndexOf("位置"), stacktrace.Length - stacktrace.IndexOf("位置"));

            MessageBox.Show("程序出现异常，请与系统管理员联系！" + "\n" + "错误的原因：" + ex.Message + "\n" + "错误的行号：" + s + "\n" +"错误方法名称:"+ ex.TargetSite.Name + "\n" +"错误的位置:"+ errpoint + "\n" + "错误的具体内容：" + ex.StackTrace );

        }
        #endregion


//结束
    }
}
