using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;

namespace WeChatAPI.Ashx
{
    /// <summary>
    /// 网页查看本地图片
    /// </summary>
    public class LocalPictureHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            var path = context.Request["path"];
            if(string.IsNullOrEmpty(path))
                throw new ArgumentNullException(nameof(path));
            try
            {
                var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
                var bitmap = new Bitmap(fileStream);
                using (var memoryStream = new MemoryStream())
                {
                    bitmap.Save(memoryStream, ImageFormat.Jpeg);
                    context.Response.ClearContent();
                    context.Response.ContentType = "image/Jpeg";
                    context.Response.BinaryWrite(memoryStream.ToArray());
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public bool IsReusable => false;
    }
}