using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace HyBy.FrameWork.Common.DEncrypt
{
    /// <summary>
    /// MD5加密验证类
    /// 作者：罗广
    /// </summary>
    public class MD5Encrypt
    {
        #region Md5加密,用于密码加密和判断
        /// <summary>
        /// 字符Md5加密
        /// </summary>
        /// <param name="input">string:需要加密的字符串</param>
        /// <returns>string:加密后的字符串</returns>
        public string GetMd5Hash(string input)
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
        public bool VerifyMd5Hash(string input, string hash)
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
    }
}
