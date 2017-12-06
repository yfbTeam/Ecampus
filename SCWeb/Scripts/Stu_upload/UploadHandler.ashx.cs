using SCBLL;
using SCModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace SCWeb.Scripts.Stu_upload
{
    /// <summary>
    /// UploadHandler 的摘要说明
    /// </summary>
    public class UploadHandler : IHttpHandler
    {        
        public HttpContext Context;
        public void ProcessRequest(HttpContext context)
        {
            Context = context;
            context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
            if (context.Request["REQUEST_METHOD"] == "OPTIONS")
            {
                context.Response.End();
            }           
            string albumid = context.Request.Form["album"];
            string loginUID = context.Request.Form["LoginUID"];
            //string activeid = context.Request.Form["activeid"];
            context.Response.ContentType = "text/plain";
            context.Response.ContentEncoding = System.Text.Encoding.UTF8;
            this.SavePhoto(albumid, loginUID);
            context.Response.End();
        }

        #region 文件保存操作
        private string SavePhoto(string albumid, string loginUID)
        {
            string result = string.Empty;
            try
            {
                //if (string.IsNullOrEmpty(activeid))
                //{ result = "{\"jsonrpc\" : \"2.0\", \"result\" : \"保存失败\", \"id\" : \"id\"}"; return result; }
                HttpFileCollection files = HttpContext.Current.Request.Files;

                //int activeId = GetActiveId(files,album);
                int activeId = 1;

                for (int i = 0; i < files.Count; i++)
                {
                    HttpPostedFile image = files[i];
                    string photoName = image.FileName;//获取初始文件名
                    string photoExt = photoName.Substring(photoName.LastIndexOf(".")); //通过最后一个"."的索引获取文件扩展名
                    string onlyName = photoName.Substring(0, photoName.LastIndexOf("."));
                    if (photoExt.ToLower() != ".gif" && photoExt.ToLower() != ".jpg" && photoExt.ToLower() != ".jpeg" && photoExt.ToLower() != ".bmp" && photoExt.ToLower() != ".png")
                    {
                        result = "{\"jsonrpc\" : \"2.0\", \"result\" : \"保存失败,请选择图片文件！\", \"id\" : \"id\"}";
                        break;
                    }
                    string serRootFolder= Context.Server.MapPath("/UploadImgFile");
                    if (!Directory.Exists(serRootFolder))
                    {
                        Directory.CreateDirectory(serRootFolder);
                    }
                    string serfolder = Context.Server.MapPath("/UploadImgFile/AlbumPic");
                    if (!Directory.Exists(serfolder))
                    {
                        Directory.CreateDirectory(serfolder);
                    }
                    string url = "/UploadImgFile/AlbumPic/" + onlyName+"_"+DateTime.Now.ToFileTime().ToString() + photoExt;
                    string fullPath = Context.Server.MapPath(url);
                    image.SaveAs(fullPath);

                    string strDocName = Path.GetFileName(files[i].FileName);
                    Com_AlbumPic pic = new Com_AlbumPic();

                    ////创建动态
                    //AssoActiveService activeservice = new AssoActiveService();
                    //AssoActive active = new AssoActive();
                    //active.Title = "上传照片";
                    //int resultcount = activeservice.Insert(active);


                    if (activeId > 0)
                    {
                        pic.PicUrl = url;
                        pic.AlbumId =Convert.ToInt32(albumid);
                        pic.ActiveId = activeId;
                        JsonModel jsonModel = new Com_AlbumPicService().Add(pic);
                        if (jsonModel.errNum == 0)
                        {
                            result = "{\"jsonrpc\" : \"2.0\", \"result\" : null, \"id\" : \"" + files[i].FileName + "\"}";
                        }
                        else { result = "{\"jsonrpc\" : \"2.0\", \"result\" : \"保存失败\", \"id\" : \"id\"}"; }
                    }
                    else
                    {
                        result = "{\"jsonrpc\" : \"2.0\", \"result\" : \"保存失败\", \"id\" : \"id\"}";
                    }

                }
            }
            catch (Exception ex)
            {
                result = "{\"jsonrpc\" : \"2.0\", \"result\" : \"保存失败\", \"id\" : \"id\"}";
            }
            return result;
        }
        #endregion
        public bool IsReusable
        {
            get { return true; }
        }
    }
}