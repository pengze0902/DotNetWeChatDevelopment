using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WeChatAPI.DAL;

namespace WeChatAPI.Page
{
    public partial class Statistics : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 用户分析数据接口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void UserAnalysis_Click(object sender, EventArgs e)
        {
            Response.Write(StatisticsOperation.UserAnalysisMethod());
        }

        /// <summary>
        /// 图文分析数据接口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GraphicAnalysis_Click(object sender, EventArgs e)
        {
           Response.Write(StatisticsOperation.GetDailyGraphicMassMethod());
        }

        /// <summary>
        /// 消息分析数据接口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void NewsAnalysis_Click(object sender, EventArgs e)
        {
            Response.Write(StatisticsOperation.GetMessageBeforeSendingMethod());
        }

        /// <summary>
        /// 接口分析数据接口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void InterfaceAnalysis_Click(object sender, EventArgs e)
        {
            Response.Write(StatisticsOperation.GetInterfaceAnalysisMethod());
        }
    }
}