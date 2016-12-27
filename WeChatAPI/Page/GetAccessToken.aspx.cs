using System;
using WeChatEntities;
using WeChatHelper;
using WeChatHelper.Interface;
using WeChatHelper.Request;

namespace WeChatAPI.Page
{
    public partial class GetAccessToken : System.Web.UI.Page
    {
        //private const string AppId = "wx6f1b781c47a4fe3b";
        //private const string AppSecret = "f3e4db65e63d927ea1ff64f76894ef8b";
        // private const string AccessToken = "d31_nMviguL21jddkJx3celbFGe0rUJOoy236FBLTvQZqGQynji976Z20AQM3onjhJ844oukZE3wOJzR4x83qDzR8FOLc1lhNjLCrUDyPApP0-DwkdsqgLNeOoDQJFE6xLoGzut9aklo1roNf3c2tA";

        private const string AppId = "wx9c31a4ca3618e66a";
        private const string AppSecret = "cef5289d07d7d4a9a77a7b4d5d671b50";
        protected void Page_Load(object sender, EventArgs e)
        {
            GetAccessTokenMethod();
        }

        public void GetAccessTokenMethod()
        {
            IMpClient mpClient = new MpClient();
            var request = new AccessTokenGetRequest()
            {
                AppIdInfo = new AppIdInfo() { AppId = AppId, AppSecret = AppSecret }
            };
            var response = mpClient.Execute(request);
            if (response.IsError)
            {
                Response.Write(string.Format("获取AccessToken发生错误，错误编码为：{response.ErrInfo.ErrCode}，错误消息为：{response.ErrInfo.ErrMsg}"));
            }
            else
            {
                Response.Write(string.Format("获取到AccessToken，值为：{0}，有效期：{1}秒", response.AccessToken.AccessToken, response.AccessToken.ExpiresIn));
            }
        }
    }
}