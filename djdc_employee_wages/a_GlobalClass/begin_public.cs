using System.Data;

using static djdc_employee_wages.a_GlobalClass.con_sql;

namespace djdc_employee_wages.a_GlobalClass
{
    class begin_public
    {

        public static void begin_public_other()
        {
            string sqlstring = "select MAX(months) months from allmax";
            DataTable dt = return_select(sqlstring);
            begin_class.allmax = dt.Rows[0]["months"].ToString();
            begin_class.public_pro3begin = begin_class.allmax;
            begin_class.public_pro3end = begin_class.allmax;

        }


        //结束
    }
}
