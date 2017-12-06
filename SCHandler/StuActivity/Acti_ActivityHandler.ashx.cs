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
    /// Acti_ActivityHandler 的摘要说明
    /// </summary>
    public class Acti_ActivityHandler : IHttpHandler
    {
        Acti_ActivityService bll = new Acti_ActivityService();
        Acti_RecruitApplyService app_bll = new Acti_RecruitApplyService();
        Acti_ProjectService pro_bll = new Acti_ProjectService();
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
                    case "GetActiActivityDataPage":
                        GetActiActivityDataPage(context);
                        break;
                    case "GetActiActivityById":
                        GetActiActivityById(context);
                        break;
                    case "AddActiActivity":
                        AddActiActivity(context);
                        break;
                    case "EditActiActivity":
                        EditActiActivity(context);
                        break;
                    case "DelActiActivity":
                        DelActiActivity(context);
                        break;
                    case "BrowsingTimesSet":
                        BrowsingTimesSet(context);
                        break;
                    case "GetActiRecruitApplyDataPage":
                        GetActiRecruitApplyDataPage(context);
                        break;
                    case "GetActiRecruitApplyById":
                        GetActiRecruitApplyById(context);
                        break;
                    case "AddActiRecruitApply":
                        AddActiRecruitApply(context);
                        break;
                    case "ActiRecruitApply_Audit":
                        ActiRecruitApply_Audit(context);
                        break;
                    case "GetActiProjectDataPage":
                        GetActiProjectDataPage(context);
                        break;
                    case "AddActiProject":
                        AddActiProject(context);
                        break;
                    case "EditActiProject":
                        EditActiProject(context);
                        break;
                    case "DelActiProject":
                        DelActiProject(context);
                        break;
                    case "DelProjectMember":
                        DelProjectMember(context);
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

        #region 获取部门活动表的分页数据
        private void GetActiActivityDataPage(HttpContext context)
        {
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("Id", context.Request["Id"] ?? "");
                ht.Add("Name", context.Request["Name"] ?? "");
                ht.Add("DepartId", context.Request["DepartId"] ?? "");
                ht.Add("ActStatus", context.Request["ActStatus"] ?? "");
                ht.Add("OSLeaderNo", context.Request["OSLeaderNo"] ?? "");
                ht.Add("MyUserNo", context.Request["MyUserNo"] ?? "");
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
                    jsonModel = nameCommon.AddCreateNameForData(jsonModel, 4, ispage, "OrganizeUserNO", "", "CreateUID");
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

        #region 根据Id获取部门活动详情
        private void GetActiActivityById(HttpContext context)
        {
            int itemid = Convert.ToInt32(context.Request["ItemId"]);
            jsonModel = bll.GetEntityById(itemid);
        }
        #endregion 

        #region 添加部门活动
        private void AddActiActivity(HttpContext context)
        {
            Acti_Activity item = new Acti_Activity();
            item.Id = 0;
            item.DepartId = Convert.ToInt32(context.Request["DepartId"]);
            item.OrganizeUserNO = context.Request["LoginUID"];
            item.Name =context.Request["Name"];
            item.Range = context.Request["Range"];
            item.Type = Convert.ToByte(context.Request["Type"]);
            item.ActStartTime = Convert.ToDateTime(context.Request["ActStartTime"]);
            item.ActEndTime = Convert.ToDateTime(context.Request["ActEndTime"]);
            item.Introduction = context.Request["Introduction"];
            item.EntStartTime = Convert.ToDateTime(context.Request["EntStartTime"]);
            item.EntEndTime = Convert.ToDateTime(context.Request["EntEndTime"]);
            item.Address= context.Request["Address"];
            item.Awards = context.Request["Awards"];
            item.AttachUrl = context.Request["AttachUrl"]??"";
            item.ActivityImg = context.Request["ActivityImg"];
            item.SendExamStatus= Convert.ToByte(context.Request["SendExamStatus"]);
            item.CreateUID = context.Request["LoginUID"];
            jsonModel = bll.EditDepartActivity(item,context.Request["ProjectStr"]??"");
        }
        #endregion        

        #region 修改部门活动
        private void EditActiActivity(HttpContext context)
        {
            int itemid = Convert.ToInt32(context.Request["ItemId"]);
            jsonModel = bll.GetEntityById(itemid);
            if (jsonModel.errNum == 0)
            {
                Acti_Activity item = jsonModel.retData as Acti_Activity;
                item.Id = itemid;
                item.Name = context.Request["Name"];
                item.Range = context.Request["Range"];
                item.Type = Convert.ToByte(context.Request["Type"]);
                item.ActStartTime = Convert.ToDateTime(context.Request["ActStartTime"]);
                item.ActEndTime = Convert.ToDateTime(context.Request["ActEndTime"]);
                item.Introduction = context.Request["Introduction"];
                item.EntStartTime = Convert.ToDateTime(context.Request["EntStartTime"]);
                item.EntEndTime = Convert.ToDateTime(context.Request["EntEndTime"]);
                item.Address = context.Request["Address"];
                item.Awards = context.Request["Awards"];
                item.AttachUrl = context.Request["AttachUrl"]??"";
                item.ActivityImg = context.Request["ActivityImg"];
                item.SendExamStatus = Convert.ToByte(context.Request["SendExamStatus"]);
                item.EditUID = context.Request["LoginUID"];
                jsonModel = bll.EditDepartActivity(item,"");
            }
        }
        #endregion

        #region 删除部门活动
        private void DelActiActivity(HttpContext context)
        {
            int itemid = Convert.ToInt32(context.Request["ItemId"]);
            jsonModel = bll.DeleteFalse(itemid);
        }
        #endregion

        #region 浏览次数设置
        private void BrowsingTimesSet(HttpContext context)
        {
            int itemid = Convert.ToInt32(context.Request["ItemId"]);
            jsonModel = bll.GetEntityById(itemid);
            if (jsonModel.errNum == 0)
            {
                Acti_Activity item = jsonModel.retData as Acti_Activity;
                item.Id = itemid;
                item.BrowsingTimes = item.BrowsingTimes + 1;
                jsonModel = bll.Update(item);
            }
        }
        #endregion    

        #region 获取招新申请表的分页数据
        private void GetActiRecruitApplyDataPage(HttpContext context)
        {
            try
            {
                Hashtable ht = new Hashtable();                
                ht.Add("ExamStatus", context.Request["ExamStatus"] ?? "");
                ht.Add("Type", context.Request["Type"] ?? "");
                ht.Add("CreateUID", context.Request["CreateUID"] ?? "");
                ht.Add("OSLeaderNo", context.Request["OSLeaderNo"] ?? "");
                ht.Add("DepartId", context.Request["DepartId"] ?? "");
                ht.Add("IsOnlyBase", context.Request["IsOnlyBase"] ?? "0"); //是否只需要表中基本信息 0是（默认）；1不是    
                bool ispage = true;
                if (!string.IsNullOrEmpty(context.Request["ispage"]))
                {
                    ispage = Convert.ToBoolean(context.Request["ispage"]);
                }
                ht.Add("PageIndex", context.Request["PageIndex"] ?? "1");
                ht.Add("PageSize", context.Request["PageSize"] ?? "10");
                jsonModel = app_bll.GetPage(ht, ispage);
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

        #region 根据Id获取招新申请详情
        private void GetActiRecruitApplyById(HttpContext context)
        {
            int itemid = Convert.ToInt32(context.Request["ItemId"]);
            jsonModel = app_bll.GetEntityById(itemid);
        }
        #endregion 

        #region 添加招新申请
        private void AddActiRecruitApply(HttpContext context)
        {
            Acti_RecruitApply item = new Acti_RecruitApply();
            item.Id = 0;
            item.ActivityId = Convert.ToInt32(context.Request["ActivityId"]);
            item.Type = Convert.ToByte(context.Request["Type"]);
            item.Content = context.Request["Content"];
            item.CreateUID = context.Request["LoginUID"];
            jsonModel = bll.AddActiRecruitApply(item);
        }
        #endregion        

        #region 招新申请审核
        private void ActiRecruitApply_Audit(HttpContext context)
        {
            int itemid = Convert.ToInt32(context.Request["ItemId"]);
            jsonModel = bll.GetEntityById(itemid);
            if (jsonModel.errNum == 0)
            {
                Acti_RecruitApply item = jsonModel.retData as Acti_RecruitApply;
                item.Id = itemid;
                item.ExamUserNo = context.Request["ExamUserNo"];
                item.ExamStatus = Convert.ToByte(context.Request["ExamStatus"]);
                item.ExamSuggest = context.Request["ExamSuggest"];
                jsonModel = bll.ActiRecruitApply_Audit(item);
            }
        }
        #endregion

        #region 获取活动项目成员的分页数据
        private void GetActiProjectDataPage(HttpContext context)
        {
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("Id", context.Request["Id"] ?? "");
                ht.Add("Name", context.Request["Name"] ?? "");
                ht.Add("ActivityId", context.Request["ActivityId"] ?? "");
                ht.Add("IsMember", context.Request["IsMember"] ?? "0");
                ht.Add("LoginUID", context.Request["LoginUID"] ?? "");
                ht.Add("IsOnlyBase", context.Request["IsOnlyBase"] ?? "0"); //是否只需要表中基本信息 0是（默认）；1不是     
                bool ispage = true;
                if (!string.IsNullOrEmpty(context.Request["ispage"]))
                {
                    ispage = Convert.ToBoolean(context.Request["ispage"]);
                }
                ht.Add("PageIndex", context.Request["PageIndex"] ?? "1");
                ht.Add("PageSize", context.Request["PageSize"] ?? "10");
                jsonModel = pro_bll.GetPage(ht, ispage);
                string isUnifiedInfo = context.Request["IsUnifiedInfo"] ?? "0";//是否需要用户信息 0不需要（默认）；1需要
                if (isUnifiedInfo != "0")
                {
                    jsonModel = nameCommon.AddCreateNameForData(jsonModel, 4, ispage, "JoinMember", "", "", context.Request["SerUserName"] ?? "");
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

        #region 添加活动项目
        private void AddActiProject(HttpContext context)
        {
            Acti_Project item = new Acti_Project();
            item.Id = 0;
            item.ActivityId = Convert.ToInt32(context.Request["ActivityId"]);
            item.Name = context.Request["Name"];           
            item.CreateUID = context.Request["LoginUID"];
            jsonModel = bll.EditActiProject(item);
        }
        #endregion        

        #region 修改活动项目
        private void EditActiProject(HttpContext context)
        {
            int itemid = Convert.ToInt32(context.Request["ItemId"]);
            jsonModel = bll.GetEntityById(itemid);
            if (jsonModel.errNum == 0)
            {
                Acti_Project item = jsonModel.retData as Acti_Project;
                item.Id = itemid;
                item.Name = context.Request["Name"];                
                item.EditUID = context.Request["LoginUID"];
                jsonModel =bll.EditActiProject(item);
            }
        }
        #endregion

        #region 删除活动项目
        private void DelActiProject(HttpContext context)
        {
            int itemid = Convert.ToInt32(context.Request["ItemId"]);
            jsonModel = pro_bll.DeleteFalse(itemid);
        }
        #endregion

        #region 报名部门活动项目
        private void JoinActivityProject(HttpContext context)
        {
            int itemid = Convert.ToInt32(context.Request["ItemId"]);
            int activityId = Convert.ToInt32(context.Request["ActivityId"]);
            jsonModel = bll.GetEntityById(activityId);
            if (jsonModel.errNum == 0)
            {
                string member = context.Request["LoginUID"];
                Acti_Activity actitem = jsonModel.retData as Acti_Activity;
                actitem.Id = activityId;
                if (Convert.ToDateTime(actitem.EntStartTime).Date >DateTime.Now.Date)
                {
                    jsonModel.errNum = -1; //报名还未开始
                }
                else if (DateTime.Now.Date > Convert.ToDateTime(actitem.EntEndTime).Date)
                {
                    jsonModel.errNum = -2; //报名已结束
                }
                else
                {
                    jsonModel = pro_bll.GetEntityById(itemid);
                    if(jsonModel.errNum == 0)
                    {
                        Acti_Project proitem = jsonModel.retData as Acti_Project;
                        proitem.JoinMembers += !string.IsNullOrEmpty(proitem.JoinMembers) ? ("," + member) : member;
                        jsonModel = pro_bll.Update(proitem);
                    }                   
                }
            }
        }
        #endregion

        #region 删除项目成员
        private void DelProjectMember(HttpContext context)
        {
            int itemid = Convert.ToInt32(context.Request["ItemId"]);
            string memberNos = context.Request["MemberNos"];
            if (!string.IsNullOrEmpty(memberNos))
            {
                jsonModel = pro_bll.GetEntityById(itemid);
                if (jsonModel.errNum == 0)
                {
                    Acti_Project proitem = jsonModel.retData as Acti_Project;
                    string[] oldMem = proitem.JoinMembers.Split(','),delMem=memberNos.Split(',');
                    string newMem =string.Join(",",oldMem.Except(delMem).ToArray());
                    proitem.JoinMembers = newMem;
                    jsonModel = pro_bll.Update(proitem);
                }
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