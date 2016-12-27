using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using WeChatEntities;
using WeChatHelper;
using WeChatHelper.Interface;
using WeChatHelper.Request;

namespace WeChatAPI.DAL
{
    public class UserManagementOperation
    {
        private const string AppId = "wx9c31a4ca3618e66a";
        private const string AppSecret = "cef5289d07d7d4a9a77a7b4d5d671b50";
        public static string RemarkName(string openid,string name)
        {
            try
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

                SetUpUserRemarkName remarkName = new SetUpUserRemarkName
                {
                    AccessToken = response.AccessToken.AccessToken,
                    UserId = openid,
                    ReamrkName = name
                };

                var createResponse = mpClient.Execute(remarkName);
                if (createResponse.IsError)
                {
                    LogHelper.WriteLog(typeof(UserManagementOperation), createResponse.ErrInfo.ErrMsg);
                    return null;
                }
                return createResponse.ErrMsg;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(typeof(UserManagementOperation), ex);
                throw new Exception(ex.Message);
            }
        }
    }
}