using System.Collections.Generic;

namespace WeChatEntities
{
    /// <summary>
    /// 菜单组
    /// </summary>
    public class MenuGroup
    {
        public Menu Menu { get; set; }

        /// <summary>
        /// 组装POST到公众平台的菜单Json字符串
        /// </summary>
        /// <returns></returns>
        public string  ToJsonString()
        {
            var s1 = string.Empty;
            for (var i = 0; i < Menu.Button.Count; i++)
            {
                if (i > 0)
                {
                    s1 += ",";
                }
                s1 += Menu.Button[i].ToJsonString();
            }
            var s = "{ \"button\":[" + s1 + "]}";
            return s;
        }
    }

    /// <summary>
    /// 自定义菜单
    /// </summary>
    public class Menu
    {
        public List<Button> Button { get; set; }
    }

    /// <summary>
    /// 自定义菜单按钮
    /// </summary>
    public class Button
    {
        /// <summary>
        /// 菜单的响应动作类型，目前有click、view两种类型 
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 菜单标题，不超过16个字节，子菜单不超过40个字节 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 菜单KEY值，用于消息接口推送，不超过128字节 
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// 网页链接，用户点击菜单可打开链接，不超过256字节 
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 二级菜单数组，个数应为1~5个 
        /// </summary>
        public List<Button> SubButton { get; set; }

        public string ToJsonString()
        {
            if (SubButton != null && SubButton.Count > 0)
            {
                var s1 = string.Empty;
                for (var i = 0; i < SubButton.Count; i++)
                {
                    if (i > 0)
                    {
                        s1 += ",";
                    }
                    s1 += SubButton[i].ToJsonString();
                }
                var s = " {\"name\":\"" + Name + "\", \"sub_button\":[" + s1 + "]}";
                return s;
            }
            if (Type == "click")
            {
                return "{ \"type\":\"click\", \"name\":\"" + Name + "\",\"key\":\"" + Key + "\" }";
            }
            return "{ \"type\":\"view\", \"name\":\"" + Name + "\",\"url\":\"" + Url + "\" }";
        }
    }
}

