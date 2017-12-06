using SCDAL;
using SCIBLL;
using SCModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCBLL
{
    public partial class Plat_MenuInfoService : BaseService<Plat_MenuInfo>, IPlat_MenuInfoService
    {
        Plat_MenuInfoDal dal = new Plat_MenuInfoDal();
        #region 获得首页左侧导航处菜单信息
        /// <summary>
        /// 获得首页左侧导航处菜单信息
        /// </summary>
        /// <returns></returns>
        public DataTable GetLeftNavigationMenu(string systemkey, string useridcard, string pid = "0")
        {
            return dal.GetLeftNavigationMenu(useridcard, pid);
        }
        #endregion

        #region 获得权限设置处菜单信息
        /// <summary>
        /// 获得权限设置处菜单信息
        /// </summary>
        /// <returns></returns>
        public DataTable GetPermissionMenu(string roleid)
        {
            return dal.GetPermissionMenu(roleid);
        }
        #endregion

        #region 设置角色菜单
        /// <summary>
        /// 设置角色菜单
        /// </summary>
        /// <param name="roleid">角色id</param>
        /// <param name="menuids">菜单ids字符串，以逗号连接</param>
        /// <returns>返回 影响行数</returns>
        public JsonModel SetRoleMenu(string roleid, string menuids)
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
                        bool isdel = new Plat_RoleOfMenuDal().Delete(new Plat_RoleOfMenu(), "RoleId", Convert.ToInt32(roleid), trans);//删除旧的角色菜单关系
                        string[] idArray = menuids.Split(',');
                        int count = 0;
                        foreach (string menuid in idArray)
                        {
                            Plat_RoleOfMenu rm = new Plat_RoleOfMenu();
                            rm.RoleId = Convert.ToInt32(roleid);
                            rm.MenuId = Convert.ToInt32(menuid);
                            int result = new Plat_RoleOfMenuDal().Add(rm, trans);
                            if (result > 0)
                            {
                                count++;
                            }
                        }
                        if (idArray.Length != count)
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


        #region 根据pid和身份证号查找菜单
        /// <summary>
        /// 根据pid和身份证号查找菜单
        /// </summary>
        /// <returns></returns>
        public JsonModel GetMenuByPidAndIDCard(string useridcard, string pid)
        {
            BLLCommon common = new BLLCommon();
            try
            {

                DataTable modList = dal.GetMenuByPidAndIDCard(useridcard, pid);

                //定义JSON标准格式实体中
                JsonModel jsonModel = null;
                if (modList == null || modList.Rows.Count <= 0)
                {
                    jsonModel = new JsonModel()
                    {
                        errNum = 100,
                        errMsg = "无数据",
                        retData = ""
                    };
                    return jsonModel;
                }
                List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
                list = common.DataTableToList(modList);

                jsonModel = new JsonModel()
                {
                    errNum = 0,
                    errMsg = "success",
                    retData = list
                };

                return jsonModel;
            }
            catch (Exception ex)
            {
                JsonModel jsonModel = new JsonModel()
                {
                    errNum = 400,
                    errMsg = ex.Message,
                    retData = ""
                };
                return jsonModel;
            }
        }
        #endregion
    }
}
