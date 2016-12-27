namespace WeChatEntities
{
    /// <summary>
    /// 事件类型
    /// </summary>
    public enum EventType
    {
        /// <summary>
        /// 未知事件
        /// </summary>
        UnKnown,

        /// <summary>
        /// 订阅事件
        /// </summary>
        Subscribe,

        /// <summary>
        /// 取消订阅事件
        /// </summary>
        UnSubscribe,

        /// <summary>
        /// 扫描二维码事件
        /// </summary>
        Scan,

        /// <summary>
        /// 上报地理位置事件
        /// </summary>
        Location,

        /// <summary>
        /// 自定义菜单事件
        /// </summary>
        Click
    }

    /// <summary>
    /// 事件推送消息
    /// </summary>
    public class EventMessage : ReceiveMessageBase
    {
        /// <summary>
        /// 事件类型，subscribe(订阅)、unsubscribe(取消订阅)、scan(扫描二维码)、LOCATION(上报地理位置)、CLICK(自定义菜单事件)
        /// </summary>
        public EventType EventType { get; set; }
    }

    /// <summary>
    /// 关注事件消息
    /// </summary>
    public class SubscribeEventMessage : EventMessage
    {
        
    }

    /// <summary>
    /// 取消关注事件
    /// </summary>
    public class UnSubscribeEventMessage : EventMessage
    {
    }


    /// <summary>
    /// 扫描二维码关注事件消息 EventType = subscribe (订阅)
    /// </summary>
    public class ScanSubscribeEventMessage : EventMessage
    {
        /// <summary>
        /// 事件KEY值，qrscene_为前缀，后面为二维码的参数值
        /// </summary>
        public string EventKey { get; set; }

        /// <summary>
        /// 二维码的ticket，可用来换取二维码图片
        /// </summary>
        public string Ticket { get; set; }
    }

    /// <summary>
    /// 扫描二维码事件消息 Event = scan
    /// </summary>
    public class ScanEventMessage : EventMessage
    {
        /// <summary>
        /// 事件KEY值，是一个32位无符号整数，即创建二维码时的二维码scene_id
        /// </summary>
        public string EventKey { get; set; }

        /// <summary>
        /// 二维码的ticket，可用来换取二维码图片
        /// </summary>
        public string Ticket { get; set; }
    }

    /// <summary>
    /// 上报地理位置事件消息 Event = LOCATION
    /// </summary>
    public class UploadLocationEventMessage : EventMessage
    {
        /// <summary>
        /// 地理位置纬度
        /// </summary>
        public string Latitude { get; set; }

        /// <summary>
        /// 地理位置经度
        /// </summary>
        public string Longitude { get; set; }

        /// <summary>
        /// 地理位置精度 
        /// </summary>
        public string Precision { get; set; }
    }

    /// <summary>
    /// 自定义菜单事件 Event = CLICK
    /// </summary>
    public class MenuEventMessage : EventMessage
    {
        /// <summary>
        /// 事件KEY值，与自定义菜单接口中KEY值对应
        /// </summary>
        public string EventKey { get; set; }
    }
}
