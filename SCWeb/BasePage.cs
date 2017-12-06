using SCModel;
using SCUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace SCWeb
{
    public class BasePage : System.Web.UI.Page
    {
        protected Sys_User UserInfo { get; set; }
        protected override void OnInit(EventArgs e)
        {
            try
            {  //登陆页地址 从Web.config 读取
                string LoginPage = ConfigHelper.GetConfigString("LoginPage"); ;
                string action = Request["action"];
                if (!string.IsNullOrEmpty(action) && action == "loginOut")   //退出登录
                {
                    Response.Cookies["LoginCookie_Author"].Expires = DateTime.Now.AddDays(-3);
                    Response.Cookies["ClassID"].Expires = DateTime.Now.AddDays(-3);
                    Response.Cookies["TokenID"].Expires = DateTime.Now.AddDays(-3);
                    //跳转登陆页面
                    Response.Redirect("/Login.aspx");
                }
                else
                {
                    JavaScriptSerializer jss = new JavaScriptSerializer();
                    if (Request.Cookies["LoginCookie_Author"] != null && !string.IsNullOrWhiteSpace(Request.Cookies["LoginCookie_Author"].Value))
                    {
                        string loginCookie = "";
                        if (Request.Cookies["TokenID"] != null && !string.IsNullOrWhiteSpace(Request.Cookies["TokenID"].Value))
                        {
                            #region
                            var postData = "&Func=GetUserInfoByToken&tokenID=" + System.Web.HttpUtility.UrlDecode(Request.Cookies["TokenID"].Value) + "&returnUrl=" + Request.Url.ToString();
                            string result = NetHelper.RequestPostUrl(ConfigHelper.GetSettingString("TokenPath"), postData);

                            if (!string.IsNullOrWhiteSpace(result))
                            {
                                int starIndex = result.IndexOf(":") + 1;
                                int endIndex = result.LastIndexOf("}");
                                result = result.Substring(starIndex, endIndex - starIndex);
                                JsonModel jsonModel = jss.Deserialize<JsonModel>(result);
                                if (jsonModel != null && jsonModel.retData != null && jsonModel.errNum == 0)
                                {
                                    Dictionary<string, object> ht = jsonModel.retData as Dictionary<string, object>;
                                    if (ht != null)
                                    {
                                        Sys_User item = new Sys_User();
                                        System.Reflection.PropertyInfo[] properties = item.GetType().GetProperties();
                                        foreach (System.Reflection.PropertyInfo property in properties)
                                        {
                                            if (ht.ContainsKey(property.Name))
                                            {
                                                if (property.PropertyType.Name.StartsWith("String"))
                                                    property.SetValue(item, ht[property.Name].SafeToString(), null);
                                                if (property.PropertyType.Name.StartsWith("Int32"))
                                                    property.SetValue(item, Convert.ToInt32(ht[property.Name]), null);
                                                if (property.PropertyType.Name.StartsWith("DateTime"))
                                                    property.SetValue(item, Convert.ToDateTime(ht[property.Name]), null);
                                                if (property.PropertyType.Name.StartsWith("Byte"))
                                                    property.SetValue(item, Convert.ToByte(ht[property.Name]), null);
                                            }
                                        }
                                        UserInfo = item;
                                        Response.Cookies["LoginCookie_Author"].Value = System.Web.HttpUtility.UrlEncode(JsonHelper.SerializeObject(UserInfo));
                                        Response.Cookies["username"].Expires = DateTime.MaxValue;
                                    }
                                }
                                else
                                {
                                    Response.Redirect("/Login.aspx");
                                    //Response.Write("<script language='javascript'>window.location='/Login.aspx'</script>");
                                }
                            }
                            #endregion
                        }
                        else
                        {
                            loginCookie = System.Web.HttpUtility.UrlEncode(Request.Cookies["LoginCookie_Author"].Value);
                            Sys_User item = jss.Deserialize<Sys_User>(loginCookie);
                            UserInfo = item;

                        }
                    }
                    else if (Request.Cookies["TokenID"] != null && !string.IsNullOrWhiteSpace(Request.Cookies["TokenID"].Value))
                    {
                        #region
                        var postData = "&Func=GetUserInfoByToken&tokenID=" + System.Web.HttpUtility.UrlDecode(Request.Cookies["TokenID"].Value) + "&returnUrl=" + Request.Url.ToString();
                        string result = NetHelper.RequestPostUrl(ConfigHelper.GetSettingString("TokenPath"), postData);

                        if (!string.IsNullOrWhiteSpace(result))
                        {
                            int starIndex = result.IndexOf(":") + 1;
                            int endIndex = result.LastIndexOf("}");
                            result = result.Substring(starIndex, endIndex - starIndex);
                            JsonModel jsonModel = jss.Deserialize<JsonModel>(result);
                            if (jsonModel != null && jsonModel.retData != null)
                            {
                                Dictionary<string, object> ht = jsonModel.retData as Dictionary<string, object>;
                                if (ht != null)
                                {
                                    Sys_User item = new Sys_User();
                                    System.Reflection.PropertyInfo[] properties = item.GetType().GetProperties();
                                    foreach (System.Reflection.PropertyInfo property in properties)
                                    {
                                        if (ht.ContainsKey(property.Name))
                                        {
                                            if (property.PropertyType.Name.StartsWith("String"))
                                                property.SetValue(item, ht[property.Name].SafeToString(), null);
                                            if (property.PropertyType.Name.StartsWith("Int32"))
                                                property.SetValue(item, Convert.ToInt32(ht[property.Name]), null);
                                            if (property.PropertyType.Name.StartsWith("DateTime"))
                                                property.SetValue(item, Convert.ToDateTime(ht[property.Name]), null);
                                            if (property.PropertyType.Name.StartsWith("Byte"))
                                                property.SetValue(item, Convert.ToByte(ht[property.Name]), null);
                                        }
                                    }
                                    UserInfo = item;
                                    //Response.Cookies["LoginCookie_Author"].Value = JsonHelper.SerializeObject(UserInfo);
                                    Response.Cookies["LoginCookie_Author"].Value = System.Web.HttpUtility.UrlEncode(HttpUtility.UrlEncode(JsonHelper.SerializeObject(UserInfo)));

                                    Response.Cookies["username"].Expires = DateTime.MaxValue;
                                }
                                if (Request.Url.ToString().IndexOf("Login.aspx") > -1 || Request.Url.ToString().IndexOf("Resgister.aspx") > -1)
                                    Response.Redirect("/Login.aspx");
                                    //Response.Write("<script language='javascript'>window.location='/Login.aspx'</script>");
                                else
                                    Response.Write("<script language='javascript'>window.location='" + Request.Url.ToString() + "'</script>");
                            }
                            else
                            {
                                Response.Redirect("/Login.aspx");
                                //Response.Write("<script language='javascript'>window.location='/Login.aspx'</script>");
                            }
                        }
                        #endregion
                    }
                    else
                    {
                        Response.Redirect("~/Login.aspx");
                    }
                  
                }
                base.OnInit(e);
            }
            catch (Exception ex)
            {
                Response.Cookies["LoginCookie_Author"].Expires = DateTime.Now.AddDays(-3);
                Response.Cookies["ClassID"].Expires = DateTime.Now.AddDays(-3);
                Response.Cookies["TokenID"].Expires = DateTime.Now.AddDays(-3);

                LogHelper.Error(ex);
                Response.Redirect("~/Login.aspx");
            }
        }
        /*protected override void OnInit(EventArgs e)
        {
            try
            {
                JavaScriptSerializer jss = new JavaScriptSerializer();
                if (Request.Cookies["LoginCookie_Author"] != null && !string.IsNullOrWhiteSpace(Request.Cookies["LoginCookie_Author"].Value))
                {

                    string loginCookie = System.Web.HttpUtility.UrlDecode(Request.Cookies["LoginCookie_Author"].Value);
                    Sys_User item = jss.Deserialize<Sys_User>(loginCookie);
                    UserInfo = item;
                }
                else if (Request.Cookies["TokenID"] != null && !string.IsNullOrWhiteSpace(Request.Cookies["TokenID"].Value))
                {
                    #region
                    var postData = "&Func=GetUserInfoByToken&tokenID=" + System.Web.HttpUtility.UrlDecode(Request.Cookies["TokenID"].Value) + "&returnUrl=" + Request.Url.ToString();
                    string result = NetHelper.RequestPostUrl(ConfigHelper.GetSettingString("TokenPath"), postData);

                    //string result = NetHelper.RequestPostUrl(Request.Url.AbsoluteUri, postData);
                    //var postData = "&Func=GetUserInfoByToken&tokenID=" + System.Web.HttpUtility.UrlDecode(Request.Cookies["TokenID"].Value) + "&returnUrl=http://192.168.1.101:8027/Login.aspx";

                    //string result = "({\"result\":{\"retData\":{\"Id\":7,\"UniqueNo\":\"啊发生\",\"UserType\":null,\"Name\":\"唐\",\"Nickname\":\"唐\",\"Sex\":1,\"Phone\":\"\",\"Birthday\":\"\",\"LoginName\":\"tang\",\"IDCard\":\"140481199805263255\",\"HeadPic\":\"\",\"RegisterOrg\":\"1000\",\"AuthenType\":0,\"Address\":\"\",\"Remarks\":\"\",\"CreateUID\":\"\",\"CreateTime\":\"2015-10-10\",\"EditUID\":null,\"EditTime\":null,\"IsEnable\":1,\"IsDelete\":0,\"CheckMsg\":\"\"},\"errMsg\":null,\"errNum\":0,\"status\":null}})";
                    if (!string.IsNullOrWhiteSpace(result))
                    {
                        int starIndex = result.IndexOf(":") + 1;
                        int endIndex = result.LastIndexOf("}");
                        result = result.Substring(starIndex, endIndex - starIndex);
                        SCModel.JsonModel jsonModel = jss.Deserialize<SCModel.JsonModel>(result);
                        if (jsonModel != null && jsonModel.retData != null)
                        {
                            Dictionary<string, object> ht = jsonModel.retData as Dictionary<string, object>;
                            if (ht != null)
                            {
                                Sys_User item = new Sys_User();
                                System.Reflection.PropertyInfo[] properties = item.GetType().GetProperties();
                                foreach (System.Reflection.PropertyInfo property in properties)
                                {
                                    if (ht.ContainsKey(property.Name))
                                    {
                                        //object obj = property.GetValue(item, null);
                                        //object obj2 = property.GetValue(item, DBNull.Value);
                                        //property.SetValue(item, property.GetValue(item,null), null);
                                        if (property.PropertyType.Name.StartsWith("String"))
                                            property.SetValue(item, ht[property.Name].SafeToString(), null);
                                        if (property.PropertyType.Name.StartsWith("Int32"))
                                            //if (string.IsNullOrWhiteSpace(Convert.ToString(ht[property.Name])))
                                            //    property.SetValue(item, -999999, null);
                                            //else
                                                property.SetValue(item, Convert.ToInt32(ht[property.Name]), null);
                                        if (property.PropertyType.Name.StartsWith("DateTime"))
                                            //if (string.IsNullOrWhiteSpace(Convert.ToString(ht[property.Name])))
                                            //    property.SetValue(item, Convert.ToDateTime("1900-01-01"), null);
                                            //else
                                                property.SetValue(item, Convert.ToDateTime(ht[property.Name]), null);
                                        if (property.PropertyType.Name.StartsWith("Byte"))
                                            //if (string.IsNullOrWhiteSpace(Convert.ToString(ht[property.Name])))
                                            //    property.SetValue(item, Convert.ToByte(255), null);
                                            //else
                                                property.SetValue(item, Convert.ToByte(ht[property.Name]), null);
                                    }
                                }                                                            
                                Response.Cookies["LoginCookie_Author"].Value = JsonHelper.SerializeObject(UserInfo);
                                Response.Cookies["username"].Expires = DateTime.MaxValue;
                            }
                            if (Request.Url.ToString().IndexOf("Login.aspx") > -1 || Request.Url.ToString().IndexOf("Resgister.aspx") > -1)
                                Response.Write("<script language='javascript'>window.location='/StuAssociate/AssoIndex.aspx'</script>");
                            // Response.Redirect("~/StuAssociate/AssoIndex.aspx");
                            else
                                Response.Write("<script language='javascript'>window.location='" + Request.Url.ToString() + "'</script>");
                            //Response.Redirect(Request.Url.ToString());
                        }
                        else
                        {
                            //Response.Redirect("~/Resgister.aspx");
                            Response.Write("<script language='javascript'>window.location='/Resgister.aspx'</script>");
                        }
                    }
                    #endregion
                }
                else
                {
                    Response.Redirect("~/Login.aspx");
                    //Response.Write("<script language='javascript'>window.location='/Login.aspx'</script>");
                }
                base.OnInit(e);
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
                Response.Redirect("~/Login.aspx");
                //Response.Write("<script language='javascript'>window.location='/Login.aspx'</script>");
            }
        }*/
    }

    [Serializable]
    public partial class Sys_User
    {
        /// <summary>
        ///主键 
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        ///用户唯一值 
        /// </summary>
        public string UniqueNo { get; set; }
        /// <summary>
        ///用户类型 
        /// </summary>
        public string UserType { get; set; }
        /// <summary>
        ///姓名 
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        ///昵称 
        /// </summary>
        public string Nickname { get; set; }
        /// <summary>
        ///性别 
        /// </summary>
        public string Sex { get; set; }
        /// <summary>
        ///联系电话 
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        ///出生日期 
        /// </summary>
        public string Birthday { get; set; }
        /// <summary>
        ///用户账号 
        /// </summary>
        public string LoginName { get; set; }
        /// <summary>
        ///密码 
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        ///身份证件号 
        /// </summary>
        public string IDCard { get; set; }
        /// <summary>
        ///相对路径头像 
        /// </summary>
        public string HeadPic { get; set; }
        /// <summary>
        /// 绝对路径头像
        /// </summary>
        public string AbsHeadPic { get; set; }
        /// <summary>
        ///注册的组织机构 
        /// </summary>
        public string RegisterOrg { get; set; }
        /// <summary>
        ///认证类型 
        /// </summary>
        public string AuthenType { get; set; }
        /// <summary>
        ///现住址 
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        ///备注 
        /// </summary>
        public string Remarks { get; set; }
        /// <summary>
        ///创建人 
        /// </summary>
        public string CreateUID { get; set; }
        /// <summary>
        ///创建时间 
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        ///修改人 
        /// </summary>
        public string EditUID { get; set; }
        /// <summary>
        ///修改时间 
        /// </summary>
        public DateTime EditTime { get; set; }
        /// <summary>
        ///启用/禁用 
        /// </summary>
        public string IsEnable { get; set; }
        /// <summary>
        ///是否删除 
        /// </summary>
        public string IsDelete { get; set; }

        public string CheckMsg { get; set; }
    }
}