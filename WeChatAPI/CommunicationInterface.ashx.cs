using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Remoting.Messaging;
using System.Web;
using WeChatEntities;
using WeChatHelper;
using WeChatHelper.Interface;
using WeChatHelper.Request;
using WeChatHelper.Response;
using WeChatHelper.Util;

namespace WeChatAPI
{
    /// <summary>
    /// CommunicationInterface 的摘要说明
    /// </summary>
    public class CommunicationInterface : IHttpHandler
    {
        private readonly string token = "allviewpassword";
        //private const string AppId = "wx77fff8564fcd3cba";
        //private const string AppSecret = "339b5eec8f2c9d1d91311426f94bc02b";
        //private const string AppId = "wx6f1b781c47a4fe3b";
        //private const string AppSecret = "f3e4db65e63d927ea1ff64f76894ef8b";
        private const string AppId = "wx9c31a4ca3618e66a";
        private const string AppSecret = "cef5289d07d7d4a9a77a7b4d5d671b50";

        public void ProcessRequest(HttpContext context)
        {
            var receiveMessageBase = MessageHandler.ConvertMsgToObject(token);
            Logger.WriteTxtLog(receiveMessageBase.MessageBody.ToString(), "D:/Log20160801.txt");
            // HttpContext.Current.Response.Write(receiveMessageBase);
            TextReturnMessage(receiveMessageBase);
            //MessageHandler.Valid(token);
        }


        public void TextReturnMessage(ReceiveMessageBase receiveMessageBase)
        {
            IMpClient mpClient = new MpClient();
            //有效性校验
            var request = new AccessTokenGetRequest()
            {
                AppIdInfo = new AppIdInfo() { AppId = AppId, AppSecret = AppSecret }
            };
            var response = mpClient.Execute(request);
            if (response.IsError)
            {
               HttpContext.Current.Response.Write("获取令牌环失败..");
            }
            MpResponse send= MessageHandler.SendTextCustomMessage(response.AccessToken.AccessToken,receiveMessageBase.FromUserName, "http://wuhanallview.ticp.net");
            Logger.WriteTxtLog(send.Body,"D:/Log20160802.txt");
            HttpContext.Current.Response.Write(send.Body.ToString());
        }

        public bool IsReusable => false;
    }
}