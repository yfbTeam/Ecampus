using SCBLL;
using SCModel;
using SCUtility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace SCHandler.StuAssociate
{
    /// <summary>
    /// Asso_ActivityHandler 的摘要说明
    /// </summary>
    public class Asso_ActivityHandler : IHttpHandler
    {
        Asso_ActivityService bll = new Asso_ActivityService();
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
                    case "GetAssoActivityDataPage":
                        GetAssoActivityDataPage(context);
                        break;
                    case "GetAssoActivityById":
                        GetAssoActivityById(context);
                        break;
                    case "AddAssoActivity":
                        AddAssoActivity(context);
                        break;
                    case "EditAssoActivity":
                        EditAssoActivity(context);
                        break;
                    case "DelAssoActivity":
                        DelAssoActivity(context);
                        break;
                    case "BrowsingTimesSet":
                        BrowsingTimesSet(context);
                        break;
                    case "JoinAssoActivity":
                        JoinAssoActivity(context);
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

        #region 获取社团活动表的分页数据
        private void GetAssoActivityDataPage(HttpContext context)
        {
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("Id", context.Request["Id"] ?? "");
                ht.Add("Name", context.Request["Name"] ?? "");
                ht.Add("AssoId", context.Request["AssoId"] ?? "");
                ht.Add("ActStatus", context.Request["ActStatus"] ?? "");
                ht.Add("MyUserNo", context.Request["MyUserNo"] ?? "");
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

        #region 根据Id获取社团活动详情
        private void GetAssoActivityById(HttpContext context)
        {
            int itemid = Convert.ToInt32(context.Request["ItemId"]);
            jsonModel = bll.GetEntityById(itemid);
        }
        #endregion 

        #region 添加社团活动
        private void AddAssoActivity(HttpContext context)
        {
            Asso_Activity item = new Asso_Activity();
            item.Id = 0;
            item.AssoId = Convert.ToInt32(context.Request["AssoId"]);
            item.Name = context.Request["Name"];
            item.StartTime =Convert.ToDateTime(context.Request["StartTime"]);
            item.EndTime = Convert.ToDateTime(context.Request["EndTime"]);
            item.Address = context.Request["Address"];
            item.Content = HttpUtility.UrlDecode(context.Request["Content"]); 
            item.AttachUrl =context.Request["AttachUrl"];
            item.ActivityImg = context.Request["ActivityImg"];
            item.CreateUID = context.Request["LoginUID"];
            jsonModel = bll.EditAssoActivity(item);
        }
        #endregion        

        #region 修改社团活动
        private void EditAssoActivity(HttpContext context)
        {
            int itemid = Convert.ToInt32(context.Request["ItemId"]);
            jsonModel = bll.GetEntityById(itemid);
            if (jsonModel.errNum == 0)
            {
                Asso_Activity item = jsonModel.retData as Asso_Activity;
                item.Id = itemid;
                item.Name = context.Request["Name"];
                item.StartTime = Convert.ToDateTime(context.Request["StartTime"]);
                item.EndTime = Convert.ToDateTime(context.Request["EndTime"]);
                item.Address = context.Request["Address"];
                item.Content = HttpUtility.UrlDecode(context.Request["Content"]);
                item.AttachUrl = context.Request["AttachUrl"];
                item.ActivityImg = context.Request["ActivityImg"];
                item.EditUID = context.Request["LoginUID"];
                jsonModel = bll.EditAssoActivity(item);
            }
        }
        #endregion

        #region 删除社团活动
        private void DelAssoActivity(HttpContext context)
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
                Asso_Activity item = jsonModel.retData as Asso_Activity;
                item.Id = itemid;
                item.BrowsingTimes = item.BrowsingTimes + 1;
                jsonModel = bll.Update(item);
            }
        }
        #endregion       

        #region 报名社团活动
        private void JoinAssoActivity(HttpContext context)
        {
            int itemid = Convert.ToInt32(context.Request["ItemId"]);
            jsonModel = bll.GetEntityById(itemid);
            if (jsonModel.errNum == 0)
            {
                string member=context.Request["LoginUID"];
                Asso_Activity item = jsonModel.retData as Asso_Activity;
                item.Id = itemid;
                if (Convert.ToDateTime(item.StartTime).Date <= DateTime.Now.Date && DateTime.Now.Date <= Convert.ToDateTime(item.EndTime).Date)
                {
                    jsonModel.errNum = -1; //活动进行中
                }
                else if (DateTime.Now.Date > Convert.ToDateTime(item.EndTime).Date)
                {
                    jsonModel.errNum = -2; //活动已结束
                }
                else
                {
                    item.JoinMembers += !string.IsNullOrEmpty(item.JoinMembers) ? ("," + member) : member;
                    jsonModel = bll.Update(item);
                }                
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