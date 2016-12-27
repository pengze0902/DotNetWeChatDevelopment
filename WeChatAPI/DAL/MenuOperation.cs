using System;
using System.Collections.Generic;
using System.Web;
using WeChatEntities;
using WeChatHelper;
using WeChatHelper.Interface;
using WeChatHelper.Request;
using WeChatHelper.Response;

namespace WeChatAPI.DAL
{
    public static class MenuOperationMethod
    {
        private const string AppId = "wx9c31a4ca3618e66a";
        private const string AppSecret = "cef5289d07d7d4a9a77a7b4d5d671b50";

        /// <summary>
        /// 创建菜单
        /// </summary>
        /// <returns></returns>
        public static CreateMenuResponse CreateMenuMethod()
        {
            IMpClient mpClient = new MpClient();
            var request = new AccessTokenGetRequest
            {
                AppIdInfo = new AppIdInfo() { AppId = AppId, AppSecret = AppSecret }
            };
            var response = mpClient.Execute(request);
            if (response.IsError)
            {
                Console.WriteLine("获取令牌环失败..");
                return null;
            }

            Menu menu = new Menu();

            List<Button> button = new List<Button>();

            var subBtn1 = new Button()
            {
                Key = "subkey1",
                Name = "测试网站",
                SubButton = null,
                Type = "click",
                 Url = "http://154h609p73.imwork.net"
                //Url = "http://203.195.235.76/jssdk"
            };
            var subBtn2 = new Button()
            {
                Key = "subkey1",
                Name = "子按钮1",
                SubButton = null,
                Type = "click",
                Url = "http://"
            };
            var subBtn3 = new Button()
            {
                Key = "subkey1",
                Name = "子按钮1",
                SubButton = null,
                Type = "click",
                Url = "http://"
            };
            var subBtnAll = new List<Button> { subBtn1, subBtn2, subBtn3 };

            Button btn = new Button()
            {
                Key = "key3",
                Name = "联系",
                Url = "httpbig",
                Type = "click",
                SubButton = subBtnAll
            };
            button.Add(btn);

            btn = new Button()
            {
                Key = "key1",
                Name = "帮助",
                Url = "httpbig",
                Type = "click",
                SubButton = subBtnAll
            };
            button.Add(btn);

            btn = new Button()
            {
                Key = "key2",
                Name = "测试网站",
                Url = "http://154h609p73.imwork.net",
                Type = "click",
                SubButton = subBtnAll
            };
            button.Add(btn);

            menu.Button = button;

            var mg = new MenuGroup()
            {
                Menu = menu
            };

            var postData = mg.ToJsonString();

            var createRequest = new CreateMenuRequest()
            {
                AccessToken = response.AccessToken.AccessToken,
                SendData = postData
            };

            return mpClient.Execute(createRequest);
        }

        /// <summary>
        /// 查询菜单测试
        /// </summary>
        public static GetMenuResponse GetMenuMethod()
        {
            IMpClient mpClient = new MpClient();
            var request = new AccessTokenGetRequest()
            {
                AppIdInfo = new AppIdInfo { AppId = AppId, AppSecret = AppSecret }
            };
            var response = mpClient.Execute(request);
            if (response.IsError)
            {
                HttpContext.Current.Response.Write("获取令牌环失败..");
                return null;
            }
            var getRequest = new GetMenuRequest()
            {
                AccessToken = response.AccessToken.AccessToken
            };
            return mpClient.Execute(getRequest);
          
        }

        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <returns></returns>
        public static DeleteMenuResponse DeleteMenuMethod()
        {
            IMpClient mpClient = new MpClient();
            var request = new AccessTokenGetRequest
            {
                AppIdInfo = new AppIdInfo { AppId = AppId, AppSecret = AppSecret }
            };
            var response = mpClient.Execute(request);
            if (response.IsError)
            {
                HttpContext.Current.Response.Write("获取令牌环失败..");
                return null;
            }
            var getRequest = new DeleteMenuRequest()
            {
                AccessToken = response.AccessToken.AccessToken
            };
            return mpClient.Execute(getRequest);
           
        }
    }
}