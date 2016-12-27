using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WeChatEntities;
using WeChatHelper;
using WeChatHelper.Interface;
using WeChatHelper.Request;
using WeChatHelper.Response;
using WeChatHelper.Util;

namespace WeChatAPI.Page
{
    public partial class QrCode : System.Web.UI.Page
    {

        private const string AppId = "wx9c31a4ca3618e66a";
        private const string AppSecret = "cef5289d07d7d4a9a77a7b4d5d671b50";
        private static string ticketKey;
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        public static void QrCodeCreateMethod()
        {
            IMpClient mpClient = new MpClient();
            var request = new AccessTokenGetRequest()
            {
                AppIdInfo = new AppIdInfo() { AppId = AppId, AppSecret = AppSecret }
            };
            var response = mpClient.Execute(request);
            if (response.IsError)
            {
                Console.WriteLine("获取令牌环失败..");
                return ;
            }

            var msg = new QrCodeCreateMessage()
            {
                SceneId = 22,
                ExpireSeconds = 1800,
                ActionName = "QR_LIMIT_SCENE",   //QR_SCENE为临时,QR_LIMIT_SCENE为永久
                LocalStoredDir = "D:\\"
            };

            var createRequest = new QrCodeCreateRequest()
            {
                AccessToken = response.AccessToken.AccessToken,
                QrCodeCreateMessage = msg,
                SendData = msg.ToJsonString()
            };

            var createResponse=mpClient.Execute(createRequest);
            ticketKey = createResponse.QrCodeUrl;
            if (createResponse.IsError)
            {
                HttpContext.Current.Response.Write("创建二维码失败，错误信息为：" + createResponse.ErrInfo.ErrCode + "-" + createResponse.ErrInfo.ErrMsg);
            }
            else
            {
               HttpContext.Current.Response.Write("创建二维码成功，二维码链接地址为：" + createResponse.QrCodeUrl + "本地存储路径为：" + createResponse.LocalFilePath);
            }
        }


        protected void QrCodeCreate_Click(object sender, EventArgs e)
        {
            QrCodeCreateMethod();
        }

        protected void ObtainQrCode_Click(object sender, EventArgs e)
        {
            var webUtils = new WebUtils();
            var t=webUtils.DoGet(ticketKey, null);
            Response.Write(t);
        }
    }
}