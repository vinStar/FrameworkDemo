namespace HyBy.FrameWork.Common
{
    using System;
    using System.Data;
    using System.IO;
    using System.Security.Cryptography;
    using System.Text;
    using System.Xml;

    public class Util
    {
        public static DataSet CDataSet(string xData)
        {
            DataSet set2;
            xData = xData.Replace("﻿<NewDataSet>", "<NewDataSet>");
            if (string.IsNullOrEmpty(xData))
            {
                return null;
            }
            DataSet set = new DataSet();
            using (StringReader reader = new StringReader(xData))
            {
                using (XmlTextReader reader2 = new XmlTextReader(reader))
                {
                    try
                    {
                        set.ReadXml(reader2);
                        return set;
                    }
                    catch (Exception exception)
                    {
                        string message = exception.Message;
                        return null;
                    }
                    finally
                    {
                        if (reader2 != null)
                        {
                            reader2.Close();
                        }
                    }
                }
            }
            return set2;
        }

        public static string DecrpyHttpGetParams(string strEncryptParams)
        {
            return DecrpyHttpGetParams(strEncryptParams, true);
        }

        public static string DecrpyHttpGetParams(string strEncryptParams, bool bWebMethod)
        {
            if ((strEncryptParams == null) || (strEncryptParams == string.Empty))
            {
                return string.Empty;
            }
            if (bWebMethod)
            {
                strEncryptParams = strEncryptParams.Replace(" ", "+");
            }
            byte[] bytes = Encoding.UTF8.GetBytes("orionosms".PadRight(8, '0').Substring(0, 8));
            byte[] rgbIV = new byte[] { 0, 0x10, 0x11, 1, 0, 0x10, 0x11, 1 };
            byte[] buffer = Convert.FromBase64String(strEncryptParams);
            DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
            MemoryStream stream = new MemoryStream();
            CryptoStream stream2 = new CryptoStream(stream, provider.CreateDecryptor(bytes, rgbIV), CryptoStreamMode.Write);
            stream2.Write(buffer, 0, buffer.Length);
            stream2.FlushFinalBlock();
            string str = new UTF8Encoding().GetString(stream.ToArray());
            stream2.Close();
            return str;
        }

        public static string DToSer(DataTable DS)
        {
            string str;
            if (DS == null)
            {
                return string.Empty;
            }
            if (DS.Rows.Count == 0)
            {
                return string.Empty;
            }
            SetDefaultValueToDataTable(DS);
            using (MemoryStream stream = new MemoryStream())
            {
                using (XmlTextWriter writer = new XmlTextWriter(stream, Encoding.Unicode))
                {
                    try
                    {
                        DS.WriteXml(writer);
                        int length = (int) stream.Length;
                        byte[] buffer = new byte[length];
                        stream.Seek(0L, SeekOrigin.Begin);
                        stream.Read(buffer, 0, length);
                        UnicodeEncoding encoding = new UnicodeEncoding();
                        return encoding.GetString(buffer).Trim();
                    }
                    catch
                    {
                        return string.Empty;
                    }
                    finally
                    {
                        if (writer != null)
                        {
                            writer.Close();
                        }
                    }
                }
            }
            return str;
        }

        public static string EncrpyHttpGetParams(string strNoneEncryptParams)
        {
            if ((strNoneEncryptParams == null) || (strNoneEncryptParams == string.Empty))
            {
                return string.Empty;
            }
            byte[] bytes = Encoding.UTF8.GetBytes("orionosms".PadRight(8, '0').Substring(0, 8));
            byte[] rgbIV = new byte[] { 0, 0x10, 0x11, 1, 0, 0x10, 0x11, 1 };
            DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
            byte[] buffer = Encoding.UTF8.GetBytes(strNoneEncryptParams);
            MemoryStream stream = new MemoryStream();
            CryptoStream stream2 = new CryptoStream(stream, provider.CreateEncryptor(bytes, rgbIV), CryptoStreamMode.Write);
            stream2.Write(buffer, 0, buffer.Length);
            stream2.FlushFinalBlock();
            string str = Convert.ToBase64String(stream.ToArray());
            stream2.Close();
            return str;
        }

        public static void SetDefaultValueToDataTable(DataTable dt)
        {
            int num = 0;
            int count = dt.Rows.Count;
            while (num < count)
            {
                foreach (DataColumn column in dt.Columns)
                {
                    if (dt.Rows[num][column] == DBNull.Value)
                    {
                        switch (column.DataType.ToString())
                        {
                            case "System.Int16":
                            {
                                column.ReadOnly = false;
                                dt.Rows[num][column] = -1;
                                continue;
                            }
                            case "System.Int32":
                            {
                                column.ReadOnly = false;
                                dt.Rows[num][column] = -1;
                                continue;
                            }
                            case "System.Int64":
                            {
                                column.ReadOnly = false;
                                dt.Rows[num][column] = -1;
                                continue;
                            }
                            case "System.Double":
                            {
                                column.ReadOnly = false;
                                dt.Rows[num][column] = -1;
                                continue;
                            }
                            case "System.Decimal":
                            {
                                column.ReadOnly = false;
                                dt.Rows[num][column] = -1;
                                continue;
                            }
                            case "System.UInt16":
                            {
                                column.ReadOnly = false;
                                dt.Rows[num][column] = -1;
                                continue;
                            }
                            case "System.UInt32":
                            {
                                column.ReadOnly = false;
                                dt.Rows[num][column] = -1;
                                continue;
                            }
                            case "System.UInt64":
                            {
                                column.ReadOnly = false;
                                dt.Rows[num][column] = -1;
                                continue;
                            }
                            case "System.Boolean":
                            case "System.Byte":
                            case "System.TimeSpan":
                            {
                                continue;
                            }
                            case "System.DateTime":
                            {
                                column.ReadOnly = false;
                                dt.Rows[num][column] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                                continue;
                            }
                        }
                        column.ReadOnly = false;
                        dt.Rows[num][column] = "";
                    }
                }
                num++;
            }
        }
    }
}

