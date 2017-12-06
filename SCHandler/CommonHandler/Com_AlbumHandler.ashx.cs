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
    /// Com_AlbumHandler 的摘要说明
    /// </summary>
    public class Com_AlbumHandler : IHttpHandler
    {
        Com_AlbumService bll = new Com_AlbumService();
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
                    case "GetComAlbumDataPage":
                        GetComAlbumDataPage(context);
                        break;
                    case "GetComAlbumById":
                        GetComAlbumById(context);
                        break;
                    case "AddComAlbum":
                        AddComAlbum(context);
                        break;
                    case "EditComAlbum":
                        EditComAlbum(context);
                        break;
                    case "DelComAlbum":
                        DelComAlbum(context);
                        break;
                    case "GetComAlbumPicDataPage":
                        GetComAlbumPicDataPage(context);
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

        #region 获取相册表的分页数据
        private void GetComAlbumDataPage(HttpContext context)
        {
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("Name", context.Request["Name"] ?? "");
                ht.Add("Type", context.Request["Type"] ?? "0");
                ht.Add("RelationId", context.Request["RelationId"] ?? "");
                ht.Add("IsOnlyBase", context.Request["IsOnlyBase"] ?? "0"); //是否只需要表中基本信息 0是（默认）；1不是                
                bool ispage = true;
                if (!string.IsNullOrEmpty(context.Request["ispage"]))
                {
                    ispage = Convert.ToBoolean(context.Request["ispage"]);
                }
                ht.Add("PageIndex", context.Request["PageIndex"] ?? "1");
                ht.Add("PageSize", context.Request["PageSize"] ?? "10");
                jsonModel = bll.GetPage(ht, ispage);
                string isUnifiedInfo= context.Request["IsUnifiedInfo"] ?? "0";//是否需要用户信息 0不需要（默认）；1需要
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

        #region 根据Id获取相册详情
        private void GetComAlbumById(HttpContext context)
        {
            int itemid = Convert.ToInt32(context.Request["ItemId"]);
            jsonModel = bll.GetEntityById(itemid);
        }
        #endregion 

        #region 添加相册
        private void AddComAlbum(HttpContext context)
        {
            Com_Album item = new Com_Album();
            item.Id = 0;
            item.Type = Convert.ToByte(context.Request["Type"]??"0");
            item.RelationId = Convert.ToInt32(context.Request["RelationId"]);
            item.Name = context.Request["Name"];
            item.Description =context.Request["Description"];
            item.CreateUID = context.Request["LoginUID"];
            jsonModel = bll.EditComAlbum(item);
        }
        #endregion        

        #region 修改相册
        private void EditComAlbum(HttpContext context)
        {
            int itemid = Convert.ToInt32(context.Request["ItemId"]);
            jsonModel = bll.GetEntityById(itemid);
            if (jsonModel.errNum == 0)
            {
                Com_Album item = jsonModel.retData as Com_Album;
                item.Id = itemid;
                item.Name = context.Request["Name"];
                item.Description = context.Request["Content"];
                item.EditUID = context.Request["LoginUID"];
                jsonModel = bll.EditComAlbum(item);
            }
        }
        #endregion

        #region 删除相册
        private void DelComAlbum(HttpContext context)
        {
            string[] ids = context.Request["ItemIdStr"].Split(',');
            jsonModel = bll.DeleteBatchFalse(1, ids);
        }
        #endregion        

        #region 获取相册图片表的分页数据
        private void GetComAlbumPicDataPage(HttpContext context)
        {
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("AlbumId", context.Request["AlbumId"] ?? "");
                bool ispage = true;
                if (!string.IsNullOrEmpty(context.Request["ispage"]))
                {
                    ispage = Convert.ToBoolean(context.Request["ispage"]);
                }
                ht.Add("PageIndex", context.Request["PageIndex"] ?? "1");
                ht.Add("PageSize", context.Request["PageSize"] ?? "10");
                jsonModel = new Com_AlbumPicService().GetPage(ht, ispage);
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
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}