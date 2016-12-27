using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Web;

namespace WeChatAPI.Ashx
{
    /// <summary>
    /// PictureHandler 的摘要说明
    /// </summary>
    public class WebPictureHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            var imageUrl = context.Request["url"];
            if (string.IsNullOrEmpty(imageUrl))
                throw new ArgumentNullException(nameof(imageUrl));
            try
            {
                var imageUri = new Uri(imageUrl);
                var webRequest = WebRequest.Create(imageUri);
                var webResponse = webRequest.GetResponse();
                var bitmap = new Bitmap(webResponse.GetResponseStream());
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