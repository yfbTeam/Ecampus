using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SCBLL;
using SCUtility;
using SCModel;
using System.Web.Script.Serialization;
using System.Collections;
namespace SCWeb.ResourceManage
{
    /// <summary>
    /// PublicResoure1 的摘要说明
    /// </summary>
    public class PublicResoure1 : IHttpHandler
    {
        ResourcesInfoService Bll = new ResourcesInfoService();

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string FuncName = context.Request["Func"].ToString();
            if (FuncName != null && FuncName != "")
            {
                switch (FuncName)
                {

                    case "Down":
                        Down(context);
                        break;
                    case "Del":
                        Del(context);
                        break;

                    default:
                        break;
                }
            }
        }
        #region 文件下载
        /// <summary>
        /// 文件下载
        /// </summary>
        /// <returns></returns>

        private void Down(HttpContext context)
        {
            string IDCard = context.Request["IDCard"].SafeToString();

            ZipHelper zip = new ZipHelper();
            JsonModel jsonModel = null;
            JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();

            string ids = context.Request.Form["DownID"] == null ? "" : context.Request.Form["DownID"].ToString();
            string ZipUrl = context.Server.MapPath(ConfigHelper.GetConfigString("ZipUrl")) + "/" + DateTime.Now.Ticks;
            string DownUrl = ConfigHelper.GetConfigString("DownUrl");
            string result = "0";

            try
            {
                string[] idarry = ids.TrimEnd(',').Split(',');
                FileHelper.CreateDirectory(ZipUrl);
                for (int i = 0; i < idarry.Length; i++)
                {
                    #region 文件移动
                    JsonModel model = Bll.GetEntityById(int.Parse(idarry[i]));
                    ResourcesInfo resource = (ResourcesInfo)(model.retData);
                    string FileUrl = resource.FileUrl;
                    if (resource.postfix == "")
                    {
                        FileHelper.CopyFolder(context.Server.MapPath(FileUrl), ZipUrl);
                    }
                    else
                    {
                        //增加下载记录
                        UpdateClick(idarry[i].ToString(), "3", IDCard, "0");
                        UpdateClick(idarry[i].ToString(), "0");

                        FileHelper.CopyTo(context.Server.MapPath(FileUrl), ZipUrl + FileUrl.Substring(FileUrl.LastIndexOf("/")));
                    }
                    #endregion
                }
                string ZipName = "/下载文件" + DateTime.Now.Ticks + ".rar";
                //文件打包
                SharpZip.PackFiles(context.Server.MapPath(ConfigHelper.GetConfigString("ZipUrl")) + ZipName, ZipUrl);

                jsonModel = new JsonModel()
                {
                    errNum = 0,
                    errMsg = "",
                    //retData = DownUrl + "\\" + ZipName + ".rar"
                    retData = DownUrl + "\\" + ZipName

                };
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

        #region 文件删除
        /// <summary>
        /// 文件删除
        /// </summary>
        /// <returns></returns>

        private void Del(HttpContext context)
        {
            JsonModel jsonModel = null;
            JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();

            string ids = context.Request.Form["DelID"] == null ? "" : context.Request.Form["DelID"].ToString();

            string result = "0";

            try
            {
                string[] idarry = ids.TrimEnd(',').Split(',');
                for (int i = 0; i < idarry.Length; i++)
                {
                    #region 文件删除
                    JsonModel model = Bll.GetEntityById(int.Parse(idarry[i]));
                    ResourcesInfo resource = (ResourcesInfo)(model.retData);
                    string FileUrl = resource.FileUrl;
                    if (resource.postfix == "")
                    {
                        FileHelper.DeleteDirectory(context.Server.MapPath(FileUrl));
                    }
                    else
                    {
                        FileHelper.DeleteFile(context.Server.MapPath(FileUrl));
                    }
                    #endregion
                    //数据删除
                    jsonModel = Bll.Delete(int.Parse(idarry[i]));
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
            result = "{\"result\":" + jss.Serialize(jsonModel) + "}";

            context.Response.Write(result);
            context.Response.End();

        }
        #endregion
        /// <summary>
        /// 点击下载评价
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="ClickType"></param>
        /// <param name="IDCard"></param>
        private void UpdateClick(string ID, string ClickType, string IDCard, string Evalue)
        {
            ClickDetailService clickBll = new ClickDetailService();
            Hashtable ht = new Hashtable();
            ht.Add("TableName", "ClickDetail");
            ClickDetail clickModeol = new ClickDetail();

            JsonModel jsonmodel = clickBll.GetPage(ht, false, " and ResourcesID=" + ID + " and CreateUID='" + IDCard + "' and ClickType=" + ClickType);
            if (jsonmodel.errNum == 0)
            {
                List<Dictionary<string, object>> list = (List<Dictionary<string, object>>)jsonmodel.retData;
                int ClickNum = 0;
                int ClickID = 0;
                foreach (Dictionary<string, object> result in list)
                {
                    ClickNum = Convert.ToInt32(result["ClickNum"]);
                    ClickID = Convert.ToInt32(result["ID"]);
                }
                if (int.Parse(Evalue) > 0)
                {
                    clickModeol.ClickNum = int.Parse(Evalue);
                }
                else
                {
                    clickModeol.ClickNum = ClickNum + 1;
                }
                clickModeol.EditTime = DateTime.Now;
                clickModeol.LastTime = DateTime.Now;
                clickModeol.ClickType = byte.Parse(ClickType);
                clickModeol.EditUID = IDCard;
                clickModeol.ID = ClickID;
                clickBll.Update(clickModeol);
            }
            else
            {
                if (Evalue.Length > 0)
                {
                    clickModeol.ClickNum = int.Parse(Evalue);
                }
                else
                {
                    clickModeol.ClickNum = 1;
                }
                clickModeol.ClickTime = DateTime.Now;
                clickModeol.CreateTime = DateTime.Now;
                clickModeol.EditTime = DateTime.Now;
                clickModeol.LastTime = DateTime.Now;
                clickModeol.ClickType = byte.Parse(ClickType);
                clickModeol.CreateUID = IDCard;
                clickModeol.EditUID = IDCard;
                clickModeol.ResourcesID = int.Parse(ID);
                clickModeol.IsDelete = 0;
                jsonmodel = clickBll.Add(clickModeol);
            }
        }
        private void UpdateClick(string ID, string Evalue)
        {
            ResourcesInfo resource = new ResourcesInfo();
            JsonModel jsonmodel = Bll.GetEntityById(int.Parse(ID));
            int DownCount = 0;
            int ClickCount = 0;
            int EvalueCount = 0;
            int EvalueResult = 0;
            //List<Dictionary<string, object>> list = (List<Dictionary<string, object>>)jsonmodel.retData;
            ResourcesInfo list = (ResourcesInfo)jsonmodel.retData;

            //foreach (Dictionary<string, object> result in list)
            //{
            DownCount = Convert.ToInt32(list.DownCount);
            ClickCount = Convert.ToInt32(list.ClickCount);
            EvalueCount = Convert.ToInt32(list.EvalueCount);
            EvalueCount = Convert.ToInt32(list.EvalueCount);
            EvalueResult = Convert.ToInt32(list.EvalueResult);
            //}
            resource.DownCount = DownCount + 1;
            resource.ClickCount = ClickCount + 1;
            resource.EvalueCount = EvalueCount + 1;
            resource.EvalueResult = EvalueResult + int.Parse(Evalue);
            resource.ID = int.Parse(ID);
            Bll.Update(resource);
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