using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace djdc_employee_wages.a_qg_trol
{
    public partial class qg_read_text : TextBox
    {
        public qg_read_text()
        {
            //InitializeComponent();
            this.SuspendLayout();
            this.ReadOnly = true;
            BackColor = System.Drawing.Color.FromArgb(255,255,170);//背景色为浅黄色
            Font= new Font(this.Font, FontStyle.Bold);//加粗
            this.ResumeLayout(false);
            InitializeComponent();
        }

        //protected override void OnPaint(PaintEventArgs pe)
        //{
        //    base.OnPaint(pe);
        //}

        /// <summary>
        /// 控件是否已初始化
        /// </summary>
        private bool _isInitialized;
        /// <summary>
        /// 控件是否已初始化：可设定如果已初始化不允许再次初始化
        /// 如不需设定则将set置为永远返回false即可
        /// </summary>
        public bool IsInitialized
        {
            get
            {
                return _isInitialized;
            }
            private set
            {
                _isInitialized = value;
            }
        }

        /// <summary>
        /// 控件功能
        /// </summary>
        private FuncType _textFuncType;
        /// <summary>
        /// 控件功能：不同的功能会对传入的参数进行不同的处理
        /// </summary>
        public FuncType TextFuncType
        {
            get
            {
                return _textFuncType;
            }
            private set
            {
                _textFuncType = value;
            }
        }

        /// <summary>
        /// 初始化时传入的原始数据
        /// </summary>
        private object _coreValue;
        /// <summary>
        /// 初始化时传入的原始数据
        /// </summary>
        public object CoreValue
        {
            get
            {
                return _coreValue;
            }
            private set
            {
                _coreValue = value;
            }
        }

        /// <summary>
        /// 精度 - 保留小数位数（仅作用于BigChnNum和BigChnCash）
        /// </summary>
        private int _precision = 2;
        /// <summary>
        /// 精度 - 保留小数位数（仅作用于BigChnNum和BigChnCash）
        /// </summary>
        public int Precision
        {
            get
            {
                return _precision;
            }
            set
            {
                if (value < 0)
                {
                    throw new Exception("精度值不能小于0");
                }
                if (value > 10) //可根据项目情况酌情修改最大容忍精度
                {
                    throw new Exception("精度值不能大于10");
                }
                _precision = value;
            }
        }

        /// <summary>
        /// 是否需要显示汉字大写数字（仅作用于BigChnNum和BigChnCash）
        /// </summary>
        private bool _needChineseNumerals;
        /// <summary>
        /// 是否需要显示汉字大写数字（仅作用于BigChnNum和BigChnCash）
        /// </summary>
        public bool NeedChineseNumerals
        {
            get
            {
                return _needChineseNumerals;
            }
            set
            {
                _needChineseNumerals = value;
            }
        }

        /// <summary>
        /// 数据字典主项（仅作用于DataDic）
        /// </summary>
        private string _dataDicEntry;
        /// <summary>
        /// 数据字典主项（仅作用于DataDic）
        /// </summary>
        public string DataDicEntry
        {
            get
            {
                return _dataDicEntry;
            }
            set
            {
                _dataDicEntry = value;
            }
        }

        private ToolTip toolTip = new ToolTip();

        /// <summary>
        /// 初始化控件
        /// </summary>
        /// <param name="val"></param>
        /// <param name="funcType"></param>
        public void InitCoreValue(object val, FuncType funcType)
        {
            if (IsInitialized && funcType != TextFuncType)
            {
                throw new Exception("不能改变控件用途");
            }

            switch (funcType)
            {
                case FuncType.Simple:
                    {
                        this.Text = "";

                        if (val != null)
                        {
                            this.CoreValue = val;
                            this.TextFuncType = FuncType.Simple;
                            this.Text = val.ToString();
                        }
                    }
                    break;
                case FuncType.BigChnNum:
                    {
                        this.Text = "";

                        if (val is decimal)
                        {
                            this.CoreValue = val;
                            this.TextFuncType = FuncType.BigChnNum;
                            decimal dVal = (decimal)val;
                            this.Text = String.Format("{0:N" + Precision + "}", dVal);
                            if (!IsInitialized && NeedChineseNumerals)
                            {
                                toolTip = new ToolTip();// 气泡弹窗
                                toolTip.IsBalloon = false;// 不显示为气泡弹窗，气泡的箭头会乱跑
                                toolTip.SetToolTip(this, "");
                                toolTip.ShowAlways = true;// 总是显示
                                toolTip.UseAnimation = true;
                                toolTip.UseFading = true;
                                this.Enter += (arg, obj) =>
                                {
                                    try
                                    {
                                        toolTip.Hide(this);
                                        toolTip.Show(RMBToChr2(decimal.Parse(this.Text)), this, 0, -22, 3000);
                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show(ex.Message);
                                    }
                                };
                                this.Leave += (arg, obj) =>
                                {
                                    try
                                    {
                                        toolTip.Hide(this);
                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show(ex.Message);
                                    }
                                };
                            }
                        }
                    }
                    break;
                case FuncType.BigChnCash:
                    {
                        this.Text = "";

                        if (val is decimal)
                        {
                            this.CoreValue = val;
                            this.TextFuncType = FuncType.BigChnCash;
                            decimal dVal = (decimal)val;
                            this.Text = String.Format("{0:N" + Precision + "}", dVal);
                            if (!IsInitialized && NeedChineseNumerals)
                            {
                                ToolTip toolTip = new ToolTip();// 气泡弹窗
                                toolTip.IsBalloon = false;// 不显示为气泡弹窗，气泡的箭头会乱跑
                                toolTip.SetToolTip(this, "");
                                toolTip.ShowAlways = true;// 总是显示
                                toolTip.UseAnimation = true;
                                toolTip.UseFading = true;

                                this.Enter += (arg, obj) =>
                                {
                                    try
                                    {
                                        toolTip.Hide(this);
                                        toolTip.Show(RMBToChr(decimal.Parse(this.Text)), this, 0, -22, 3000);
                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show(ex.Message);
                                    }
                                };
                                this.Leave += (arg, obj) =>
                                {
                                    try
                                    {
                                        toolTip.Hide(this);
                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show(ex.Message);
                                    }
                                };
                            }
                        }
                    }
                    break;
                case FuncType.Date:
                    {
                        this.Text = "";

                        DateTime dtVal = new DateTime();
                        if (val is int || val is long)
                        {
                            this.CoreValue = val;
                            this.TextFuncType = FuncType.Date;
                            dtVal = DateTime.ParseExact(val.ToString(), "yyyyMMdd", null);
                            this.Text = dtVal.ToLongDateString();
                        }
                        else if (val is DateTime)
                        {
                            this.CoreValue = val;
                            this.TextFuncType = FuncType.Date;
                            dtVal = (DateTime)val;
                            this.Text = dtVal.ToLongDateString();
                        }
                    }
                    break;
                case FuncType.Time:
                    {
                        this.Text = "";

                        DateTime dtVal = new DateTime();
                        if (val is int || val is long)
                        {
                            this.CoreValue = val;
                            this.TextFuncType = FuncType.Time;
                            dtVal = DateTime.ParseExact(val.ToString(), "HHmmss", null);
                            this.Text = dtVal.ToLongTimeString();
                        }
                        else if (val is DateTime)
                        {
                            this.CoreValue = val;
                            this.TextFuncType = FuncType.Time;
                            dtVal = (DateTime)val;
                            this.Text = dtVal.ToLongTimeString();
                        }
                    }
                    break;
                case FuncType.DataDic:
                    {
                        this.Text = "";

                        string dicEntry = "";
                        string subDicEntry = "";
                        if (val is KeyValuePair<string, string>)
                        {
                            this.CoreValue = val;
                            this.TextFuncType = FuncType.DataDic;
                            KeyValuePair<string, string> pair = (KeyValuePair<string, string>)val;
                            dicEntry = pair.Key;
                            subDicEntry = pair.Value;
                        }
                        else if (val is string)
                        {
                            this.CoreValue = val;
                            this.TextFuncType = FuncType.DataDic;
                            dicEntry = DataDicEntry;
                            subDicEntry = val.ToString();
                        }
                        if (!string.IsNullOrWhiteSpace(dicEntry) && !string.IsNullOrWhiteSpace(subDicEntry))
                        {
                            //TODO：获取对应数据字典值
                            this.Text = "TODO：获取对应数据字典值";
                        }
                    }
                    break;
            }

            IsInitialized = true;
        }

        public static string RMBToChr(decimal x)
        {
            string s = x.ToString("#L#E#D#C#K#E#D#C#J#E#D#C#I#E#D#C#H#E#D#C#G#E#D#C#F#E#D#C#.0B0A");
            string d = Regex.Replace(s, @"((?<=-|^)[^1-9]*)|((?'z'0)[0A-E]*((?=[1-9])|(?'-z'(?=[F-L\.]|$))))|((?'b'[F-L])(?'z'0)[0A-L]*((?=[1-9])|(?'-z'(?=[\.]|$))))", "${b}${z}");
            return Regex.Replace(d, ".", m => "负元空零壹贰叁肆伍陆柒捌玖空空空空空空空分角拾佰仟萬億兆京垓秭穰"[m.Value[0] - '-'].ToString());
        }

        public static string RMBToChr2(decimal x)
        {
            string s = x.ToString("#L#E#D#C#K#E#D#C#J#E#D#C#I#E#D#C#H#E#D#C#G#E#D#C#F#E#D#C#");
            string d = Regex.Replace(s, @"((?<=-|^)[^1-9]*)|((?'z'0)[0A-E]*((?=[1-9])|(?'-z'(?=[F-L\.]|$))))|((?'b'[F-L])(?'z'0)[0A-L]*((?=[1-9])|(?'-z'(?=[\.]|$))))", "${b}${z}");
            string integerPart = Regex.Replace(d, ".", m => "负元空零壹贰叁肆伍陆柒捌玖空空空空空空空分角拾佰仟萬億兆京垓秭穰"[m.Value[0] - '-'].ToString());
            if (integerPart == "")
            {
                integerPart = "零";
            }

            string fractionalPart = "";
            if (x.ToString().IndexOf('.') != -1)
            {
                fractionalPart = x.ToString().Substring(x.ToString().IndexOf('.'));
                fractionalPart = fractionalPart.Replace(".", "点");
                fractionalPart = fractionalPart.Replace("0", "零");
                fractionalPart = fractionalPart.Replace("1", "壹");
                fractionalPart = fractionalPart.Replace("2", "贰");
                fractionalPart = fractionalPart.Replace("3", "叁");
                fractionalPart = fractionalPart.Replace("4", "肆");
                fractionalPart = fractionalPart.Replace("5", "伍");
                fractionalPart = fractionalPart.Replace("6", "陆");
                fractionalPart = fractionalPart.Replace("7", "柒");
                fractionalPart = fractionalPart.Replace("8", "捌");
                fractionalPart = fractionalPart.Replace("9", "玖");
            }

            return integerPart + fractionalPart;
        }

    }


    public enum FuncType
    {
        /// <summary>
        /// 普通类型
        /// </summary>
        Simple = 0,
        /// <summary>
        /// 大写汉字（普通）
        /// </summary>
        BigChnNum = 1,
        /// <summary>
        /// 大写汉字（金额）
        /// </summary>
        BigChnCash = 2,
        /// <summary>
        /// 日期
        /// </summary>
        Date = 3,
        /// <summary>
        /// 时间
        /// </summary>
        Time = 4,
        /// <summary>
        /// 数据字典类型
        /// </summary>
        DataDic = 5
    }
}
