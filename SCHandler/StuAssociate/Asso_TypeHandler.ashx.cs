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
    /// Asso_TypeHandler 的摘要说明
    /// </summary>
    public class Asso_TypeHandler : IHttpHandler
    {
        Asso_TypeService bll = new Asso_TypeService();
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
                    case "GetAssoTypeDataPage":
                        GetAssoTypeDataPage(context);
                        break;                    
                    case "AddAssoType":
                        AddAssoType(context);
                        break;
                    case "EditAssoType":
                        EditAssoType(context);
                        break;
                    case "DelAssoType":
                        DelAssoType(context);
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

        #region 获取社团类型表的分页数据
        private void GetAssoTypeDataPage(HttpContext context)
        {
            try
            {               
                Hashtable ht = new Hashtable();
                ht.Add("Name", context.Request["Name"] ?? "");
                bool ispage = true;
                if (!string.IsNullOrEmpty(context.Request["ispage"]))
                {
                    ispage = Convert.ToBoolean(context.Request["ispage"]);
                }
                ht.Add("PageIndex", context.Request["PageIndex"] ?? "1");
                ht.Add("PageSize", context.Request["PageSize"] ?? "10");
                jsonModel = bll.GetPage(ht, ispage);
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

        #region 添加社团类型
        private void AddAssoType(HttpContext context)
        {
            string name = context.Request["Name"];
            jsonModel = bll.IsNameExists(name);
            if (jsonModel.errNum == 0)
            {
                if (jsonModel.retData.ToString().ToLower() == "true")
                {
                    jsonModel = new JsonModel()
                    {
                        errNum = -1,
                        errMsg = "exist",
                        retData = ""
                    };
                }
                else
                {
                    Asso_Type item = new Asso_Type();
                    item.Name = name;
                    item.CreateUID = context.Request["LoginUID"];
                    item.CreateTime = DateTime.Now;
                    jsonModel = bll.Add(item);
                }
            }
        }
        #endregion      

        #region 编辑社团类型
        private void EditAssoType(HttpContext context)
        {
            int itemid = Convert.ToInt32(context.Request["ItemId"]);
            string name = context.Request["Name"];
            jsonModel = bll.IsNameExists(name, itemid);
            if (jsonModel.errNum == 0)
            {
                if (jsonModel.retData.ToString().ToLower() == "true")
                {
                    jsonModel = new JsonModel()
                    {
                        errNum = -1,
                        errMsg = "exist",
                        retData = ""
                    };
                }
                else
                {
                    jsonModel = bll.GetEntityById(itemid);
                    if (jsonModel.errNum == 0)
                    {
                        Asso_Type item = jsonModel.retData as Asso_Type;
                        item.Id = itemid;
                        item.Name = name;
                        item.EditUID = context.Request["LoginUID"];
                        item.EditTime = DateTime.Now;
                        jsonModel = bll.Update(item);
                    }
                }
            }
        }
        #endregion      

        #region 删除社团类型
        private void DelAssoType(HttpContext context)
        {
            int itemid = Convert.ToInt32(context.Request["ItemId"]);
            jsonModel = bll.DeleteFalse(itemid);
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