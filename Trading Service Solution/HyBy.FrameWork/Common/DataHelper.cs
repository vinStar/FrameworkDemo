using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace HyBy.FrameWork.Common
{
    /// <summary>
    /// 版权所有: 版权所有(C) 2013，华跃博弈
    /// 内容摘要: 对系统中得到的数据或者数据集进行处理
    /// 完成日期：2014-5-16
    /// 版    本：V1.0
    /// 作    者：龙元元
    /// </summary>
    public class DataHelper
    {
        #region 判断DataSet,DataTable是否有数据
        /// <summary>
        /// 判断DataSet是否有数据
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public Boolean IsHaveData(DataSet ds)
        {
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 判断DataTable是否有数据
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public Boolean IsHaveData(DataTable dt)
        {
            if (dt != null && dt.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region 数据库取出的转换对应的字符格式
        /// <summary>
        /// DataTable数据(DBNull)空值转换
        /// </summary>
        /// <param name="obj">数据值</param>
        /// <param name="type">类型：1string,2int,3DateTime,4double</param>
        /// <returns></returns>
        public object SetInitData(object obj, int type)
        {
            object returnValue = obj;
            if (obj == null || string.IsNullOrEmpty(obj.ToString()))
            {
                switch (type)
                {
                    case 1:
                        returnValue = "无";
                        break;
                    case 2:
                        returnValue = 0;
                        break;
                    case 3:
                        returnValue = DateTime.MinValue;
                        break;
                    case 4:
                        returnValue = 0;
                        break;
                }
            }
            return returnValue;
        }
        /// <summary>
        /// DataTable数据(DBNull)空值转换
        /// </summary>
        /// <param name="obj">数据值</param>
        /// <param name="type">类型：1string,2int,3DateTime,4double</param>
        /// <param name="value">obj为空时的默认值</param>
        /// <returns></returns>
        public object SetInitData(object obj, int type, object value)
        {
            object returnValue = obj;
            if (obj == null || string.IsNullOrEmpty(obj.ToString()))
            {
                if (value != null)
                {
                    returnValue = value;
                }
                else
                {
                    switch (type)
                    {
                        case 1:
                            returnValue = "无";
                            break;
                        case 2:
                            returnValue = 0;
                            break;
                        case 3:
                            returnValue = DateTime.MinValue;
                            break;
                        case 4:
                            returnValue = 0;
                            break;
                    }
                }
            }
            return returnValue;
        }
        #endregion
    }
}
