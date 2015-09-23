using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Win32;
using ExpertPdf.HtmlToPdf;
using System.Configuration;

namespace WebSite.Extentions
{
    /// <summary>
    /// IE打印修改注册表
    /// </summary>
    public class Print
    {
        public Print()
        {
            //if (ReadReg() == 1)
            //{
            //    if (WriteReg() == 1)
            //    {
            //        ReadReg();
            //        // Response.Write("Game over!");
            //    }
            //}
            WriteReg();

        }



        /// <summary>
        /// 读注册表
        /// </summary>
        /// <returns></returns>
        protected int ReadReg()
        {
            //读注册表
            RegistryKey regRead;
            //读取HKEY_CURRENT_USER主键里的子键
            regRead = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Internet Explorer\\PageSetup", true);
            if (regRead == null) //如果该子键不存在
            {
                // Response.Write("PageSetup:null</br>");
                return 0;
            }
            else
            {
                //  Response.Write("PageSetup:OK</br>");

                object obj = regRead.GetValue("header") == null ? "null" : regRead.GetValue("header"); //读取值
                //  Response.Write("header:" + obj.ToString() + "</br>");

                obj = regRead.GetValue("footer") == null ? "null" : regRead.GetValue("footer"); //读取值
                // Response.Write("footer:" + obj.ToString() + "</br>");

                obj = regRead.GetValue("margin_left") == null ? "null" : regRead.GetValue("margin_left"); //读取值
                //  Response.Write("margin_left:" + obj.ToString() + "</br>");

                obj = regRead.GetValue("margin_right") == null ? "null" : regRead.GetValue("margin_right"); //读取值
                // Response.Write("margin_right:" + obj.ToString() + "</br>");

                obj = regRead.GetValue("margin_top") == null ? "null" : regRead.GetValue("margin_top"); //读取值
                // Response.Write("margin_top:" + obj.ToString() + "</br>");

                obj = regRead.GetValue("margin_bottom") == null ? "null" : regRead.GetValue("margin_bottom"); //读取值
                //     Response.Write("margin_bottom:" + obj.ToString() + "</br>");

                obj = regRead.GetValue("Print_Background") == null ? "null" : regRead.GetValue("Print_Background"); //读取值
                // Response.Write("Print_Background:" + obj.ToString() + "</br>");

                //  Response.Write("</br>");
            }
            regRead.Close();
            return 1;
        }

        /// <summary>
        /// 写注册表
        /// </summary>
        protected int WriteReg()
        {
            //写注册表
            RegistryKey regWrite;
            //往HKEY_CURRENT_USER主键里的写子键
            //如果子键已经存在系统会自动覆盖它
            regWrite = Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Internet Explorer\\PageSetup");
            //往Test子键里添数据项
            regWrite.SetValue("header", "");
            regWrite.SetValue("footer", "");
            regWrite.SetValue("margin_left", 0);
            regWrite.SetValue("margin_right", 0);
            regWrite.SetValue("margin_top", 0);
            regWrite.SetValue("margin_bottom", 0);
            regWrite.SetValue("Print_Background", "yes");
            //关闭该对象
            regWrite.Close();

            return 1;

        }



        public static byte[] PrintToPDFByUrl(string url)
        {
            byte[] pdfBytes = null;
            try
            {

                //bool selectablePDF = true;
                PdfConverter pdfConverter = new PdfConverter();
                // pdfConverterKey
                pdfConverter.LicenseKey = ConfigurationManager.AppSettings["pdfConverterKey"];
                pdfConverter.PdfDocumentOptions.PdfPageSize = PdfPageSize.A4;
                pdfConverter.PdfDocumentOptions.PdfCompressionLevel = PdfCompressionLevel.Normal;
                pdfConverter.PdfDocumentOptions.PdfPageOrientation = PDFPageOrientation.Portrait;
                //是否显示顶部和底部的横线
                pdfConverter.PdfDocumentOptions.ShowHeader = false;
                pdfConverter.PdfDocumentOptions.ShowFooter = false;
                pdfConverter.PdfDocumentOptions.GenerateSelectablePdf = true;
                pdfConverter.PdfDocumentOptions.FitWidth = true;
                pdfConverter.PdfDocumentOptions.EmbedFonts = false;
                pdfConverter.PdfDocumentOptions.LiveUrlsEnabled = true;
                //pdfConverter.PdfDocumentOptions.LeftMargin = 15;
                //pdfConverter.PdfDocumentOptions.RightMargin = 15;
                pdfConverter.ScriptsEnabled = true;
                pdfConverter.ActiveXEnabled = true;
                pdfConverter.PageWidth = 840;
                
                
                //if (true)
                //{
                //    pdfConverter.ScriptsEnabled = true;
                //    pdfConverter.ActiveXEnabled = true;
                //}
                //else
                //{
                //    //pdfConverter.ScriptsEnabledInImage = true;
                //    //pdfConverter.ActiveXEnabledInImage = true;
                //}

                pdfConverter.PdfDocumentOptions.JpegCompressionEnabled = true;

                //if (false)
                //{
                //    pdfConverter.PdfBookmarkOptions.TagNames = new string[] { "H1", "H2" };
                //}

                pdfConverter.PdfDocumentInfo.AuthorName = "ExperPDF HTML to PDF Converter";



                //byte[] pdfBytes = null;
                pdfBytes = pdfConverter.GetPdfBytesFromUrl(url);

                //HttpResponse response = HttpContext.Current.Response;
                //response.Clear();
                //response.AddHeader("Content-Type", "binary/octet-stream");
                //response.AddHeader("Content-Disposition",
                //    string.Format("attachment; filename={0}.pdf; size={1}", Guid.NewGuid(), pdfBytes.Length.ToString()));
                //response.Flush();

                //response.BinaryWrite(pdfBytes);
                //response.Flush();
                //response.End();
            }
            catch 
            {

            }
            return pdfBytes;
        }


    }
}
