using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;

namespace HyBy.Trading.BusinessFramework
{
    public class ImageHelper : IDisposable
    {
        private bool disposed = false;
        /// <summary>
        /// 生成缩略图
        /// </summary>
        /// <param name="originalImagePath">源图路径（物理路径）</param>
        /// <param name="thumbnailPath">缩略图路径（物理路径）</param>
        /// <param name="width">缩略图宽度</param>
        /// <param name="height">缩略图高度</param>
        /// <param name="mode">生成缩略图的方式</param>    
        public void MakeThumbnail(string originalImagePath, string thumbnailPath, int width, int height, string mode)
        {
            Image originalImage = Image.FromFile(originalImagePath);

            int towidth = width;
            int toheight = height;

            int x = 0;
            int y = 0;
            int ow = originalImage.Width;
            int oh = originalImage.Height;

            double rate = 1;
            switch (mode)
            {
                case "HW"://指定高宽缩放（可能变形）     
                    if (originalImage.Width > width || originalImage.Height > height)
                    {
                        if ((double)width / originalImage.Width < (double)height / originalImage.Height)
                        {
                            rate = (double)width / originalImage.Width;
                        }
                        else
                        {
                            rate = (double)height / originalImage.Height;
                        }

                    }
                    towidth = int.Parse(Math.Round(originalImage.Width * rate).ToString());
                    toheight = int.Parse(Math.Round(originalImage.Height * rate).ToString());
                    break;
                case "W"://指定宽，高按比例                    
                    toheight = originalImage.Height * width / originalImage.Width;
                    break;
                case "H"://指定高，宽按比例
                    towidth = originalImage.Width * height / originalImage.Height;
                    break;
                case "Cut"://指定高宽裁减（不变形）                
                    if ((double)originalImage.Width / (double)originalImage.Height > (double)towidth / (double)toheight)
                    {
                        oh = originalImage.Height;
                        ow = originalImage.Height * towidth / toheight;
                        y = 0;
                        x = (originalImage.Width - ow) / 2;
                    }
                    else
                    {
                        ow = originalImage.Width;
                        oh = originalImage.Width * height / towidth;
                        x = 0;
                        y = (originalImage.Height - oh) / 2;
                    }
                    break;
                default:
                    break;
            }

            //新建一个bmp图片
            Image bitmap = new System.Drawing.Bitmap(towidth, toheight);

            //新建一个画板
            Graphics g = System.Drawing.Graphics.FromImage(bitmap);

            //设置高质量插值法
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;

            //设置高质量,低速度呈现平滑程度
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            //清空画布并以透明背景色填充
            g.Clear(Color.Transparent);

            //在指定位置并且按指定大小绘制原图片的指定部分
            g.DrawImage(originalImage, new Rectangle(0, 0, towidth, toheight),
                new Rectangle(x, y, ow, oh),
                GraphicsUnit.Pixel);

            try
            {
                if (!Directory.Exists(Path.GetDirectoryName(thumbnailPath)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(thumbnailPath));
                }

                if (!File.Exists(thumbnailPath))
                {
                    //以jpg格式保存缩略图
                    bitmap.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Jpeg);
                }
            }
            catch (System.Exception e)
            {
                originalImage.Dispose();
                bitmap.Dispose();
                g.Dispose();
                throw e;
            }
            finally
            {
                
                originalImage.Dispose();
                bitmap.Dispose();
                g.Dispose();
            }
        }

        /// <summary>
        /// 获取缩略图路径
        /// </summary>
        /// <param name="imagePath">完整路径</param>
        /// <param name="type">缩略图类型</param>
        /// <returns></returns>
        public string GetImagePath(string imagePath, UploadThumbnailType type)
        {
            return imagePath.Insert(imagePath.LastIndexOf("/") + 1, "thumbnail/" + type.ToString() + "/");
        }

        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);

        }

        protected virtual void Dispose(bool disposing)
        {

            if (disposed)

                return;

            if (disposing)
            {

                // TODO: 在这里释放托管资源

            }

            // TODO: 在这里释放非托管资源

            disposed = true;

        }



        ~ImageHelper()
        {

            Dispose(false);

        }

    }

    public class UshareImageHelper : ImageHelper
    {
        public void GenerateImage(string originalImagePath,ref Dictionary<string,string> outImgPaths)
        {
            string path1=GetThumbnailPath(originalImagePath, "big");
            MakeThumbnail(originalImagePath, GetThumbnailPath(originalImagePath, "small"), 315, 210, "HW");
            MakeThumbnail(originalImagePath, path1, 690, 460, "HW");

            outImgPaths.Add("big", path1);
        }

        string GetThumbnailPath(string thumbnailPath,string t)
        {
            return thumbnailPath.Insert(thumbnailPath.LastIndexOf("\\") + 1, "thumbnail\\" + t.ToString() + "\\");
        }
    }

    public enum UploadThumbnailType
    {
        Small,
        Big
    }
}
