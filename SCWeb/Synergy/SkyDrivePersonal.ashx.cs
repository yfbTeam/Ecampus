using SCBLL;
using SCModel;
using SCUtility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using WopiService_Proxy;
namespace SCWeb.Synergy
{
    /// <summary>
    /// SkyDrivePersonal1 的摘要说明
    /// </summary>
    public class SkyDrivePersonal1 : IHttpHandler
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
                    case "AddFolder":
                        AddFolder(context);
                        break;
                    case "Down":
                        Down(context);
                        break;
                    case "Del":
                        Del(context);
                        break;
                    case "MoveTo":
                        MoveTo(context);
                        break;
                    case "reName":
                        reName(context);
                        break;
                    case "Wopi_Proxy":
                        Wopi_Proxy1(context);
                        break;
                    default:
                        break;
                }
            }
        }
        private void Wopi_Proxy1(HttpContext context)
        {
            string result = "";
            string filePath = context.Server.MapPath(context.Request["filepath"]);
            string fileFullName = Path.GetFileName(filePath);
            string extension = Path.GetExtension(fileFullName);
            string dateNow = DateTime.Now.ToString("yyyyMMddHHmmss");

            string destFileName = dateNow + extension;
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                byte[] data = new byte[fs.Length];
                fs.Read(data, 0, data.Length);
                Wopi_Proxy.UploadFile_GetLink(destFileName, data, new Action<string>((link) => { result = link; }));
            }
            context.Response.Write(result);
            context.Response.End();

        }


        #region 修改文件夹（文件夹）名称
        /// <summary>
        /// 修改文件夹（文件夹）名称
        /// </summary>
        /// <param name="context"></param>
        private void reName(HttpContext context)
        {
            string result = "";
            JsonModel jsonModel = null;
            JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();

            MyResource modol = new MyResource();
            string Name = context.Request["NewName"].SafeToString();
            string FileUrl = context.Request["FileUrl"].SafeToString();
            string oldName = context.Request["oldname"].SafeToString();
            int LastLen = FileUrl.LastIndexOf("/");
            string LastName = FileUrl.Substring(LastLen, FileUrl.Length - LastLen);
            string newUrl = FileUrl.Substring(0, LastLen) + LastName.Replace(oldName, Name);
            if (LastName.IndexOf('.') > 0)
            {
                FileHelper.Move(context.Server.MapPath(FileUrl), context.Server.MapPath(newUrl));
            }
            else
            {
                Directory.Move(context.Server.MapPath(FileUrl), context.Server.MapPath(newUrl));
            }
            modol.ID = Convert.ToInt32(context.Request["ID"]);
            if (Name.IndexOf(".") > 0)
            {
                Name = Name.Split('.')[0];
            }
            modol.FileUrl = newUrl;
            modol.Name = Name;
            modol.EditTime = DateTime.Now;
            jsonModel = Bll.Update(modol);
            result = "{\"result\":" + jss.Serialize(jsonModel) + "}";
            context.Response.Write(result);
            context.Response.End();
        }

        #endregion

        #region 新增文件夹
        /// <summary>
        /// 新增文件夹
        /// </summary>
        /// <returns></returns>

        private void AddFolder(HttpContext context)
        {
            JsonModel jsonModel = null;
            JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();
            string CreateUID = context.Request.Form["CreateUID"].SafeToString();
            string FoldUrl = "";
            if (context.Request["FoldUrl"].SafeToString().IndexOf(ConfigHelper.GetConfigString("FileManageName")) < 0)
            {
                FoldUrl = ConfigHelper.GetConfigString("FileManageName") + context.Request["FoldUrl"].SafeToString();
            }
            else
            {
                FoldUrl = context.Request["FoldUrl"].SafeToString();
            }

            // string FoldUrl = ConfigHelper.GetConfigString("FileManageName") + context.Request.Form["FoldUrl"].SafeToString();
            if (FoldUrl.IndexOf(CreateUID) < 0)
            {
                FoldUrl += "/" + CreateUID;
            }
            string result = "0";

            try
            {

                string FileName = context.Request.Form["FileName"].ToString().Trim();
                string pid = context.Request.Form["Pid"].ToString().Trim();
                string code = context.Request.Form["code"].ToString().Trim();

                string RealPath = FoldUrl + "/" + FileName;// +DateTime.Now.Ticks.ToString();
                string RealFullPath = context.Server.MapPath(RealPath);
                #region 处理文件夹重名问题
                if (FileHelper.IsExistDirectory(RealFullPath))
                {
                    int i = 0;
                    while (true)
                    {
                        i++;
                        if (!FileHelper.IsExistDirectory(RealFullPath + "(" + i + ")"))
                        {
                            FileName += "(" + i + ")";

                            RealPath += "(" + i + ")";
                            RealFullPath += "(" + i + ")";
                            break;
                        }
                    }
                }
                #endregion
                if (FileHelper.CreateDirectory(RealFullPath))
                {
                    MyResource re = new MyResource();
                    re.Name = FileName;
                    re.PID = int.Parse(pid);
                    re.FileSize = 0;
                    re.FileUrl = RealPath;
                    re.FileIcon = "ico-file.png";
                    re.postfix = "";
                    re.CreateUID = CreateUID;
                    re.EditUID = CreateUID;
                    re.IsFolder = 1;
                    if (pid == "0")
                    {
                        re.code = "0";
                    }
                    else
                    {
                        if (code == "0")
                        {
                            re.code = re.PID.ToString();
                        }
                        else
                        {
                            re.code = code + "|" + re.PID;
                        }
                    }
                    jsonModel = Bll.Add(re);
                }
                else
                {
                    jsonModel = new JsonModel()
                    {
                        errNum = 500,
                        errMsg = "文件夹添加失败",
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
            result = "{\"result\":" + jss.Serialize(jsonModel) + "}";

            context.Response.Write(result);
            context.Response.End();

        }
        #endregion

        #region 文件下载
        /// <summary>
        /// 文件下载
        /// </summary>
        /// <returns></returns>

        private void Down(HttpContext context)
        {
            ZipHelper zip = new ZipHelper();
            JsonModel jsonModel = null;
            JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();

            string ids = context.Request.Form["DownID"].SafeToString();
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
                    MyResource resource = (MyResource)(model.retData);
                    string FileUrl = resource.FileUrl;
                    if (resource.postfix == "")
                    {
                        FileHelper.CopyFolder(context.Server.MapPath(FileUrl), ZipUrl);
                    }
                    else
                    {
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
                    retData = DownUrl + "\\" + ZipName //+ ".rar"
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

            string ids = context.Request.Form["DelID"].SafeToString();

            string result = "0";

            try
            {
                string[] idarry = ids.TrimEnd(',').Split(',');
                for (int i = 0; i < idarry.Length; i++)
                {
                    #region 文件删除
                    JsonModel model = Bll.GetEntityById(int.Parse(idarry[i]));
                    MyResource resource = (MyResource)(model.retData);
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

        #region 文件移动
        /// <summary>
        /// 文件删除
        /// </summary>
        /// <returns></returns>

        private void MoveTo(HttpContext context)
        {
            JsonModel jsonModel = null;
            JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();

            string ids = context.Request.Form["MoveIDs"].SafeToString();
            string url = context.Request.Form["Url"];
            string code = context.Request.Form["code"];
            string pid = context.Request.Form["pid"];
            string result = "0";

            try
            {
                string[] idarry = ids.Split(',');
                foreach (string id in idarry)
                {
                    if (id != "")
                    {
                        #region 文件移动
                        JsonModel model = Bll.GetEntityById(int.Parse(id));
                        MyResource resource = (MyResource)(model.retData);
                        string FileUrl = resource.FileUrl;

                        if (resource.postfix == "")
                        {
                            resource.FileUrl = url + resource.FileUrl.Substring(resource.FileUrl.LastIndexOf("/"));
                            if (pid == id)
                            {
                                jsonModel = new JsonModel
                                {
                                    errNum = 500,
                                    errMsg = "目标目录和当前目录一致",
                                    retData = ""
                                };
                                break;
                            }
                            Directory.Move(context.Server.MapPath(FileUrl), context.Server.MapPath(resource.FileUrl));
                            if (code != "0")
                            {
                                resource.code = code + "|" + pid;
                            }
                            else
                                resource.code = pid;
                            resource.PID = int.Parse(pid);

                        }
                        else
                        {
                            resource.FileUrl = url + resource.FileUrl.Substring(resource.FileUrl.LastIndexOf("/"));
                            if (pid == id)
                            {
                                jsonModel = new JsonModel
                                {
                                    errNum = 500,
                                    errMsg = "目标目录和当前目路一致",
                                    retData = ""
                                };
                                break;
                            }
                            FileHelper.Move(context.Server.MapPath(FileUrl), context.Server.MapPath(resource.FileUrl));
                            if (code != "0")
                            {
                                resource.code = code + "|" + pid;
                            }
                            else
                                resource.code = pid;
                            resource.PID = int.Parse(pid);
                        }
                        jsonModel = Bll.Update(resource);
                        #endregion
                    }
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

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}