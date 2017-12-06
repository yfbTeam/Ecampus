using SCBLL;
using SCModel;
using SCUtility;
using System;
using System.Collections;
using System.Web;
using System.Web.Script.Serialization;

namespace SCHandler.CommonHandler
{
    /// <summary>
    /// Com_NewHandler 的摘要说明
    /// </summary>
    public class Com_NewHandler : IHttpHandler
    {
        Com_NewInfoService bll = new Com_NewInfoService();
        Com_NewCommentService comm_bll = new Com_NewCommentService();
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
                    case "GetComNewInfoDataPage":
                        GetComNewInfoDataPage(context);
                        break;
                    case "GetComNewInfoById":
                        GetComNewInfoById(context);
                        break;
                    case "AddComNewInfo":
                        AddComNewInfo(context);
                        break;
                    case "EditComNewInfo":
                        EditComNewInfo(context);
                        break;
                    case "DelComNewInfo":
                        DelComNewInfo(context);
                        break;                    
                    case "GoodClick":
                        GoodClick(context);
                        break;
                    case "NewTopSet":
                        NewTopSet(context);
                        break;
                    case "NewEliteSet":
                        NewEliteSet(context);
                        break;
                    case "BrowsingTimesSet":
                        BrowsingTimesSet(context);
                        break;
                    case "GetComNewCommentDataPage":
                        GetComNewCommentDataPage(context);
                        break;
                    case "AddComNewComment":
                        AddComNewComment(context);
                        break;
                    case "DelComNewComment":
                        DelComNewComment(context);
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

        #region 获取资讯表的分页数据
        private void GetComNewInfoDataPage(HttpContext context)
        {
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("Id", context.Request["Id"] ?? "");
                ht.Add("Name", context.Request["Name"] ?? "");
                ht.Add("Type", context.Request["Type"] ?? "0");
                ht.Add("RelationId", context.Request["RelationId"] ?? "");
                ht.Add("NewType", context.Request["NewType"] ?? "0");
                ht.Add("IsRecruit", context.Request["IsRecruit"] ?? "");
                ht.Add("IsElite", context.Request["IsElite"] ?? "");
                ht.Add("CreateUID", context.Request["CreateUID"] ?? ""); 
                ht.Add("CommontCount", context.Request["CommontCount"] ?? "");
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
                    jsonModel = nameCommon.AddCreateNameForData(jsonModel, 4, ispage, "CreateUID", "", "LastComUID");
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

        #region 根据Id获取资讯详情
        private void GetComNewInfoById(HttpContext context)
        {
            int itemid = Convert.ToInt32(context.Request["ItemId"]);
            jsonModel = bll.GetEntityById(itemid);
        }
        #endregion 

        #region 添加资讯
        private void AddComNewInfo(HttpContext context)
        {
            Com_NewInfo item = new Com_NewInfo();
            item.Id = 0;
            item.Type= Convert.ToByte(context.Request["Type"]??"0");
            item.RelationId = Convert.ToInt32(context.Request["RelationId"]);
            item.NewType= Convert.ToByte(context.Request["NewType"]??"0");
            item.Name = context.Request["Name"];
            item.Content = HttpUtility.UrlDecode(context.Request["Content"]);
            item.CreateUID = context.Request["LoginUID"];
            jsonModel = bll.EditComNewInfo(item);
        }
        #endregion        

        #region 修改资讯
        private void EditComNewInfo(HttpContext context)
        {
            int itemid = Convert.ToInt32(context.Request["ItemId"]);
            jsonModel = bll.GetEntityById(itemid);
            if (jsonModel.errNum == 0)
            {
                Com_NewInfo item = jsonModel.retData as Com_NewInfo;
                item.Id = itemid;
                item.Name = context.Request["Name"];
                item.Content = HttpUtility.UrlDecode(context.Request["Content"]);
                item.EditUID = context.Request["LoginUID"];
                jsonModel = bll.EditComNewInfo(item);
            }
        }
        #endregion

        #region 删除资讯
        private void DelComNewInfo(HttpContext context)
        {
            int itemid = Convert.ToInt32(context.Request["ItemId"]);
            jsonModel = bll.DeleteFalse(itemid);
        }
        #endregion
        
        #region 点赞
        private void GoodClick(HttpContext context)
        {
            Com_GoodClick item = new Com_GoodClick();
            item.Type = Convert.ToByte(context.Request["Type"]??"0");
            item.RelationId = Convert.ToInt32(context.Request["RelationId"]);
            item.CreateUID= context.Request["LoginUID"];
            jsonModel = bll.GoodClick(item);
        }
        #endregion

        #region 置顶
        private void NewTopSet(HttpContext context)
        {
            int itemid = Convert.ToInt32(context.Request["ItemId"]);
            jsonModel = bll.GetEntityById(itemid);
            if (jsonModel.errNum == 0)
            {
                Com_NewInfo item = jsonModel.retData as Com_NewInfo;
                item.Id = itemid;
                item.IsTop = Convert.ToByte(context.Request["IsTop"]); ;               
                jsonModel = bll.Update(item);
            }
        }
        #endregion

        #region 精华
        private void NewEliteSet(HttpContext context)
        {
            int itemid = Convert.ToInt32(context.Request["ItemId"]);
            jsonModel = bll.GetEntityById(itemid);
            if (jsonModel.errNum == 0)
            {
                Com_NewInfo item = jsonModel.retData as Com_NewInfo;
                item.Id = itemid;
                item.IsElite = Convert.ToByte(context.Request["IsElite"]); ;
                jsonModel = bll.Update(item);
            }
        }
        #endregion

        #region 浏览次数设置
        private void BrowsingTimesSet(HttpContext context)
        {
            int itemid = Convert.ToInt32(context.Request["ItemId"]);
            jsonModel = bll.GetEntityById(itemid);
            if (jsonModel.errNum == 0)
            {
                Com_NewInfo item = jsonModel.retData as Com_NewInfo;
                item.Id = itemid;
                item.BrowsingTimes = item.BrowsingTimes + 1;
                jsonModel = bll.Update(item);
            }
        }
        #endregion

        #region 获取资讯评论表的分页数据
        private void GetComNewCommentDataPage(HttpContext context)
        {
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("NewId", context.Request["NewId"] ?? "");
                ht.Add("OrderBy", context.Request["OrderBy"] ?? "Id ");
                ht.Add("IsOnlyBase", context.Request["IsOnlyBase"] ?? "0"); //是否只需要表中基本信息 0是（默认）；1不是               
                bool ispage = true;
                if (!string.IsNullOrEmpty(context.Request["ispage"]))
                {
                    ispage = Convert.ToBoolean(context.Request["ispage"]);
                }
                ht.Add("PageIndex", context.Request["PageIndex"] ?? "1");
                ht.Add("PageSize", context.Request["PageSize"] ?? "10");
                jsonModel = comm_bll.GetPage(ht, ispage);
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

        #region 添加评论
        private void AddComNewComment(HttpContext context)
        {
            Com_NewComment item = new Com_NewComment();
            item.NewId = Convert.ToInt32(context.Request["NewId"]);
            item.ParentId = 0;
            item.Content = HttpUtility.UrlDecode(context.Request["Content"]);
            item.GoodCount = 0;
            item.CreateUID = context.Request["LoginUID"];
            item.CreateTime = DateTime.Now;
            item.IsDelete = 0;
            jsonModel = comm_bll.Add(item);
        }
        #endregion

        #region 删除评论
        private void DelComNewComment(HttpContext context)
        {
            int itemid = Convert.ToInt32(context.Request["ItemId"]);
            jsonModel = comm_bll.DeleteFalse(itemid);
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