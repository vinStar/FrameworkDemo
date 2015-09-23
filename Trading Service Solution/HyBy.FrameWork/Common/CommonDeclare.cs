using System;
using System.Collections;
using System.Web;
using System.ComponentModel;

namespace HyBy.FrameWork.Common
{
    /// <summary>
    /// 版权所有: 版权所有(C) 2013，华跃博弈
    /// 内容摘要: 公用的枚举、常量声明类
    /// 完成日期：2014-5-16
    /// 版    本：V1.0
    /// 作    者：段帅峰
    /// </summary>
    [Serializable]
    public class CommonDeclare
    {
        #region 日志操作分类
        /// <summary>
        /// 日志操作分类
        /// </summary>
        public enum EnumLogOperateCatalog
        {
            /// <summary>
            /// 添加
            /// </summary>
            [EnumDescription(DefaultDescription = "添加", DefaultValueText = "1")]
            Add,
            /// <summary>
            /// 删除
            /// </summary>
            [EnumDescription(DefaultDescription = "删除", DefaultValueText = "2")]
            Delete,
            /// <summary>
            /// 修改
            /// </summary>
            [EnumDescription(DefaultDescription = "修改", DefaultValueText = "3")]
            Modify,
            /// <summary>
            /// 查找
            /// </summary>
            [EnumDescription(DefaultDescription = "查找", DefaultValueText = "4")]
            Find
        }
        #endregion

        #region 月份
        public enum Month
        {
            /// <summary>
            /// 一月
            /// </summary>
            [EnumDescription(DefaultDescription = "一月", DefaultValueText = "1")]
            Jan,
            /// <summary>
            /// 二月
            /// </summary>
            [EnumDescription(DefaultDescription = "二月", DefaultValueText = "2")]
            Feb,
            /// <summary>
            /// 三月
            /// </summary>
            [EnumDescription(DefaultDescription = "三月", DefaultValueText = "3")]
            Mar,
            /// <summary>
            /// 四月
            /// </summary>
            [EnumDescription(DefaultDescription = "四月", DefaultValueText = "4")]
            Apr,
            /// <summary>
            /// 五月
            /// </summary>
            [EnumDescription(DefaultDescription = "五月", DefaultValueText = "5")]
            May,
            /// <summary>
            /// 六月
            /// </summary>
            [EnumDescription(DefaultDescription = "六月", DefaultValueText = "6")]
            Jun,
            /// <summary>
            /// 七月
            /// </summary>
            [EnumDescription(DefaultDescription = "七月", DefaultValueText = "7")]
            Jul,
            /// <summary>
            /// 八月
            /// </summary>
            [EnumDescription(DefaultDescription = "八月", DefaultValueText = "8")]
            Aug,
            /// <summary>
            /// 九月
            /// </summary>
            [EnumDescription(DefaultDescription = "九月", DefaultValueText = "9")]
            Sep,
            /// <summary>
            /// 十月
            /// </summary>
            [EnumDescription(DefaultDescription = "十月", DefaultValueText = "10")]
            Oct,
            /// <summary>
            /// 十一月
            /// </summary>
            [EnumDescription(DefaultDescription = "十一月", DefaultValueText = "11")]
            Nov,
            /// <summary>
            /// 十二月
            /// </summary>
            [EnumDescription(DefaultDescription = "十二月", DefaultValueText = "12")]
            Dec
        }
        #endregion

        #region 异常级别
        /// <summary>
        /// 异常级别
        /// </summary>
        [Serializable]
        public enum EnumExceptionLevel
        {
            /// <summary>
            /// 未知
            /// </summary>
            UNKNOWN = 0,
            /// <summary>
            /// 致命性错误，如NCache/数据库挂掉，影响到所有用户
            /// </summary>
            FAULT = 1,
            /// <summary>
            /// 严重错误，有错误发生，但是只影响到操作的用户本身，对其他用户不影响
            /// </summary>
            ERROR = 2,
            /// <summary>
            /// 一般性错误，有错误发生，但是可恢复，用户重试操作即可
            /// </summary>
            WARNING = 3,
            /// <summary>
            /// 一般性信息，有错误发生，但是不影响用户使用
            /// </summary>
            INFO = 4,
            /// <summary>
            /// 调试信息。没有错误发生，只是加入用来判别程序执行路径
            /// </summary>
            DEBUG = 99
        }

