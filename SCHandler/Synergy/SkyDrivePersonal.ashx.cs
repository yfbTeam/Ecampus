using SCModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using SCUtility;
using SCBLL;
namespace SCHandler.Synergy
{
    /// <summary>
    /// SkyDrivePersonal 的摘要说明
    /// </summary>
    public class SkyDrivePersonal : IHttpHandler
    {

        MyResourceService Bll = new MyResourceService();

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string FuncName = context.Request["Func"].ToString();
            if (FuncName != null && FuncName != "")
            {
                switch (FuncName)
                {
                    case "GetPageList":
                        GetPage(context);
                        break;
                    case "TreeNodes":
                        TreeNodes(context);
                        break;
                    case "BindNav":
                        BindNav(context);
                        break;

                    default:
                        break;
                }
            }
        }
        #region 面包屑
        /// <summary>
        /// 文件目录结构
        /// </summary>
        /// <param name="pid"></param>
        private void BindNav(HttpContext context)
        {
            JsonModel jsonModel = null;
            JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();
            string result = "";
            StringBuilder sbjson = new StringBuilder();
            Hashtable ht = new Hashtable();
            ht.Add("TableName", "MyResource");
            Hashtable ht1 = new Hashtable();
            ht1.Add("ID", context.Request["Pid"].SafeToString());
            ht1.Add("TableName", "MyResource");
            ht1.Add("CreateUID", context.Request["CreateUID"].SafeToString());
            jsonModel = Bll.GetPage(ht1, false);
            List<Dictionary<string, object>> list = (List<Dictionary<string, object>>)jsonModel.retData;

            string ids = "";
            if (list.Count > 0)
            {
                string code = list[0]["code"].SafeToString().Replace('|', ',').SafeToString();
                ids = code == "0" ? "" : code;
                if (ids.Length > 0)
                {
                    ids += "," + list[0]["ID"].SafeToString();
                }
                else { ids = list[0]["ID"].SafeToString(); }

            }
            if (ids == "")
            {
                ids = "0";
            }
            ht.Add("IDS", ids);
            ht.Add("OrderBy", "ID");
            ht.Add("CreateUID", context.Request["CreateUID"].SafeToString());

            jsonModel = Bll.GetPage(ht, false);
            result = "{\"result\":" + jss.Serialize(jsonModel) + "}";

            context.Response.Write(result);
            context.Response.End();
        }
        #endregion

        #region 文件目录结构
        /// <summary>
        /// 文件目录结构
        /// </summary>
        /// <param name="pid"></param>
        private void TreeNodes(HttpContext context)
        {
            StringBuilder sbjson = new StringBuilder();
            Hashtable ht = new Hashtable();
            ht.Add("IsFolder", 1);
            ht.Add("TableName", "MyResource");
            ht.Add("CreateUID", context.Request["CreateUID"].SafeToString());
            sbjson.Append("{ \"id\":0,\"root\":\"/DriveFolder/" + context.Request["CreateUID"] + "\", \"pId\": 0, \"name\":\"根目录\", \"open\":\"true\"},");

            JsonModel model = Bll.GetPage(ht, false);
            List<Dictionary<string, object>> list = (List<Dictionary<string, object>>)model.retData;
            if (list.Count > 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    string name = list[i]["Name"].SafeToString();
                    string ID = list[i]["ID"].SafeToString();
                    string FileUrl = list[i]["FileUrl"].SafeToString();
                    string pid = list[i]["Pid"].SafeToString();
                    sbjson.Append("{\"id\": " + ID + ",\"root\":\"" + FileUrl + "\", \"pId\": " + pid + ", \"name\":\"" + name + "\"},");
                }
            }
            string returnJson = "[" + sbjson.ToString().TrimEnd(',') + "]";

            context.Response.Write(returnJson);
            context.Response.End();
        }
        #endregion

        #region 获取分页数据
        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="context"></param>
        private void GetPage(HttpContext context)
        {
            string result = "";
            JsonModel jsonModel = null;
            JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();

            try
            {
                MyResource resource = new MyResource();
                Hashtable ht = new Hashtable();
                ht.Add("PageIndex", context.Request["PageIndex"].SafeToString());
                ht.Add("PageSize", context.Request["PageSize"].SafeToString());
                ht.Add("TableName", "MyResource");
                ht.Add("Pid", context.Request["Pid"].SafeToString());
                ht.Add("DocName", context.Request["DocName"].SafeToString());
                ht.Add("Postfixs", context.Request["Postfixs"].SafeToString());
                ht.Add("CreateUID", context.Request["CreateUID"].SafeToString());
                DateTime datetime = DateTime.Now;
                string Time = context.Request["CreateTime"].SafeToString();
                switch (Time)
                {
                    case "Week":
                        datetime = datetime.AddDays(-7);
                        break;
                    case "Month":
                        datetime = datetime.AddMonths(-1);
                        break;
                    case "Year":
                        datetime = datetime.AddMonths(-6);
                        break;
                    default:
                        datetime = datetime.AddYears(-1);
                        break;
                }
                ht.Add("CreateTime", datetime);
                jsonModel = Bll.GetPage(ht);
                result = "{\"result\":" + jss.Serialize(jsonModel) + "}";
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