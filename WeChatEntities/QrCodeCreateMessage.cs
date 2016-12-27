
namespace WeChatEntities
{
    /// <summary>
    /// 创建二维码请求信息
    /// </summary>
    public class QrCodeCreateMessage
    {
        /// <summary>
        /// 该二维码有效时间，以秒为单位。 最大不超过1800. ActionName = "QR_LIMIT_SCENE" 时无效
        /// </summary>
        public int ExpireSeconds { get; set; }

        /// <summary>
        /// 二维码类型，QR_SCENE为临时,QR_LIMIT_SCENE为永久 
        /// </summary>
        public string ActionName { get; set; }

        /// <summary>
        /// 场景值ID，临时二维码时为32位非0整型，永久二维码时最大值为100000（目前参数只支持1--100000） 
        /// </summary>
        public int SceneId { get; set; }

        /// <summary>
        /// 本地存储目录，如果为空，则不会做本地存储
        /// </summary>
        public string LocalStoredDir { get; set; }

        /// <summary>
        /// 将对象转化为POST给公众平台的Json字符串
        /// </summary>
        /// <returns></returns>
        public string ToJsonString()
        {
            string s;
            if (ActionName == "QR_LIMIT_SCENE")
            {
                s = "{\"action_name\": \"QR_LIMIT_SCENE\", \"action_info\": {\"scene\": {\"scene_id\": " + SceneId + "}}}";
            }
            else
            {
                s = "{\"expire_seconds\": " + ExpireSeconds + ", \"action_name\": \"QR_SCENE\", \"action_info\": {\"scene\": {\"scene_id\": " + SceneId + "}}}";
            }
            return s;
        }
    }
}
