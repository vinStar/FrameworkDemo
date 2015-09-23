/**********************************************************************************************************************
 * 
 *  版权所有：(c)2014， 华跃博弈有限公司
 * 
 * ********************************************************************************************************************/

using System;
using System.Security.Cryptography;
using System.Text;
using System.Globalization;

namespace HyBy.Trading.BusinessFramework
{
    /// <summary>
    /// 用于处理密码。
    /// </summary>
    public class Salt
    {
        private const int saltLength = 5;

        public Salt()
        { }

        public static byte[] CreateSHA1Password(string Password)
        {
            SHA1 sha1 = SHA1.Create();
            byte[] password = sha1.ComputeHash(Encoding.Unicode.GetBytes(Password));
            return password;
        }
        /// <summary>
        /// 对密码进行处理，以存储到数据库中。
        /// </summary>
        /// <param name="Password">明文的密码</param>
        /// <returns>处理过的密码</returns>
        public static string CreateDbPassword(string Password)
        {
            return BitConverter.ToString(CreateBinDbPassword(Password));
        }

        /// <summary>
        /// 对密码进行处理，以存储到数据库中。
        /// </summary>
        /// <param name="password">明文的密码</param>
        /// <returns>处理过的密码</returns>
        public static byte[] CreateBinDbPassword(string password)
        {
            byte[] unsaltedPassword = CreateSHA1Password(password);
            //Create a salt value.
            byte[] saltValue = new byte[saltLength];
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            rng.GetBytes(saltValue);

            return CreateSaltedPassword(saltValue, unsaltedPassword);
        }
        /// <summary>
        /// 密码验证。
        /// </summary>
        /// <param name="dbPassword">数据库存储的处理过的密码</param>
        /// <param name="Password">输入的明文密码</param>
        /// <returns>是否通过验证</returns>
        public static bool ValidatePassword(string dbPassword, string Password)
        {
            if (string.IsNullOrEmpty(dbPassword) || string.IsNullOrEmpty(Password))
                return false;
            string[] strdbPW = dbPassword.Split('-');
            byte[] bytedbPW = new byte[strdbPW.Length];
            for (int i = 0; i < strdbPW.Length; i++)
            {
                bytedbPW[i] = Byte.Parse(strdbPW[i], NumberStyles.HexNumber);
            }
            return ValidatePassword(bytedbPW, Password);
        }
        /// <summary>
        /// 密码验证。
        /// </summary>
        /// <param name="dbPassword">数据库存储的处理过的密码</param>
        /// <param name="password">输入的明文密码</param>
        /// <returns>是否通过验证</returns>
        public static bool ValidatePassword(byte[] dbPassword, string password)
        {
            return ComparePasswords(dbPassword, password);
        }
        // Compare the contents of two byte arrays.
        private static bool CompareByteArray(byte[] array1, byte[] array2)
        {
            if (array1.Length != array2.Length)
            {
                return false;
            }
            for (int i = 0; i < array1.Length; i++)
            {
                if (array1[i] != array2[i])
                {
                    return false;
                }
            }
            return true;
        }
        private static bool ComparePasswords(byte[] storedPassword, byte[] hashedPassword)
        {
            if (storedPassword == null || hashedPassword == null || hashedPassword.Length != storedPassword.Length - saltLength)
            {
                return false;
            }

            // Get the saved saltValue.
            byte[] saltValue = new byte[saltLength];
            int saltOffset = storedPassword.Length - saltLength;
            for (int i = 0; i < saltLength; i++)
            {
                saltValue[i] = storedPassword[saltOffset + i];
            }

            byte[] saltedPassword = CreateSaltedPassword(saltValue, hashedPassword);

            // Compare the values.
            return CompareByteArray(storedPassword, saltedPassword);
        }
        private static bool ComparePasswords(byte[] dbPassword, string Password)
        {
            SHA1 sha1 = SHA1.Create();
            byte[] password = sha1.ComputeHash(Encoding.Unicode.GetBytes(Password));

            if (ComparePasswords(dbPassword, password))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        // Create a salted password given the salt value.
        private static byte[] CreateSaltedPassword(byte[] saltValue, byte[] unsaltedPassword)
        {
            // Add the salt to the hash.
            byte[] rawSalted = new byte[unsaltedPassword.Length + saltValue.Length];
            unsaltedPassword.CopyTo(rawSalted, 0);
            saltValue.CopyTo(rawSalted, unsaltedPassword.Length);

            //Create the salted hash.         
            SHA1 sha1 = SHA1.Create();
            byte[] saltedPassword = sha1.ComputeHash(rawSalted);

            // Add the salt value to the salted hash.
            byte[] dbPassword = new byte[saltedPassword.Length + saltValue.Length];
            saltedPassword.CopyTo(dbPassword, 0);
            saltValue.CopyTo(dbPassword, saltedPassword.Length);

            return dbPassword;
        }
    }
}
