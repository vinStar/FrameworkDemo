using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;

namespace WebSite
{
    public static class ImageHelper
    {
        /// <summary> 
        /// 缩小图片 
        /// </summary> 
        /// <param name="stroldpic">源图文件名(包括路径)</param> 
        /// <param name="strnewpic">缩小后保存为文件名(包括路径)</param> 
        /// <param name="intwidth">缩小至宽度</param> 
        /// <param name="intheight">缩小至高度</param> 
        public static void smallpic(string stroldpic, string strnewpic, int intwidth, int intheight)
        {

            Bitmap objpic, objnewpic;
            try
            {
                objpic = new Bitmap(stroldpic);
                objnewpic = new Bitmap(objpic, intwidth, intheight);
                objnewpic.Save(strnewpic);

            }
            catch (Exception exp) { throw exp; }
            finally
            {
                objpic = null;
                objnewpic = null;
            }
        }
    }
}