���ȣ��ҵ�109�еĴ���
1
 var swf = './expressInstall.swf';
�޸�Ϊ���ĵ�ַ
1
2
//�޸�����flash��ַ
var swf = './Scripts/webuploader-0.1.5/examples/image-upload/expressInstall.swf';
��Σ��ҵ�151�У���ʵ������ʱ���޸������ϴ�flash�ĵ�ַ��
1
  swf: '../../dist/Uploader.swf',
�޸�Ϊ
1
swf: './Scripts/webuploader-0.1.5/dist/Uploader.swf',
�������ҵ�154�У���ͼƬ�ϴ���ַ�޸�Ϊ.net��һ�㴦�����������ַ
1
server: '../../server/fileupload.php',
�޸�Ϊ����һ�㴦������ַ
1
server: './server/fileupload.ashx',
���ģ��ҵ�260���޸�Ԥ���ķ����������ַ(��û��д��Ӱ���ļ��ϴ�)
1
$.ajax('../../server/preview.php', {
�޸�Ϊ����һ�㴦������Ԥ����ַ
1
 $.ajax('./server/preview.ashx', {
���ˣ����������ǽ�upload.js�޸�����ˡ�
�������д�˴�������ˣ�����Ŀ�д���һ��server�ļ��в�������������ļ�fileupload.ashx��preview.ashx��
�������fileupload.ashx��ʵ�ַ���

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Text;
 
namespace Blog.server
{
    /// <summary>
    /// Summary description for fileupload
    /// </summary>
    public class fileupload : IHttpHandler
    {
 
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            //ָ���ַ���
            context.Response.ContentEncoding = Encoding.UTF8;
            if (context.Request["REQUEST_METHOD"] == "OPTIONS")
            {
                context.Response.End();
            }
            SaveFile();
        }
        /// <summary>
        /// �ļ��������
        /// </summary>
        /// <param name="basePath"></param>
        private void SaveFile(string basePath = "~/Upload/Images/")
        {
            var name = "<a href='https://www.baidu.com/s?wd=string.Empty&tn=44039180_cpr&fenlei=mv6quAkxTZn0IZRqIHckPjm4nH00T1dBPjN-nhcvPvcYuAfdnhmY0ZwV5Hcvrjm3rH6sPfKWUMw85HfYnjn4nH6sgvPsT6K1TL0qnfK1TL0z5HD0IgF_5y9YIZ0lQzqlpA-bmyt8mh7GuZR8mvqVQL7dugPYpyq8Q1cznjn4nWRzrHRLPWTvnWR4nj6' target='_blank' class='baidu-highlight'>string.Empty</a>";
            basePath = (basePath.IndexOf("~") > -1) ? System.Web.HttpContext.Current.Server.MapPath(basePath) :
            basePath;
            HttpFileCollection files = System.Web.HttpContext.Current.Request.Files;
            //���Ŀ¼�����ڣ��򴴽�Ŀ¼
            if (!Directory.Exists(basePath))
            {
                Directory.CreateDirectory(basePath);
            }
 
            var suffix = files[0].ContentType.Split('/');
            //��ȡ�ļ���ʽ
            var _suffix = suffix[1].Equals("jpeg",StringComparison.CurrentCultureIgnoreCase) ? "" : suffix[1];
            var _temp = System.Web.HttpContext.Current.Request["name"];
            //������޸��ļ������򴴽�����ļ���
            if (!string.IsNullOrEmpty(_temp))
            {
                name = _temp;
            }
            else
            {
                Random rand = new Random(24 * (int)DateTime.Now.Ticks);
                name = rand.Next() + "." + _suffix;
            }
            //�ļ�����
            var full = basePath + name;
            files[0].SaveAs(full);
            var _result = "{\"jsonrpc\" : \"2.0\", \"result\" : null, \"id\" : \"" + name + "\"}";
            System.Web.HttpContext.Current.Response.Write(_result);
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