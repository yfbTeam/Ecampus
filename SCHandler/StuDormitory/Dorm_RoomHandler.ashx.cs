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
    /// Dorm_RoomHandler 的摘要说明
    /// </summary>
    public class Dorm_RoomHandler : IHttpHandler
    {
        Dorm_RoomService bll = new Dorm_RoomService();
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
                    case "GetDormRoomDataPage":
                        GetDormRoomDataPage(context);
                        break;
                    case "GetDormRoomById":
                        GetDormRoomById(context);
                        break;
                    case "AddDormRoom":
                        AddDormRoom(context);
                        break;
                    case "EditDormRoom":
                        EditDormRoom(context);
                        break;
                    case "DelDormRoom":
                        DelDormRoom(context);
                        break;
                    case "ChangeBackPic":
                        ChangeBackPic(context);
                        break;
                    case "GetRoomStuListDataPage":
                        GetRoomStuListDataPage(context);
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

        #region 获取宿舍信息表的分页数据
        private void GetDormRoomDataPage(HttpContext context)
        {
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("Name", context.Request["Name"] ?? "");
                ht.Add("BuildId", context.Request["BuildId"] ?? "");
                ht.Add("IsDelete", context.Request["IsDelete"] ?? "0");
                ht.Add("Id", context.Request["Id"] ?? "");
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

        #region 根据Id获取宿舍详情
        private void GetDormRoomById(HttpContext context)
        {
            int itemid = Convert.ToInt32(context.Request["ItemId"]);
            jsonModel = bll.GetEntityById(itemid);
        }
        #endregion 

        #region 添加宿舍
        private void AddDormRoom(HttpContext context)
        {
            Dorm_Room item = new Dorm_Room();
            item.Id = 0;
            item.BuildId = Convert.ToInt32(context.Request["BuildId"]);
            item.Name = context.Request["Name"];
            item.FloorNo = Convert.ToByte(context.Request["FloorNo"]);
            item.Beds = Convert.ToByte(context.Request["Beds"]);
            item.Introduce = context.Request["Introduce"];
            item.PicURL = context.Request["PicURL"];
            item.CreateUID = context.Request["LoginUID"];
            jsonModel = bll.EditDormRoom(item);
        }
        #endregion        

        #region 修改宿舍
        private void EditDormRoom(HttpContext context)
        {
            int itemid = Convert.ToInt32(context.Request["ItemId"]);
            jsonModel = bll.GetEntityById(itemid);
            if (jsonModel.errNum == 0)
            {
                Dorm_Room item = jsonModel.retData as Dorm_Room;
                item.Id = itemid;
                item.BuildId = Convert.ToInt32(context.Request["BuildId"]);
                item.Name = context.Request["Name"];
                item.FloorNo = Convert.ToByte(context.Request["FloorNo"]);
                item.Beds = Convert.ToByte(context.Request["Beds"]);
                item.Introduce = context.Request["Introduce"];
                item.PicURL = context.Request["PicURL"];
                item.EditUID = context.Request["LoginUID"];
                jsonModel = bll.EditDormRoom(item);
            }
        }
        #endregion

        #region 删除宿舍
        private void DelDormRoom(HttpContext context)
        {
            int itemid = Convert.ToInt32(context.Request["ItemId"]);
            jsonModel = bll.DeleteFalse(itemid);
        }
        #endregion

        #region 更换宿舍背景
        private void ChangeBackPic(HttpContext context)
        {
            int itemid = Convert.ToInt32(context.Request["ItemId"]);
            jsonModel = bll.GetEntityById(itemid);
            if (jsonModel.errNum == 0)
            {
                Dorm_Room item = jsonModel.retData as Dorm_Room;
                item.Id = itemid;
                item.BackPicUrl = context.Request["BackPicUrl"];
                jsonModel = bll.Update(item);
            }
        }
        #endregion

        #region 获取宿舍成员表的分页数据
        private void GetRoomStuListDataPage(HttpContext context)
        {
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("RoomId", context.Request["RoomId"] ?? "");
                ht.Add("NewMemerDay", context.Request["NewMemerDay"] ?? "");
                ht.Add("IsOnlyBase", context.Request["IsOnlyBase"] ?? "0"); //是否只需要表中基本信息 0是（默认）；1不是   
                bool ispage = true;
                if (!string.IsNullOrEmpty(context.Request["ispage"]))
                {
                    ispage = Convert.ToBoolean(context.Request["ispage"]);
                }
                ht.Add("PageIndex", context.Request["PageIndex"] ?? "1");
                ht.Add("PageSize", context.Request["PageSize"] ?? "10");
                jsonModel = new Dorm_RoomStuListService().GetPage(ht, ispage);
                string isUnifiedInfo = context.Request["IsUnifiedInfo"] ?? "0";//是否需要用户信息 0不需要（默认）；1需要
                if (isUnifiedInfo != "0")
                {
                    jsonModel = nameCommon.AddCreateNameForData(jsonModel, 0, ispage, "StuNo", "", "", context.Request["Name"] ?? "");
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

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}