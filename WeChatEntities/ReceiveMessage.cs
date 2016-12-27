namespace WeChatEntities
{

    /// <summary>
    /// 接收消息实体类
    /// </summary>
    public class ReceiveMessage : ReceiveMessageBase
    {
        /// <summary>
        /// 消息id，64位整型 
        /// </summary>
        public long MsgId { get; set; }
    }


    /// <summary>
    /// 文本消息
    /// </summary>
    public class TextReceiveMessage : ReceiveMessage
    {
        /// <summary>
        /// 文本消息内容
        /// </summary>
        public string Content { get; set; }
    }

    /// <summary>
    /// 图片信息
    /// </summary>
    public class ImageReceiveMessage : ReceiveMessage
    {
        /// <summary>
        /// 图片链接 
        /// </summary>
        public string PicUrl { get; set; }

        /// <summary>
        /// 图片消息媒体id，可以调用多媒体文件下载接口拉取数据。
        /// </summary>
        public string MediaId { get; set; }
    }

    /// <summary>
    /// 语音消息
    /// </summary>
    public class VoiceReceiveMessage : ReceiveMessage
    {
        /// <summary>
        /// 语音消息媒体id，可以调用多媒体文件下载接口拉取数据
        /// </summary>
        public string MediaId { get; set; }

        /// <summary>
        /// 语音格式，如amr，speex等
        /// </summary>
        public string Format { get; set; }

        /// <summary>
        /// 语音识别结果，UTF8编码 
        /// </summary>
        public string Recognition { get; set; }
    }

    /// <summary>
    /// 视频信息
    /// </summary>
    public class VideoReceiveMessage : ReceiveMessage
    {
        /// <summary>
        /// 视频消息媒体id，可以调用多媒体文件下载接口拉取数据。
        /// </summary>
        public string MediaId { get; set; }

        /// <summary>
        ///  	视频消息缩略图的媒体id，可以调用多媒体文件下载接口拉取数据。
        /// </summary>
        public string ThumbMediaId { get; set; }
    }

    /// <summary>
    /// 地理位置消息
    /// </summary>
    public class LocationReceiveMessage : ReceiveMessage
    {
        /// <summary>
        /// 地理位置维度 
        /// </summary>
        public string LocationX { get; set; }

        /// <summary>
        /// 地理位置经度 
        /// </summary>
        public string LocationY { get; set; }

        /// <summary>
        /// 地图缩放大小
        /// </summary>
        public string Scale { get; set; }

        /// <summary>
        /// 地理位置信息
        /// </summary>
        public string Label { get; set; }
    }

    /// <summary>
    /// 链接消息
    /// </summary>
    public class LinkReceiveMessage : ReceiveMessage
    {
        /// <summary>
        /// 消息标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 消息描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 消息链接
        /// </summary>
        public string Url { get; set; }
    }
}

