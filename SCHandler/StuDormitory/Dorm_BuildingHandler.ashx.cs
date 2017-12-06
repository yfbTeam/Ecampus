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
    /// Dorm_BuildingHandler 的摘要说明
    /// </summary>
    public class Dorm_BuildingHandler : IHttpHandler
    {
        Dorm_BuildingService bll = new Dorm_BuildingService();
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
                    case "GetDormBuildingDataPage":
                        GetDormBuildingDataPage(context);
                        break;
                    case "GetDormBuildingById":
                        GetDormBuildingById(context);
                        break;
                    case "AddDormBuilding":
                        AddDormBuilding(context);
                        break;
                    case "EditDormBuilding":
                        EditDormBuilding(context);
                        break;
                    case "DelDormBuilding":
                        DelDormBuilding(context);
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

        #region 获取宿舍楼信息表的分页数据
        private void GetDormBuildingDataPage(HttpContext context)
        {
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("Name", context.Request["Name"] ?? "");
                ht.Add("Type", context.Request["Type"] ?? "");
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
                    jsonModel = nameCommon.AddCreateNameForData(jsonModel, 4, ispage, "ManagerNo", "", "CreateUID");
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

        #region 根据Id获取宿舍楼详情
        private void GetDormBuildingById(HttpContext context)
        {
            int itemid = Convert.ToInt32(context.Request["ItemId"]);
            jsonModel = bll.GetEntityById(itemid);
        }
        #endregion 

        #region 添加宿舍楼
        private void AddDormBuilding(HttpContext context)
        {
            Dorm_Building item = new Dorm_Building();
            item.Id = 0;
            item.Name = context.Request["Name"];
            item.FloorCount =Convert.ToByte(context.Request["FloorCount"]);
            item.Type = Convert.ToByte(context.Request["Type"]);
            item.ManagerNo = context.Request["ManagerNo"];           
            item.CreateUID = context.Request["LoginUID"];
            jsonModel = bll.EditDormBuilding(item);
        }
        #endregion        

        #region 修改宿舍楼
        private void EditDormBuilding(HttpContext context)
        {
            int itemid = Convert.ToInt32(context.Request["ItemId"]);
            jsonModel = bll.GetEntityById(itemid);
            if (jsonModel.errNum == 0)
            {
                Dorm_Building item = jsonModel.retData as Dorm_Building;
                item.Id = itemid;
                item.Name = context.Request["Name"];
                item.FloorCount = Convert.ToByte(context.Request["FloorCount"]);
                item.Type = Convert.ToByte(context.Request["Type"]);
                item.ManagerNo = context.Request["ManagerNo"];
                item.EditUID = context.Request["LoginUID"];
                jsonModel = bll.EditDormBuilding(item);
            }
        }
        #endregion

        #region 删除宿舍楼
        private void DelDormBuilding(HttpContext context)
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