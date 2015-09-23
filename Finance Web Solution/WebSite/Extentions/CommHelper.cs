using HyBy.Trading.BusinessFramework;
using System;
using System.Data;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Web.UI.WebControls;

namespace WebSite
{
    public class CommHelper
    {

        public static string GetClientIP()
        {
            string userIP;
            System.Web.HttpContext context = System.Web.HttpContext.Current;
            if (context.Request.ServerVariables["HTTP_VIA"] == null)
            {
                userIP = context.Request.UserHostAddress;
            }
            else
            {
                userIP = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            }
            return userIP;
        }

        public static string GetAreaByCurrentClientIP()
        {
            return GetAreaByIP(GetClientIP());
        }

        public static string GetAreaByIP(string ipAddress)
        {
            if (!IpAddressAvailable(ipAddress))
            {
                return "";
            }

            long value = GetIPCount(ipAddress);

            //C_StatIPInfoEntity entity = new C_StatIPInfoEntity();
            //using (IC_StatIPInfoService service = new C_StatIPInfoService())
            //{
            //    Dictionary<string,object> dic = new Dictionary<string,object>();
            //    dic.Add("IP",value);
            //    //entity = service.GetC_StatIPInfoEntityByIP(dic);
            //}

            //return entity.AreaName ?? "";
            return "";
        }



        //取得ip的long值 3.254.255.255 = 3*256^3 + 254 *256^2
        public static long GetIPCount(string ipAddress)
        {
            ipAddress = ipAddress.Trim();
            string[] ipSecs = ipAddress.Split('.');
            long value = 0;
            for (int i = 0; i < 4; i++)
            {
                int ipSecDec = int.Parse(ipSecs[i]);
                int power = 3 - i;
                long ipSecValue = (long)(ipSecDec * Math.Pow(256, power));
                value = value + ipSecValue;
            }
            value = value + 1;
            return value;
        }

        /// <summary>
        /// 判断ip地址是否有问题  1 地址段数， 地址段数里面是否是数字，数字是否在 0-255范围内
        /// 从以上三个方面监测
        /// </summary>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        private static bool IpAddressAvailable(string ipAddress)
        {
            ipAddress = ipAddress.Trim();
            string[] ipSecs = ipAddress.Split('.');
            if (ipSecs.Length != 4) return false;

            //如果每个段都可以转为int则返回真
            for (int i = 0; i < ipSecs.Length; i++)
            {
                try
                {
                    int ipSecDec = int.Parse(ipSecs[i]);
                    if (ipSecDec < 0 || ipSecDec > 255)
                    {
                        return false;
                    }
                }
                catch
                {
                    return false;
                }
            }
            return true;
        }
        /// <summary>
        /// 格式化消息时间
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string GetFormatMessageTimeString(DateTime date)
        {
            string outStr = string.Format("{0:yyyy年M月d HH:mm}", date);
            TimeSpan ts = DateTime.Now - date;
            if (ts.TotalMinutes < 1)
            {
                //如果少于1分钟
                outStr = "1分钟内";
            }
            else if (ts.TotalHours < 1)
            {
                //如果少于1小时
                outStr = ts.Minutes + "分钟前";
            }
            else if (ts.TotalDays < 1)
            {
                //如果少于1天
                string dayStr = "今天"; //用于判断是今天还是昨天
                if (DateTime.Now.Day > date.Day) { dayStr = "昨天"; };
                outStr = dayStr + " " + date.Hour.ToString().PadLeft(2, '0') + ":" + date.Minute.ToString().PadLeft(2, '0');
            }

            return outStr;
        }
        /// <summary>
        /// 上传图片
        /// </summary>
        /// <param name="request"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string UploadImageToString(System.Web.HttpRequestBase request, string path)
        {
            string result = string.Empty;//返回的上传后的文件名
            if (request.Files.Count > 0)
            {
                var file = request.Files[0];
                try
                {
                    int filelength = file.ContentLength;
                    int fileSize = CommonUnit.UpLoadImageSize;
                    if (filelength <= fileSize)
                    {
                        string name = DateTime.Now.ToString("yyyyMMddHHmmss") + (new Random().Next(100000, 999999)) + Path.GetExtension(file.FileName);

                        file.SaveAs("http://10.10.30.115" + path + name);
                        result = name;
                    }
                    else
                    {
                        result = "-1";
                    }

                }
                catch
                {
                    result = "Error";
                }
            }
            return result;
        }


        #region Md5加密
        /// <summary>
        /// 字符Md5加密
        /// </summary>
        /// <param name="input">string:需要加密的字符串</param>
        /// <returns>string:加密后的字符串</returns>
        public static string GetMd5Hash(string input)
        {
            string txtStr = string.Empty;
            string niXuStr = string.Empty;
            //创建MD5CryptoServiceProvider对象实例.
            MD5 md5Hasher = MD5.Create();
            // 输入字符串转换为一个字节数组并计算哈希
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));

