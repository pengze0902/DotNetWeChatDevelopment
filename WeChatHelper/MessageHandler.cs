using System;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Xml;
using WeChatEntities;
using WeChatHelper.Interface;
using WeChatHelper.Request;
using WeChatHelper.Response;
using WeChatHelper.Util;

namespace WeChatHelper
{
    /// <summary>
    /// 消息处理类
    /// </summary>
    public class MessageHandler
    {
        /// <summary>
        /// 校验消息真实性，验证请求是否合法
        /// </summary>
        /// <param name="token">用户在公众平台填写的token</param>
        /// <returns></returns>
        public static bool CheckSignature(string token)
        {

            var signature = HttpContext.Current.Request.QueryString["signature"] == null ? "" : HttpContext.Current.Request.QueryString["signature"].Trim();
            var timestamp = HttpContext.Current.Request.QueryString["timestamp"] == null ? "" : HttpContext.Current.Request.QueryString["timestamp"].Trim();
            var nonce = HttpContext.Current.Request.QueryString["nonce"] == null ? "" : HttpContext.Current.Request.QueryString["nonce"].Trim();
            string[] arrTmp = { token, timestamp, nonce };
            Array.Sort(arrTmp);
            var tmpStr = string.Join("", arrTmp);
            tmpStr = Sha1UtilHelper.GetSha1(tmpStr);
            tmpStr = tmpStr?.ToLower();
            return tmpStr == signature;
        }

        /// <summary>
        /// 第一次接入时验证
        /// </summary>
        /// <param name="token">用户在公众平台填写的token</param>
        public static void Valid(string token)
        {
            if (HttpContext.Current.Request.QueryString["echoStr"] == null)
            {
                HttpContext.Current.Response.Write("Null");
                HttpContext.Current.Response.End();
                return;
            }
            var echoStr = HttpContext.Current.Request.QueryString["echoStr"];
            if (CheckSignature(token))
            {
                if (string.IsNullOrEmpty(echoStr)) return;
                HttpContext.Current.Response.Write(echoStr);
                HttpContext.Current.Response.End();
            }
            else
            {
                HttpContext.Current.Response.Write(Guid.NewGuid().ToString());
                HttpContext.Current.Response.End();
            }
        }

