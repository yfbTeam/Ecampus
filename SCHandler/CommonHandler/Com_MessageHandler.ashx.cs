using SCBLL;
using SCModel;
using SCUtility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace SCHandler.CommonHandler
{
    /// <summary>
    /// Com_MessageHandler 的摘要说明
    /// </summary>
    public class Com_MessageHandler : IHttpHandler
    {
        Com_MessageService BllSMS = new Com_MessageService();
        JsonModel jsonModel = new JsonModel() { errNum = 0, errMsg = "success", retData = "" };
        JavaScriptSerializer jss = new JavaScriptSerializer();
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string func = context.Request["Func"];
            if (!string.IsNullOrEmpty(func))
            {
                switch (func)
                {
                    case "GetComMessageDataPage": GetComMessageDataPage(context); break;
                    case "AddMessage": AddMessage(context); break;
                    case "UpdateMessage": UpdateMessage(context); break;                    
                    case "ReaderMessage": ReaderMessage(context); break;
                    case "GetMessage": GetMessage(context); break;
                    case "MoreSendMessage": MoreSendMessage(context); break;
                    case "AdminSendMessage": AdminSendMessage(context); break;
                    case "WebMessage": WebMessage(context); break;
                    default:
                        jsonModel = new JsonModel()
                        {
                            errNum = 404,
                            errMsg = "没有此方法",
                            retData = ""
                        };
                        break;
                }
                LogService.WriteLog(func);
            }
            else
            {
                context.Response.Write("System Error");
            }
            context.Response.End();
        }
        #region 获取消息列表
        public void GetComMessageDataPage(HttpContext context)
        {
            try
            {
                bool Ispage = true;
                Hashtable ht = new Hashtable();
                if (!string.IsNullOrWhiteSpace(context.Request["PageIndex"])) ht.Add("PageIndex", context.Request["PageIndex"].SafeToString());
                if (!string.IsNullOrWhiteSpace(context.Request["PageSize"])) ht.Add("PageSize", context.Request["PageSize"].SafeToString());
                ht.Add("TableName", "Com_Message");
                if (!string.IsNullOrWhiteSpace(context.Request["Ispage"]))
                {
                    Ispage = Convert.ToBoolean(context.Request["Ispage"]);
                }
                string where = string.Empty;
                if (!string.IsNullOrWhiteSpace(context.Request["type"]))
                    where += " and [type]=" + context.Request["type"].ToString();
                if (!string.IsNullOrWhiteSpace(context.Request["Status"]))
                    where += " and [Status]=" + context.Request["Status"].ToString();
                if (!string.IsNullOrWhiteSpace(context.Request["CreateUID"]))
                    where += " and [CreateUID]='" + context.Request["CreateUID"].ToString() + "'";
                if (!string.IsNullOrWhiteSpace(context.Request["Receiver"]))
                    where += " and [Receiver]='" + context.Request["Receiver"].ToString() + "'";
                if (!string.IsNullOrWhiteSpace(context.Request["StarDate"]) && !string.IsNullOrWhiteSpace(context.Request["EndDate"]))
                    where += " and (CreateTime<='" + context.Request["EndDate"] + " 23:59:59' and CreateTime>='" + context.Request["StarDate"] + " 00:00:01')";
                where += " and IsDelete !=" + (int)SysStatus.删除;
                ht.Add("Order", "CreateTime desc");
                jsonModel = BllSMS.GetPage(ht, Ispage, where);
                context.Response.Write("{\"result\":" + jss.Serialize(jsonModel) + "}");
            }
            catch (Exception ex)
            {
                jsonModel = new JsonModel()
                {
                    errMsg = ex.Message,
                    retData = "",
                    status = "no"
                };
                context.Response.Write("{\"result\":" + jss.Serialize(jsonModel) + "}");
            }
        }
        #endregion

        #region 修改消息
        public void UpdateMessage(HttpContext context)
        {
            if (!string.IsNullOrWhiteSpace(context.Request["Id"]))
            {
                try
                {
                    Com_Message sn = BllSMS.GetEntityById(int.Parse(context.Request["Id"])).retData as Com_Message;
                    if (sn != null)
                    {
                        if (!string.IsNullOrWhiteSpace(context.Request["Status"]))
                            sn.Status = Convert.ToByte(context.Request["Status"]);
                        if (!string.IsNullOrWhiteSpace(context.Request["IsDelete"]))
                            sn.IsDelete = Convert.ToByte(context.Request["IsDelete"]);
                        sn.Id = int.Parse(context.Request["id"]);
                        jsonModel = BllSMS.Update(sn);
                        jsonModel.errMsg = sn.Href;
                        context.Response.Write("{\"result\":" + jss.Serialize(jsonModel) + "}");
                    }
                }
                catch (Exception ex)
                {
                    jsonModel = new JsonModel()
                    {
                        errMsg = ex.Message,
                        retData = "",
                        status = "no"
                    };
                    context.Response.Write("{\"result\":" + jss.Serialize(jsonModel) + "}");
                }
            }
        }
        #endregion

        #region 添加消息
        public void AddMessage(HttpContext context)
        {
            try
            {
                Com_Message sm = new Com_Message();
                sm.Contents = context.Request["Contents"];//内容
                sm.Title = context.Request["Title"];//标题
                sm.Type = Convert.ToByte(context.Request["Type"]);
                sm.Status = Convert.ToByte((int)MessageStatus.未读);                
                sm.Receiver = context.Request["Receiver"];//接受人
                sm.Href = context.Request["Href"];//链接地址（可不提供）
                sm.ReceiverEmail = context.Request["ReceiverEmail"];//收件邮箱
                sm.IsDelete = Convert.ToByte((int)SysStatus.正常);
                sm.IsSend = Convert.ToByte((int)MessageIsSend.未发送);                
                sm.ReceiverName = context.Request["ReceiverName"];
                sm.FilePath = context.Request["FilePath"];
                sm.Timing = string.IsNullOrWhiteSpace(context.Request["Timing"]) ? Convert.ToByte((int)MessageTiming.立即发送) : Convert.ToByte((int)MessageTiming.定时发送);
                sm.CreateName = context.Request["CreateName"];
                sm.CreateTime = DateTime.Now;
                sm.CreateUID = context.Request["CreateUID"];//发送人
                jsonModel = BllSMS.Add(sm);
                jsonModel.status = "ok";
                context.Response.Write("{\"result\":" + jss.Serialize(jsonModel) + "}");
            }
            catch (Exception ex)
            {
                jsonModel = new JsonModel()
                {
                    errMsg = ex.Message,
                    retData = "",
                    status = "no"
                };

                context.Response.Write("{\"result\":" + jss.Serialize(jsonModel) + "}");
            }
        }
        #endregion

        #region 标记已读/删除 消息
        public void ReaderMessage(HttpContext context)
        {
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("ids", context.Request["ids"]);
                if (!string.IsNullOrWhiteSpace(context.Request["Status"]))
                    ht.Add("Status", context.Request["Status"]);
                if (!string.IsNullOrWhiteSpace(context.Request["IsDelete"]))
                    ht.Add("IsDelete", context.Request["IsDelete"]);
                if (!string.IsNullOrWhiteSpace(context.Request["Receiver"]))
                    ht.Add("Receiver", context.Request["Receiver"]);
                jsonModel = BllSMS.ReaderMessage(ht);
                context.Response.Write("{\"result\":" + jss.Serialize(jsonModel) + "}");
            }
            catch (Exception ex)
            {
                jsonModel = new JsonModel()
                {
                    errMsg = ex.Message,
                    retData = "",
                };

                context.Response.Write("{\"result\":" + jss.Serialize(jsonModel) + "}");
            }
        }
        #endregion

        #region 获取消息详情
        public void GetMessage(HttpContext context)
        {            
            try
            {
                if (!string.IsNullOrWhiteSpace(context.Request["Id"]))
                {
                    jsonModel = BllSMS.GetEntityById(int.Parse(context.Request["Id"]));
                }
                context.Response.Write("{\"result\":" + jss.Serialize(jsonModel) + "}");
            }
            catch (Exception ex)
            {
                jsonModel = new JsonModel()
                {
                    errMsg = ex.Message,
                    retData = "",
                };
                context.Response.Write("{\"result\":" + jss.Serialize(jsonModel) + "}");
            }
        }

        #endregion

        #region 站内信
        private void WebMessage(HttpContext context)
        {
            Com_Message model = new Com_Message();            
            try
            {
                if (!string.IsNullOrWhiteSpace(context.Request["Receivers"]))
                {
                    List<Com_Message> list = jss.Deserialize<List<Com_Message>>(context.Request["Receivers"]);
                    Hashtable ht = new Hashtable();
                    string CreateUID = context.Request["CreateUID"].SafeToString();
                    string CreateName = context.Request["CreateName"].SafeToString();
                    string Title = context.Request["Title"].SafeToString();
                    string Contents = HttpUtility.UrlDecode(context.Request["Contents"]).SafeToString();
                    string Type = context.Request["Type"].SafeToString();

                    string date = string.IsNullOrWhiteSpace(context.Request["CreateTime"]) ? DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") : context.Request["CreateTime"];
                    for (int i = 0; i < list.Count; i++)
                    {
                        model.CreateTime = Convert.ToDateTime(date);                        
                        model.Title = Title;
                        model.Contents = Contents;
                        model.Type = Convert.ToByte(Type);
                        model.Receiver = list[i].Receiver;
                        model.CreateName = CreateName;
                        model.CreateUID = CreateUID;                        
                        jsonModel = BllSMS.Add(model);
                    }
                    //jsonModel = BllSMS.SendMessage(ht, list);
                    context.Response.Write("{\"result\":" + jss.Serialize(jsonModel) + "}");
                }
            }
            catch (Exception ex)
            {
                jsonModel = new JsonModel()
                {
                    errMsg = ex.Message
                };
                context.Response.Write("{\"result\":" + jss.Serialize(jsonModel) + "}");
            }
        }
        #endregion

        #region 批量发送消息
        public void MoreSendMessage(HttpContext context)
        {          
            try
            {
                if (!string.IsNullOrWhiteSpace(context.Request["Receivers"]))
                {
                    List<Com_Message> list = jss.Deserialize<List<Com_Message>>(context.Request["Receivers"]);
                    bool isend = string.IsNullOrWhiteSpace(context.Request["isSendEmail"]) ? false : Convert.ToBoolean(context.Request["isSendEmail"]);
                    Hashtable ht = new Hashtable();
                    ht.Add("CreateUID", context.Request["CreateUID"]);
                    ht.Add("CreateName", context.Request["CreateName"]);
                    ht.Add("Title", context.Request["Title"]);
                    ht.Add("Contents", HttpUtility.UrlDecode(context.Request["Contents"]));
                    ht.Add("Timing", context.Request["Timing"]);
                    ht.Add("FilePath", context.Request["FilePath"]);
                    ht.Add("isSendEmail", isend);
                    ht.Add("Type", context.Request["Type"]);
                    if (!string.IsNullOrWhiteSpace(context.Request["FilePath"]))
                    {
                        string path = context.Server.MapPath("/");
                        string preth = path.Substring(0, path.LastIndexOf("\\"));
                        preth = preth.Substring(0, preth.LastIndexOf("\\"));
                        string newpath = preth + "\\SMSWeb" + context.Request["FilePath"].ToString().Replace("/", "\\");
                        ht.Add("FileEmailPath", newpath);
                    }
                    string date = string.IsNullOrWhiteSpace(context.Request["CreateTime"]) ? DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") : context.Request["CreateTime"];
                    ht.Add("CreateTime", date);
                    jsonModel = BllSMS.SendMessage(ht, list);
                    context.Response.Write("{\"result\":" + jss.Serialize(jsonModel) + "}");
                }
            }
            catch (Exception ex)
            {
                jsonModel = new JsonModel()
                {
                    errMsg = ex.Message
                };
                context.Response.Write("{\"result\":" + jss.Serialize(jsonModel) + "}");
            }
        }
        #endregion

        #region 管理员发送邮件
        public void AdminSendMessage(HttpContext context)
        {            
            try
            {
                string Subject = context.Request["Title"];
                string Body = context.Request["Contents"];
                string Email = context.Request["Email"];
                SCUtility.Mail.SendMailMessage.SendMessage(Subject, Body, Email);
                jsonModel = new JsonModel()
                {
                    errMsg = "success",
                    errNum = 0
                };
                context.Response.Write("{\"result\":" + jss.Serialize(jsonModel) + "}");
            }
            catch (Exception ex)
            {
                jsonModel = new JsonModel()
                {
                    errMsg = ex.Message,
                    errNum = 400
                };
                context.Response.Write("{\"result\":" + jss.Serialize(jsonModel) + "}");
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