            StringBuilder sBuilder = new StringBuilder();
            // 循环每个哈希数，作为十六进制去匹配
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            sBuilder.Append("RXJY");
            txtStr = sBuilder.ToString();
            for (int i = txtStr.Length - 1; i >= 0; i--)
            {
                niXuStr += txtStr[i];
            }
            // 返回十六进制数组
            return niXuStr;
        }
        // 检测一个输入的数组是否等于已md5加密的数
        /// <summary>
        /// 检测一个输入的数组是否等于已md5加密的数
        /// </summary>
        /// <param name="input">string:需要加密的字符串</param>
        /// <param name="hash">string:加密后的字符串</param>
        /// <returns>bool true false</returns>
        public bool verifyMd5Hash(string input, string hash)
        {
            // 把输入字符串转化为Hash.
            string hashOfInput = GetMd5Hash(input);
            // 创建字符串比较器与已转换过的数据来比较，相等返回0
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;
            if (0 == comparer.Compare(hashOfInput, hash))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion


        #region 绑定生成一个有树结构的下拉菜单select >option
        ///   <summary>
        ///   绑定生成一个有树结构的下拉菜单select >option
        ///   如："<option value='177'>├天津商务部</option>"
        ///   </summary>
        ///   <param   name="dtNodeSets">菜单记录数据所在的表</param>
        ///   <param   name="strParentColumn">表中用于标记父记录的字段</param>
        ///   <param   name="strRootValue">第一层记录的父记录值(通常设计为0或者-1或者Null)用来表示没有父记录</param>
        ///   <param   name="strIndexColumn">索引字段，也就是放在DropDownList的Value里面的字段</param>
        ///   <param   name="strTextColumn">显示文本字段，也就是放在DropDownList的Text里面的字段</param>
        ///   <param   name="drpBind">需要绑定的DropDownList</param>
        ///   <param   name="i">用来控制缩入量的值，请输入-1</param>      
        public static string MakeTree(DataTable dtNodeSets, string strParentColumn, string strRootValue, string strIndexColumn, string strTextColumn, int i)
        {


            StringBuilder sb = new StringBuilder("");

            //每向下一层，多一个缩入单位      
            i++;

            DataView dvNodeSets = new DataView(dtNodeSets);

            dvNodeSets.RowFilter = strParentColumn + "=" + strRootValue;

            string strPading = "";     //缩入字符     

            //通过i来控制缩入字符的长度，我这里设定的是一个全角的空格      
            for (int j = 0; j < i; j++)
                strPading += "　";//如果要增加缩入的长度，改成两个全角的空格就可以了     

            foreach (DataRowView drv in dvNodeSets)
            {
                TreeNode tnNode = new TreeNode();
                //传入 ddl控件
                //ListItem li = new ListItem(strPading + "├" + drv[strTextColumn].ToString(), drv[strIndexColumn].ToString());
                //drpBind.Items.Add(li);

                var selectText = strPading + "├" + drv[strTextColumn].ToString();
                var selectValue = drv[strIndexColumn].ToString();

                //拼接HTML
                sb.Append("<option value='" + selectValue + "'>" + selectText + "</option>");

                var strChild = MakeTree(dtNodeSets, strParentColumn, drv[strIndexColumn].ToString(), strIndexColumn, strTextColumn, i);
                //添加子循环字符串
                sb.Append(strChild);
            }

            //递归结束，要回到上一层，所以缩入量减少一个单位      
            i--;

            return sb.ToString();
        }
        #endregion

        #region 得到各地区的地区编码
        public string GetDiQuCode(string diqu)
        {
            string BMName = "JT";
            switch (diqu)
            {
                case "28":
                    BMName = "CS";
                    break;
                case "35":
                    BMName = "SZ";
                    break;
                case "36":
                    BMName = "NB";
                    break;
                case "16":
                    BMName = "HF";
                    break;
                case "29":
                    BMName = "QD";
                    break;
                case "31":
                    BMName = "SJZ";
                    break;
                case "19":
                    BMName = "TJ";
                    break;
                case "10":
                    BMName = "JT";
                    break;
                case "11":
                    BMName = "R6";
                    break;
                case "12":
                    BMName = "BJ";
                    break;
                case "22":
                    BMName = "CD";
                    break;
                case "21":
                    BMName = "CQ";
                    break;
                case "15":
                    BMName = "NJ";
                    break;
                case "26":
                    BMName = "HZ";
                    break;
                case "18":
                    BMName = "WH";
                    break;
                case "25":
                    BMName = "XA";
                    break;
                case "38":
                    BMName = "SH";
                    break;
                case "30":
                    BMName = "JN";
                    break;
                case "39":
                    BMName = "JT";
                    break;
                default:
                    break;
            }
            return BMName;
        }
        #endregion

    }
}