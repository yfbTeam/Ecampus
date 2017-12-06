using SCBLL;
using SCModel;
using SCUtility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace SCHandler.CommonHandler
{
    /// <summary>
    /// Com_EvalHandler 的摘要说明
    /// </summary>
    public class Com_EvalHandler : IHttpHandler
    {
        Com_EvaTempService bll = new Com_EvaTempService();        
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
                    case "GetComEvaTempDataPage":
                        GetComEvaTempDataPage(context);
                        break;
                    case "GetComEvaTempById":
                        GetComEvaTempById(context);
                        break;
                    case "AddComEvaTemp":
                        AddComEvaTemp(context);
                        break;
                    case "EditComEvaTemp":
                        EditComEvaTemp(context);
                        break;
                    case "DelComEvaTemp":
                        DelComEvaTemp(context);
                        break;
                    case "GetComEvaBaseDataPage":
                        GetComEvaBaseDataPage(context);
                        break;
                    case "GetComEvaBaseById":
                        GetComEvaBaseById(context);
                        break;
                    case "AddComEvaBase":
                        AddComEvaBase(context);
                        break;
                    case "EditComEvaBase":
                        EditComEvaBase(context);
                        break;
                    case "DelComEvaBase":
                        DelComEvaBase(context);
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

        #region 获取考评模板表的分页数据
        private void GetComEvaTempDataPage(HttpContext context)
        {
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("Name", context.Request["Name"] ?? "");
                ht.Add("Cycle", context.Request["Cycle"] ?? "");
                ht.Add("ApplyRange", context.Request["ApplyRange"] ?? "");
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

        #region 根据Id获取考评模板详情
        private void GetComEvaTempById(HttpContext context)
        {
            int itemid = Convert.ToInt32(context.Request["ItemId"]);
            jsonModel = bll.GetEntityById(itemid);
        }
        #endregion 

        #region 添加考评模板
        private void AddComEvaTemp(HttpContext context)
        {
            Com_EvaTemp item = new Com_EvaTemp();
            item.Id = 0;
            item.Type = Convert.ToByte(context.Request["Type"]);
            item.Name = context.Request["Name"];
            item.Content = HttpUtility.UrlDecode(context.Request["Content"]);
            item.Cycle= Convert.ToByte(context.Request["Cycle"]);
            item.ApplyRange = Convert.ToByte(context.Request["ApplyRange"]);
            item.CreateUID = context.Request["LoginUID"];
            jsonModel = bll.EditComEvaTemp(item);
        }
        #endregion        

        #region 修改考评模板
        private void EditComEvaTemp(HttpContext context)
        {
            int itemid = Convert.ToInt32(context.Request["ItemId"]);
            jsonModel = bll.GetEntityById(itemid);
            if (jsonModel.errNum == 0)
            {
                Com_EvaTemp item = jsonModel.retData as Com_EvaTemp;
                item.Id = itemid;
                item.Name = context.Request["Name"];
                item.Content = HttpUtility.UrlDecode(context.Request["Content"]);
                item.Cycle = Convert.ToByte(context.Request["Cycle"]);
                item.ApplyRange = Convert.ToByte(context.Request["ApplyRange"]);
                item.EditUID = context.Request["LoginUID"];
                jsonModel = bll.EditComEvaTemp(item);
            }
        }
        #endregion

        #region 删除考评模板
        private void DelComEvaTemp(HttpContext context)
        {
            string[] ids = context.Request["ItemIdStr"].Split(',');
            jsonModel = bll.DeleteBatchFalse(1, ids);
        }
        #endregion


        Com_EvaBaseService base_bll = new Com_EvaBaseService();

        #region 获取考评基础项表的分页数据
        private void GetComEvaBaseDataPage(HttpContext context)
        {
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("Name", context.Request["Name"] ?? "");
                ht.Add("Cycle", context.Request["Cycle"] ?? "");
                ht.Add("Target", context.Request["Target"] ?? "");
                bool ispage = true;
                if (!string.IsNullOrEmpty(context.Request["ispage"]))
                {
                    ispage = Convert.ToBoolean(context.Request["ispage"]);
                }
                ht.Add("PageIndex", context.Request["PageIndex"] ?? "1");
                ht.Add("PageSize", context.Request["PageSize"] ?? "10");
                jsonModel = base_bll.GetPage(ht, ispage);
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

        #region 根据Id获取考评基础项详情
        private void GetComEvaBaseById(HttpContext context)
        {
            int itemid = Convert.ToInt32(context.Request["ItemId"]);
            jsonModel = base_bll.GetEntityById(itemid);
        }
        #endregion 

        #region 添加考评基础项
        private void AddComEvaBase(HttpContext context)
        {
            Com_EvaBase item = new Com_EvaBase();
            item.Id = 0;
            item.Type = Convert.ToByte(context.Request["Type"]);
            item.Name = context.Request["Name"];
            item.Content = HttpUtility.UrlDecode(context.Request["Content"]);
            item.Cycle = Convert.ToByte(context.Request["Cycle"]);
            item.Target = Convert.ToByte(context.Request["Target"]);
            item.Score = Convert.ToInt32(context.Request["Score"]);
            item.CreateUID = context.Request["LoginUID"];
            jsonModel = bll.EditComEvaBase(item);
        }
        #endregion        

        #region 修改考评基础项
        private void EditComEvaBase(HttpContext context)
        {
            int itemid = Convert.ToInt32(context.Request["ItemId"]);
            jsonModel = bll.GetEntityById(itemid);
            if (jsonModel.errNum == 0)
            {
                Com_EvaBase item = jsonModel.retData as Com_EvaBase;
                item.Id = itemid;
                item.Name = context.Request["Name"];
                item.Content = HttpUtility.UrlDecode(context.Request["Content"]);
                item.Cycle = Convert.ToByte(context.Request["Cycle"]);
                item.Target = Convert.ToByte(context.Request["Target"]);
                item.Score = Convert.ToInt32(context.Request["Score"]);
                item.EditUID = context.Request["LoginUID"];
                jsonModel = bll.EditComEvaBase(item);
            }
        }
        #endregion

        #region 删除考评基础项
        private void DelComEvaBase(HttpContext context)
        {
            string[] ids = context.Request["ItemIdStr"].Split(',');
            jsonModel = base_bll.DeleteBatchFalse(1, ids);
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