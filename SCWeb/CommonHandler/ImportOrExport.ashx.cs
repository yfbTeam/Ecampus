using Newtonsoft.Json.Linq;
using SCUtility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace SCWeb.CommonHandler
{
    /// <summary>
    /// ImportOrExport 的摘要说明
    /// </summary>
    public class ImportOrExport : IHttpHandler
    {
        JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();
        string self_handlerUrl = ConfigHelper.GetConfigString("HttpService").SafeToString();
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string func = context.Request["Func"];
            string result = string.Empty;
            try
            {
                switch (func)
                {
                    case "Export_AssoMember": //导出社团成员
                        Export_AssoMember(context);
                        break;
                    case "Export_DepartMember": //导出部门成员
                        Export_DepartMember(context);
                        break;
                    case "Export_ActiProMember": //导出部门活动项目报名成员
                        Export_ActiProMember(context);
                        break;
                    case "Export_DormRoomStuList": //导出宿舍成员
                        Export_DormRoomStuList(context);
                        break;
                    default:
                        break;
                }
                LogService.WriteLog(func);
            }
            catch (Exception ex)
            {
                LogService.WriteErrorLog(ex.Message);
            }
        }

        #region 导出社团成员
        public void Export_AssoMember(HttpContext context)
        {
            string[] columnArr = new string[] { "CreateName", "AssoName", "Position", "GradeName", "OrgName"};
            string dtHeader = "姓名,社团,职位,年级,班级";
            string fileName = "社团成员信息";
            DataTable excelUserDt = GetAssoMemberData(context);
            DataTable newDt = excelUserDt.DefaultView.ToTable(true, columnArr);
            ExcelHelper.ExportByWeb(newDt, dtHeader, fileName, "Sheet1");          
        }
        #endregion
        #region 获取社团成员
        private DataTable GetAssoMemberData(HttpContext context)
        {
            string urlHead = self_handlerUrl + "/StuAssociate/Asso_InfoHandler.ashx?";
            string urlbady = "Func=GetAssoMemberDataPage&ispage=false&IsOnlyBase=1&IsUnifiedInfo=1";
            if (!string.IsNullOrEmpty(context.Request["AssoId"]))
            {
                urlbady += "&AssoId=" + context.Request["AssoId"];
            }
            string PageUrl = urlHead + urlbady;
            string result = NetHelper.RequestPostUrl(PageUrl, urlbady);
            return ListToDataTable(AnalyticalReturnData(result));
        }
        #endregion

        #region 导出部门成员
        public void Export_DepartMember(HttpContext context)
        {
            string[] columnArr = new string[] { "CreateName", "DepartName", "Position", "GradeName", "OrgName" };
            string dtHeader = "姓名,部门,职位,年级,班级";
            string fileName = "部门成员信息";
            DataTable excelUserDt = GetDepartMemberData(context);
            DataTable newDt = excelUserDt.DefaultView.ToTable(true, columnArr);
            ExcelHelper.ExportByWeb(newDt, dtHeader, fileName, "Sheet1");
        }
        #endregion
        #region 获取部门成员
        private DataTable GetDepartMemberData(HttpContext context)
        {
            string urlHead = self_handlerUrl + "/StuActivity/Acti_DepartHandler.ashx?";
            string urlbady = "Func=GetDepartMemberDataPage&ispage=false&IsOnlyBase=1&IsUnifiedInfo=1";
            if (!string.IsNullOrEmpty(context.Request["DepartId"]))
            {
                urlbady += "&DepartId=" + context.Request["DepartId"];
            }
            string PageUrl = urlHead + urlbady;
            string result = NetHelper.RequestPostUrl(PageUrl, urlbady);
            return ListToDataTable(AnalyticalReturnData(result));
        }
        #endregion

        #region 导出部门活动项目报名成员
        public void Export_ActiProMember(HttpContext context)
        {
            string[] columnArr = new string[] { "DepartName", "ActivityName", "Name","CreateName", "GradeName", "OrgName" };
            string dtHeader = "部门,活动,项目,姓名,年级,班级";
            string fileName = "活动项目报名成员信息";
            DataTable excelUserDt = GetActiProjectData(context);
            DataTable newDt = excelUserDt.DefaultView.ToTable(true, columnArr);
            ExcelHelper.ExportByWeb(newDt, dtHeader, fileName, "Sheet1");
        }
        #endregion
        #region 获取部门活动项目报名成员
        private DataTable GetActiProjectData(HttpContext context)
        {
            string urlHead = self_handlerUrl + "/StuActivity/Acti_ActivityHandler.ashx?";
            string urlbady = "Func=GetActiProjectDataPage&ispage=false&IsOnlyBase=1&IsUnifiedInfo=1";
            if (!string.IsNullOrEmpty(context.Request["ActivityId"]))
            {
                urlbady += "&ActivityId=" + context.Request["ActivityId"];
            }
            urlbady += "&OrderBy=DepartId desc,T.ActivityId desc ";
            string PageUrl = urlHead + urlbady;
            string result = NetHelper.RequestPostUrl(PageUrl, urlbady);
            return ListToDataTable(AnalyticalReturnData(result));
        }
        #endregion

        #region 导出宿舍成员
        public void Export_DormRoomStuList(HttpContext context)
        {
            string[] columnArr = new string[] { "CreateName", "BuildName", "RoomName", "GradeName", "OrgName" };
            string dtHeader = "姓名,宿舍楼,宿舍,年级,班级";
            string fileName = "宿舍成员信息";
            DataTable excelUserDt = GetDormRoomStuListData(context);
            DataTable newDt = excelUserDt.DefaultView.ToTable(true, columnArr);
            ExcelHelper.ExportByWeb(newDt, dtHeader, fileName, "Sheet1");
        }
        #endregion
        #region 获取部门成员
        private DataTable GetDormRoomStuListData(HttpContext context)
        {
            string urlHead = self_handlerUrl + "/StuDormitory/Dorm_RoomHandler.ashx?";
            string urlbady = "Func=GetRoomStuListDataPage&ispage=false&IsOnlyBase=1&IsUnifiedInfo=1";
            if (!string.IsNullOrEmpty(context.Request["RoomId"]))
            {
                urlbady += "&RoomId=" + context.Request["RoomId"];
            }
            string PageUrl = urlHead + urlbady;
            string result = NetHelper.RequestPostUrl(PageUrl, urlbady);
            return ListToDataTable(AnalyticalReturnData(result));
        }
        #endregion

        #region 将接口返回的信息解析为List
        public List<Dictionary<string, object>> AnalyticalReturnData(string result)
        {
            JObject rtnObj = JObject.Parse(result);
            JObject resultObj = JsonTool.GetObjVal(rtnObj, "result");
            List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
            if (JsonTool.GetStringVal(resultObj, "errNum") == "0")
            {
                list = jss.Deserialize<List<Dictionary<string, object>>>(resultObj["retData"].ToString());                
            }
            return list;
        }
        #endregion

        #region List 转换为 DataTable数据集合        
        public DataTable ListToDataTable(List<Dictionary<string, object>> list)
        {
            DataTable dataTable = new DataTable();  //实例化
            DataTable result;
            try
            {        
                if (list.Count > 0)
                {
                    foreach (Dictionary<string, object> dictionary in list)
                    {
                        if (dictionary.Keys.Count<string>() == 0)
                        {
                            result = dataTable;
                            return result;
                        }
                        if (dataTable.Columns.Count == 0)
                        {
                            foreach (string current in dictionary.Keys)
                            {
                                dataTable.Columns.Add(current, dictionary[current].GetType());
                            }
                        }
                        DataRow dataRow = dataTable.NewRow();
                        foreach (string current in dictionary.Keys)
                        {
                            dataRow[current] = dictionary[current];
                        }
                        dataTable.Rows.Add(dataRow); //循环添加行到DataTable中
                    }
                }
            }
            catch{}
            result = dataTable;
            return result;
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