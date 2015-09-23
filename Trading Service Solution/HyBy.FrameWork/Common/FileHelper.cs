using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Web;
using System.Collections;

namespace HyBy.FrameWork.Common
{
    /// <summary>
    /// 版权所有: 版权所有(C) 2013，华跃博弈
    /// 内容摘要: 文件操作类，读写文件，删除文件，拷贝移动，创建目录，下载文件等
    /// 完成日期：2014-5-16
    /// 版    本：V1.0
    /// 作    者：段帅峰
    /// </summary>
    public class FileHelper
    {
        #region 下载文件
        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="sourceFile">下载文件完整路径</param>
        /// <param name="displayName">显示名称，可以不传入，默认为原文件名</param>
        public static void Download(string sourceFile, string displayName = "")
        {
            try
            {
                if (File.Exists(sourceFile))
                {
                    if (displayName == "")
                    {
                        displayName = Path.GetFileName(sourceFile);
                    }
                    FileInfo DownloadFile = new FileInfo(sourceFile);

                    HttpContext.Current.Response.Clear();
                    HttpContext.Current.Response.ClearHeaders();
                    HttpContext.Current.Response.ContentType = "application/octet-stream";
                    HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(displayName, System.Text.Encoding.UTF8));
                    HttpContext.Current.Response.AppendHeader("Content-Length", DownloadFile.Length.ToString());
                    HttpContext.Current.Response.TransmitFile(sourceFile);
                    if (HttpContext.Current.Response.IsClientConnected)
                    {
                        HttpContext.Current.Response.Flush();
                    }
                    HttpContext.Current.Response.Clear();
                    HttpContext.Current.ApplicationInstance.CompleteRequest();
                }

            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }


        public static void BulkDownload(string[] paths,string downloadPath)
        {

            foreach (string item in paths)
            {
                string filePath = downloadPath + @"\" + item;
                Download(filePath);
            }
        }
        #endregion

        #region 取得站点的物理路径
        public static string GetAppPath()
        {
            return HttpContext.Current.Request.PhysicalApplicationPath;
        }
        #endregion

        #region 取得文件后缀名
        public static string GetFilefixStr(string sourceFile)
        {
            return Path.GetExtension(sourceFile).ToLower();
        }
        #endregion

        #region 创建文件
        public static void CreateFile(string sourceFile)
        {
            if (!Directory.Exists(Path.GetDirectoryName(sourceFile)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(sourceFile));
            }
            File.Create(sourceFile);
        }
        #endregion

        #region 写文件
        public static void WriteFile(string sourceFile, string Strings)
        {
            if (!File.Exists(sourceFile))
            {
                FileStream f = File.Create(sourceFile);
                f.Close();
            }
            StreamWriter f2 = new StreamWriter(sourceFile, false, System.Text.Encoding.GetEncoding("gb2312"));
            f2.Write(Strings);
            f2.Close();
            f2.Dispose();
        }
        #endregion

        #region 读文件
        public static string ReadFile(string sourceFile)
        {
            string s = "";
            if (!File.Exists(sourceFile))
                s = "不存在相应的目录";
            else
            {
                StreamReader f2 = new StreamReader(sourceFile, System.Text.Encoding.GetEncoding("gb2312"));
                s = f2.ReadToEnd();
                f2.Close();
                f2.Dispose();
            }

            return s;
        }
        #endregion

        #region 追加文件
        /// <summary>
        /// 追加文件
        /// </summary>
        /// <param name="Path">文件路径</param>
        /// <param name="strings">内容</param>
        public static void FileAdd(string sourceFile, string strings)
        {
            StreamWriter sw = File.AppendText(sourceFile);
            sw.Write(strings);
            sw.Flush();
            sw.Close();
        }
        #endregion

        #region 拷贝文件
        /// <summary>
        /// 拷贝文件
        /// </summary>
        /// <param name="OrignFile">原始文件</param>
        /// <param name="NewFile">新文件路径</param>
        public static void FileCoppy(string orignFile, string NewFile)
        {
            File.Copy(orignFile, NewFile, true);
        }

        #endregion

        #region 删除文件
        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="Path">路径</param>
        public static bool FileDel(string sourceFile)
        {
            bool flag = true;
            try
            {
                if (File.Exists(sourceFile))
                {
                    File.Delete(sourceFile);
                }
            }
            catch(Exception ex)
            {
                ExceptionToMessageHelper.WriteLog(ex);
                flag = false;
            }
            return flag;
        }
        #endregion

        #region 移动文件
        /// <summary>
        /// 移动文件
        /// </summary>
        /// <param name="OrignFile">原始路径</param>
        /// <param name="NewFile">新路径</param>
        public static void FileMove(string orignFile, string NewFile)
        {
            File.Move(orignFile, NewFile);
        }
        #endregion

        #region 在当前目录下创建目录
        /// <summary>
        /// 在当前目录下创建目录
        /// </summary>
        /// <param name="OrignFolder">当前目录</param>
        /// <param name="NewFloder">新目录</param>
        public static void FolderCreate(string orignFolder, string NewFloder)
        {
            Directory.SetCurrentDirectory(orignFolder);
            Directory.CreateDirectory(NewFloder);
        }
        #endregion

        #region 递归删除文件夹目录及文件
        /// <summary>
        /// 递归删除文件夹目录及文件
        /// </summary>
        /// <param name="dir"></param>
        /// <returns></returns>
        public static void DeleteFolder(string dir)
        {
            if (Directory.Exists(dir)) //如果存在这个文件夹删除之
            {
                foreach (string d in Directory.GetFileSystemEntries(dir))
                {
                    if (File.Exists(d))
                        File.Delete(d); //直接删除其中的文件
                    else
                        DeleteFolder(d); //递归删除子文件夹
                }
                Directory.Delete(dir); //删除已空文件夹
            }

        }

        #endregion

        #region 将指定文件夹下面的所有内容copy到目标文件夹下面。
        /// <summary>
        /// 指定文件夹下面的所有内容copy到目标文件夹下面
        /// </summary>
        /// <param name="srcPath">原始路径</param>
        /// <param name="aimPath">目标文件夹</param>
        public static void CopyDir(string srcPath, string aimPath)
        {
            try
            {
                // 检查目标目录是否以目录分割字符结束如果不是则添加之
                if (aimPath[aimPath.Length - 1] != Path.DirectorySeparatorChar)
                    aimPath += Path.DirectorySeparatorChar;
                // 判断目标目录是否存在如果不存在则新建之
                if (!Directory.Exists(aimPath))
                    Directory.CreateDirectory(aimPath);
                // 得到源目录的文件列表，该里面是包含文件以及目录路径的一个数组
                //如果你指向copy目标文件下面的文件而不包含目录请使用下面的方法
                //string[] fileList = Directory.GetFiles(srcPath);
                string[] fileList = Directory.GetFileSystemEntries(srcPath);
                //遍历所有的文件和目录
                foreach (string file in fileList)
                {
                    //先当作目录处理如果存在这个目录就递归Copy该目录下面的文件

                    if (Directory.Exists(file))
                        CopyDir(file, aimPath + Path.GetFileName(file));
                    //否则直接Copy文件
                    else
                        File.Copy(file, aimPath + Path.GetFileName(file), true);
                }

            }
            catch (Exception ee)
            {
                throw new Exception(ee.ToString());
            }
        }
        #endregion
        
    }
}
