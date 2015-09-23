using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace HyBy.FrameWork.Common
{
    /// <summary>
    /// 版权所有: 版权所有(C) 2013，华跃博弈
    /// 内容摘要: 金额大小写转换
    /// 完成日期：2014-5-16
    /// 版    本：V1.0
    /// 作    者：刘同
    /// </summary>
    public class Rmb
    {
        #region 转换人民币大小金额

        /// <summary> 
        /// 转换人民币大小金额 
        /// </summary> 
        /// <param name="num">金额</param> 
        /// <returns>返回大写形式</returns> 
        public static string CmycurD(decimal num)
        {
            string str1 = "零壹贰叁肆伍陆柒捌玖";            //0-9所对应的汉字 
            string str2 = "万仟佰拾亿仟佰拾万仟佰拾元角分"; //数字位所对应的汉字 
            string str3 = "";    //从原num值中取出的值 
            string str4 = "";    //数字的字符串形式 
            string str5 = "";  //人民币大写金额形式 
            int i;    //循环变量 
            int j;    //num的值乘以100的字符串长度 
            string ch1 = "";    //数字的汉语读法 
            string ch2 = "";    //数字位的汉字读法 
            int nzero = 0;  //用来计算连续的零值是几个 
            int temp;            //从原num值中取出的值 

            num = Math.Round(Math.Abs(num), 2);    //将num取绝对值并四舍五入取2位小数 
            str4 = ((long)(num * 100)).ToString();        //将num乘100并转换成字符串形式 
            j = str4.Length;      //找出最高位 
            if (j > 15) { return "溢出"; }
            str2 = str2.Substring(15 - j);   //取出对应位数的str2的值。如：200.55,j为5所以str2=佰拾元角分 

            //循环取出每一位需要转换的值 
            for (i = 0; i < j; i++)
            {
                str3 = str4.Substring(i, 1);          //取出需转换的某一位的值 
                temp = Convert.ToInt32(str3);      //转换为数字 
                if (i != (j - 3) && i != (j - 7) && i != (j - 11) && i != (j - 15))
                {
                    //当所取位数不为元、万、亿、万亿上的数字时 
                    if (str3 == "0")
                    {
                        ch1 = "";
                        ch2 = "";
                        nzero = nzero + 1;
                    }
                    else
                    {
                        if (str3 != "0" && nzero != 0)
                        {
                            ch1 = "零" + str1.Substring(temp * 1, 1);
                            ch2 = str2.Substring(i, 1);
                            nzero = 0;
                        }
                        else
                        {
                            ch1 = str1.Substring(temp * 1, 1);
                            ch2 = str2.Substring(i, 1);
                            nzero = 0;
                        }
                    }
                }
                else
                {
                    //该位是万亿，亿，万，元位等关键位 
                    if (str3 != "0" && nzero != 0)
                    {
                        ch1 = "零" + str1.Substring(temp * 1, 1);
                        ch2 = str2.Substring(i, 1);
                        nzero = 0;
                    }
                    else
                    {
                        if (str3 != "0" && nzero == 0)
                        {
                            ch1 = str1.Substring(temp * 1, 1);
                            ch2 = str2.Substring(i, 1);
                            nzero = 0;
                        }
                        else
                        {
                            if (str3 == "0" && nzero >= 3)
                            {
                                ch1 = "";
                                ch2 = "";
                                nzero = nzero + 1;
                            }
                            else
                            {
                                if (j >= 11)
                                {
                                    ch1 = "";
                                    nzero = nzero + 1;
                                }
                                else
                                {
                                    ch1 = "";
                                    ch2 = str2.Substring(i, 1);
                                    nzero = nzero + 1;
                                }
                            }
                        }
                    }
                }
                if (i == (j - 11) || i == (j - 3))
                {
                    //如果该位是亿位或元位，则必须写上 
                    ch2 = str2.Substring(i, 1);
                }
                str5 = str5 + ch1 + ch2;

                if (i == j - 1 && str3 == "0")
                {
                    //最后一位（分）为0时，加上“整” 
                    str5 = str5 + '整';
                }
            }
            if (num == 0)
            {
                str5 = "零元整";
            }
            return str5;
        }

        /**/
        /// <summary> 
        /// 一个重载，将字符串先转换成数字在调用CmycurD(decimal num) 
        /// </summary> 
        /// <param name="num">用户输入的金额，字符串形式未转成decimal</param> 
        /// <returns></returns> 
        public static string CmycurD(string numstr)
        {
            try
            {
                decimal num = Convert.ToDecimal(numstr);
                return CmycurD(num);
            }
            catch
            {
                return "非数字形式！";
            }
        }

        #endregion

        #region 格式化（大小写互转）

        /// <summary>
        /// 格式化（大写转小写）
        /// </summary>
        /// <param name="strRMB"></param>
        /// <returns></returns>
        public static double Format(string strRMB)
        {
            try
            {
                //正则表达式，验证第一位是否阿拉伯数字，确定转换格式
                //1.5亿----混写格式
                if (Regex.IsMatch(strRMB, "^\\d"))
                {
                    //去掉元单位
                    strRMB = Regex.Replace(strRMB, "元|圆", "");
                    char temp = strRMB[strRMB.Length - 1];
                    if (temp == '万' || temp == '萬' || temp == '亿')
                    {
                        return Convert.ToDouble(strRMB.Substring(0, strRMB.Length - 1)) * Math.Pow(10, GetExp(temp));
                    }
                    else
                    {
                        return Convert.ToDouble(strRMB);
                    }
                }
                //壹亿伍千万-----大写格式
                else
                {
                    return Eval(strRMB);
                }

            }
            catch
            {
                return -1;
            }
        }

        /// <summary>
        /// 格式化（小写转大写）
        /// </summary>
        /// <param name="numRMB"></param>
        /// <returns></returns>
        public static string Format(double numRMB)
        {
            try
            {
                if (0 == numRMB)
                    return "零元整";

                StringBuilder szRMB = new StringBuilder();

                //乘100以格式成整型，便于处理
                ulong iRMB = Convert.ToUInt64(numRMB * 100);

                szRMB.Insert(0, ToUpper(Convert.ToInt32(iRMB % 100), -2));

                //去掉原来的小数位
                iRMB = iRMB / 100;

                int iUnit = 0;

                //以每4位为一个单位段进行处理，所以下边除以10000
                while (iRMB != 0)
                {
                    szRMB.Insert(0, ToUpper(Convert.ToInt32(iRMB % 10000), iUnit));
                    iRMB = iRMB / 10000;
                    iUnit += 4;
                }

                string strRMB = szRMB.ToString();

                //格式修正
                strRMB = Regex.Replace(strRMB, "零+", "零");
                strRMB = strRMB.Replace("元零整", "元整");
                strRMB = strRMB.Replace("零元", "元");

                return strRMB.Trim('零');
            }
            catch
            {
                return "";
            }
        }
        #region 私有方法

        /// <summary>
        /// 计算表达式（大写表达式求值）
        /// </summary>
        /// <param name="strRMB"></param>
        /// <returns></returns>
        private static double Eval(string strRMB)
        {
            try
            {
                if (null == strRMB)
                    return 0;

                strRMB = Replace(strRMB, false);

                if ("" == strRMB)
                    return 0;

                #region 利用位权进行计算

                //基础指数
                int basicExp = 0;
                //当前指数
                int currExp = 0;

                double numRMB = 0;

                for (int i = strRMB.Length - 1; i > -1; i--)
                {
                    char temp = strRMB[i];

                    if (temp == '元' || temp == '万' || temp == '亿' || temp == '圆' || temp == '萬')
                    {
                        basicExp = GetExp(temp);
                        currExp = 0;

                        continue;
                    }
                    else
                    {
                        if (Regex.IsMatch(temp.ToString(), "^\\d"))
                        {
                            numRMB = numRMB + Convert.ToInt32(temp.ToString()) * Math.Pow(10, (basicExp + currExp));
                        }
                        else
                        {
                            currExp = GetExp(temp);
                        }

                    }
                }

                #endregion

                return numRMB;
            }
            catch
            {
                return -1;
            }
        }

        /// <summary>
        /// 计算表达式（小写数值求大写字符串）
        /// </summary>
        /// <param name="numRMB"></param>
        /// <param name="iUnit"></param>
        /// <returns></returns>
        private static string ToUpper(int numRMB, int iUnit)
        {
            try
            {
                if (0 == numRMB)
                {
                    if (iUnit == -2)
                    {
                        return "整";
                    }

                    if (iUnit == 0)
                    {
                        return "元";
                    }

                    return "零";
                }

                StringBuilder szRMB = new StringBuilder();

                string strRMB = "";

                #region 对角/分做特殊处理

                if (iUnit == -2)
                {
                    int jiao = numRMB / 10;
                    int fen = numRMB % 10;

                    if (jiao > 0)
                    {
                        szRMB.Append(jiao);
                        szRMB.Append(GetUnit(-1));

                        if (fen > 0)
                        {
                            szRMB.Append(fen);
                            szRMB.Append(GetUnit(-2));
                        }
                    }
                    else
                    {
                        szRMB.Append(fen);
                        szRMB.Append(GetUnit(-2));
                    }

                    return Replace(szRMB.ToString(), true);
                }

                #endregion

                #region 以下为整数部分正常处理

                strRMB = numRMB.ToString("0000");

                //前一位是否是0
                bool hasZero = false;

                for (int i = 0; i < strRMB.Length; i++)
                {
                    //只有四位，最高位为‘千’，所以下边的3-i为单位修正
                    if ((3 - i) > 0)
                    {
                        if ('0' != strRMB[i])
                        {
                            szRMB.Append(strRMB[i]);
                            szRMB.Append(GetUnit(3 - i));
                            hasZero = false;
                        }
                        else
                        {
                            if (!hasZero)
                                szRMB.Append(strRMB[i]);

                            hasZero = true;
                        }
                    }
                    //最后一位，特别格式处理
                    //如最后一位是零，则单位应在零之前
                    else
                    {
                        if ('0' != strRMB[i])
                        {
                            szRMB.Append(strRMB[i]);
                            szRMB.Append(GetUnit(iUnit));
                            hasZero = false;
                        }
                        else
                        {
                            if (hasZero)
                            {
                                szRMB.Insert(szRMB.Length - 1, GetUnit(iUnit));
                            }
                            else
                            {
                                szRMB.Append(GetUnit(iUnit));
                                szRMB.Append(strRMB[i]);
                            }
                        }
                    }
                }

                //转换大写后返回
                return Replace(szRMB.ToString(), true);

                #endregion
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// 将中文大写换成阿拉伯数字
        /// </summary>
        /// <param name="strRMB"></param>
        /// <param name="toUpper">true--转换为大写/false--转换为小写</param>
        /// <returns></returns>
        private static string Replace(string strRMB, bool toUpper)
        {
            if (toUpper)
            {
                strRMB = strRMB.Replace("0", "零");
                strRMB = strRMB.Replace("1", "壹");
                strRMB = strRMB.Replace("2", "贰");
                strRMB = strRMB.Replace("3", "叁");
                strRMB = strRMB.Replace("4", "肆");
                strRMB = strRMB.Replace("5", "伍");
                strRMB = strRMB.Replace("6", "陆");
                strRMB = strRMB.Replace("7", "柒");
                strRMB = strRMB.Replace("8", "捌");
                strRMB = strRMB.Replace("9", "玖");
            }
            else
            {
                strRMB = strRMB.Replace("零", "0");
                strRMB = strRMB.Replace("壹", "1");
                strRMB = strRMB.Replace("贰", "2");
                strRMB = strRMB.Replace("叁", "3");
                strRMB = strRMB.Replace("肆", "4");
                strRMB = strRMB.Replace("伍", "5");
                strRMB = strRMB.Replace("陆", "6");
                strRMB = strRMB.Replace("柒", "7");
                strRMB = strRMB.Replace("捌", "8");
                strRMB = strRMB.Replace("玖", "9");
            }
            return strRMB;
        }

        /// <summary>
        /// 获取单位名称
        /// </summary>
        /// <param name="iCode"></param>
        /// <returns></returns>
        private static string GetUnit(int iCode)
        {
            switch (iCode)
            {
                case -2:
                    return "分";
                case -1:
                    return "角";
                case 0:
                    return "元";
                case 1:
                    return "拾";
                case 2:
                    return "佰";
                case 3:
                    return "仟";
                case 4:
                    return "萬";
                case 8:
                    return "亿";
                default:
                    return "";
            }
        }

        /// <summary>
        /// 获取位权指数
        /// </summary>
        /// <param name="cUnit"></param>
        /// <returns></returns>
        private static int GetExp(char cUnit)
        {
            switch (cUnit)
            {
                case '分':
                    return -2;
                case '角':
                    return -1;
                case '元':
                case '圆':
                    return 0;
                case '十':
                case '拾':
                    return 1;
                case '百':
                case '佰':
                    return 2;
                case '千':
                case '仟':
                    return 3;
                case '万':
                case '萬':
                    return 4;
                case '亿':
                    return 8;
                default:
                    return 0;
            }
        }

        #endregion
        #endregion

    } 

}
