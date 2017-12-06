using SCModel;
using SCUtility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace SCWeb.CommonHandler
{
    /// <summary>
    /// Upload 的摘要说明
    /// </summary>
    public class Upload : IHttpHandler
    {
        JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string func = context.Request["Func"];
            string result = string.Empty;
            try
            {
                switch (func)
                {
                    case "Upload_AssoImg": //社团封面图片
                        Upload_Img(context, "/UploadImgFile/AssoPic");
                        break;
                    case "Upload_AssoActivityImg": //上传社团活动封面
                        Upload_Img(context, "/UploadImgFile/AssoActivityPic");
                        break;
                    case "Upload_AssoHomePic":  //上传社团背景图
                        Upload_Img(context, "/UploadImgFile/AssoHomePic");
                        break;
                    case "Upload_DepartImg": //部门封面图片
                        Upload_Img(context, "/UploadImgFile/DepartPic");
                        break;
                    case "Upload_DeparActivityImg": //上传部门活动封面
                        Upload_Img(context, "/UploadImgFile/DeparActivityPic");
                        break;
                    case "Upload_DeparHomePic":  //上传部门背景图
                        Upload_Img(context, "/UploadImgFile/DeparHomePic");
                        break;
                    case "Upload_DormImg": //宿舍封面图片
                        Upload_Img(context, "/UploadImgFile/DormPic");
                        break;
                    case "Upload_DormActivityImg": //上传宿舍活动封面
                        Upload_Img(context, "/UploadImgFile/DormActivityPic");
                        break;
                    case "Upload_DormHomePic":  //上传宿舍背景图
                        Upload_Img(context, "/UploadImgFile/DormHomePic");
                        break;
                    case "Upload_NewInfoPic":  //上传通知图片
                        Upload_Img(context, "/UploadImgFile/NewInfoPic",false);
                        break;
                }
                LogService.WriteLog(func);
            }
            catch (Exception ex)
            {
                LogService.WriteErrorLog(ex.Message);
            }
        }

        #region 上传图片
        public void Upload_Img(HttpContext context, string imgPath,bool isHtml5=true)
        {
            string result = "";
            HttpPostedFile file = HttpContext.Current.Request.Files[0];
            string DirePath = context.Server.MapPath(imgPath);
            if (!Directory.Exists(DirePath))
            {
                Directory.CreateDirectory(DirePath);
            }
            string ext = System.IO.Path.GetExtension(file.FileName);
            string fileName = DateTime.Now.Ticks + ext;
            string p = imgPath + "/" + fileName;
            string path = context.Server.MapPath(p);
            file.SaveAs(path);
            if (isHtml5)
            {
                JsonModel jsonModel= new JsonModel()
                {
                    errNum = 0,
                    errMsg = "",
                    retData = p
                };
                result = string.IsNullOrEmpty(result) ? "{\"result\":" + jss.Serialize(jsonModel) + "}" : result;
            }
            else
            {
                result = "{\"error\":0,\"url\":\"" + p + "\"}";
            }
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