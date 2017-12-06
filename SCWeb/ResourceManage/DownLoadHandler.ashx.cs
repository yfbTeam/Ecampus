using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using SCUtility;
namespace SCWeb.ResourceManage
{
    /// <summary>
    /// DownLoadHandler 的摘要说明
    /// </summary>
    public class DownLoadHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string filepath = context.Request["filepath"].SafeToString();
            try
            {
                int index = filepath.LastIndexOf('\\') + 1;
                filepath = context.Request.MapPath(filepath);
                string filename = System.IO.Path.GetFileName(filepath);
                if (filename.IndexOf(" ") != -1)
                {
                    filename = filename.Replace(" ", "_");
                }
                filename = context.Server.UrlDecode(filename);
                FileInfo info = new FileInfo(filepath);
                long filesize = info.Length;
                context.Response.Clear();
                context.Response.ClearContent();
                context.Response.ClearHeaders();
                context.Response.ContentType = "application/octet-stream";
                context.Response.AddHeader("Content-Disposition", "attachement;filename=" + HttpUtility.UrlEncode(filename, System.Text.Encoding.UTF8).ToString());
                context.Response.AddHeader("Content-Length", filesize.ToString());
                context.Response.WriteFile(filepath, 0, filesize);
                context.Response.Flush();
                context.Response.Close();
            }
            catch (Exception ex)
            {
                context.Response.Write("-1");
                context.Response.End();
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