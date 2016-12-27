using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WeChatAPI.DAL;
using WeChatEntities;
using WeChatHelper;
using WeChatHelper.Interface;
using WeChatHelper.Request;
using WeChatHelper.Util;

namespace WeChatAPI.Page
{
    public partial class CreativeManagement : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 新增临时素材
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void NewCreativeTemporary_Click(object sender, EventArgs e)
        {
            Response.Write(CreativeManagementOperation.NewCreativeTemporaryMethod());
        }

        /// <summary>
        /// 获取临时素材
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GetCreativeTemporary_Click(object sender, EventArgs e)
        {
            //Response.Write(CreativeManagementOperation.GetCreativeTemporaryMethod());
            const string AppId = "wx9c31a4ca3618e66a";
            const string AppSecret = "cef5289d07d7d4a9a77a7b4d5d671b50";
            string MediaId = "7udfbNhsspUAm-y_TGbNBE6liHH7G1WyZefiTiz4Q9dJ_zJQmHh9nHQg46dK1FTG";
            WebUtils utils=new WebUtils();
            IMpClient mpClient = new MpClient();
            var request = new AccessTokenGetRequest()
            {
                AppIdInfo = new AppIdInfo() { AppId = AppId, AppSecret = AppSecret }
            };
            var response = mpClient.Execute(request);
            string urlFormat = string.Format("http://file.api.weixin.qq.com/cgi-bin/media/get?access_token={0}&media_id={1}", response.AccessToken.AccessToken, MediaId);
            var image = utils.GetImage(urlFormat);
            Response.BinaryWrite(image);
        }

        /// <summary>
        /// 新增永久素材
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void NewPermanentMaterial_Click(object sender, EventArgs e)
        {
            Response.Write(CreativeManagementOperation.NewPermanentMaterialMethod());
        }

        /// <summary>
        /// 获取永久素材
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GetPermanentMaterial_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 删除永久素材
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void PermanentlyDeletedMaterial_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 修改永久图文素材
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ModifyPermanentGraphicMaterial_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 获取素材总数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GetsTotalCreative_Click(object sender, EventArgs e)
        {
            Response.Write(CreativeManagementOperation.NumberOfCreativesMethod());
        }

        /// <summary>
        /// 获取素材列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GetCreativeList_Click(object sender, EventArgs e)
        {
            Response.Write(CreativeManagementOperation.GetCreativeList());
        }
    }
}