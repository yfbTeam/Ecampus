
using SCBLL;
using SCUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace SCWeb.SystemSettings
{
    /// <summary>
    /// UserInfo 的摘要说明
    /// </summary>
    public class UserInfo : IHttpHandler
    {


        JavaScriptSerializer jss = new JavaScriptSerializer();
        string sysAccountNo = ConfigHelper.GetConfigString("SysAccountNo");
        string u_handlerUrl = ConfigHelper.GetConfigString("Unified_HandlerUrl").SafeToString();

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string FuncName = context.Request["Func"].ToString();
            if (FuncName != null && FuncName != "")
            {
                switch (FuncName)
                {
                    case "Login":
                        Login(context);
                        break;
                    case "GetUserRole":
                        GetUserRole(context);
                        break;
                    case "UpdatePwd":
                        UpdatePwd(context);
                        break;
                    case "UserSkim":
                        UserSkim(context);
                        break;
                    case "GetUserSkim":
                        GetUserSkim(context);
                        break;
                    default:
                        break;
                }
            }
        }
        private void GetUserSkim(HttpContext context)
        {
            string urlHead = u_handlerUrl + "/UserSkinHander.ashx?";
            string MinLong = context.Request["MinLong"].SafeToString();
            string MaxLong = context.Request["MaxLong"].SafeToString();
            string MinTime = context.Request["MinTime"].SafeToString();
            string MaxTime = context.Request["MaxTime"].SafeToString();
            string ToUrl = context.Request["ToUrl"].SafeToString();
            string UserName = context.Request["UserName"].SafeToString();
            string PageIndex = context.Request["PageIndex"].SafeToString();
            string PageSize = context.Request["PageSize"].SafeToString();

            string Func = context.Request["Func"].SafeToString();
            string urlbady = "Func=GetData&WebSite='Sinptech_Study_System'&MinLong=" + MinLong + "&MaxLong=" + MaxLong + "&MinTime=" + MinTime + "&MaxTime=" + MaxTime + "&ToUrl=" + ToUrl
              + "&UserName=" + UserName + "&PageIndex=" + PageIndex + "&PageSize=" + PageSize;
            string PageUrl = urlHead + urlbady;
            string result = NetHelper.RequestPostUrl(PageUrl, urlbady);
            context.Response.Write(result);
            context.Response.End();
        }
        private void UserSkim(HttpContext context)
        {
            string urlHead = u_handlerUrl + "/UserSkinHander.ashx?";
            string LoginName = context.Request["LoginName"].SafeToString();
            string UniqueNo = context.Request["UniqueNo"].SafeToString();
            string ToUrl = context.Request["ToUrl"].SafeToString();
            string FromUrl = context.Request["FromUrl"].SafeToString();
            string WebSite = context.Request["WebSite"].SafeToString();
            string SkinLong = context.Request["SkinLong"].SafeToString();

            string Func = context.Request["Func"].SafeToString();
            string urlbady = "Func=AddSkim&UniqueNo=" + UniqueNo + "&LoginName=" + LoginName + "&ToUrl=" + ToUrl + "&FromUrl=" + FromUrl + "&WebSite=" + WebSite + "&SkinLong=" + SkinLong;
            string PageUrl = urlHead + urlbady;
            string result = NetHelper.RequestPostUrl(PageUrl, urlbady);
            context.Response.Write(result);
            context.Response.End();
        }
        private void UpdatePwd(HttpContext context)
        {
            string urlHead = u_handlerUrl + "/UserManage/UserInfo.ashx?";
            string loginName = context.Request["loginName"].SafeToString();
            string OldPwd = context.Request["OldPwd"].SafeToString();
            string NewPwd = context.Request["NewPwd"].SafeToString();
            string urlbady = "Func=UpdatePwd&SysAccountNo=" + sysAccountNo + "&LoginName=" + loginName + "&OldPwd=" + OldPwd + "&NewPwd=" + NewPwd;
            string PageUrl = urlHead + urlbady;
            string result = NetHelper.RequestPostUrl(PageUrl, urlbady);
            context.Response.Write(result);
            context.Response.End();
        }
        #region 登录
        private void Login(HttpContext context)
        {
            string urlHead = u_handlerUrl + "/UserManage/UserInfo.ashx?";
            string loginName = context.Request["loginName"].SafeToString();
            string passWord = context.Request["passWord"].SafeToString();
            string urlbady = "Func=Login&SysAccountNo=" + sysAccountNo + "&loginName=" + loginName + "&passWord=" + passWord;
            string PageUrl = urlHead + urlbady;
            string result = NetHelper.RequestPostUrl(PageUrl, urlbady);
            context.Response.Write(result);
            context.Response.End();
        }
        #endregion

        private void GetUserRole(HttpContext context)
        {
            Plat_RoleOfUserService bll = new Plat_RoleOfUserService();
            string result = bll.GetUserOfAdmin();
            context.Response.Write(result);
            context.Response.End();
        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}