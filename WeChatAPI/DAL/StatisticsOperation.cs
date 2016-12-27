using System;
using System.Web.Script.Serialization;
using WeChatEntities;
using WeChatHelper;
using WeChatHelper.Interface;
using WeChatHelper.Request;
using WeChatHelper.Response;

namespace WeChatAPI.DAL
{
    public static class StatisticsOperation
    {
        private const string AppId = "wx9c31a4ca3618e66a";
        private const string AppSecret = "cef5289d07d7d4a9a77a7b4d5d671b50";
        public static string UserAnalysisMethod()
        {
            string StartDate = "2016-08-01";
            string EndDate = "2016-08-07";
            // DateTime StartDate=DateTime.Parse("2016-08-01");
            //DateTime EndDate=DateTime.Parse("2016-08-04");
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

                var startDate=new Statistics
                {
                   BeginDate = StartDate,
                   EndDate = EndDate
                };

                var createRequest = new GetCutBackStatisticsRequest
                {
                    AccessToken = response.AccessToken.AccessToken,
                    SendData = startDate.ToCreateJsonString()
                };

                var createResponse = mpClient.Execute(createRequest);
                if (createResponse.IsError)
                {
                    LogHelper.WriteLog(typeof(StatisticsOperation), createResponse.ErrInfo.ErrMsg);
                    return createResponse.ErrInfo.ErrMsg;
                }
                var jsonSerializer = new JavaScriptSerializer();
                return jsonSerializer.Serialize(createResponse.StatisticalDataListResponse);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(typeof(StatisticsOperation), ex);
                throw new Exception(ex.Message);
            }
        }

        public static string GetDailyGraphicMassMethod()
        {
            //DateTime StartDate = DateTime.Parse("2016-08-01");
            //DateTime EndDate = DateTime.Parse("2016-08-04");
            string StartDate = "2016-08-01";
            string EndDate = "2016-08-01";
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

                var startDate = new Statistics
                {
                    BeginDate = StartDate,
                    EndDate = EndDate
                };

                var createRequest = new GetDailyGraphicMassRequest
                {
                    AccessToken = response.AccessToken.AccessToken,
                    SendData = startDate.ToCreateJsonString()
                };

                var createResponse = mpClient.Execute(createRequest);
                if (createResponse.IsError)
                {
                    LogHelper.WriteLog(typeof(StatisticsOperation), createResponse.ErrInfo.ErrMsg);
                    return createResponse.ErrInfo.ErrMsg;
                }
                var jsonSerializer = new JavaScriptSerializer();
                return jsonSerializer.Serialize(createResponse.DailyGraphicBulkStatistics);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(typeof(StatisticsOperation), ex);
                throw new Exception(ex.Message);
            }
        }

        public static string GetMessageBeforeSendingMethod()
        {
            //DateTime StartDate = DateTime.Parse("2016-08-01");
            //DateTime EndDate = DateTime.Parse("2016-08-04");
            string StartDate = "2016-08-01";
            string EndDate = "2016-08-02";
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

                var startDate = new Statistics
                {
                    BeginDate = StartDate,
                    EndDate = EndDate
                };

                var createRequest = new GetMessageBeforeSendingRequest
                {
                    AccessToken = response.AccessToken.AccessToken,
                    SendData = startDate.ToCreateJsonString()
                };

                var createResponse = mpClient.Execute(createRequest);
                if (createResponse.IsError)
                {
                    LogHelper.WriteLog(typeof(StatisticsOperation), createResponse.ErrInfo.ErrMsg);
                    return createResponse.ErrInfo.ErrMsg;
                }
                var jsonSerializer = new JavaScriptSerializer();
                return jsonSerializer.Serialize(createResponse.NewsOverview);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(typeof(StatisticsOperation), ex);
                throw new Exception(ex.Message);
            }
        }

        public static string GetInterfaceAnalysisMethod()
        {
            //DateTime StartDate = DateTime.Parse("2016-08-01");
            //DateTime EndDate = DateTime.Parse("2016-08-04");
            string StartDate = "2016-08-01";
            string EndDate = "2016-08-02";
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

                var startDate = new Statistics
                {
                    BeginDate = StartDate,
                    EndDate = EndDate
                };

                var createRequest = new GetInterfaceAnalysisRequest
                {
                    AccessToken = response.AccessToken.AccessToken,
                    SendData = startDate.ToCreateJsonString()
                };

                var createResponse = mpClient.Execute(createRequest);
                if (createResponse.IsError)
                {
                    LogHelper.WriteLog(typeof(StatisticsOperation), createResponse.ErrInfo.ErrMsg);
                    return createResponse.ErrInfo.ErrMsg;
                }
                var jsonSerializer = new JavaScriptSerializer();
                return jsonSerializer.Serialize(createResponse.InterfaceAnalysis);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(typeof(StatisticsOperation), ex);
                throw new Exception(ex.Message);
            }
        }
    }
}