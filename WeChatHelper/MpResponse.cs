using System;
using WeChatEntities;
using WeChatHelper.Util;

namespace WeChatHelper
{
    [Serializable]
    public abstract class MpResponse
    {
        /// <summary>
        /// 错误信息
        /// </summary>
        public ErrInfo ErrInfo { get; set; }

        /// <summary>
        /// 响应原始内容
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// 请求Url
        /// </summary>
        public string ReqUrl { get; set; }

        /// <summary>
        /// 响应结果是否错误
        /// </summary>
        public bool IsError => ErrInfo != null;

        /// <summary>
        /// 判断是否返回了错误信息
        /// </summary>
        /// <returns></returns>
        internal bool HasError()
        {
            if (string.IsNullOrWhiteSpace(Body))
            {
                return false;
            }
            var html = Body.Trim().ToLower();
            if (!html.StartsWith("{\"errcode\"", StringComparison.InvariantCultureIgnoreCase)) return false;
            return Convert.ToInt64(Tools.GetJosnValue(Body, "errcode")) != 0;
        }

        /// <summary>
        /// 获取错误代码和错误信息
        /// </summary>
        /// <returns></returns>
        internal ErrInfo GetErrInfo()
        {
            var err = new ErrInfo()
            {
                ErrCode = Convert.ToInt64(Tools.GetJosnValue(Body, "errcode")),
                ErrMsg = Tools.GetJosnValue(Body, "errmsg")
            };
            return err;
        }

        /// <summary>
        /// 分组成功的状态码
        /// </summary>
        public string ErrMsg { get; set; }
    }
}
