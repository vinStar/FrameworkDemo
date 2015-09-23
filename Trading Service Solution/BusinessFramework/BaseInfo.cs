using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyBy.Trading.BusinessFramework
{
    #region 命名空间引用
    using System;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Configuration;
    using System.Data;
    using System.Data.SqlClient;
    using System.Web;
    using System.Web.UI.WebControls;
    using System.Text;
    using System.Xml;
    using System.Net.Configuration;
    using System.Web.Configuration;
    using System.Web.UI.HtmlControls;
    using CommonUtilities;
    #endregion

    namespace CommonUtilities
    {
        /// <summary>
        /// 用于获取系统的基础配置信息
        /// </summary>    
        public sealed class BaseInfo
        {
            #region 字段定义
            /// <summary>
            /// BS程序中BaseInfo.xml配置文件的相对路径
            /// </summary>
            private const string BaseInfoBSFilePath = @"~\app_data\BaseInfo.xml";
            /// <summary>
            /// CS程序中BaseInfo.xml配置文件的相对路径
            /// </summary>
            private const string BaseInfoCSFilePath = @"~\BaseInfo.xml";
            /// <summary>
            /// XML操作公共类
            /// </summary>
            private static XmlHelper xmlHelper;
            #endregion

            #region 静态构造函数
            static BaseInfo()
            {
                //初始化XML操作公共类
                if (AppType == AppType.BS)
                {
                    xmlHelper = new XmlHelper(BaseInfoBSFilePath);
                }
                else
                {
                    xmlHelper = new XmlHelper(BaseInfoCSFilePath);
                }
            }
            #endregion

            #region 读取Web.config或App.config或BaseInfo.xml文件中<appSettings>配置节中的值

            #region GetAppSection( 读取Web.config或App.config或BaseInfo.xml文件中<appSettings>配置节中的值 )
            /// <summary>
            /// 读取Web.config或App.config或BaseInfo.xml文件中appSettings配置节中的值
            /// </summary>
            /// <param name="key">配置节中的键名</param>        
            public static string GetAppSection(string key)
            {
                //检测Web.config或App.config文件的<appSettings>配置节中是否包含指定键
                if (ContainsAppSettings(key))
                {
                    return ConfigurationManager.AppSettings[key].ToString().Trim();
                }
                else
                {
                    return GetBaseInfoAppSection(key);
                }
            }
            #endregion

            #region GetSystemAppSection( 读取Web.config或App.config文件中<appSettings>配置节中的值 )
            /// <summary>
            /// 读取Web.config或App.config文件中appSettings配置节中的值
            /// </summary>
            /// <param name="key">配置节中的键名</param>        
            public static string GetSystemAppSection(string key)
            {
                //检测Web.config或App.config文件的<appSettings>配置节中是否包含指定键
                if (ContainsAppSettings(key))
                {
                    return ConfigurationManager.AppSettings[key].ToString().Trim();
                }
                else
                {
                    return "";
                }
            }
            #endregion

            #region GetBaseInfoAppSection( 读取BaseInfo.xml文件中<appSettings>配置节中的值 )
            /// <summary>
            /// 读取BaseInfo.xml文件中appSettings配置节中的值
            /// </summary>
            /// <param name="key">键名</param>
            public static string GetBaseInfoAppSection(string key)
            {
                try
                {
                    if (AppType == AppType.BS)
                    {
                        //读取BS程序中BaseInfo.xml文件appSettings配置节中的值
                        return xmlHelper.GetAttributeValue(string.Format(@"appSettings/add[@key='{0}']", key), "value");
                    }
                    else
                    {
                        //读取CS程序中BaseInfo.xml文件appSettings配置节中的值
                        return "";
                    }
                }
                catch
                {
                    return "";
                }
            }
            #endregion

            #endregion

            #region 获取Web.config或App.config文件中<connectionStrings>配置节中的值
            /// <summary>
            /// 获取Web.config或App.config文件中connectionStrings配置节中的值
            /// </summary>
            /// <param name="key">配置节中的键名</param>        
            public static string GetConnectionString(string key)
            {
                //检测Web.config或App.config文件的<ConnectionStrings>配置节中是否包含指定键
                if (ContainsConStrings(key))
                {
                    return ConfigurationManager.ConnectionStrings[key].ToString();
                }
                else
                {
                    return "";
                }
            }
            #endregion

            #region 获取Web.config或App.config配置文件的根节点
            /// <summary>
            /// 获取Web.config或App.config配置文件的根节点
            /// </summary>        
            public static Configuration GetConfiguration()
            {
                try
                {
                    if (AppType == AppType.BS)
                    {
                        return WebConfigurationManager.OpenWebConfiguration("~/web.config");
                    }
                    else
                    {
                        return ConfigurationManager.OpenExeConfiguration(SysHelper.WinFormName + ".exe");
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            #endregion

            #region 检测在Web.config或App.config文件的<appSettings>配置节中是否包含指定键
            /// <summary>
            /// 检测在Web.config或App.config文件的appSettings配置节中是否包含指定键,包含返回true
            /// </summary>
            /// <param name="key">键名</param>
            public static bool ContainsAppSettings(string key)
            {
                try
                {
                    return ValidationHelper.IsNullOrEmpty(ConfigurationManager.AppSettings[key]) ? false : true;
                }
                catch
                {
                    return false;
                }
            }
            #endregion

            #region 检测在Web.config或App.config文件的<ConnectionStrings>配置节中是否包含指定键
            /// <summary>
            /// 检测在Web.config或App.config文件的ConnectionStrings配置节中是否包含指定键,包含返回true
            /// </summary>
            /// <param name="key">键名</param>
            public static bool ContainsConStrings(string key)
            {
                try
                {
                    return ValidationHelper.IsNullOrEmpty(ConfigurationManager.ConnectionStrings[key]) ? false : true;
                }
                catch
                {
                    return false;
                }
            }
            #endregion

            #region 获取当前应用程序的类型
            /// <summary>
            /// 获取当前应用程序的类型,该字符串位于appSettings配置节中。
            /// 键名必须是"AppType",键值只能是"BS"或"CS"。
            /// </summary>
            public static AppType AppType
            {
                get
                {
                    if (!ContainsAppSettings("AppType"))
                    {
                        return AppType.BS;
                    }
                    else
                    {
                        if (GetAppSection("AppType").ToUpper() == "BS")
                        {
                            return AppType.BS;
                        }
                        else
                        {
                            return AppType.CS;
                        }
                    }
                }
            }
            #endregion

            #region 获取默认数据库连接字符串
            /// <summary>
            /// 获取默认数据库连接字符串。
            /// 该字符串位于connectionStrings配置节中,键名必须是"ConStr"或"ConnectionString"。
            /// 该字符串也可位于appsettings配置节中,键名必须是"ConStr"或"ConnectionString"。
            /// </summary>        
            public static string ConnectionString
            {
                get
                {
                    if (ContainsConStrings("ConStr"))
                    {
                        return GetConnectionString("ConStr");
                    }
                    else if (ContainsConStrings("ConnectionString"))
                    {
                        return GetConnectionString("ConnectionString");
                    }
                    else if (ContainsAppSettings("ConStr"))
                    {
                        return GetAppSection("ConStr");
                    }
                    else if (ContainsAppSettings("ConnectionString"))
                    {
                        return GetAppSection("ConnectionString");
                    }
                    else if (!ValidationHelper.IsNullOrEmpty(GetBaseInfoAppSection("ConStr")))
                    {
                        return GetBaseInfoAppSection("ConStr");
                    }
                    else
                    {
                        return GetBaseInfoAppSection("ConnectionString");
                    }
                }
            }
            #endregion

            #region 获取数据库类型
            /// <summary>
            /// 数据库类型，该字符串位于AppSettings配置节中。
            /// 键名必须是"DataBaseType",键值是数据库名称,比如"SqlServer"。
            /// </summary>
            public static DataBaseType DataBaseType
            {
                get
                {
                    if (ValidationHelper.IsNullOrEmpty(GetAppSection("DataBaseType")))
                    {
                        return EnumHelper.GetInstance<DataBaseType>("SqlServer");
                    }
                    else
                    {
                        return EnumHelper.GetInstance<DataBaseType>(GetAppSection("DataBaseType"));
                    }
                }
            }
            #endregion

            #region 获取数据库公共操作类的类名
            /// <summary>
            /// 数据库类型，该字符串位于AppSettings配置节中。
            /// 键名必须是"DBHelperName",键值是数据库名称,比如"SQLHelper"。
            /// </summary>
            public static string DBHelperName
            {
                get
                {
                    if (ValidationHelper.IsNullOrEmpty(GetAppSection("DBHelperName")))
                    {
                        return "SQLHelper";
                    }
                    else
                    {
                        return GetAppSection("DBHelperName");
                    }
                }
            }
            #endregion

            #region 获取帐号被临时锁定的时间间隔
            /// <summary>
            /// 获取帐号被临时锁定的时间间隔，单位为小时。
            /// 键名必须是"AcctDisabledInterval".
            /// </summary>
            public static double AcctDisabledInterval
            {
                get
                {
                    if (ValidationHelper.IsNullOrEmpty(GetAppSection("AcctDisabledInterval")))
                    {
                        return 1;
                    }
                    else
                    {
                        return ConvertHelper.ToDouble(GetAppSection("AcctDisabledInterval"));
                    }
                }
            }
            #endregion

            #region 获取SMTP服务器配置信息
            /// <summary>
            /// 获取发件人的电子邮件地址
            /// </summary>
            public static string SendEmailAddress
            {
                get
                {
                    //创建SMTP配置节
                    SmtpSection smtp = NetSectionGroup.GetSectionGroup(GetConfiguration()).MailSettings.Smtp;

                    return smtp.From;
                }
            }
            #endregion

            #region 获取单个日志文件的大小
            /// <summary>
            /// 获取单个日志文件的大小,单位为k,该字符串位于appSettings配置节中。
            /// 键名必须是"MaxLogSize"
            /// </summary>
            public static double MaxLogSize
            {
                get
                {
                    if (ValidationHelper.IsNullOrEmpty(GetAppSection("MaxLogSize")))
                    {
                        return 100;
                    }
                    else
                    {
                        return ConvertHelper.ToDouble(GetAppSection("MaxLogSize"));
                    }
                }
            }
            #endregion

            #region 获取单个跟踪日志文件最大的行数
            /// <summary>
            /// 获取单个跟踪日志文件最大的行数,该字符串位于appSettings配置节中。
            /// 键名必须是"MaxTraceLogLineCount"
            /// </summary>
            public static int MaxTraceLogLineCount
            {
                get
                {
                    if (ValidationHelper.IsNullOrEmpty(GetAppSection("MaxTraceLogLineCount")))
                    {
                        return 1000;
                    }
                    else
                    {
                        return ConvertHelper.ToInt32(GetAppSection("MaxTraceLogLineCount"));
                    }
                }
            }
            #endregion

            #region 是否启用日志功能
            /// <summary>
            /// 是否启用日志功能,该字符串位于appSettings配置节中。
            /// 键名必须是"EnableLog",可选值为"true"或"false"
            /// </summary>
            public static bool IsEnabledLog
            {
                get
                {
                    if (ValidationHelper.IsNullOrEmpty(GetAppSection("EnableLog")))
                    {
                        return true;
                    }
                    else
                    {
                        return ConvertHelper.ToBoolean(GetAppSection("EnableLog"));
                    }
                }
            }
            #endregion

            #region 获取跟踪日志文件的跟踪等级
            /// <summary>
            /// 获取跟踪日志文件的跟踪等级,该字符串位于appSettings配置节中。
            /// 键名必须是"TraceLogLevel",可选值为"None"或"0"、"Error"或"1"、"Warning"或"2"、"Information"或"3"
            /// </summary>
            public static TraceLogLevel TraceLogLevel
            {
                get
                {
                    //获取跟踪日志文件的跟踪等级
                    string level = GetAppSection("TraceLogLevel");

                    switch (level.Trim().ToUpper())
                    {
                        case "NONE":
                            return TraceLogLevel.None;
                        case "0":
                            return TraceLogLevel.None;
                        case "ERROR":
                            return TraceLogLevel.Error;
                        case "1":
                            return TraceLogLevel.Error;
                        case "WARNING":
                            return TraceLogLevel.Warning;
                        case "2":
                            return TraceLogLevel.Warning;
                        case "INFORMATION":
                            return TraceLogLevel.Information;
                        case "3":
                            return TraceLogLevel.Information;
                        default:
                            return TraceLogLevel.Error;
                    }
                }
            }
            #endregion

            #region 获取登陆业务系统的操作人ID
            /// <summary>
            /// 获取登陆业务系统的操作人ID。
            /// 如果是BS程序，则取Session值,键名为"UserID";
            /// CS程序取值尚未实现,返回""。
            /// </summary>
            public static string UserID
            {
                get
                {
                    if (AppType == AppType.BS)
                    {
                        return WebHelper.GetSession<string>("UserID");
                    }
                    else
                    {
                        return "";
                    }
                }
            }
            #endregion

            #region 获取登陆业务系统的操作人名称
            /// <summary>
            /// 获取登陆业务系统的操作人名称。
            /// 如果是BS程序，则取Session值,键名为"UserName";
            /// CS程序取值尚未实现,返回""。
            /// </summary>
            public static string UserName
            {
                get
                {
                    if (AppType == AppType.BS)
                    {
                        return WebHelper.GetSession<string>("FullName");
                    }
                    else
                    {
                        return "";
                    }
                }
            }
            #endregion

            #region 获取默认字符编码
            /// <summary>
            /// 获取默认字符编码
            /// </summary>
            public static Encoding DefaultEncoding
            {
                get
                {
                    if (ValidationHelper.IsNullOrEmpty(GetAppSection("DefaultEncoding")))
                    {
                        return Encoding.UTF8;
                    }
                    else
                    {
                        return Encoding.GetEncoding(GetAppSection("DefaultEncoding"));
                    }
                }
            }
            #endregion

            #region 获取FusionChart图表控件存放的相对路径
            /// <summary>
            /// 获取FusionChart图表控件存放的相对路径
            /// </summary>
            public static string FusionChartPath
            {
                get
                {
                    if (ValidationHelper.IsNullOrEmpty(GetAppSection("FusionChartPath")))
                    {
                        return @"App_Themes/FusionCharts";
                    }
                    else
                    {
                        return GetAppSection("FusionChartPath");
                    }
                }
            }
            #endregion
        }
    }
}