        /// <summary>
        /// 将公众平台POST过来的数据转化成实体对象
        /// </summary>
        /// <param name="token">用户在公众平台填写的token</param>
        /// <returns>消息处理的基类</returns>
        public static ReceiveMessageBase ConvertMsgToObject(string token)
        {
            if (!CheckSignature(token))
            {
                return null;
            }
            //获取传入的HTTP消息
            var s = HttpContext.Current.Request.InputStream;
            var b = new byte[s.Length];
            s.Read(b, 0, (int)s.Length);
            var msgBody = Encoding.UTF8.GetString(b);
            if (string.IsNullOrWhiteSpace(msgBody))
            {
                return null;
            }
            var msgType = MsgType.UnKnown;
            //获取消息主体和设置消息类型枚举
            var msg = new ReceiveMessageBase
            {
                MsgType = msgType,
                MessageBody = msgBody
            };

            try
            {
                //读取XML字符串，返回消息为xml文档类型
                var doc = new XmlDocument();                
                doc.LoadXml(msgBody);
                //获取xml根节点
                var rootElement = doc.DocumentElement;
                if (rootElement != null)
                {
                    //获取字符串中的消息类型，搜索xml消息节点信息
                    var msgTypeNode = rootElement.SelectSingleNode("MsgType");
                    var node = rootElement.SelectSingleNode("FromUserName");
                    if (node != null)
                    {
                        msg.FromUserName = node.InnerText;
                    }
                    node = rootElement.SelectSingleNode("ToUserName");
                    if (node != null)
                    {
                        msg.ToUserName = node.InnerText;
                    }
                    node = rootElement.SelectSingleNode("CreateTime");
                    if (node != null)
                    {
                        msg.CreateTime = Convert.ToInt64(node.InnerText);
                    }
                    //根据获取传入的消息主体数据，判断消息类型，获取具体的消息对象
                    if (msgTypeNode != null)
                    {
                        var strMsgType = msgTypeNode.InnerText;
                        var msgId = string.Empty;
                        var content = string.Empty;
                        var tmpNode = rootElement.SelectSingleNode("MsgId");
                        if (tmpNode != null)
                        {
                            msgId = tmpNode.InnerText.Trim();
                        }

                        switch (strMsgType)
                        {
                            case "text":
                                msgType = MsgType.Text;
                                //获取文本消息内容
                                tmpNode = rootElement.SelectSingleNode("Content");
                                if (tmpNode != null)
                                {
                                    content = tmpNode.InnerText.Trim();
                                }
                                var txtMsg = new TextReceiveMessage()
                                {
                                    CreateTime = msg.CreateTime,
                                    FromUserName = msg.FromUserName,
                                    MessageBody = msg.MessageBody,
                                    MsgType = msgType,
                                    ToUserName = msg.ToUserName,
                                    MsgId = Convert.ToInt64(msgId),
                                    Content = content
                                };
                                return txtMsg;
                            case "image":
                                msgType = MsgType.Image;
                                ImageReceiveMessage imgMsg = null;
                                var selectSingleNode = rootElement.SelectSingleNode("MediaId");
                                if (selectSingleNode != null)
                                {
                                    var singleNode = rootElement.SelectSingleNode("PicUrl");
                                    if (singleNode != null)
                                        imgMsg = new ImageReceiveMessage()
                                        {
                                            CreateTime = msg.CreateTime,
                                            FromUserName = msg.FromUserName,
                                            MessageBody = msg.MessageBody,
                                            MsgId = Convert.ToInt64(msgId),
                                            MsgType = msgType,
                                            ToUserName = msg.ToUserName,
                                            MediaId = selectSingleNode.InnerText,
                                            PicUrl = singleNode.InnerText
                                        };
                                }
                                return imgMsg;
                            case "voice":
                                msgType = MsgType.Voice;
                                VoiceReceiveMessage voiceMsg = null;
                                var node1 = rootElement.SelectSingleNode("Recognition");
                                if (node1 != null)
                                {
                                    msgType = MsgType.VoiceResult;
                                }
                                var xmlNode = rootElement.SelectSingleNode("Format");
                                if (xmlNode != null)
                                {
                                    var selectSingleNode1 = rootElement.SelectSingleNode("MediaId");
                                    if (selectSingleNode1 != null)
                                        voiceMsg = new VoiceReceiveMessage()
                                        {
                                            CreateTime = msg.CreateTime,
                                            FromUserName = msg.FromUserName,
                                            ToUserName = msg.ToUserName,
                                            MessageBody = msg.MessageBody,
                                            MsgId = Convert.ToInt64(msgId),
                                            MsgType = msgType,
                                            Recognition = node1?.InnerText.Trim() ?? string.Empty,
                                            Format = xmlNode.InnerText,
                                            MediaId = selectSingleNode1.InnerText
                                        };
                                }

                                return voiceMsg;
                            case "video":
                                msgType = MsgType.Video;
                                VideoReceiveMessage videoMsg = null;
                                var singleNode1 = rootElement.SelectSingleNode("MediaId");
                                if (singleNode1 != null)
                                {
                                    var singleNode = rootElement.SelectSingleNode("ThumbMediaId");
                                    if (singleNode != null)
                                    {
                                        videoMsg = new VideoReceiveMessage()
                                        {
                                            CreateTime = msg.CreateTime,
                                            FromUserName = msg.FromUserName,
                                            MediaId = singleNode1.InnerText,
                                            MessageBody = msg.MessageBody,
                                            MsgId = Convert.ToInt64(msgId),
                                            MsgType = msgType,
                                            ToUserName = msg.ToUserName,
                                            ThumbMediaId = singleNode.InnerText
                                        };
                                    }
                                }
                                return videoMsg;
                            case "location":
                                msgType = MsgType.Location;
                                LocationReceiveMessage locationMsg = null;
                                var xmlNode1 = rootElement.SelectSingleNode("Label");
                                if (xmlNode1 != null)
                                {
                                    var selectSingleNode2 = rootElement.SelectSingleNode("Location_X");
                                    if (selectSingleNode2 == null) return null;
                                    var singleNode = rootElement.SelectSingleNode("Location_Y ");
                                    if (singleNode == null) return null;
                                    var selectSingleNode1 = rootElement.SelectSingleNode("Scale");
                                    if (selectSingleNode1 != null)
                                        locationMsg = new LocationReceiveMessage()
                                        {
                                            CreateTime = msg.CreateTime,
                                            FromUserName = msg.FromUserName,
                                            MessageBody = msg.MessageBody,
                                            MsgId = Convert.ToInt64(msgId),
                                            MsgType = msgType,
                                            ToUserName = msg.ToUserName,
                                            Label = xmlNode1.InnerText,
                                            LocationX = selectSingleNode2.InnerText,
                                            LocationY = singleNode.InnerText,
                                            Scale = selectSingleNode1.InnerText
                                        };
                                }
                                return locationMsg;
                            case "link":
                                msgType = MsgType.Link;
                                LinkReceiveMessage linkMsg = null;
                                var singleNode2 = rootElement.SelectSingleNode("Description");
                                if (singleNode2 != null)
                                {
                                    var xmlNode2 = rootElement.SelectSingleNode("Title");
                                    if (xmlNode2 == null) return (LinkReceiveMessage) null;
                                    var singleNode = rootElement.SelectSingleNode("Url ");
                                    if (singleNode != null)
                                        linkMsg = new LinkReceiveMessage()
                                        {
                                            CreateTime = msg.CreateTime,
                                            Description = singleNode2.InnerText,
                                            FromUserName = msg.FromUserName,
                                            MessageBody = msg.MessageBody,
                                            MsgId = Convert.ToInt64(msgId),
                                            MsgType = msgType,
                                            Title = xmlNode2.InnerText,
                                            ToUserName = msg.ToUserName,
                                            Url = singleNode.InnerText
                                        };
                                }
                                return linkMsg;
                            case "event":
                                msgType = MsgType.Event;
                                msg.MsgType = msgType;
                                ScanSubscribeEventMessage scanSubEvt = null;
                                var eventNode = rootElement.SelectSingleNode("Event");
                                if (eventNode != null)
                                {
                                    EventType eventType;
                                    switch (eventNode.InnerText)
                                    {
                                        case "subscribe":
                                            tmpNode = rootElement.SelectSingleNode("EventKey");
                                            if (tmpNode != null)
                                                // && (!string.IsNullOrWhiteSpace(tmpNode.InnerText)))  //&& tmpNode.InnerText.StartsWith("qrscene_")
                                            {
                                                //扫描二维码关注事件
                                                var singleNode = rootElement.SelectSingleNode("EventKey");
                                                if (singleNode == null) return null;
                                                var selectSingleNode1 = rootElement.SelectSingleNode("Ticket");
                                                if (selectSingleNode1 != null)
                                                    scanSubEvt = new ScanSubscribeEventMessage()
                                                    {
                                                        CreateTime = msg.CreateTime,
                                                        EventKey = singleNode.InnerText,
                                                        EventType = EventType.Subscribe,
                                                        FromUserName = msg.FromUserName,
                                                        MessageBody = msg.MessageBody,
                                                        MsgType = msgType,
                                                        ToUserName = msg.ToUserName,
                                                        Ticket = selectSingleNode1.InnerText
                                                    };
                                                return scanSubEvt;
                                            }
                                            else
                                            {
                                                //普通关注事件
                                                var subEvt = new SubscribeEventMessage()
                                                {
                                                    CreateTime = msg.CreateTime,
                                                    EventType = EventType.Subscribe,
                                                    FromUserName = msg.FromUserName,
                                                    MessageBody = msg.MessageBody,
                                                    MsgType = msgType,
                                                    ToUserName = msg.ToUserName
                                                };
                                                return subEvt;
                                            }
                                        case "unsubscribe":
                                            eventType = EventType.UnSubscribe;
                                            var unSubEvt = new UnSubscribeEventMessage()
                                            {
                                                CreateTime = msg.CreateTime,
                                                EventType = eventType,
                                                FromUserName = msg.FromUserName,
                                                MessageBody = msg.MessageBody,
                                                MsgType = msgType,
                                                ToUserName = msg.ToUserName
                                            };
                                            return unSubEvt;
                                        case "scan":
                                            eventType = EventType.Scan;
                                            ScanEventMessage scanEvt = null;
                                            var selectSingleNode2 = rootElement.SelectSingleNode("EventKey");
                                            if (selectSingleNode2 != null)
                                            {
                                                var xmlNode2 = rootElement.SelectSingleNode("Ticket");
                                                if (xmlNode2 != null)
                                                    scanEvt = new ScanEventMessage()
                                                    {
                                                        CreateTime = msg.CreateTime,
                                                        EventKey = selectSingleNode2.InnerText,
                                                        EventType = eventType,
                                                        FromUserName = msg.FromUserName,
                                                        MessageBody = msg.MessageBody,
                                                        MsgType = msgType,
                                                        Ticket = xmlNode2.InnerText,
                                                        ToUserName = msg.ToUserName
                                                    };
                                            }
                                            return scanEvt;
                                        case "LOCATION":
                                            eventType = EventType.Location;
                                            UploadLocationEventMessage locationEvt = null;
                                            var node2 = rootElement.SelectSingleNode("Latitude");
                                            if (node2 != null)
                                            {
                                                var selectSingleNode3 = rootElement.SelectSingleNode("Longitude");
                                                if (selectSingleNode3 == null) return null;
                                                var singleNode = rootElement.SelectSingleNode("Precision");
                                                if (singleNode != null)
                                                    locationEvt = new UploadLocationEventMessage()
                                                    {
                                                        CreateTime = msg.CreateTime,
                                                        EventType = eventType,
                                                        FromUserName = msg.FromUserName,
                                                        Latitude = node2.InnerText,
                                                        Longitude = selectSingleNode3.InnerText,
                                                        MessageBody = msg.MessageBody,
                                                        MsgType = msgType,
                                                        Precision = singleNode.InnerText,
                                                        ToUserName = msg.ToUserName
                                                    };
                                            }
                                            return locationEvt;
                                        case "CLICK":
                                            eventType = EventType.Click;
                                            MenuEventMessage menuEvt = null;
                                            var singleNode3 = rootElement.SelectSingleNode("EventKey");
                                            if (singleNode3 != null)
                                                menuEvt = new MenuEventMessage()
                                                {
                                                    CreateTime = msg.CreateTime,
                                                    EventKey = singleNode3.InnerText,
                                                    EventType = eventType,
                                                    FromUserName = msg.FromUserName,
                                                    MessageBody = msg.MessageBody,
                                                    MsgType = msgType,
                                                    ToUserName = msg.ToUserName
                                                };
                                            return menuEvt;
                                        default:
                                            var evtMsg = new EventMessage()
                                            {
                                                CreateTime = msg.CreateTime,
                                                EventType = EventType.UnKnown,
                                                FromUserName = msg.FromUserName,
                                                MessageBody = msg.MessageBody,
                                                MsgType = MsgType.Event,
                                                ToUserName = msg.ToUserName
                                            };
                                            return evtMsg;
                                    }
                                }
                                break;
                        }
                    }
                }
                msg.MsgType = msgType;
            }
            finally
            {
                msg.MsgType = msgType;
               
            }
            return msg;
        }

