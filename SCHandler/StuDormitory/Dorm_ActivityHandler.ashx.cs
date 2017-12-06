using SCBLL;
using SCModel;
using SCUtility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace SCHandler.StuDormitory
{
    /// <summary>
    /// Dorm_ActivityHandler 的摘要说明
    /// </summary>
    public class Dorm_ActivityHandler : IHttpHandler
    {
        Dorm_ActivityService bll = new Dorm_ActivityService();
        JsonModel jsonModel = new JsonModel() { errNum = 0, errMsg = "success", retData = "" };
        JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();
        GetUserNameHandler nameCommon = new GetUserNameHandler();
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string func = context.Request["Func"];
            string result = string.Empty;
            try
            {
                switch (func)
                {
                    case "GetDormActivityDataPage":
                        GetDormActivityDataPage(context);
                        break;
                    case "GetDormActivityById":
                        GetDormActivityById(context);
                        break;
                    case "AddDormActivity":
                        AddDormActivity(context);
                        break;
                    case "EditDormActivity":
                        EditDormActivity(context);
                        break;
                    case "DelDormActivity":
                        DelDormActivity(context);
                        break;
                    case "BrowsingTimesSet":
                        BrowsingTimesSet(context);
                        break;
                    default:
                        jsonModel = new JsonModel()
                        {
                            errNum = 404,
                            errMsg = "没有此方法",
                            retData = ""
                        };
                        break;
                }
                LogService.WriteLog(func);
            }
            catch (Exception ex)
            {
                jsonModel = new JsonModel()
                {
                    errNum = 400,
                    errMsg = ex.Message,
                    retData = ""
                };
                LogService.WriteErrorLog(ex.Message);
            }
            result = "{\"result\":" + jss.Serialize(jsonModel) + "}";
            context.Response.Write(result);
            context.Response.End();
        }

        #region 获取宿舍活动表的分页数据
        private void GetDormActivityDataPage(HttpContext context)
        {
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("Id", context.Request["Id"] ?? "");
                ht.Add("Name", context.Request["Name"] ?? "");
                ht.Add("RoomId", context.Request["RoomId"] ?? "");
                ht.Add("ActStatus", context.Request["ActStatus"] ?? "");
                ht.Add("LoginUID", context.Request["LoginUID"] ?? "");
                ht.Add("IsOnlyBase", context.Request["IsOnlyBase"] ?? "0"); //是否只需要表中基本信息 0是（默认）；1不是         
                bool ispage = true;
                if (!string.IsNullOrEmpty(context.Request["ispage"]))
                {
                    ispage = Convert.ToBoolean(context.Request["ispage"]);
                }
                ht.Add("PageIndex", context.Request["PageIndex"] ?? "1");
                ht.Add("PageSize", context.Request["PageSize"] ?? "10");
                jsonModel = bll.GetPage(ht, ispage);
                string isUnifiedInfo = context.Request["IsUnifiedInfo"] ?? "0";//是否需要用户信息 0不需要（默认）；1需要
                if (isUnifiedInfo != "0")
                {
                    jsonModel = nameCommon.AddCreateNameForData(jsonModel, 4, ispage);
                }                
            }
            catch (Exception ex)
            {
                jsonModel = new JsonModel()
                {
                    errNum = 400,
                    errMsg = ex.Message,
                    retData = ""
                };
                LogService.WriteErrorLog(ex.Message);
            }
        }
        #endregion

        #region 根据Id获取宿舍活动详情
        private void GetDormActivityById(HttpContext context)
        {
            int itemid = Convert.ToInt32(context.Request["ItemId"]);
            jsonModel = bll.GetEntityById(itemid);
        }
        #endregion 

        #region 添加宿舍活动
        private void AddDormActivity(HttpContext context)
        {
            Dorm_Activity item = new Dorm_Activity();
            item.Id = 0;
            item.RoomId = Convert.ToInt32(context.Request["RoomId"]);
            item.Name = context.Request["Name"];
            item.StartTime = Convert.ToDateTime(context.Request["StartTime"]);
            item.EndTime = Convert.ToDateTime(context.Request["EndTime"]);
            item.Address = context.Request["Address"];
            item.Content = context.Request["Content"];
            item.AttachUrl = context.Request["AttachUrl"];
            item.ActivityImg = context.Request["ActivityImg"];
            item.CreateUID = context.Request["LoginUID"];
            jsonModel = bll.EditDormActivity(item);
        }
        #endregion        

        #region 修改宿舍活动
        private void EditDormActivity(HttpContext context)
        {
            int itemid = Convert.ToInt32(context.Request["ItemId"]);
            jsonModel = bll.GetEntityById(itemid);
            if (jsonModel.errNum == 0)
            {
                Dorm_Activity item = jsonModel.retData as Dorm_Activity;
                item.Id = itemid;
                item.Name = context.Request["Name"];
                item.StartTime = Convert.ToDateTime(context.Request["StartTime"]);
                item.EndTime = Convert.ToDateTime(context.Request["EndTime"]);
                item.Address = context.Request["Address"];
                item.Content = context.Request["Content"];
                item.AttachUrl = context.Request["AttachUrl"];
                item.ActivityImg = context.Request["ActivityImg"];
                item.EditUID = context.Request["LoginUID"];
                jsonModel = bll.EditDormActivity(item);
            }
        }
        #endregion

        #region 删除宿舍活动
        private void DelDormActivity(HttpContext context)
        {
            int itemid = Convert.ToInt32(context.Request["ItemId"]);
            jsonModel = bll.DeleteFalse(itemid);
        }
        #endregion

        #region 浏览次数设置
        private void BrowsingTimesSet(HttpContext context)
        {
            int itemid = Convert.ToInt32(context.Request["ItemId"]);
            jsonModel = bll.GetEntityById(itemid);
            if (jsonModel.errNum == 0)
            {
                Dorm_Activity item = jsonModel.retData as Dorm_Activity;
                item.Id = itemid;
                item.BrowsingTimes = item.BrowsingTimes + 1;
                jsonModel = bll.Update(item);
            }
        }
        #endregion      
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}