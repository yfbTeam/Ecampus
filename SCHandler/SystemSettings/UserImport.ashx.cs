
using SCBLL;
using SCModel;
using SCUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace SCHandler.SystemSettings
{
    /// <summary>
    /// UserImport 的摘要说明
    /// </summary>
    public class UserImport : IHttpHandler
    {

        Plat_RoleOfUserService bll = new Plat_RoleOfUserService();
        JsonModel jsonModel = new JsonModel() { errNum = 0, errMsg = "success", retData = "" };
        string sysAccountNo = ConfigHelper.GetConfigString("SysAccountNo");
        string u_handlerUrl = ConfigHelper.GetConfigString("Unified_HandlerUrl").SafeToString();
        JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string func = context.Request["Func"].SafeToString();
            if (func == "UserAsync")
            {
                UserAsync(context);
            }
        }
        private void UserAsync(HttpContext context)
        {
            string ReturnResult = "";

            string urlHead = u_handlerUrl + "/UserManage/UserInfo.ashx?";
            string urlbady = "Func=GetData&IsPage=false";
            string PageUrl = urlHead + urlbady;
            string result = NetHelper.RequestPostUrl(PageUrl, urlbady);
            int starIndex = result.IndexOf(":") + 1;
            int endIndex = result.LastIndexOf("}");

            result = result.Substring(starIndex, endIndex - starIndex);

            jsonModel = jss.Deserialize<JsonModel>(result);
            DataTable dtb = new DataTable();
            for (int i = 0; i < ((object[])(jsonModel.retData)).Length; i++)
            {
                Dictionary<string, object> ht = (Dictionary<string, object>)(((object[])(jsonModel.retData))[i]);
                if (dtb.Columns.Count == 0)
                {
                    foreach (string key in ht.Keys)
                    {
                        dtb.Columns.Add(key, ht[key].GetType());//添加dt的列名
                    }
                }
                DataRow row = dtb.NewRow();
                foreach (string key in ht.Keys)
                {

                    row[key] = ht[key];//添加列值
                }
                dtb.Rows.Add(row);//添加一行
            }

            ReturnResult = bll.ImportUser(dtb);
            context.Response.Write(ReturnResult);
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