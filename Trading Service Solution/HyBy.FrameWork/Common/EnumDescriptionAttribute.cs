using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace HyBy.FrameWork.Common
{
    /// <summary>
    /// 版权所有: 版权所有(C) 2013，华跃博弈
    /// 内容摘要: 扩展枚举操作类,举例：
    ///     public enum EnumLogOperateCatalog
    ///     {
    ///         /// <summary>
    ///         /// 添加
    ///         /// </summary>
    ///         [EnumDescription(DefaultDescription = "添加", DefaultValueText = "1")]
    ///         Add,
    ///         /// <summary>
    ///         /// 删除
    ///         /// </summary>
    ///         [EnumDescription(DefaultDescription = "删除", DefaultValueText = "2")]
    ///         Delete
    ///     }
    /// 完成日期：2014-5-16
    /// 版    本：V1.0
    /// 作    者：段帅峰
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class EnumDescriptionAttribute : Attribute
    {
        private string defaultDesc;

        /// <summary>
        /// The default desc
        /// </summary>
        public string DefaultDescription
        {
            get
            {
                return defaultDesc;
            }
            set
            {
                defaultDesc = value;
            }
        }

        private string defaultValue;
        public string DefaultValueText
        {
            get
            {
                return defaultValue;
            }
            set
            {
                defaultValue = value;
            }
        }

        /// <summary>
        /// Get desc of specific enum value
        /// </summary>
        /// <param name="enumValue"></param>
        /// <returns></returns>
        public virtual string GetDescription(object enumValue)
        {
            Check.Require(enumValue != null, "enumValue could not be null.");

            return DefaultDescription ?? enumValue.ToString();
        }

        public virtual string GetValueText(object enumValue)
        {
            Check.Require(enumValue != null, "enumValue could not be null.");

            return DefaultValueText ?? enumValue.ToString();
        }


        /// <summary>
        /// Get desc of specific enum value of specific enum type
        /// </summary>
        /// <param name="enumType"></param>
        /// <param name="enumIntValue"></param>
        /// <returns></returns>
        public static string GetDescription(Type enumType, int enumIntValue)
        {            
            Dictionary<int, string> descs = EnumDescriptionAttribute.GetDescriptions(enumType);
            return descs[enumIntValue];
        }

        public static string GetValueText(Type enumType, int enumIntValue)
        {
            Dictionary<int, string> texts = EnumDescriptionAttribute.GetValueTexts(enumType);
            return texts[enumIntValue];
        }

        /// <summary>
        /// 根据描述取得枚举值
        /// </summary>
        /// <param name="enumType"></param>
        /// <param name="enumIntDesc"></param>
        /// <returns></returns>
        public static int GetEnumByDescription(Type enumType, string enumIntDesc)
        {
            Dictionary<int, string> descs = EnumDescriptionAttribute.GetDescriptions(enumType);
            foreach (var desc in descs)
            {
                if( enumIntDesc.Equals(desc.Value) )
                {
                    return desc.Key;
                }
            }
            return 0;
        }

        /// <summary>
        /// 根据值获取枚举
        /// </summary>
        /// <param name="enumType"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int GetEnumByValue(Type enumType, string value)
        {
            Dictionary<int, string> texts = EnumDescriptionAttribute.GetValueTexts(enumType);
            foreach (var text in texts)
            {
                if( value.Equals(text.Value) )
                {
                    return text.Key;
                }
            }
            return 0;
        }


        public static Dictionary<int, string> GetValueTexts(Type enumType)
        {
            Check.Require(enumType != null && enumType.IsEnum, "enumType must be an enum type.");
            FieldInfo[] fields = enumType.GetFields();
            Dictionary<int, string> texts = new Dictionary<int, string>();
            for (int i = 1; i < fields.Length; ++i)
            {
                object fieldValue = Enum.Parse(enumType, fields[i].Name);
                object[] attrs = fields[i].GetCustomAttributes(true);
                bool findAttr = false;
                foreach (object attr in attrs)
                {
                    if (typeof(EnumDescriptionAttribute).IsAssignableFrom(attr.GetType()))
                    {
                        texts.Add((int)fieldValue, ((EnumDescriptionAttribute)attr).GetValueText(fieldValue));
                        findAttr = true;
                        break;
                    }
                }
                if (!findAttr)
                {
                    texts.Add((int)fieldValue, fieldValue.ToString());
                }
            }
            return texts;
        }

        /// <summary>
        /// Get descs of specific enum type
        /// </summary>
        /// <param name="enumType"></param>
        /// <returns></returns>
        public static Dictionary<int, string> GetDescriptions(Type enumType)
        {
            Check.Require(enumType != null && enumType.IsEnum, "enumType must be an enum type.");

            FieldInfo[] fields = enumType.GetFields();
            Dictionary<int, string> descs = new Dictionary<int, string>();
            for (int i = 1; i < fields.Length; ++i)
            {
                object fieldValue = Enum.Parse(enumType, fields[i].Name);
                object[] attrs = fields[i].GetCustomAttributes(true);
                bool findAttr = false;
                foreach (object attr in attrs)
                {
                    if (typeof(EnumDescriptionAttribute).IsAssignableFrom(attr.GetType()))
                    {
                        descs.Add((int)fieldValue, ((EnumDescriptionAttribute)attr).GetDescription(fieldValue));
                        findAttr = true;
                        break;
                    }
                }
                if (!findAttr)
                {
                    descs.Add((int)fieldValue, fieldValue.ToString());
                }
            }

            return descs;
        }
    }
}