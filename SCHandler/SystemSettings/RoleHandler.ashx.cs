

using SCBLL;
using SCModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;

namespace SCHandler.SystemSettings
{
    /// <summary>
    /// RoleHandler 的摘要说明
    /// </summary>
    public class RoleHandler : IHttpHandler
    {
        Plat_RoleService bll = new Plat_RoleService();
        JsonModel jsonModel = new JsonModel() { errNum = 0, errMsg = "success", retData = "" };
        JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();
        BLLCommon bll_com = new BLLCommon();

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string syskey = context.Request["SystemKey"];
            string func = context.Request["func"];
            string loginname = HttpContext.Current.Request["loginname"] ?? "";
            string idcard = context.Request["useridcard"];
            string result = string.Empty;
            string optionType = string.Empty;
            string methodDescrip = string.Empty;
            try
            {

                switch (func)
                {
                    case "GetRoleTreeData":
                        GetSysRoleTreeData();
                        break;
                    case "GetRoleByUser":
                        GetRoleByUser(idcard);
                        break;
                    case "AddRole":
                        AddRole(syskey);
                        break;
                    case "EditRole":
                        EditRole(syskey);
                        break;
                    case "DeleteRole":
                        DeleteRole(syskey);
                        break;
                    case "GetUserDataByRoleId":
                        GetUserDataByRoleId();
                        break;
                    case "GetNotDataByRoleId":
                        GetUserDataByRoleId(" not in ");
                        break;
                    case "SetRoleMember":
                        SetRoleMember(syskey);
                       
                        break;
                    case "DeleteUserRelation":
                        DeleteUserRelation(syskey);
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
                //LogService.WriteErrorLog(ex.Message);
            }
            result = "{\"result\":" + jss.Serialize(jsonModel) + "}";
            context.Response.Write(result);
            context.Response.End();
        }
        #region 获取系统的角色树信息
        private void GetSysRoleTreeData()
        {
            try
            {
                DataTable roledt = bll.GetAllRoleList();
                StringBuilder roleJson = new StringBuilder();
                if (roledt != null)
                {
                    if (roledt.Rows.Count > 0)
                    {
                        for (int i = 0; i < roledt.Rows.Count; i++)
                        {
                            DataRow row = roledt.Rows[i];
                            roleJson.Append("{\"id\":" + row["Id"].ToString() + ", \"pId\": 0, \"name\":\"" + row["Name"].ToString() + "\"},");
                        }
                    }
                    jsonModel = new JsonModel()
                    {
                        errNum = 0,
                        errMsg = "success",
                        retData = "[" + roleJson.ToString().TrimEnd(',') + "]"
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
                //LogService.WriteErrorLog(ex.Message);
            }
        }
        #endregion

        #region
        private void GetRoleByUser( string useridcard)
        {
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("UserIDCard", useridcard);
                jsonModel = bll.GetRoleByUser(ht);
            }
            catch (Exception ex)
            {
                jsonModel = new JsonModel()
                {
                    errNum = 400,
                    errMsg = ex.Message,
                    retData = ""
                };
                //LogService.WriteErrorLog(ex.Message);
            }
        }
        #endregion

        #region 添加角色
        private void AddRole(string syskey)
        {
            string callback = HttpContext.Current.Request["jsoncallback"];
            string name = HttpContext.Current.Request["name"];
            jsonModel = bll.IsNameExists( name, 0, "Name");
            if (jsonModel.errNum == 0)
            {
                if (jsonModel.retData.ToString().ToLower() == "true")
                {
                    jsonModel = new JsonModel()
                    {
                        errNum = -1,
                        errMsg = "exist",
                        retData = ""
                    };
                }
                else
                {
                    string useridcard = HttpContext.Current.Request["useridcard"];
                    Plat_Role role = new Plat_Role();
                    role.SystemKey = "0";
                    role.Name = name;
                    role.Creator = useridcard;
                    role.CreateTime = DateTime.Now;
                    role.IsDelete = 0;
                    jsonModel = bll.Add(role);
                }
            }
        }
        #endregion

        #region 编辑角色
        private void EditRole(string syskey)
        {
            string callback = HttpContext.Current.Request["jsoncallback"];
            int roleid = Convert.ToInt32(HttpContext.Current.Request["roleid"]);
            string name = HttpContext.Current.Request["name"];
            string useridcard = HttpContext.Current.Request["useridcard"];
            jsonModel = bll.IsNameExists(name, roleid, "Name");
            if (jsonModel.errNum == 0)
            {
                if (jsonModel.retData.ToString().ToLower() == "true")
                {
                    jsonModel = new JsonModel()
                    {
                        errNum = -1,
                        errMsg = "exist",
                        retData = ""
                    };
                }
                else
                {
                    jsonModel = bll.GetEntityById(roleid);
                    if (jsonModel.errNum == 0)
                    {
                        Plat_Role role = jsonModel.retData as Plat_Role;
                        role.Id = roleid;
                        role.Name = name;
                        role.Editor = useridcard;
                        role.EditTime = DateTime.Now;
                        jsonModel = bll.Update(role);
                    }
                }
            }
        }
        #endregion

        #region 删除角色
        private void DeleteRole(string syskey)
        {
            string callback = HttpContext.Current.Request["jsoncallback"];
            int roleid = Convert.ToInt32(HttpContext.Current.Request["roleid"]);
            jsonModel = bll.DeleteRole(roleid);
        }
        #endregion

        #region 根据角色id获取角色下的用户
        private void GetUserDataByRoleId(string joinStr = " in ")
        {
            try
            {
                Hashtable ht = new Hashtable();
                if (!string.IsNullOrEmpty(HttpContext.Current.Request["name"]))
                {
                    ht.Add("Name", HttpContext.Current.Request["name"].ToString());
                }
                if (!string.IsNullOrEmpty(HttpContext.Current.Request["roleid"]))
                {
                    ht.Add("RoleId", HttpContext.Current.Request["roleid"].ToString());
                    ht.Add("JoinStr", joinStr);
                }
                if (!string.IsNullOrEmpty(HttpContext.Current.Request["sel_Type"]))
                {
                    ht.Add("sel_Type", HttpContext.Current.Request["sel_Type"].ToString());
                }

                bool ispage = true;
                if (!string.IsNullOrEmpty(HttpContext.Current.Request["ispage"]))
                {
                    ispage = false;
                }
                ht.Add("PageIndex", HttpContext.Current.Request["PageIndex"] ?? "1");
                ht.Add("PageSize", HttpContext.Current.Request["PageSize"] ?? "10");
                jsonModel = new Plat_RoleOfUserService().GetPage(ht, ispage);
            }
            catch (Exception ex)
            {
                jsonModel = new JsonModel()
                {
                    errNum = 400,
                    errMsg = ex.Message,
                    retData = ""
                };
                //LogService.WriteErrorLog(ex.Message);
            }
        }
        #endregion

        #region 设置角色成员
        private void SetRoleMember(string syskey)
        {
            try
            {
                string roleid = HttpContext.Current.Request["roleid"] ?? "";
                string idCardStr = HttpContext.Current.Request["idcardStr"];
                jsonModel = bll.SetRoleMember(roleid, idCardStr);
            }
            catch (Exception ex)
            {
                jsonModel = new JsonModel()
                {
                    errNum = 400,
                    errMsg = ex.Message,
                    retData = ""
                };
                //LogService.WriteErrorLog(ex.Message);
            }
        }
        #endregion

        #region 将用户移出角色
        private void DeleteUserRelation(string syskey)
        {
            Plat_RoleOfUser ruser = new Plat_RoleOfUser();
            ruser.RoleId = Convert.ToInt32(HttpContext.Current.Request["roleid"]);
            ruser.UserIDCard = HttpContext.Current.Request["deluseridcard"];
            jsonModel = new Plat_RoleOfUserService().DeleteUserRelation(ruser);
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