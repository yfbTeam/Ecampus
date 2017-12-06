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
    /// Asso_ApplyHandler 的摘要说明
    /// </summary>
    public class Asso_ApplyHandler : IHttpHandler
    {
        Asso_ApplyService bll = new Asso_ApplyService();
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
                    case "GetAssoApplyDataPage":
                        GetAssoApplyDataPage(context);
                        break;
                    case "GetAssoApplyById":
                        GetAssoApplyById(context);
                        break;
                    case "AddAssoApply":
                        AddAssoApply(context);
                        break;
                    case "AssoApply_Audit":
                        AssoApply_Audit(context);
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

        #region 获取社团申请表的分页数据
        private void GetAssoApplyDataPage(HttpContext context)
        {
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("ExamStatus", context.Request["ExamStatus"] ?? "");
                ht.Add("ApplyType", context.Request["ApplyType"] ?? "");
                ht.Add("ApplyUserNo", context.Request["ApplyUserNo"] ?? "");
                ht.Add("OSLeaderNo", context.Request["OSLeaderNo"] ?? "");
                ht.Add("AssoId", context.Request["AssoId"] ?? "");
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
                    jsonModel = nameCommon.AddCreateNameForData(jsonModel, 0, ispage, "ApplyUserNo");
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

        #region 根据Id获取社团申请详情
        private void GetAssoApplyById(HttpContext context)
        {
            int itemid = Convert.ToInt32(context.Request["ItemId"]);
            jsonModel = bll.GetEntityById(itemid);
        }
        #endregion 

        #region 添加社团申请
        private void AddAssoApply(HttpContext context)
        {
            Asso_Apply item = new Asso_Apply();
            item.Id = 0;
            item.AssoId =Convert.ToInt32(context.Request["AssoId"]);
            item.ApplyUserNo= context.Request["ApplyUserNo"];
            item.ApplyType = Convert.ToByte(context.Request["ApplyType"]);
            item.ApplyIntroduce = context.Request["ApplyIntroduce"];            
            jsonModel = bll.AddAssoApply(item);
        }
        #endregion        

        #region 社团申请审核
        private void AssoApply_Audit(HttpContext context)
        {
            int itemid = Convert.ToInt32(context.Request["ItemId"]);
            jsonModel = bll.GetEntityById(itemid);
            if (jsonModel.errNum == 0)
            {
                Asso_Apply item = jsonModel.retData as Asso_Apply;
                item.Id = itemid;
                item.ExamUserNo = context.Request["ExamUserNo"];
                item.ExamStatus =Convert.ToByte(context.Request["ExamStatus"]);
                item.ExamSuggest = context.Request["ExamSuggest"];                
                jsonModel = bll.AssoApply_Audit(item);
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