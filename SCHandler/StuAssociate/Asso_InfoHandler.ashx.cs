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
    /// Asso_InfoHandler 的摘要说明
    /// </summary>
    public class Asso_InfoHandler : IHttpHandler
    {
        Asso_InfoService bll = new Asso_InfoService();
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
                    case "GetAssoInfoDataPage":
                        GetAssoInfoDataPage(context);
                        break;
                    case "GetAssoInfoById":
                        GetAssoInfoById(context);
                        break;
                    case "AddAssoInfo":
                        AddAssoInfo(context);
                        break;
                    case "EditAssoInfo":
                        EditAssoInfo(context);
                        break;
                    case "DelAssoInfo":
                        DelAssoInfo(context);
                        break;
                    case "ChangeBackPic":
                        ChangeBackPic(context);
                        break;
                    case "AddAssoMember":
                        AddOrDelAssoMember(0, context);
                        break;
                    case "DelAssoMember":
                        AddOrDelAssoMember(1,context);
                        break;
                    case "GetAssoMemberDataPage":
                        GetAssoMemberDataPage(context);
                        break;
                    case "GetAssoHisLeaderDataPage":
                        GetAssoHisLeaderDataPage(context);
                        break;
                    case "GetAssoHisMemberDataPage":
                        GetAssoHisMemberDataPage(context);
                        break;
                    case "GetAssoEnteredData":
                        GetAssoEnteredData(context);
                        break;
                    case "AssoEnteredSet":
                        AssoEnteredSet(context);
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

        #region 获取社团信息表的分页数据
        private void GetAssoInfoDataPage(HttpContext context)
        {
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("Name", context.Request["Name"] ?? "");
                ht.Add("AssoType", context.Request["AssoType"] ?? "");
                ht.Add("UnAssoTypes", context.Request["UnAssoTypes"] ?? "");
                ht.Add("IsDelete", context.Request["IsDelete"] ?? "0");
                ht.Add("Id", context.Request["Id"] ?? "");
                ht.Add("MyUserNo", context.Request["MyUserNo"] ?? "");
                ht.Add("LoginUID", context.Request["LoginUID"] ?? "");
                ht.Add("Href", context.Request["Href"] ?? "");
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

        #region 根据Id获取社团详情
        private void GetAssoInfoById(HttpContext context)
        {
            int itemid = Convert.ToInt32(context.Request["ItemId"]);
            jsonModel = bll.GetEntityById(itemid);
        }
        #endregion 

        #region 添加社团
        private void AddAssoInfo(HttpContext context)
        {
            Asso_Info item = new Asso_Info();
            item.Id = 0;
            item.Name = context.Request["Name"];
            item.Slogan = context.Request["Slogan"];
            item.Introduce = context.Request["Introduce"];
            item.LeaderNo = context.Request["LeaderNo"];
            item.SecondLeaderNo = context.Request["SecondLeaderNo"];
            item.AssoType =Convert.ToInt32(context.Request["AssoType"]);
            item.ApplyType = Convert.ToByte(context.Request["ApplyType"]??"0");
            item.PicURL = context.Request["PicURL"];
            item.CreateUID = context.Request["LoginUID"];
            jsonModel = bll.EditAssoInfo(item);
        }
        #endregion        

        #region 修改社团
        private void EditAssoInfo(HttpContext context)
        {
            int itemid = Convert.ToInt32(context.Request["ItemId"]);
            jsonModel = bll.GetEntityById(itemid);
            if (jsonModel.errNum == 0)
            {
                Asso_Info item = jsonModel.retData as Asso_Info;
                item.Id = itemid;
                item.Name = context.Request["Name"];
                item.Slogan = context.Request["Slogan"];
                item.Introduce = context.Request["Introduce"];
                if (!string.IsNullOrEmpty(context.Request["LeaderNo"]))
                {
                    item.LeaderNo = context.Request["LeaderNo"];
                }
                if (!string.IsNullOrEmpty(context.Request["SecondLeaderNo"]))
                {
                    item.SecondLeaderNo = context.Request["SecondLeaderNo"];
                }                
                item.AssoType = Convert.ToInt32(context.Request["AssoType"]);
                item.ApplyType = Convert.ToByte(context.Request["ApplyType"] ?? "0");
                item.PicURL = context.Request["PicURL"];
                item.EditUID = context.Request["LoginUID"];
                jsonModel = bll.EditAssoInfo(item);
            }
        }
        #endregion

        #region 删除社团
        private void DelAssoInfo(HttpContext context)
        {
            int itemid = Convert.ToInt32(context.Request["ItemId"]);
            jsonModel = bll.DeleteFalse(itemid);
        }
        #endregion

        #region 更换社团背景
        private void ChangeBackPic(HttpContext context)
        {
            int itemid = Convert.ToInt32(context.Request["ItemId"]);
            jsonModel = bll.GetEntityById(itemid);
            if (jsonModel.errNum == 0)
            {
                Asso_Info item = jsonModel.retData as Asso_Info;
                item.Id = itemid;
                item.BackPicUrl = context.Request["BackPicUrl"];
                jsonModel = bll.Update(item);
            }
        }
        #endregion

        #region 添加或删除社团成员
        private void AddOrDelAssoMember(int type,HttpContext context)
        {
            Asso_Member item = new Asso_Member();
            item.AssoId =Convert.ToInt32(context.Request["AssoId"]);
            item.Introduction = context.Request["Introduction"] ?? "";
            item.CreateUID = context.Request["LoginUID"];
            jsonModel = bll.AddOrDelAssoMember(type, item, context.Request["MemberNos"]);
        }
        #endregion

        #region 获取社团成员表的分页数据
        private void GetAssoMemberDataPage(HttpContext context)
        {
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("AssoId", context.Request["AssoId"] ?? "");
                ht.Add("NewMemerDay", context.Request["NewMemerDay"] ?? "");
                ht.Add("IsOnlyBase", context.Request["IsOnlyBase"] ?? "0"); //是否只需要表中基本信息 0是（默认）；1不是                
                bool ispage = true;
                if (!string.IsNullOrEmpty(context.Request["ispage"]))
                {
                    ispage = Convert.ToBoolean(context.Request["ispage"]);
                }
                ht.Add("PageIndex", context.Request["PageIndex"] ?? "1");
                ht.Add("PageSize", context.Request["PageSize"] ?? "10");
                jsonModel = new Asso_MemberService().GetPage(ht, ispage);
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

        #region 获取社团历任社团长的分页数据
        private void GetAssoHisLeaderDataPage(HttpContext context)
        {
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("AssoId", context.Request["AssoId"] ?? "");               
                bool ispage = true;
                if (!string.IsNullOrEmpty(context.Request["ispage"]))
                {
                    ispage = Convert.ToBoolean(context.Request["ispage"]);
                }
                ht.Add("PageIndex", context.Request["PageIndex"] ?? "1");
                ht.Add("PageSize", context.Request["PageSize"] ?? "10");
                jsonModel = new Asso_HisMemberService().GetPage(ht, ispage);
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

        #region 获取社团历史成员表的分页数据
        private void GetAssoHisMemberDataPage(HttpContext context)
        {
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("AssoId", context.Request["AssoId"] ?? "");
                ht.Add("IsOnlyBase", context.Request["IsOnlyBase"] ?? "0"); //是否只需要表中基本信息 0是（默认）；1不是                
                bool ispage = true;
                if (!string.IsNullOrEmpty(context.Request["ispage"]))
                {
                    ispage = Convert.ToBoolean(context.Request["ispage"]);
                }
                ht.Add("PageIndex", context.Request["PageIndex"] ?? "1");
                ht.Add("PageSize", context.Request["PageSize"] ?? "10");
                jsonModel = new Asso_MemberService().GetPage(ht, ispage);
                string isUnifiedInfo = context.Request["IsUnifiedInfo"] ?? "0";//是否需要用户信息 0不需要（默认）；1需要
                if (isUnifiedInfo != "0")
                {
                    jsonModel = nameCommon.AddCreateNameForData(jsonModel, 0, ispage, "MemberNo", "", "", "", context.Request["AcademicId"] ?? "0");
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

        #region 获取社团报名时间
        private void GetAssoEnteredData(HttpContext context)
        {
            try
            {
                jsonModel = bll.GetAssoEnteredData();
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

        #region 社团报名时间设置
        private void AssoEnteredSet(HttpContext context)
        {
            Asso_EnteredSet item = new Asso_EnteredSet();
            item.IsOnly = Convert.ToByte(context.Request["IsOnly"]??"1");
            item.StartTime = Convert.ToDateTime(context.Request["StartTime"]);
            item.EndTime = Convert.ToDateTime(context.Request["EndTime"]);
            item.CreateUID = context.Request["LoginUID"];
            jsonModel = bll.SetAssoEntered(item);
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