        /// <summary>
        /// 发送被动响应消息(根据传递的参数是对应不同的子类发送不同的子类消息)
        /// </summary>
        /// <param name="msg">发送的消息内容</param>
        /// <returns>是否成功</returns>
        public static void SendReplyMessage(ReplyMessage msg)
        {
            HttpContext.Current.Response.Write(msg.ToXmlString());
        }

        /// <summary>
        /// 发送被动响应文本消息
        /// </summary>
        /// <param name="fromUserName">发送方</param>
        /// <param name="toUserName">接收方</param>
        /// <param name="content">文本内容</param>
        public static void SendTextReplyMessage(string fromUserName, string toUserName, string content)
        {
            var msg = new TextReplyMessage()
            {
                CreateTime = Tools.ConvertDateTimeInt(DateTime.Now),
                FromUserName = fromUserName,
                ToUserName = toUserName,
                Content = content
            };
            HttpContext.Current.Response.Write(msg.ToXmlString());
        }

        /// <summary>
        /// 发送被动响应图片信息，图片上传失败，则返回失败
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <param name="fromUserName">发送方</param>
        /// <param name="toUserName">接收方</param>
        /// <param name="imgPath">图片绝对路径(最大128K，目前只支持jpg格式)</param>
        /// <returns>是否成功</returns>
        public static bool SendImageReplyMessage(string accessToken, string fromUserName, string toUserName, string imgPath)
        {
            IMpClient mpClient = new MpClient();
            var request = new UploadMediaRequest()
            {
                AccessToken = accessToken,
                Type = "image",
                FileName = imgPath
            };

            var response = mpClient.Execute(request);
            if (response.IsError)
            {
                return false;
            }
            var msg = new ImageReplyMessage()
            {
                CreateTime = Tools.ConvertDateTimeInt(DateTime.Now),
                FromUserName = fromUserName,
                ToUserName = toUserName,
                MediaId = response.MediaId
            };
            HttpContext.Current.Response.Write(msg.ToXmlString());
            return true;
        }

