namespace WeChatHelper.Response
{
    /// <summary>
    /// 上传多媒体文件回应信息
    /// </summary>
    public class UploadMediaResponse : MpResponse
    {
        /// <summary>
        /// 媒体文件类型，分别有图片（image）、语音（voice）、视频（video）和缩略图（thumb，主要用于视频与音乐格式的缩略图） 
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 媒体文件上传后，获取时的唯一标识 
        /// </summary>
        public string MediaId { get; set; }

        /// <summary>
        /// 媒体文件上传时间戳
        /// </summary>
        public string CreatedAt { get; set; }

    }
}
