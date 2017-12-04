using System;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace djdc_employee_wages.a_GlobalClass
{
     public class con_sql
    {
        public static SqlConnection cn=null;//前台与后台数据库建立的连接
        public static userInfo allczy_user=new userInfo();//操作员的信息


        public  con_sql()
        {
            create_cn();
        }


        public static void create_cn()
        {

            string connectString;

            //if (local_bool())
            //{
            //    connectString = "Data Source=FX\\FX;Initial Catalog=feixiangpu;User ID=qg1977721;pwd=qg1977721@163.net;;Connect Timeout=5";
            //}
            //else
            //{
                connectString = "server=qds16172939.my3w.com;database=qds16172939_db;uid=qds16172939;pwd=infowindow2015;Connect Timeout=5";
            //}

            //connectString = "server=home-qg;uid=qg1977721;pwd=qg1977721@163.net;database=feixiangpu";
              try
            {
                cn = new SqlConnection(connectString);
            }
            catch (Exception ex)
            {
                throw new Exception("连接数据库失败！请确保网络正常及服务器正常开启!");
                System.Environment.Exit(0);
            }
        }

        //执行一条修改或插入或删除的程序
        public static void insert_update_delete(string sql_string)
        {
            if (cn==null)
            { create_cn(); }
            try
            {
                //begin_class.jytt_all = true;
                SqlCommand cmd = new SqlCommand(sql_string, cn);
                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("查询数据库失败！请与系统管理员联系!\n" + "出错语句:" + sql_string);
                //begin_class.jytt_all = false;
  
                //System.Environment.Exit(0);
            }

        }

        //直接根据查询语句返回一个datatable
        public static DataTable return_select(string sql_string)
        {
            if (cn == null)
            { create_cn(); }

            SqlDataAdapter da;//形成的游标
            DataTable dt = new DataTable() ;
            try
            {
                //begin_class.jytt_all = true;
                SqlCommand cmd = new SqlCommand(sql_string, cn);
                da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();//数据库
                da.Fill(ds);

                dt = ds.Tables[0];

                for(int i=0;i<dt.Columns.Count;i++)
                {
                    string columnname = dt.Columns[i].ColumnName.ToString();
                    string columntype = dt.Columns[i].DataType.ToString();

                    if (columntype.Contains("String"))
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            row[columnname] = row[columnname].ToString().Trim();
                        }
                    }
                }
                cn.Close();

                return dt;
            }
            catch(Exception ex)
            {
                throw new Exception("查询返回数据库失败！请与系统管理员联系!\n"+"出错语句:"+sql_string);
                //begin_class.jytt_all = false;


                //System.Environment.Exit(0);

                //create_cn();

                //SqlCommand cmd = new SqlCommand(sql_string, cn);
                //da = new SqlDataAdapter(cmd);
                //DataSet ds = new DataSet();//数据库
                //da.Fill(ds);

                //DataTable dt = ds.Tables[0];
                //cn.Close();

                //return dt;
            }
            return dt;

        }
        //根据本机的IP值判断是否为内网的IP
        //通过对本公司的IP进行设置,本公司直属内网的IP第三段均为211,比如192.168.211.XXX
        //本公司的路由器的对外的IP设置为61.184.86.205
        public static bool local_bool()
        {
            bool jytt = true;
            
            string local_IP = GetIP();
            string[] local_IP_group = local_IP.Split('.');

            if (local_IP_group[2].ToString().Trim() == "211")
            {
                jytt = true;
            }
            else
            { jytt = false; }

            return jytt;
        }


        //返回本机的IP(程序中加了对IPV4的正则静态式验证,所以返回的是IPV4的IP)
        public static  string GetIP()
        {
            //IPV4的正则表态式
            String regex = "(?<=(\\b|\\D))(((\\d{1,2})|(1\\d{2})|(2[0-4]\\d)|(25[0-5]))\\.){3}((\\d{1,2})|(1\\d{2})|(2[0-4]\\d)|(25[0-5]))(?=(\\b|\\D))";

            string hostName = Dns.GetHostName();//本机名   
            //System.Net.IPAddress[] addressList = Dns.GetHostByName(hostName).AddressList;//会警告GetHostByName()已过期，我运行时且只返回了一个IPv4的地址   
            System.Net.IPAddress[] addressList = Dns.GetHostAddresses(hostName);//会返回所有地址，包括IPv4和IPv6   
            foreach (IPAddress ip in addressList)
            {
                if (QuickValidate(regex, ip.ToString().Trim()))
                {
                    return ip.ToString().Trim();
                }
            }

            return "";
        }
        /// <summary>
        /// 快速验证一个字符串是否符合指定的正则表达式。
        /// </summary>
        /// <param name="_express">正则表达式的内容。</param>
        /// <param name="_value">需验证的字符串。</param>
        /// <returns>是否合法的bool值。</returns>
        public static bool QuickValidate(string _express, string _value)
        {
            if (_value == null) return false;
            Regex myRegex = new Regex(_express);
            if (_value.Length == 0)
            {
                return false;
            }
            return myRegex.IsMatch(_value);
        }



        //结束
    }
}
