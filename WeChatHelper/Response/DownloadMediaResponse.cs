namespace WeChatHelper.Response
{
    /// <summary>
    /// 多媒体文件下载回应信息
    /// </summary>
    public class DownloadMediaResponse : MpResponse
    {
        /// <summary>
        /// 下载文件保存路径
        /// </summary>
        public string SaveFileName { get; set; }
    }
}
