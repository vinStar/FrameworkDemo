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
    /// 内容摘要: 文件上传操作类
    /// 完成日期：2014-5-16
    /// 版    本：V1.0
    /// 作    者：段帅峰
    /// </summary>
    public class FileUploadHelper
    {
        #region 上传文件
        private string fileType ="jpg|bmp|png|gif";
        private int sizes = 1000;  //单位为KB
        private string uploadFilePath = null;
        ///<summary>
        /// 设置上传文件的类型,如:jpg|gif|bmp
        ///</summary>
        public string FileType
        {
            set
            {
                fileType = value.ToLower();
            }
        }
        ///<summary>
        /// 设置上传文件大小,单位为KB
        ///</summary>
        public int Sizes
        {
            set
            {
                sizes = value * 1024;
            }
        }
        public bool UploadFile(System.Web.UI.HtmlControls.HtmlInputFile Filename, string stockPath, bool creatDirectory,out string msg,out string path)
        {
            msg = "";
            path = "";
            try
            {
                if (creatDirectory)
                {
                    string datetime = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString();
                    stockPath = stockPath + @"\" + datetime;
                    uploadFilePath = stockPath + @"\";
                }
                else
                {
                    uploadFilePath = stockPath + @"\";
                }
                //获得文件的上传的路径
                string sourceFilePath = Filename.Value.Trim();
                if (sourceFilePath == "" || sourceFilePath == null)
                {
                    return false;
                }
                //获得文件扩展名
                //string sEx = Path.GetExtension(Filename.PostedFile.FileName).Replace(".", "");
                ////获得文件名
                string oldFileName = Path.GetFileName(Filename.PostedFile.FileName);
                string[] strs = oldFileName.Split('.');
                oldFileName = strs[0]+"_" + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second + DateTime.Now.Millisecond + "." + strs[1];
                ////获得上传文件的大小
                //long postFileSize = Filename.PostedFile.ContentLength;
                ////分解允许上传文件的格式
                //string[] temp = fileType.Split('|');
                ////设置上传的文件是否是允许的格式
                //bool flag = false;
                ////判断上传文件大小
                //if (postFileSize >= sizes)
                //{
                //    msg = "上传的文件不能大于" + sizes + "KB";
                //    //message("上传的文件不能大于" + sizes + "KB");
                //    return false;
                //}
                //foreach (string data in temp)
                //{
                //    if (data == sEx)
                //    {
                //        flag = true;
                //        break;
                //    }
                //}
                //if (!flag)
                //{
                //    msg = "目前本系统支持的格式为:" + fileType;
                //    //message("目前本系统支持的格式为:" + fileType);
                //    return false;
                //}
                DirectoryInfo dir = new DirectoryInfo(uploadFilePath);
                if (!dir.Exists)
                {
                    dir.Create();
                }

                string filePath = uploadFilePath + oldFileName;
                Filename.PostedFile.SaveAs(filePath);
                path = filePath;
                return true;
            }
            catch
            {
                msg = "出现未知错误";
                //message("出现未知错误");
                return false;
            }
        }

        #endregion
    }
}