        #endregion

        #region 性别
        /// <summary>
        /// 性别
        /// </summary>
        public enum EnumGender
        {
            /// <summary>
            /// 男
            /// </summary>
            [EnumDescription(DefaultDescription = "男", DefaultValueText = "1")]
            Male,
            /// <summary>
            /// 女
            /// </summary>
            [EnumDescription(DefaultDescription = "女", DefaultValueText = "2")]
            Female,
            /// <summary>
            /// 保密
            /// </summary>
            [EnumDescription(DefaultDescription = "保密", DefaultValueText = "3")]
            Unknown
        }
        #endregion

        #region 配置文件名称
        /// <summary>
        /// 配置文件名称
        /// </summary>
        public const string ConfigurationFileName = "CommonSystem.config";
        #endregion

        #region 操作类型
        /// <summary>
        /// 操作类型
        /// <remarks>
        ///     操作类型分为两类：
        ///     编辑状态和非编辑状态
        /// </remarks>
        /// </summary>
        public enum CommonEditType
        {
            Unknown,         ///未知操作
            Editing,        ///编辑状态
            NonEditing,      ///非编辑状态
            NonAdding,          ///不需要添加
            NonDeleting     ///不需要删除
        }
        #endregion

        #region 列显示方式枚举
        public enum CommonColumnShowStyle
        {
            /// <summary>
            /// 未知
            /// </summary>
            Unknown,
            /// <summary>
            /// 显示在普通状态
            /// </summary>    
            ShowInNormal,
            /// <summary>
            /// 显示在Web页中
            /// </summary>
            ShowInWeb,
            /// <summary>
            /// 显示在用户客户端
            /// </summary>
            ShowInUserClient,
            /// <summary>
            /// 显示在便携设备
            /// </summary>
            ShowInMobile
        }
        #endregion

        #region 证件类型
        public enum EnumCredentType
        {

            /// <summary>
            /// 身份证
            /// </summary>
            [EnumDescription(DefaultDescription = "身份证", DefaultValueText = "1")]
            IDCard,
            /// <summary>
            /// 签证
            /// </summary>
            [EnumDescription(DefaultDescription = "港澳通行证", DefaultValueText = "2")]
            Visa,
            /// <summary>
            /// 护照
            /// </summary>
            [EnumDescription(DefaultDescription = "护照", DefaultValueText = "3")]
            Passport,
            /// <summary>
            /// 未知
            /// </summary>
            [EnumDescription(DefaultDescription = "未知", DefaultValueText = "4")]
            Unknown
        }
        #endregion

        #region 周期类别
        public enum EnumDurationType
        {
            /// <summary>
            /// 未知
            /// </summary>
            [EnumDescription(DefaultDescription = "未知", DefaultValueText = "0")]
            Unknown,
            /// <summary>
            /// 小时
            /// </summary>
            [EnumDescription(DefaultDescription = "小时", DefaultValueText = "1")]
            Hour,
            /// <summary>
            /// 天
            /// </summary>
            [EnumDescription(DefaultDescription = "天", DefaultValueText = "2")]
            Day,
            /// <summary>
            /// 周
            /// </summary>
            [EnumDescription(DefaultDescription = "周", DefaultValueText = "3")]
            Week,
            /// <summary>
            /// 月
            /// </summary>
            [EnumDescription(DefaultDescription = "月", DefaultValueText = "4")]
            Month,
            /// <summary>
            /// 年
            /// </summary>
            [EnumDescription(DefaultDescription = "年", DefaultValueText = "5")]
            Year

        }
        #endregion
    }
}
