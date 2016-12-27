using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using WeChatEntities;
using WeChatHelper;
using WeChatHelper.Interface;
using WeChatHelper.Request;

namespace WeChatAPI.DAL
{
  public  static class MessageInterfaceOperation
    {
        private const string AppId = "wx9c31a4ca3618e66a";
        private const string AppSecret = "cef5289d07d7d4a9a77a7b4d5d671b50";
        public static string UploadCreativeGraphicMessageMethod()
      {
            IMpClient mpClient = new MpClient();
            var request = new AccessTokenGetRequest()
            {
                AppIdInfo = new AppIdInfo() { AppId = AppId, AppSecret = AppSecret }
            };
            var response = mpClient.Execute(request);
            if (response.IsError)
            {
                return null;
            }
            GraphicInterfaceMessage permanent = new GraphicInterfaceMessage
            {
                ThumbMediaId = "TpuIFr0VPcAnD3nObZzDiuLFJeEK8repP9HOTg7WwBmgHCTZdWHRj7ZOsO67H8SG",
                Author = "ceshi",
                Title = "测试标题",
                ContentSourceUrl = "www.baidu.com",
                Content = "测试内容",
                Digest = "测试的描述打发打发",
                ShowCoverPic = "1"
            };

            List<GraphicInterfaceMessage> list = new List<GraphicInterfaceMessage> { permanent };

            GraphicInterfaceMessageList meList = new GraphicInterfaceMessageList { GraphicInterfaceMessagesList = list };

            InterfaceMessageList meMaterial = new InterfaceMessageList
            {
                GraphicInterface = meList
            };

            var s = meMaterial.ToJsonString();
            var requestUpload = new GraphicInterfaceMessageRequest
            {
                AccessToken = response.AccessToken.AccessToken,
                SendData = s
            };
            var createResponse = mpClient.Execute(requestUpload);
            if (createResponse.IsError)
            {
                LogHelper.WriteLog(typeof(StatisticsOperation), createResponse.ErrInfo.ErrMsg);
                return createResponse.ErrInfo.ErrMsg;
            }
            var jsonSerializer = new JavaScriptSerializer();
            return jsonSerializer.Serialize(createResponse.Body);
        }


      public static string BulkMessagePacketMethod()
      {
            IMpClient mpClient = new MpClient();
            var request = new AccessTokenGetRequest()
            {
                AppIdInfo = new AppIdInfo() { AppId = AppId, AppSecret = AppSecret }
            };
            var response = mpClient.Execute(request);
            if (response.IsError)
            {
                return null;
            }
            PacketInterfaceMessage permanent = new PacketInterfaceMessage
            {
                MediaId = "9onupBukQHhG1T8Pvkau1D4KK6igthNYMPykga803f7j3YvNvawzv5qeZyjEndkA",
                Title = "测试标题1",
                IsToAll = "false",
                GroupId = 101,
                Msgtype = "mpnews"
            };

            var requestUpload = new GraphicInterfaceMessageRequest
            {
                AccessToken = response.AccessToken.AccessToken,
                SendData = permanent.ToCreateJsonString()
            };
            var createResponse = mpClient.Execute(requestUpload);
            if (createResponse.IsError)
            {
                LogHelper.WriteLog(typeof(StatisticsOperation), createResponse.ErrInfo.ErrMsg);
                return createResponse.ErrInfo.ErrMsg;
            }
            var jsonSerializer = new JavaScriptSerializer();
            return jsonSerializer.Serialize(createResponse.Body);
        }


      public static string OpenIdMessageMethod()
      {
            IMpClient mpClient = new MpClient();
            var request = new AccessTokenGetRequest()
            {
                AppIdInfo = new AppIdInfo() { AppId = AppId, AppSecret = AppSecret }
            };
            var response = mpClient.Execute(request);
            if (response.IsError)
            {
                return null;
            }

          string openid = "\"oIXLKvpVPUpvQ7yR2mLSgNer-K5Y\",\"oIXLKvh_-HrDq8KHULTJ99mIHbMA\"";
            OpenIdMessageInterface permanent = new OpenIdMessageInterface
            {
                MediaId = "9onupBukQHhG1T8Pvkau1D4KK6igthNYMPykga803f7j3YvNvawzv5qeZyjEndkA",
                Title = "测试标题2",
                Msgtype = "mpnews",
                Touser = openid
            };

            var requestUpload = new OpenIdMessageInterfaceRequest
            {
                AccessToken = response.AccessToken.AccessToken,
                SendData = permanent.ToCreateJsonString()
            };
            var createResponse = mpClient.Execute(requestUpload);
            if (createResponse.IsError)
            {
                LogHelper.WriteLog(typeof(StatisticsOperation), createResponse.ErrInfo.ErrMsg);
                return createResponse.ErrInfo.ErrMsg;
            }
            var jsonSerializer = new JavaScriptSerializer();
            return jsonSerializer.Serialize(createResponse.Body);
        }

    }
}