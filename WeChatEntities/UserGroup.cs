using System.Collections.Generic;

namespace WeChatEntities
{
    /// <summary>
    /// 用户分组
    /// </summary>
    public class Groups
    {
        /// <summary>
        /// 公众平台分组信息列表 
        /// </summary>
        public List<Group> groups { get; set; }
    }

    /// <summary>
    /// 分组信息
    /// </summary>
    public class Group
    {
        /// <summary>
        /// 分组id，由微信分配 
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 分组名字，UTF8编码 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 分组内用户数量 
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// 创建分组数据包
        /// </summary>
        /// <returns></returns>
        public string ToCreateJsonString()
        {
            var s = "{\"group\":{\"name\":\"" + Name + "\"}}";
            return s;
        }

        /// <summary>
        /// 修改分组信息数据包
        /// </summary>
        /// <returns></returns>
        public string ToModifyJsonString()
        {
            var s = "{\"group\":{\"id\":" + Id + ",\"name\":\"" + Name + "\"}}";
            return s;
        }
    }
}
