using SCBLL;
using SCHandler.CommonHandler;
using SCModel;
using SCUtility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace SCHandler.Synergy
{
    /// <summary>
    /// PublicResoure 的摘要说明
    /// </summary>
    public class PublicResoure : IHttpHandler
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
                    case "GetPageList":
                        GetPage(context);
                        break;

                    case "ResourceType":
                        ResourceType(context);
                        break;
                    case "ResourceClick":
                        ResourceClick(context);
                        break;

                    case "Evalue":
                        Evalue(context);
                        break;

                    case "GetDowDetail":
                        GetDowDetail(context);
                        break;
                    default:
                        break;
                }
            }
        }
        #region 获取下载数据
        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="context"></param>
        private void GetDowDetail(HttpContext context)
        {
            GetUserNameHandler common = new GetUserNameHandler();
            ClickDetailService Clickbll = new ClickDetailService();
            string result = "";
            JsonModel jsonModel = null;
            JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();

            try
            {
                ResourcesInfo resource = new ResourcesInfo();
                Hashtable ht = new Hashtable();
                ht.Add("PageIndex", context.Request["PageIndex"].SafeToString());
                ht.Add("PageSize", context.Request["PageSize"].SafeToString());

                jsonModel = common.AddCreateNameForData(Clickbll.GetPage(ht, true), 1, true);

                //result = "{\"result\":" + jss.Serialize(jsonModel) + "}";
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


        #region 文件点击下载评价
        /// <summary>
        /// 文件评价
        /// </summary>
        /// <param name="context"></param>
        private void Evalue(HttpContext context)
        {
            JsonModel jsonModel = null;
            string result = "";
            JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();

            try
            {
                string ID = context.Request.Form["ID"].SafeToString();
                string ClickType = context.Request.Form["ClickType"].SafeToString();
                string IDCard = context.Request["IDCard"].SafeToString();
                string Evalue = context.Request["Evalue"].SafeToString();
                UpdateClick(ID, ClickType, IDCard, Evalue);
                UpdateClick(ID, Evalue);
                jsonModel = new JsonModel()
                {
                    errNum = 0,
                    errMsg = "",
                    retData = ""
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
            }
            result = "{\"result\":" + jss.Serialize(jsonModel) + "}";
            context.Response.Write(result);
            context.Response.End();

        }
        /// <summary>
        /// 文件下载
        /// </summary>
        /// <returns></returns>

        private void ResourceClick(HttpContext context)
        {
            string ID = context.Request.Form["ID"].SafeToString();
            string ClickType = context.Request.Form["ClickType"].SafeToString();
            string IDCard = context.Request["IDCard"].SafeToString();
            UpdateClick(ID, ClickType, IDCard, "0");
            UpdateClick(ID, "0");
        }
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
        #endregion

        #region 获取分页数据
        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="context"></param>
        private void ResourceType(HttpContext context)
        {

            ResourceTypeService reType = new ResourceTypeService();

            string result = "";
            JsonModel jsonModel = null;
            JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();
            Hashtable ht = new Hashtable();
            ht.Add("TableName", "ResourceType");
            try
            {
                ResourceType resource = new ResourceType();

                jsonModel = reType.GetPage(ht, false);
                // result = "{\"result\":" + jss.Serialize(jsonModel) + "}";
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





        #region 获取分页数据
        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="context"></param>
        private void GetPage(HttpContext context)
        {
            string IDCard = context.Request["IDCard"].SafeToString();
            string result = "";
            JsonModel jsonModel = null;
            JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();

            try
            {
                ResourcesInfo resource = new ResourcesInfo();
                Hashtable ht = new Hashtable();
                ht.Add("PageIndex", context.Request["PageIndex"].SafeToString());
                ht.Add("PageSize", context.Request["PageSize"].SafeToString());
                ht.Add("IDCard", IDCard);// "ResourcesInfo T left join ClickDetail detail on T.ID=detail.ResourcesID and detail.ClickType=2 and detail.CreateUID='" + IDCard + "'");
                ht.Add("DocName", context.Request.Form["DocName"].SafeToString());
                ht.Add("GroupName", context.Request.Form["GroupName"].SafeToString());
                ht.Add("Postfixs", context.Request.Form["Postfixs"].SafeToString());
                ht.Add("CatagoryID", context.Request.Form["CatagoryID"].SafeToString().Replace("|0", ""));
                ht.Add("ChapterID", context.Request.Form["ChapterID"].SafeToString());

                jsonModel = Bll.GetPage(ht);
                //result = "{\"result\":" + jss.Serialize(jsonModel) + "}";
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