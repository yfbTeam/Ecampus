using SCDAL;
using SCIBLL;
using SCModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCBLL
{
    public partial class Plat_RoleService : BaseService<Plat_Role>, IPlat_RoleService
    {
        Plat_RoleDal dal = new Plat_RoleDal();
        BLLCommon common = new BLLCommon();
        #region 获取全部角色，返回DataTable
        /// <summary>
        /// 获取全部角色，返回DataTable
        /// </summary>
        public DataTable GetAllRoleList()
        {
            return dal.GetAllRoleList();
        }
        #endregion

        #region 删除角色并删除该角色的相关数据
        /// <summary>
        /// 删除角色并删除该角色的相关数据
        /// </summary>
        /// <param name="roleid">角色id</param>
        /// <returns></returns>
        public JsonModel DeleteRole(int roleid)
        {
            //定义JSON标准格式实体中
            JsonModel jsonModel = new JsonModel();
            try
            {
                //事务
                using (SqlTransaction trans = dal.GetTran())
                {
                    try
                    {
                        Plat_Role role = dal.GetEntityById(new Plat_Role(), roleid);
                        if (role != null)
                        {
                            role.Id = roleid;
                            role.IsDelete = 1;
                            if (!dal.Update(role, trans))
                            {
                                trans.Rollback();//回滚
                                jsonModel = new JsonModel()
                                {
                                    errNum = 1,
                                    errMsg = "fial",
                                    retData = "操作失败"
                                };
                                return jsonModel;
                            }
                            new Plat_RoleOfMenuDal().Delete(new Plat_RoleOfMenu(), "RoleId", roleid, trans);
                            new Plat_RoleOfUserDal().Delete(new Plat_RoleOfUser(), "RoleId", roleid, trans);
                            trans.Commit();//提交                   
                        }
                        else
                        {
                            jsonModel = new JsonModel()
                            {
                                errNum = 1,
                                errMsg = "fial",
                                retData = "操作失败"
                            };
                        }
                    }
                    catch (Exception)
                    {
                        trans.Rollback();//回滚
                        throw;
                    }
                }
                jsonModel = new JsonModel()
                {
                    errNum = 0,
                    errMsg = "success",
                    retData = "操作成功"
                };
                return jsonModel;
            }
            catch (Exception ex)
            {
                jsonModel = new JsonModel()
                {
                    errNum = 400,
                    errMsg = ex.Message,
                    retData = ""
                };
                return jsonModel;
            }
        }
        #endregion

        #region 设置角色成员
        /// <summary>
        /// 设置角色成员
        /// </summary>
        /// <param name="roleid">角色id</param>
        /// <param name="idCardStr">身份证号字符串，以逗号连接</param>
        /// <returns>返回 JsonModel</returns>
        public JsonModel SetRoleMember(string roleid, string idCardStr)
        {
            //定义JSON标准格式实体中
            JsonModel jsonModel = new JsonModel();
            try
            {
                //事务
                using (SqlTransaction trans = dal.GetTran())
                {
                    try
                    {
                        string[] idcardArray = idCardStr.Split(',');
                        int count = 0;
                        foreach (string idcard in idcardArray)
                        {
                            Plat_RoleOfUser ru = new Plat_RoleOfUser();
                            ru.RoleId = Convert.ToInt32(roleid);
                            ru.UserIDCard = idcard;
                            int result = new Plat_RoleOfUserDal().Add(ru, trans);
                            if (result > 0)
                            {
                                count++;
                            }
                        }
                        if (idcardArray.Length != count)
                        {
                            trans.Rollback();//回滚
                            jsonModel = new JsonModel()
                            {
                                errNum = 1,
                                errMsg = "fial",
                                retData = "操作失败"
                            };
                            return jsonModel;
                        }
                        else
                        {
                            trans.Commit();//提交
                        }
                    }
                    catch (Exception)
                    {
                        trans.Rollback();//回滚
                        throw;
                    }
                }
                jsonModel = new JsonModel()
                {
                    errNum = 0,
                    errMsg = "success",
                    retData = "操作成功"
                };
                return jsonModel;
            }
            catch (Exception ex)
            {
                jsonModel = new JsonModel()
                {
                    errNum = 400,
                    errMsg = ex.Message,
                    retData = ""
                };
                return jsonModel;
            }
        }
        #endregion

        #region 获取某用户的角色信息
        /// <summary>
        /// 获取某用户的角色信息
        /// </summary>
        public JsonModel GetRoleByUser(Hashtable ht)
        {
            try
            {
                DataTable modList = dal.GetRoleByUser(ht);
                //定义JSON标准格式实体中
                JsonModel jsonModel = null;
                if (modList.Rows.Count <= 0)
                {
                    jsonModel = new JsonModel()
                    {
                        status = "no",
                        errMsg = "无数据"
                    };
                    return jsonModel;
                }
                List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
                list = common.DataTableToList(modList);
                jsonModel = new JsonModel()
                {
                    retData = list,
                    errMsg = "",
                    status = "ok"
                };
                return jsonModel;
            }
            catch (Exception ex)
            {
                JsonModel jsonModel = new JsonModel();
                jsonModel.status = "error";
                jsonModel.retData = ex.ToString();
                return jsonModel;
            }
        }
        #endregion
    }
}
