using System;
using System.Configuration;

namespace HyBy.FrameWork.Common
{
    /// <summary>
    /// ��Ȩ����: ��Ȩ����(C) 2013����Ծ����
    /// ����ժҪ: web.config������
    /// ������ڣ�2014-5-16
    /// ��    ����V1.0
    /// ��    �ߣ���˧��
    /// </summary>
	public sealed class ConfigHelper
	{
		/// <summary>
		/// �õ�AppSettings�е������ַ�����Ϣ
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public static string GetConfigString(string key)
		{
            string strValue = string.Empty;

            try
            {
                strValue = ConfigurationManager.AppSettings.Get(key);
            }
            catch
            {
            }
            return strValue;
		}

		/// <summary>
		/// �õ�AppSettings�е�����Bool��Ϣ
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public static bool GetConfigBool(string key)
		{
			bool result = false;
			string cfgVal = GetConfigString(key);
			if(null != cfgVal && string.Empty != cfgVal)
			{
				try
				{
					result = bool.Parse(cfgVal);
				}
				catch(FormatException)
				{
					// Ignore format exceptions.
				}
			}
			return result;
		}
		/// <summary>
		/// �õ�AppSettings�е�����Decimal��Ϣ
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public static decimal GetConfigDecimal(string key)
		{
			decimal result = 0;
			string cfgVal = GetConfigString(key);
			if(null != cfgVal && string.Empty != cfgVal)
			{
				try
				{
					result = decimal.Parse(cfgVal);
				}
				catch(FormatException)
				{
					// Ignore format exceptions.
				}
			}

			return result;
		}
		/// <summary>
		/// �õ�AppSettings�е�����int��Ϣ
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public static int GetConfigInt(string key)
		{
			int result = 0;
			string cfgVal = GetConfigString(key);
			if(null != cfgVal && string.Empty != cfgVal)
			{
				try
				{
					result = int.Parse(cfgVal);
				}
				catch(FormatException)
				{
					// Ignore format exceptions.
				}
			}

			return result;
		}
	}
}
