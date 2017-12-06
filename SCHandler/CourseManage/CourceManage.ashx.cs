using SCBLL;
using SCModel;
using SCUtility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace SCHandler.CourseManage
{
    /// <summary>
    /// CourceManage 的摘要说明
    /// </summary>
    public class CourceManage : IHttpHandler
    {
        Course_ManageService bll = new Course_ManageService();
        JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();
        JsonModel jsonModel = null;
        Course_ChapterService courcebll = new Course_ChapterService();

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string FuncName = context.Request["Func"].ToString();
            string result = string.Empty;

            if (FuncName != null && FuncName != "")
            {
                try
                {
                    switch (FuncName)
                    {
                            //课程
                        case "GetPageList":
                            GetPageList(context);
                            break;
                        case "AddCource":
                            AddCource(context);
                            break;
                        case "DelCourse":
                            DelCourse(context);
                            break;
                        case "SingUp":
                            SingUp(context);
                            break;
                        case "CourceCheck":
                            CourceCheck(context);
                            break;
                            //目录
                        case "Chapator":
                            Chapator(context);
                            break;
                        case "AddNewMenu":
                            AddNewMenu(context);
                            break;
                        case "DelMenu":
                            DelMenu(context);
                            break;      
                        case "reMenuName":
                            reMenuName(context);
                            break;                       
                        case "SortMenu":
                            SortMenu(context);
                            break;

                        default:
                            jsonModel = new JsonModel()
                            {
                                errNum = 404,
                                errMsg = "无此方法",
                                retData = ""
                            };
                            break;
                    }
                    LogService.WriteLog("");
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
        }

        #region 调整目录排序
        /// <summary>
        /// 调整目录排序
        /// </summary>
        /// <param name="context"></param>
        private void SortMenu(HttpContext context)
        {
            Course_ChapterService chapterdll = new Course_ChapterService();
            try
            {
                int ID = Convert.ToInt32(context.Request["ID"]);
                string Type = context.Request["Type"].SafeToString();
                string result = chapterdll.EditChapterSort(ID, Type);
                if (result == "")
                {
                    jsonModel = new JsonModel
                    {
                        errNum = 0,
                        errMsg = "修改成功",
                        retData = ""
                    };
                }
                else
                {
                    jsonModel = new JsonModel
                    {
                        errNum = 999,
                        errMsg = result,
                        retData = ""
                    };
                }
            }
            catch (Exception ex)
            {
                jsonModel = new JsonModel
                {
                    errNum = 400,
                    errMsg = ex.Message,
                    retData = ""
                };
            }
        }
        #endregion

        #region 删除课程

        private void DelCourse(HttpContext context)
        {
            string ID = context.Request["ID"].SafeToString();
            string Message = "";
            try
            {

                Message = bll.DelCourse(ID);
                if (Message == "")
                {
                    jsonModel = new JsonModel()
                    {
                        errNum = 0,
                        errMsg = "操作成功",
                        retData = ""
                    };
                }
                else
                {
                    jsonModel = new JsonModel()
                    {
                        errNum = 999,
                        errMsg = Message,
                        retData = ""
                    };
                }
            }
            catch (Exception ex)
            {
                jsonModel = new JsonModel()
                {
                    errNum = 404,
                    errMsg = ex.Message,
                    retData = ""
                };
            }
        }
        #endregion

        #region 修改课程目录名称
        /// <summary>
        /// 修改文件夹（文件夹）名称
        /// </summary>
        /// <param name="context"></param>
        private void reMenuName(HttpContext context)
        {
            string result = "";
            Course_Chapter modol = new Course_Chapter();
            string Name = context.Request["NewName"].SafeToString();
            modol.ID = Convert.ToInt32(context.Request["ID"]);
            modol.Name = Name;
            jsonModel = courcebll.Update(modol);
            result = "{\"result\":" + jss.Serialize(jsonModel) + "}";
            context.Response.Write(result);
            context.Response.End();
        }

        #endregion

        #region 课程审核
        /// <summary>
        /// 课程审核
        /// </summary>
        /// <param name="contex"></param>
        private void CourceCheck(HttpContext contex)
        {
            Course_Manage cource = new Course_Manage();
            cource.Status = int.Parse(contex.Request["Status"].SafeToString());
            cource.CheckMes = contex.Request["CheckMsg"].SafeToString();
            cource.ID = Convert.ToInt32(contex.Request["ID"]);
            jsonModel = bll.Update(cource);
        }
        #endregion

        #region 报名
        /// <summary>
        /// 报名
        /// </summary>
        /// <param name="context"></param>
        private void SingUp(HttpContext context)
        {
            try
            {
                string CouseID = context.Request["CourseID"].ToString();
                string StuNo = context.Request["StuNo"].ToString();
                int Rank = int.Parse(context.Request["Rank"]);
                string flag = bll.StuSingUp(CouseID, StuNo, Rank);
                int returnNo = 0;
                if (flag.Length > 0)
                {
                    returnNo = 5;
                }
                jsonModel = new JsonModel
                {
                    errNum = returnNo,
                    errMsg = flag,
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
                LogService.WriteErrorLog(ex.Message);
            }
        }
        #endregion        

        #region 获取左侧导航
        /// <summary>
        /// 获取左侧导航
        /// </summary>
        /// <param name="context"></param>
        private void Chapator(HttpContext context)
        {
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("TableName", "Course_Chapter");
                ht.Add("CourseID", context.Request["CourseID"].ToString());
                ht.Add("Pid", context.Request["Pid"] ?? "");
                jsonModel = courcebll.GetPage(ht, false);
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
       
        /// <summary>
        /// 添加左侧导航
        /// </summary>
        /// <param name="context"></param>
        private void AddNewMenu(HttpContext context)
        {
            try
            {
                string Type = context.Request["type"].SafeToString();
                Course_Chapter chapter = new Course_Chapter();
                chapter.Name = context.Request["FileName"].SafeToString();
                chapter.CourseID = Convert.ToInt32(context.Request["CourseID"]);
                chapter.Pid = Convert.ToInt32(context.Request["Pid"]);
                jsonModel = courcebll.Add(chapter);
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
        /// <summary>
        /// 删除左侧导航
        /// </summary>
        /// <param name="context"></param>
        private void DelMenu(HttpContext context)
        {
            try
            {
                int ID = Convert.ToInt32(context.Request["ID"]);
                string DelResult = courcebll.DelChapter(ID);
                if (DelResult == "0")
                {
                    jsonModel = new JsonModel()
                    {
                        errNum = 0,
                        errMsg = "删除成功",
                        retData = ""
                    };
                }
                else
                {
                    jsonModel = new JsonModel()
                    {
                        errNum = 100,
                        errMsg = DelResult,
                        retData = ""
                    };
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

        #region 获取课程信息
        /// <summary>
        /// 获取课程信息
        /// </summary>
        /// <param name="context"></param>
        private void GetPageList(HttpContext context)
        {
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("PageIndex", context.Request["PageIndex"].SafeToString());
                ht.Add("PageSize", context.Request["PageSize"].SafeToString());
                ht.Add("TableName", "Course");
                ht.Add("OperSymbol", context.Request["OperSymbol"].SafeToString());
                ht.Add("ID", context.Request["ID"].SafeToString());
                ht.Add("StudyTerm", context.Request["StudyTerm"].SafeToString());
                ht.Add("IdCard", context.Request["IdCard"].SafeToString());
                ht.Add("CourseType", context.Request["CourseType"].SafeToString());
                ht.Add("Name", context.Request["Name"].SafeToString());
                bool Ispage = true;
                if (context.Request["Ispage"].SafeToString().Length > 0)
                {
                    Ispage = Convert.ToBoolean(context.Request["Ispage"]);
                }
                jsonModel = bll.GetPage(ht, Ispage);
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

        #region 添加课程

        private void AddCource(HttpContext context)
        {
            Key key = new Key();
            try
            {
                Course_Manage cource = new Course_Manage();
                string Name = context.Request["Name"].SafeToString();
                cource.Name = context.Request["Name"].SafeToString();
                cource.CourseIntro = context.Request["CourseIntro"].SafeToString();
                cource.ImageUrl = context.Request["CoursePic"].SafeToString();
                cource.StudyPlace = context.Request["StudyPlace"].SafeToString();
                cource.CourceType = Convert.ToByte(context.Request["CourceType"]);
                string OldCourceType = context.Request["OldCourceType"].SafeToString();
                string OldStatus = context.Request["OldStatus"].SafeToString();
                string Course=context.Request["Course"];

                if (cource.CourceType == 1)
                {
                    cource.Status = 1;
                }
                else { cource.Status = 0; }
                cource.GradeID = context.Request["GradeID"].SafeToString();
                cource.EvalueStandard = context.Request["EvalueStandard"].SafeToString();
                cource.StudyTerm = int.Parse(context.Request["StudyTerm"]);
                cource.CourseHardware = context.Request["CourseHardware"].SafeToString();
                cource.WeekName = context.Request["WeekName"].SafeToString();
                cource.StartTime = Convert.ToDateTime(context.Request["StartTime"]);
                cource.EndTime = Convert.ToDateTime(context.Request["EndTime"]);
                cource.StuMaxCount = Convert.ToInt32(context.Request["StuMaxCount"]);
               
                string Message = "";
                if (context.Request["ID"].SafeToString().Length > 0)
                {
                    if (OldCourceType == "2" && cource.CourceType == 2)
                    {
                        cource.Status = int.Parse(OldStatus);
                    }
                    cource.ID = Convert.ToInt32(context.Request["ID"]);
                    Message = bll.UpdateCourse(cource, Course);
                }
                else
                {
                    Message = bll.AddCourse(cource, Course);
                }
                if (Message == "")
                {
                    jsonModel = new JsonModel()
                    {
                        errNum = 0,
                        errMsg = "操作成功",
                        retData = ""
                    };
                }
                else
                {
                    jsonModel = new JsonModel()
                    {
                        errNum = 999,
                        errMsg = Message,
                        retData = ""
                    };
                }
            }
            catch (Exception ex)
            {
                jsonModel = new JsonModel()
                {
                    errNum = 404,
                    errMsg = ex.Message,
                    retData = ""
                };
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