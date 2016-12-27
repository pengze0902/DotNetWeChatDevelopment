namespace WeChatHelper.Interface
{
    /// <summary>
    /// 微信公众平台客户端接口
    /// </summary>
    public interface IMpClient
    {
        /// <summary>
        /// 执行微信公众平台API请求
        /// </summary>
        /// <typeparam name="T">领域对象</typeparam>
        /// <param name="request">具体的微信公众平台请求</param>
        /// <returns>领域对象</returns>
        T Execute<T>(IMpRequest<T> request) where T : MpResponse;
    }
}
