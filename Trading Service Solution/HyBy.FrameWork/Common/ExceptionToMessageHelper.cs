using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.ServiceModel;

namespace HyBy.FrameWork.Common
{
    /// <summary>
    /// ��Ȩ����: ��Ȩ����(C) 2013����Ծ����
    /// ����ժҪ: ������Ϣ������
    /// ������ڣ�2014-5-16
    /// ��    ����V1.0
    /// ��    �ߣ���˧��
    /// </summary>
    public class ExceptionToMessageHelper
    {
        /// <summary>
        /// ��Exception��Ϣת��Ϊstring��ʾ
        /// </summary>
        /// <param name="ex"></param>
        public static void ToString(Exception ex, ref string message)
        {
            string targetSite = string.Empty;
            if (ex.TargetSite != null)
                targetSite = ex.TargetSite.ReflectedType.FullName;
            message += "\tTargetSite:" + targetSite + "\r\n\tHelpLink: " + ex.HelpLink + "\r\n\tSource: " + ex.Source + "\r\n\tMessage: " + ex.Message + "\r\n\tTraceStack: " + ex.StackTrace + "\r\n";

            if (ex.InnerException != null)
                ToString(ex.InnerException, ref message);

            if (ex is FaultException)
            {
                FaultException<string> fex = ex as FaultException<string>;
                message += "��̨�쳣��\r\n\t";
                message += fex.Detail;
            }

        }

        #region log��־
 
        /// <summary>
        /// д��־
        /// </summary>
        /// <param name="ex">�쳣��Ϣ</param>
        public static void WriteLog(Exception ex)
        {
            try
            {
                WriteLog(ex, string.Empty);
            }
            catch
            {
                //д��־�������ǲ��׳��쳣��
            }
        }

        /// <summary>
        /// д��־
        /// </summary>
        /// <param name="message">��־����</param>
        public static void WriteLog(string message)
        {
            try
            {
                WriteLog(null, message);
            }
            catch
            {
                //д��־�������ǲ��׳��쳣��
            }
        }

        /// <summary>
        /// д��־:������IsSaveLog��SysLogSavePath��
        /// </summary>
        /// <param name="ex">�쳣��Ϣ</param>
        /// <param name="log">��־��Ϣ</param>
        public static void WriteLog(Exception ex, string log)
        {
            try
            {
                bool saveLog = (ConfigHelper.GetConfigString("IsSaveLog") != null ? ConfigHelper.GetConfigString("IsSaveLog") == "yes" : false);
                ///�Ƿ��¼
                if (!saveLog)
                    return;

                string path = ConfigHelper.GetConfigString("SysLogSavePath");
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                string filename = DateTime.Now.ToString("yyyyMMddHH") + ".txt";

                FileInfo logFile = new FileInfo(path + "\\" + filename);
                if (logFile.Exists && logFile.Length > 1048576)
                {
                    DirectoryInfo dir = new DirectoryInfo(path);
                    FileInfo[] files = dir.GetFiles(DateTime.Now.ToString("yyyyMMddHH") + "*");
                    File.Copy(path + "\\" + filename, path + "\\" + DateTime.Now.ToString("yyyyMMddHH") + "_" + files.Length + ".txt");
                    logFile.Delete();
                }
                DateTime exceptionTime = DateTime.Now;

                using (FileStream stream = new FileStream(path + "\\" + filename, FileMode.Append, FileAccess.Write, FileShare.Write, 4096, false))
                {
                    string message = string.Empty;
                    if (ex != null)
                    {
                        ToString(ex, ref message);
                    }
                    //if (ex is FaultException)
                    //{
                    //    FaultException faultException = ex as FaultException;
                    //    message = "\r\nError Code: " + faultException.Code.Name + "\r\n"
                    //        + "Error Level: " + faultException.Action + "\r\n"
                    //        + message;
                    //}
                    message = log + "\r\n" + message;

                    StreamWriter writer = new StreamWriter(stream);

                    writer.WriteLine("Begin --------------------------------------------" + exceptionTime.ToLongDateString() + "  " + exceptionTime.ToLongTimeString() + "  --------------------------------------------\r\n");
                    writer.WriteLine("{0}:{1}\r\n\t{2}", DateTime.Now.ToLongTimeString(), DateTime.Now.Millisecond, message);
                    writer.WriteLine("End   --------------------------------------------" + exceptionTime.ToLongDateString() + "  " + exceptionTime.ToLongTimeString() + "  --------------------------------------------\r\n\r\n\r\n");
                    writer.Flush();
                    stream.Flush();
                    writer.Close();
                    stream.Close();
                }
            }
            catch
            {
                //д��־�������ǲ��׳��쳣��
            }
        }
        
        #endregion

    }
}