        /// <summary>
        /// 发送被动响应语音消息
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <param name="fromUserName">发送方</param>
        /// <param name="toUserName">接收方</param>
        /// <param name="voicePath">语音文件路径(支持AMR\MP3,最大256K，播放长度不超过60s)</param>
        /// <returns>是否成功</returns>
        public static bool SendVoiceReplyMessage(string accessToken, string fromUserName, string toUserName, string voicePath)
        {
            IMpClient mpClient = new MpClient();
            var request = new UploadMediaRequest()
            {
                AccessToken = accessToken,
                Type = "voice",
                FileName = voicePath
            };

            var response = mpClient.Execute(request);
            if (response.IsError)
            {
                return false;
            }
            var msg = new VoiceReplyMessage()
            {
                CreateTime = Tools.ConvertDateTimeInt(DateTime.Now),
                FromUserName = fromUserName,
                ToUserName = toUserName,
                MediaId = response.MediaId
            };
            HttpContext.Current.Response.Write(msg.ToXmlString());
            return true;
        }

        /// <summary>
        /// 发送被动响应视频消息
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <param name="fromUserName">发送方</param>
        /// <param name="toUserName">接收方</param>
        /// <param name="title">标题</param>
        /// <param name="description">描述</param>
        /// <param name="videoPath">视频文件路径(1MB，支持MP4格式)</param>
        /// <returns>是否成功</returns>
        public static bool SendVideoReplyMessage(string accessToken, string fromUserName, string toUserName, string title, string description, string videoPath)
        {
            IMpClient mpClient = new MpClient();
            var request = new UploadMediaRequest()
            {
                AccessToken = accessToken,
                Type = "video",
                FileName = videoPath
            };

            var response = mpClient.Execute(request);
            if (response.IsError)
            {
                return false;
            }
            var msg = new VideoReplyMessage()
            {
                CreateTime = Tools.ConvertDateTimeInt(DateTime.Now),
                FromUserName = fromUserName,
                ToUserName = toUserName,
                MediaId = response.MediaId,
                Description = description,
                Title = title
            };
            HttpContext.Current.Response.Write(msg.ToXmlString());
            return true;
        }

