using WeChatEntities;

namespace WeChatHelper.Interface
{
    /// <summary>
    /// 信息处理接口
    /// </summary>
    public interface IMessageProcessor
    {
       /// <summary>
       /// 处理消息
       /// </summary>
       /// <param name="msg">消息对象</param>
       /// <param name="args">参数（用于具体业务传递参数用）</param>
       /// <returns>是否处理成功</returns>
        bool ProcessMessage(ReceiveMessageBase msg, params object[] args);

        /// <summary>
        /// 处理文本消息
        /// </summary>
        /// <param name="msg">消息对象</param>
        /// <param name="args">参数（用于具体业务传递参数用）</param>
        /// <returns>是否处理成功</returns>
        bool ProcessTextMessage(TextReceiveMessage msg, params object[] args);

        /// <summary>
        /// 处理图片消息
        /// </summary>
        /// <param name="msg">消息对象</param>
        /// <param name="args">参数（用于具体业务传递参数用）</param>
        /// <returns>是否处理成功</returns>
        bool ProcessImageMessage(ImageReceiveMessage msg, params object[] args);

        /// <summary>
        /// 处理语音消息
        /// </summary>
        /// <param name="msg">消息对象</param>
        /// <param name="args">参数（用于具体业务传递参数用）</param>
        /// <returns>是否处理成功</returns>
        bool ProcessVoiceMessage(VoiceReceiveMessage msg, params object[] args);

        /// <summary>
        /// 处理视频消息
        /// </summary>
        /// <param name="msg">消息对象</param>
        /// <param name="args">参数（用于具体业务传递参数用）</param>
        /// <returns>是否处理成功</returns>
        bool ProcessVideoMessage(VideoReceiveMessage msg, params object[] args);

        /// <summary>
        /// 处理地理位置消息
        /// </summary>
        /// <param name="msg">消息对象</param>
        /// <param name="args">参数（用于具体业务传递参数用）</param>
        /// <returns>是否处理成功</returns>
        bool ProcessLocationMessage(LocationReceiveMessage msg, params object[] args);

        /// <summary>
        /// 处理链接消息
        /// </summary>
        /// <param name="msg">消息对象</param>
        /// <param name="args">参数（用于具体业务传递参数用）</param>
        /// <returns>是否处理成功</returns>
        bool ProcessLinkMessage(LinkReceiveMessage msg, params object[] args);

        /// <summary>
        /// 处理关注事件
        /// </summary>
        /// <param name="msg">事件消息</param>
        /// <param name="args">参数（用于具体业务传递参数用）</param>
        /// <returns>是否处理成功</returns>
        bool ProcessSubscribeEvent(SubscribeEventMessage msg, params object[] args);

        /// <summary>
        /// 处理取消关注事件
        /// </summary>
        /// <param name="msg">事件消息</param>
        /// <param name="args">参数（用于具体业务传递参数用）</param>
        /// <returns>是否处理成功</returns>
        bool ProcessUnSubscribeEvent(UnSubscribeEventMessage msg, params object[] args);

        /// <summary>
        /// 处理扫描二维码关注事件
        /// </summary>
        /// <param name="msg">事件消息</param>
        /// <param name="args">参数（用于具体业务传递参数用）</param>
        /// <returns>是否处理成功</returns>
        bool ProcessScanSubscribeEvent(ScanSubscribeEventMessage msg, params object[] args);

        /// <summary>
        /// 处理扫描二维码事件
        /// </summary>
        /// <param name="msg">事件消息</param>
        /// <param name="args">参数（用于具体业务传递参数用）</param>
        /// <returns>是否处理成功</returns>
        bool ProcessScanEvent(ScanEventMessage msg, params object[] args);

        /// <summary>
        /// 处理上报地理位置事件
        /// </summary>
        /// <param name="msg">事件消息</param>
        /// <param name="args">参数（用于具体业务传递参数用）</param>
        /// <returns>是否处理成功</returns>
        bool ProcessUploadLocationEvent(UploadLocationEventMessage msg, params object[] args);

        /// <summary>
        /// 处理自定义菜单事件
        /// </summary>
        /// <param name="msg">事件消息</param>
        /// <param name="args">参数（用于具体业务传递参数用）</param>
        /// <returns>是否处理成功</returns>
        bool ProcessMenuEvent(MenuEventMessage msg, params object[] args);

        /// <summary>
        /// 处理未定义处理方法消息
        /// </summary>
        /// <param name="msg">消息对象</param>
        /// <param name="args">参数（用于具体业务传递参数用）</param>
        /// <returns>是否处理成功</returns>
        bool ProcessNotHandlerMsg(ReceiveMessageBase msg, params object[] args);
    }
}
