using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using WeChatEntities;
using WeChatHelper;
using WeChatHelper.Interface;
using WeChatHelper.Request;
using WeChatHelper.Response;
using WeChatHelper.Util;

namespace WeChatAPI.DAL
{
    public static class UserGroupOperation
    {
        private const string AppId = "wx9c31a4ca3618e66a";
        private const string AppSecret = "cef5289d07d7d4a9a77a7b4d5d671b50";

        /// <summary>
        /// 创建分组
        /// 用户每次只能创建一个分组，返回后，可重新再创建分组
        /// </summary>
        public static CreateGroupResponse CreateGroup(string groupName)
        {
            IMpClient mpClient = new MpClient();
            var response = UserValidityJudgment(mpClient);
            if (response == null)return null;
            var msg = new Group()
            {
                Name = groupName
            };
            //传入对象
            var createRequest = new CreateGroupRequest()
            {
                AccessToken = response.AccessToken.AccessToken,
                SendData = msg.ToCreateJsonString()
            };

            return mpClient.Execute(createRequest);
          
        }


        /// <summary>
        /// 查询所有分组
        /// </summary>
        public static string GetGroups()
        {
            try
            {
                IMpClient mpClient = new MpClient();
                var request = new AccessTokenGetRequest()
                {
                    AppIdInfo = new AppIdInfo() { AppId = AppId, AppSecret = AppSecret }
                };
                var response = mpClient.Execute(request);
                if (response.IsError)
                {
                    return null;
                }

                var createRequest = new GetGroupsRequest()
                {
                    AccessToken = response.AccessToken.AccessToken,
                };

                var createResponse = mpClient.Execute(createRequest);
                if (createResponse.IsError)
                {
                    LogHelper.WriteLog(typeof(UserGroupOperation),createResponse.ErrInfo.ErrMsg);
                    return null;
                }
                var jsonSerializer = new JavaScriptSerializer();
                return  jsonSerializer.Serialize(createResponse.Groups);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(typeof(UserGroupOperation),ex);
                throw new Exception(ex.Message);
            }
           
        }

        /// <summary>
        /// 获取用户分组ID
        /// </summary>
        public static GetUserGroupResponse GetUserGroupId(string userId)
        {
            try
            {
                IMpClient mpClient = new MpClient();
                var request = new AccessTokenGetRequest()
                {
                    AppIdInfo = new AppIdInfo() { AppId = AppId, AppSecret = AppSecret }
                };
                var response = mpClient.Execute(request);
                if (response.IsError)
                {
                    return null;
                }

                var request2 = new GetUserGroupRequest()
                {
                    AccessToken = response.AccessToken.AccessToken,
                    UserId = userId
                };

                return mpClient.Execute(request2);
               
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(typeof(UserGroupOperation),ex);
                throw new Exception(ex.Message);
            }
            
        }

        /// <summary>
        /// 修改分组
        /// </summary>
        public static void ModifyGroup()
        {
            try
            {
                IMpClient mpClient = new MpClient();
                var request = new AccessTokenGetRequest()
                {
                    AppIdInfo = new AppIdInfo() { AppId = AppId, AppSecret = AppSecret }
                };
                var response = mpClient.Execute(request);
                if (response.IsError)
                {
                    return;
                }

                var groupInfo = new Group()
                {
                    Id = "101",
                    Name = "修改后的分组"
                };

                var request2 = new ModifyGroupRequest()
                {
                    AccessToken = response.AccessToken.AccessToken,
                    GroupInfo = groupInfo,
                    SendData = groupInfo.ToModifyJsonString()
                };

                var response2 = mpClient.Execute(request2);
                if (response2.IsError)
                {
                    LogHelper.WriteLog(typeof(UserGroupOperation),response2.ErrInfo.ErrMsg);
                }
                else
                {
                    GetGroups(); //查询一把，显示一下信息
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(typeof(UserGroupOperation),ex);
                throw new Exception(ex.Message);
            }
           
        }

        /// <summary>
        /// 移动用户分组测试
        /// </summary>
        public static void SetUserGroup(string userId,string groupId)
        {
            try
            {
                IMpClient mpClient = new MpClient();
                var request = new AccessTokenGetRequest()
                {
                    AppIdInfo = new AppIdInfo() { AppId = AppId, AppSecret = AppSecret }
                };
                var response = mpClient.Execute(request);
                if (response.IsError)
                {
                    return;
                }

                var request2 = new SetUserGroupRequest()
                {
                    AccessToken = response.AccessToken.AccessToken,
                    UserId = userId,
                    ToGroupId = groupId
                };

                var response2 = mpClient.Execute(request2);
                if (response2.IsError)
                {
                    LogHelper.WriteLog(typeof(UserGroupOperation),response2.ErrInfo.ErrMsg);
                }
                else
                {
                    GetGroups(); //查询一把，显示一下信息
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(typeof(UserGroupOperation),ex);
                throw new Exception(ex.Message);
            }
           
        }

        /// <summary>
        /// 获取用户基本信息
        /// </summary>
        public static string GetUserInfo(string openId)
        {
            try
            {
                IMpClient mpClient = new MpClient();
                var request = new AccessTokenGetRequest()
                {
                    AppIdInfo = new AppIdInfo() { AppId = AppId, AppSecret = AppSecret }
                };
                var response = mpClient.Execute(request);
                if (response.IsError)
                {
                    return null;
                }

                var request2 = new GetUserInfoRequest()
                {
                    AccessToken = response.AccessToken.AccessToken,
                    OpenId = openId
                };

                var response2 = mpClient.Execute(request2);

                return Tools.ToJsonString(response2.UserInfo);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(typeof(UserGroupOperation),ex.Message);
                throw new Exception(ex.Message);
            }
            
        }

        /// <summary>
        /// 获取关注者列表测试
        /// </summary>
        public static string GetAttentions()
        {
            try
            {
                IMpClient mpClient = new MpClient();
                var request = new AccessTokenGetRequest()
                {
                    AppIdInfo = new AppIdInfo() { AppId = AppId, AppSecret = AppSecret }
                };
                var response = mpClient.Execute(request);
                if (response.IsError)
                {
                    return null;
                }

                var request2 = new GetAttentionsRequest()
                {
                    AccessToken = response.AccessToken.AccessToken
                };

                var response2 = mpClient.Execute(request2);
                if (response2.IsError)
                {
                    LogHelper.WriteLog(typeof(UserGroupOperation),response2.ErrInfo.ErrMsg);
                    return null;
                }
                return Tools.ToJsonString(response2.Attentions);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(typeof(UserGroupOperation),ex);
                throw new Exception(ex.Message);
            }
          
        }

        private static AccessTokenGetResponse UserValidityJudgment(IMpClient mpClient)
        {
            try
            {
                var request = new AccessTokenGetRequest()
                {
                    AppIdInfo = new AppIdInfo() { AppId = AppId, AppSecret = AppSecret }
                };
                return mpClient.Execute(request);                
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(typeof(UserGroupOperation),ex);
                throw new Exception(ex.Message);
            }
           
        }
    }
}