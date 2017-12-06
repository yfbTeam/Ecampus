using SCBLL;
using SCModel;
using SCUtility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace SCHandler.StuActivity
{
    /// <summary>
    /// Acti_DepartHandler 的摘要说明
    /// </summary>
    public class Acti_DepartHandler : IHttpHandler
    {
        Acti_DepartInfoService bll = new Acti_DepartInfoService();
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
                    case "GetDepartInfoDataPage":
                        GetDepartInfoDataPage(context);
                        break;
                    case "GetDepartInfoById":
                        GetDepartInfoById(context);
                        break;
                    case "AddDepartInfo":
                        AddDepartInfo(context);
                        break;
                    case "EditDepartInfo":
                        EditDepartInfo(context);
                        break;
                    case "DelDepartInfo":
                        DelDepartInfo(context);
                        break;
                    case "ChangeBackPic":
                        ChangeBackPic(context);
                        break;
                    case "AddDepartMember":
                        AddOrDelDepartMember(0,context);
                        break;
                    case "DelDepartMember":
                        AddOrDelDepartMember(1,context);
                        break;
                    case "GetDepartMemberDataPage":
                        GetDepartMemberDataPage(context);
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

        #region 获取部门信息表的分页数据
        private void GetDepartInfoDataPage(HttpContext context)
        {
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("Name", context.Request["Name"] ?? "");
                ht.Add("ParentId", context.Request["ParentId"] ?? "");
                ht.Add("IsDelete", context.Request["IsDelete"] ?? "0");
                ht.Add("Id", context.Request["Id"] ?? "");
                ht.Add("MyUserNo", context.Request["MyUserNo"] ?? "");
                ht.Add("LoginUID", context.Request["LoginUID"] ?? "");
                ht.Add("OrderBy", context.Request["OrderBy"] ?? "");
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
                    jsonModel = nameCommon.AddCreateNameForData(jsonModel, 4, ispage, "LeaderNo", "", "SecondLeaderNo");
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

        #region 根据Id获取部门详情
        private void GetDepartInfoById(HttpContext context)
        {
            int itemid = Convert.ToInt32(context.Request["ItemId"]);
            jsonModel = bll.GetEntityById(itemid);
        }
        #endregion 

        #region 添加部门
        private void AddDepartInfo(HttpContext context)
        {
            Acti_DepartInfo item = new Acti_DepartInfo();
            item.Id = 0;
            item.Name = context.Request["Name"];
            item.ParentId =Convert.ToInt32(context.Request["ParentId"]);
            item.Introduce = context.Request["Introduce"];
            item.LeaderNo = context.Request["LeaderNo"];
            item.SecondLeaderNo = context.Request["SecondLeaderNo"];
            item.PicURL = context.Request["PicURL"];
            item.CreateUID = context.Request["LoginUID"];
            jsonModel = bll.EditDepartInfo(item);
        }
        #endregion        

        #region 修改部门
        private void EditDepartInfo(HttpContext context)
        {
            int itemid = Convert.ToInt32(context.Request["ItemId"]);
            jsonModel = bll.GetEntityById(itemid);
            if (jsonModel.errNum == 0)
            {
                Acti_DepartInfo item = jsonModel.retData as Acti_DepartInfo;
                item.Id = itemid;
                item.Name = context.Request["Name"];
                item.Introduce = context.Request["Introduce"];
                item.LeaderNo = context.Request["LeaderNo"];
                item.SecondLeaderNo = context.Request["SecondLeaderNo"];
                item.PicURL = context.Request["PicURL"];
                item.EditUID = context.Request["LoginUID"];
                jsonModel = bll.EditDepartInfo(item);
            }
        }
        #endregion

        #region 删除部门
        private void DelDepartInfo(HttpContext context)
        {
            int itemid = Convert.ToInt32(context.Request["ItemId"]);
            jsonModel = bll.DeleteFalse(itemid);
        }
        #endregion

        #region 更换部门背景
        private void ChangeBackPic(HttpContext context)
        {
            int itemid = Convert.ToInt32(context.Request["ItemId"]);
            jsonModel = bll.GetEntityById(itemid);
            if (jsonModel.errNum == 0)
            {
                Acti_DepartInfo item = jsonModel.retData as Acti_DepartInfo;
                item.Id = itemid;
                item.BackPicUrl = context.Request["BackPicUrl"];
                jsonModel = bll.Update(item);
            }
        }
        #endregion

        #region 添加或删除部门成员
        private void AddOrDelDepartMember(int type, HttpContext context)
        {
            Acti_DepartMember item = new Acti_DepartMember();
            item.DepartId = Convert.ToInt32(context.Request["DepartId"]);
            item.Introduction = context.Request["Introduction"] ?? "";
            item.CreateUID = context.Request["LoginUID"];
            jsonModel = bll.AddOrDelDepartMember(type, item, context.Request["MemberNos"]);
        }
        #endregion

        #region 获取部门成员表的分页数据
        private void GetDepartMemberDataPage(HttpContext context)
        {
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("DepartId", context.Request["DepartId"] ?? "");
                ht.Add("NewMemerDay", context.Request["NewMemerDay"] ?? "");
                ht.Add("IsOnlyBase", context.Request["IsOnlyBase"] ?? "0"); //是否只需要表中基本信息 0是（默认）；1不是    
                bool ispage = true;
                if (!string.IsNullOrEmpty(context.Request["ispage"]))
                {
                    ispage = Convert.ToBoolean(context.Request["ispage"]);
                }
                ht.Add("PageIndex", context.Request["PageIndex"] ?? "1");
                ht.Add("PageSize", context.Request["PageSize"] ?? "10");
                jsonModel = new Acti_DepartMemberService().GetPage(ht, ispage);
                string isUnifiedInfo = context.Request["IsUnifiedInfo"] ?? "0";//是否需要用户信息 0不需要（默认）；1需要
                if (isUnifiedInfo != "0")
                {
                    jsonModel = nameCommon.AddCreateNameForData(jsonModel, 0, ispage, "MemberNo", "", "", context.Request["Name"] ?? "");
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