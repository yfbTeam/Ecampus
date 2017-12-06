using SCBLL;
using SCModel;
using SCUtility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace SCHandler.BookSubject
{
    /// <summary>
    /// SubJect 的摘要说明
    /// </summary>
    public class SubJect : IHttpHandler
    {
        JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();
        JsonModel jsonModel = null;
        string result = "";
        public void ProcessRequest(HttpContext context)
        {
            string func = context.Request["func"] ?? "";
            try
            {
                switch (func)
                {
                    case "GetSubJectData":
                        GetSubJectData(context);
                        break;
                    case "DelSubJect":
                        DelSubJect(context);
                        break;
                    case "AddSubJect":
                        AddSubJect(context);
                        break;
                        
                    case "SubJect":
                        GetSubJect(context);
                        break;
                    case "Version":
                        GetVersion(context);
                        break;
                    case "AddVersion":
                        AddVersion(context);
                        break;
                    case "DelVersion":
                        DelVersion(context);
                        break;
                        
                    case "Grade":
                        GetGrade(context);
                        break;
                    case "AddGrade":
                        AddGrade(context);
                        break;
                    case "DelGrade":
                        DelGrade(context);
                        break;
                    case "Chapator":
                        Chapator(context);
                        break;
                    case "DelCatalog":
                        DelCatalog(context);
                        break;
                    case "AddCatalog":
                        AddCatalog(context);
                        break;
                    case "UpdateCatalog":
                        UpdateCatalog(context);
                        break;
                    default:
                        jsonModel = new JsonModel()
                        {
                            errNum = 5,
                            errMsg = "没有此方法",
                            retData = ""
                        };
                        break;
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
                result = "{\"result\":" + jss.Serialize(jsonModel) + "}";
            }
            context.Response.ContentType = "text/plain";
            context.Response.Write(result);
            context.Response.End();
        }
        public void DelCatalog(HttpContext context)
        {
            try
            {
                //科目
                Book_CatologService bll = new Book_CatologService();
                int ID = Convert.ToInt32(context.Request["ID"]);
                JsonModel jsonModel = bll.Delete(ID);
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
                return;
            }
        }

        public void UpdateCatalog(HttpContext context)
        {
            try
            {
                //科目
                Book_CatologService bll = new Book_CatologService();
                Book_Catolog catolog = new Book_Catolog();
                catolog.Name = context.Request["Name"].SafeToString();
                catolog.ID = int.Parse(context.Request["ID"]);
                JsonModel jsonModel = bll.Update(catolog);
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
                return;
            }
        }
        public void AddCatalog(HttpContext context)
        {
            try
            {
                //科目
                Book_CatologService bll = new Book_CatologService();
                Book_Catolog catolog = new Book_Catolog();
                catolog.Name = context.Request["Name"].SafeToString();
                catolog.SubjectID = int.Parse(context.Request["SubID"]);
                catolog.Pid = int.Parse(context.Request["PID"]);
                JsonModel jsonModel = bll.Add(catolog);
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
                return;
            }
        }
        public void Chapator(HttpContext context)
        {
            try
            {
                //科目
                Book_CatologService bll = new Book_CatologService();
                Hashtable ht = new Hashtable();
                ht.Add("SubName", context.Request["SubName"]);
                ht.Add("IsPage", context.Request["IsPage"]);
                ht.Add("VersionID", context.Request["VersionID"]);
                ht.Add("GradeID", context.Request["GradeID"]);
                ht.Add("SubID", context.Request["SubID"]);
                ht.Add("PID", context.Request["PID"]);                
                JsonModel jsonModel = bll.GetPage(ht, false);
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
                return;
            }
        }
        public void AddSubJect(HttpContext context)
        {
            try
            {
                //科目
                Book_SubjectService SubjectBll = new Book_SubjectService();
                Book_Subject modol = new Book_Subject();
                modol.Name = context.Request["Name"];
                modol.VersionID = int.Parse(context.Request["VersionID"]);
                modol.GradeID = int.Parse(context.Request["GradeID"]);
                JsonModel jsonModel = SubjectBll.Add(modol);
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
                return;
            }
        }
        public void DelSubJect(HttpContext context)
        {
            try
            {
                //科目
                Book_SubjectService SubjectBll = new Book_SubjectService();
                int ID = Convert.ToInt32(context.Request["ID"]);
                JsonModel jsonModel = SubjectBll.Delete(ID);
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
                return;
            }
        }
        public void GetSubJect(HttpContext context)
        {
            try
            {
                //科目
                Book_SubjectService SubjectBll = new Book_SubjectService();
                Hashtable ht = new Hashtable();
                ht.Add("Distinct", context.Request["Distinct"]);
                ht.Add("IsPage", context.Request["IsPage"]);
                ht.Add("VersionID", context.Request["VersionID"]);
                ht.Add("GradeID", context.Request["GradeID"]);
                JsonModel jsonModel = SubjectBll.GetPage(ht, false);
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
                return;
            }
        }
        public void DelVersion(HttpContext context)
        {
            try
            {
                //科目
                Book_VersionService VersionBll = new Book_VersionService();
                int ID = int.Parse(context.Request["Id"]);
                JsonModel jsonModel = VersionBll.Delete(ID);
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
                return;
            }
        }
        public void AddVersion(HttpContext context)
        {
            try
            {
                //科目
                Book_VersionService VersionBll = new Book_VersionService();
                Book_Version model = new Book_Version();
                model.Name = context.Request["Name"];
                JsonModel jsonModel = VersionBll.Add(model);
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
                return;
            }
        }
        public void GetVersion(HttpContext context)
        {
            try
            {
                //科目
                Book_VersionService VersionBll = new Book_VersionService();
                Hashtable ht = new Hashtable();
                ht.Add("SubjectName", context.Request["SubjectName"]);
                ht.Add("IsPage", context.Request["IsPage"]);
                JsonModel jsonModel = VersionBll.GetPage(ht, false);
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
                return;
            }
        }
        public void DelGrade(HttpContext context)
        {
            try
            {
                //科目
                Book_GradeService VersionBll = new Book_GradeService();
                int ID = int.Parse(context.Request["Id"]);
                JsonModel jsonModel = VersionBll.Delete(ID);
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
                return;
            }
        }
        public void AddGrade(HttpContext context)
        {
            try
            {
                //科目
                Book_GradeService VersionBll = new Book_GradeService();
                Book_Grade model = new Book_Grade();
                model.Name = context.Request["Name"];
                JsonModel jsonModel = VersionBll.Add(model);
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
                return;
            }
        }
        public void GetGrade(HttpContext context)
        {
            try
            {
                //科目
                Book_GradeService GradeBll = new Book_GradeService();
                Hashtable ht = new Hashtable();
                ht.Add("SubjectName", context.Request["SubjectName"]);
                ht.Add("IsPage", context.Request["IsPage"]);
                ht.Add("VersionID", context.Request["VersionID"]);
                JsonModel jsonModel = GradeBll.GetPage(ht, false);
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
                return;
            }
        }
        /// <summary>
        /// 获得科目、教材、教材版本
        /// </summary>
        /// <param name="context"></param>
        public void GetSubJectData(HttpContext context)
        {
            try
            {
                Book_SubjectService SubjectBll = new Book_SubjectService();
                Hashtable ht = new Hashtable();
                ht.Add("PageIndex", context.Request["PageIndex"].SafeToString());
                ht.Add("PageSize", context.Request["pageSize"].SafeToString());
                ht.Add("VersionID", context.Request["VersionID"].SafeToString());
                ht.Add("GradeID", context.Request["GradeID"].SafeToString());
                JsonModel jsonModel = SubjectBll.GetPage(ht);
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
                return;
            }
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