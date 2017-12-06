using Newtonsoft.Json.Linq;
using SCModel;
using SCUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace SCHandler
{
    /// <summary>
    /// GetUserNameHandler 的摘要说明
    /// </summary>
    public class GetUserNameHandler : IHttpHandler
    {
        JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();
        string sysAccountNo = ConfigHelper.GetConfigString("SysAccountNo");
        string u_handlerUrl = ConfigHelper.GetConfigString("Unified_HandlerUrl").SafeToString();
        public void ProcessRequest(HttpContext context)
        {

        }
        #region 为返回的数据添加用户姓名列
        /// <summary>
        /// 为返回的数据添加用户姓名列
        /// </summary>
        /// <param name="jsonModel">数据</param>
        /// <param name="type">type 0获取所有学生(默认)；1获取所有教师；2 根据UniqueNo获取用户；3 根据UniqueNo获取学生；4 获取所有用户信息</param>
        /// <param name="ispage">数据是否是分页的</param>
        /// <param name="oneUserField">第一个需要返回用户姓名的列</param>
        /// <param name="uniqueNos">uniqueNos字符串,以逗号分隔，默认为空</param>
        /// <param name="twoUserField">第二个需要返回用户姓名的列</param>
        /// <param name="name">根据名称搜索</param>
        /// <param name="AcademicId">默认0 当前学期； 历史学期 传学期id</param>
        /// <returns></returns>
        public JsonModel AddCreateNameForData(JsonModel jsonModel,int type = 0, bool ispage = false,string oneUserField = "CreateUID",string uniqueNos="",string twoUserField="",string name="",string AcademicId="0")
        {
            if (jsonModel.errNum == 0)
            {
                List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
                List<Dictionary<string, object>> classList = new List<Dictionary<string, object>>();
                PagedDataModel<Dictionary<string, object>> pageModel = null;
                if (ispage)
                {
                    pageModel = jsonModel.retData as PagedDataModel<Dictionary<string, object>>;
                    list = pageModel.PagedData as List<Dictionary<string, object>>;
                }
                else
                {
                    list = jsonModel.retData as List<Dictionary<string, object>>;
                }
                List<Dictionary<string, object>> allList = new List<Dictionary<string, object>>();
                allList = GetUnifiedUserData(type, uniqueNos,name, AcademicId);
                if (!string.IsNullOrEmpty(name))
                {
                    List<string> stuUniqueNo = (from dic in allList select dic["UniqueNo"].ToString()).ToList();
                    list = (from dic in list
                            where stuUniqueNo.Contains(dic[oneUserField].ToString())
                            select dic).ToList();
                }
                foreach (Dictionary<string, object> item in list)
                {
                    try
                    {
                        Dictionary<string, object> dicItem = (from dic in allList
                                                              where dic["UniqueNo"].ToString() == item[oneUserField].ToString()
                                                              select dic).FirstOrDefault();
                        item.Add("CreateName", dicItem==null?"":dicItem["Name"].ToString());
                        item.Add("AbsHeadPic", dicItem == null ? "" : dicItem["AbsHeadPic"].ToString());
                        item.Add("Sex", dicItem == null ? "" : dicItem["Sex"].ToString());
                        if (dicItem !=null&&dicItem.ContainsKey("OrgName"))
                        {
                            item.Add("OrgName", dicItem["OrgName"].ToString());
                        }
                        if (dicItem != null && dicItem.ContainsKey("GradeName"))
                        {
                            item.Add("GradeName", dicItem["GradeName"].ToString());
                        }
                        if (!string.IsNullOrEmpty(twoUserField))
                        {
                            Dictionary<string, object> dicItem_two = (from dic in allList
                                                                  where dic["UniqueNo"].ToString() == item[twoUserField].ToString()
                                                                  select dic).FirstOrDefault();
                            item.Add("TwoUserName", dicItem_two==null?"": dicItem_two["Name"].ToString());
                            item.Add("TwoAbsHeadPic", dicItem_two == null ? "" : dicItem_two["AbsHeadPic"].ToString());
                        }                        
                    }
                    catch (Exception ex)
                    {
                        LogService.WriteErrorLog(ex.Message);
                    }

                }
                if (ispage)
                {
                    pageModel.PagedData = list;
                    jsonModel.retData = pageModel;
                }
                else { jsonModel.retData = list; }
            }
            return jsonModel;
        }
        #endregion

        #region 获取统一认证中心用户信息
        public List<Dictionary<string, object>> GetUnifiedUserData(int type,string uniqueNos = "",string name="", string AcademicId = "0")
        {
            string urlHead = u_handlerUrl + "/UserManage/UserInfo.ashx?";
            string urlbady = "Func=GetData&SysAccountNo=" + sysAccountNo+"&Ispage=false";
            if (type == 0) //type 0获取所有学生(默认)；1获取所有教师；2 根据UniqueNo获取用户；3 根据UniqueNo获取学生；4 获取所有用户信息
            {
                urlbady += "&IsStu=true&AcademicId="+ AcademicId;
            }
            else if(type == 1)
            {
                urlbady += "&IsStu=false";
            }
            else if (type == 2)
            {
                if (!string.IsNullOrEmpty(uniqueNos))
                {
                    urlbady += "&UniqueNo=" + uniqueNos;
                }
            }
            else if (type == 3)
            {
                if (!string.IsNullOrEmpty(uniqueNos))
                {
                    urlbady += "&IsStu=true&UniqueNo=" + uniqueNos+ "&AcademicId = "+ AcademicId;
                }
            }
            else //全部用户
            {

            }
            if (!string.IsNullOrEmpty(name))
            {
                urlbady += "&Name="+name;
            }          
            string PageUrl = urlHead + urlbady;
            return AnalyticalReturnData(NetHelper.RequestPostUrl(PageUrl, urlbady));
        }
        #endregion

        #region 获取班级信息
        private List<Dictionary<string, object>> GetClassInfoData()
        {
            string urlHead = u_handlerUrl + "/EduManage/ClassHandler.ashx?";
            string urlbady = "Func=GetData&SysAccountNo=" + sysAccountNo+ "&Ispage=false";
            string PageUrl = urlHead + urlbady;
            return AnalyticalReturnData(NetHelper.RequestPostUrl(PageUrl, urlbady));
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
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}