        /// <summary>
        /// 发送被动响应音乐消息
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <param name="fromUserName">发送方</param>
        /// <param name="toUserName">接收方</param>
        /// <param name="title">标题</param>
        /// <param name="description">描述</param>
        /// <param name="musicUrl">音乐链接</param>
        /// <param name="hqMusicUrl">高质量音乐链接</param>
        /// <param name="thumbMediaFilePath">缩略图文件路径(64KB，支持JPG格式 )</param>
        /// <returns>是否成功</returns>
        public static bool SendMusicReplyMessage(string accessToken, string fromUserName, string toUserName, string title, string description, string musicUrl, string hqMusicUrl, string thumbMediaFilePath)
        {
            IMpClient mpClient = new MpClient();
            var request = new UploadMediaRequest()
            {
                AccessToken = accessToken,
                Type = "thumb",
                FileName = thumbMediaFilePath
            };

            var response = mpClient.Execute(request);
            if (response.IsError)
            {
                return false;
            }
            var msg = new MusicReplyMessage()
            {
                CreateTime = Tools.ConvertDateTimeInt(DateTime.Now),
                FromUserName = fromUserName,
                ToUserName = toUserName,
                Description = description,
                Title = title,
                ThumbMediaId = response.MediaId,
                HqMusicUrl = hqMusicUrl,
                MusicUrl = musicUrl
            };
            HttpContext.Current.Response.Write(msg.ToXmlString());
            return true;
        }

