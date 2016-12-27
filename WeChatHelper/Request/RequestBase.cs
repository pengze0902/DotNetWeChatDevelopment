using WeChatEntities;

namespace WeChatHelper.Request
{
    /// <summary>
    /// 请求基类
    /// </summary>
    public abstract class RequestBase<T> where T : MpResponse 
    {
        /* 测试账号申请地址
         *  http://mp.weixin.qq.com/debug/cgi-bin/sandbox?t=sandbox/login
        */

        /// <summary>
        /// 获取默认AppInfo信息
        /// </summary>
        /// <returns></returns>
        protected AppIdInfo GetDefaultAppIdInfo()
        {
            var info = new AppIdInfo()
            {
                AppId = "1",
                AppSecret = "2",
                CallBack = "3"
            };
            return info;
        }

    }
}
