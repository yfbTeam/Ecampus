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
    /// Com_FavoritesHandler 的摘要说明
    /// </summary>
    public class Com_FavoritesHandler : IHttpHandler
    {
        Com_FavoritesService bll = new Com_FavoritesService();
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
                    case "GetComFavoritesDataPage":
                        GetComFavoritesDataPage(context);
                        break;
                    case "GetComFavoritesById":
                        GetComFavoritesById(context);
                        break;
                    case "AddComFavorites":
                        AddComFavorites(context);
                        break;                    
                    case "DelComFavorites":
                        DelComFavorites(context);
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

        #region 获取收藏夹表的分页数据
        private void GetComFavoritesDataPage(HttpContext context)
        {
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("Name", context.Request["Name"] ?? "");
                ht.Add("Type", context.Request["Type"] ?? "0");
                ht.Add("RelationId", context.Request["RelationId"] ?? "");
                ht.Add("CreateUID", context.Request["CreateUID"] ?? "");
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

        #region 根据Id获取收藏夹详情
        private void GetComFavoritesById(HttpContext context)
        {
            int itemid = Convert.ToInt32(context.Request["ItemId"]);
            jsonModel = bll.GetEntityById(itemid);
        }
        #endregion 

        #region 添加收藏夹
        private void AddComFavorites(HttpContext context)
        {
            Com_Favorites item = new Com_Favorites();
            item.Id = 0;
            item.Type = Convert.ToByte(context.Request["Type"] ?? "0");
            item.RelationId = Convert.ToInt32(context.Request["RelationId"]);
            item.Name = context.Request["Name"];
            item.Href = context.Request["Href"];
            item.CreateUID = context.Request["LoginUID"];
            jsonModel = bll.AddComFavorites(item);
        }
        #endregion        

        #region 移除收藏夹
        private void DelComFavorites(HttpContext context)
        {
            string[] ids = context.Request["ItemIdStr"].Split(',');
            jsonModel = bll.DeleteBatchFalse(1, ids);
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