        /// <summary>
        /// 发送客服信息
        /// </summary>
        /// <param name="accessToken">调用凭据</param>
        /// <param name="msg">客服消息</param>
        /// <returns></returns>
        public static SendCustomMessageResponse SendCustomMessage(string accessToken, CustomMessage msg)
        {
            IMpClient mpClient = new MpClient();
            var request = new SendCustomMessageRequest
            {
                AccessToken = accessToken,
                SendData = msg.ToJsonString()
            };
            var response = mpClient.Execute(request);
            return response;
        }

        /// <summary>
        /// 发送文本客服信息
        /// </summary>
        /// <param name="accessToken">调用凭据</param>
        /// <param name="toUser">接收方</param>
        /// <param name="content">信息内容</param>
        /// <returns></returns>
        public static SendCustomMessageResponse SendTextCustomMessage(string accessToken, string toUser, string content)
        {
            var msg = new TextCustomMessage()
            {
                AccessToken = accessToken,
                ToUser = toUser,
                Content = content,
                MsgType = "text"
            };
            return SendCustomMessage(accessToken, msg);
        }

        /// <summary>
        /// 发送图片客服消息
        /// </summary>
        /// <param name="accessToken">调用凭据</param>
        /// <param name="toUser">接收方</param>
        /// <param name="imgPath">图片路径</param>
        /// <returns></returns>
        public static SendCustomMessageResponse SendImageCustomMessage(string accessToken, string toUser, string imgPath)
        {
            IMpClient mpClient = new MpClient();
            var request = new UploadMediaRequest()
            {
                AccessToken = accessToken,
                Type = "image",
                FileName = imgPath
            };

            var response = mpClient.Execute(request);
            if (response.IsError)
            {
                var response2 = new SendCustomMessageResponse()
                {
                    Body = response.Body,
                    ErrInfo = response.ErrInfo,
                    ReqUrl = response.ReqUrl
                };
                return response2;
            }
            var msg = new ImageCustomMessage()
            {
                AccessToken = accessToken,
                MediaId = response.MediaId,
                MsgType = "image",
                ToUser = toUser
            };
            return SendCustomMessage(accessToken, msg);
        }

