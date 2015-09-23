using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.IO.Compression;
using HyBy.Trading.BusinessFramework;

namespace WebSite
{
    public class CompressAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var request = filterContext.HttpContext.Request;
            string acceptEncoding = request.Headers["Accept-Encoding"];
            if (string.IsNullOrEmpty(acceptEncoding))
            {
                return;
            }
            acceptEncoding = acceptEncoding.ToUpperInvariant();
            HttpResponseBase response = filterContext.HttpContext.Response;
            if (response.Filter != null)
            {
                if (acceptEncoding.Contains("GZIP"))
                {
                    //非重定向，启用压缩
                    if (response.StatusCode != 302)
                    {
                        response.AppendHeader("Content-Encoding", "gzip");
                        response.Filter = new GZipStream(response.Filter, CompressionMode.Compress);
                        //string keyHead = response.Headers.Get("Content-Encoding");
                        //MessageHelper.WriteLog("GZIP" + keyHead);
                    }
                }
                else
                {
                    response.AppendHeader("Content-Encoding", "deflate");
                    response.Filter = new DeflateStream(response.Filter, CompressionMode.Compress);
                }
            }
        }
    }
}