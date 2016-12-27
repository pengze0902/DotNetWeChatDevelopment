namespace WeChatEntities
{
    /// <summary>
    /// 根据OpenID群发消息，返回的结果值
    /// </summary>
  public  class ReturnOpenIdMessageInterface
    {
        /// <summary>
        /// 媒体文件类型
        /// </summary>
      public string Type { get; set; }

        /// <summary>
        /// 错误码
        /// </summary>
      public string Errcode { get; set; }

        /// <summary>
        /// 错误消息
        /// </summary>
      public string Errmsg { get; set; }

        /// <summary>
        /// 消息发送任务ID
        /// </summary>
      public string MsgId { get; set; }

        /// <summary>
        /// 消息的数据ID
        /// </summary>
      public string MsgDataId { get; set; }
    }
}