        /// <summary>
        /// 发送语音客服信息
        /// </summary>
        /// <param name="accessToken">调用凭据</param>
        /// <param name="toUser">接收方</param>
        /// <param name="voicePath">语音文件路径</param>
        /// <returns></returns>
        public static SendCustomMessageResponse SendVoiceCustomMessage(string accessToken, string toUser, string voicePath)
        {
            IMpClient mpClient = new MpClient();
            var request = new UploadMediaRequest()
            {
                AccessToken = accessToken,
                Type = "voice",
                FileName = voicePath
            };

            var response = mpClient.Execute(request);
            if (response.IsError)
            {
                var response2 = new SendCustomMessageResponse()
                {
                    Body = response.Body,
                    ErrInfo = response.ErrInfo,
                    ReqUrl = response.ReqUrl
                };
                return response2;
            }
            var msg = new VoiceCustomMessage()
            {
                AccessToken = accessToken,
                MediaId = response.MediaId,
                MsgType = "voice",
                ToUser = toUser
            };
            return SendCustomMessage(accessToken, msg);
        }

        /// <summary>
        /// 发送视频客服信息
        /// </summary>
        /// <param name="accessToken">调用凭据</param>
        /// <param name="toUser">接收方</param>
        /// <param name="title">视频标题</param>
        /// <param name="description">视频描述</param>
        /// <param name="videoPath">视频文件路径</param>
        /// <returns></returns>
        public static SendCustomMessageResponse SendVideoCustomMessage(string accessToken, string toUser, string title, string description, string videoPath)
        {
            IMpClient mpClient = new MpClient();
            var request = new UploadMediaRequest()
            {
                AccessToken = accessToken,
                Type = "video",
                FileName = videoPath
            };

            var response = mpClient.Execute(request);
            if (response.IsError)
            {
                var response2 = new SendCustomMessageResponse()
                {
                    Body = response.Body,
                    ErrInfo = response.ErrInfo,
                    ReqUrl = response.ReqUrl
                };
                return response2;
            }
            var msg = new VideoCustomMessage()
            {
                AccessToken = accessToken,
                MediaId = response.MediaId,
                MsgType = "video",
                ToUser = toUser,
                Description = description,
                Title = title
            };
            return SendCustomMessage(accessToken, msg);
        }

        /// <summary>
        /// 发送音乐客服信息
        /// </summary>
        /// <param name="accessToken">调用凭据</param>
        /// <param name="toUser">接收方</param>
        /// <param name="title">音乐标题</param>
        /// <param name="description">音乐描述</param>
        /// <param name="musicUrl">音乐地址</param>
        /// <param name="hqMusicUrl">高质量音乐地址</param>
        /// <param name="thumbMediaFilePath">音乐缩略图路径</param>
        /// <returns></returns>
        public static SendCustomMessageResponse SendMusicCustomMessage(string accessToken, string toUser, string title, string description, string musicUrl, string hqMusicUrl, string thumbMediaFilePath)
        {
            IMpClient mpClient = new MpClient();
            var request = new UploadMediaRequest()
            {
                AccessToken = accessToken,
                Type = "thumb",
                FileName = thumbMediaFilePath
            };

            var response = mpClient.Execute(request);
            if (response.IsError)
            {
                var response2 = new SendCustomMessageResponse()
                {
                    Body = response.Body,
                    ErrInfo = response.ErrInfo,
                    ReqUrl = response.ReqUrl
                };
                return response2;
            }
            var msg = new MusicCustomMessage()
            {
                AccessToken = accessToken,
                ThumbMediaId = response.MediaId,
                HqMusicUrl = hqMusicUrl,
                MusicUrl = musicUrl,
                MsgType = "music",
                ToUser = toUser,
                Description = description,
                Title = title
            };
            return SendCustomMessage(accessToken, msg);
        }

        /// <summary>
        /// 发送图文客服消息
        /// </summary>
        /// <param name="accessToken">调用凭据</param>
        /// <param name="msg">消息</param>
        /// <returns></returns>
        public static SendCustomMessageResponse SendNewsCustomMessage(string accessToken, NewsCustomMessage msg)
        {
            msg.AccessToken = accessToken;
            return SendCustomMessage(accessToken, msg);
        }

    } 
}
