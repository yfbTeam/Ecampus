using SCIDAL;
using SCModel;
using SCUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCDAL
{
    public partial class Plat_MenuInfoDal : BaseDal<Plat_MenuInfo>, IPlat_MenuInfoDal
    {
        #region 获得首页左侧导航处菜单信息
        /// <summary>
        /// 获得首页左侧导航处菜单信息
        /// </summary>  
        public DataTable GetLeftNavigationMenu(string useridcard, string pid)
        {
            StringBuilder sbSql4org;
            List<SqlParameter> pms = new List<SqlParameter>();

            sbSql4org = new StringBuilder();
            if (useridcard == "00000000000000000X")//如果是默认最大权限的管理员
            {
                sbSql4org.Append(@"select distinct mi.* from Plat_SysOfMenu_Rel sm
                                 inner join ModelManage mi on sm.MenuId=mi.Id and  mi.Id is not null and mi.IsMenu=0 and  mi.isShow=3
                                 where  mi.Pid=@Pid ");
            }
            else
            {
                sbSql4org.Append(@"select distinct mi.* from Plat_RoleOfUser ru
                                inner join Plat_RoleOfMenu rm on ru.RoleId=rm.RoleId   
                                inner join ModelManage mi on mi.Id=rm.MenuId  
                                where mi.Id is not null and mi.IsMenu=0 and mi.Pid=@Pid and mi.isShow=3 and ru.UserIDCard=@UserIDCard ");
            }

            sbSql4org.Append(" order by mi.Id ");
            pms.Add(new SqlParameter("@UserIDCard", useridcard));
            pms.Add(new SqlParameter("@Pid", pid));
            return SQLHelp.ExecuteDataTable(sbSql4org.ToString(), CommandType.Text, pms.ToArray());
        }
        #endregion

        #region 获得权限设置处菜单信息
        /// <summary>
        /// 获得权限设置处菜单信息
        /// </summary>  
        public DataTable GetPermissionMenu(string roleid)
        {
            StringBuilder sbSql4org;
            List<SqlParameter> pms = new List<SqlParameter>();

            sbSql4org = new StringBuilder();
            sbSql4org.Append(@"select mi.*,ISNULL(rm.MenuId,0) as ischeck from ModelManage mi
                     left join Plat_RoleOfMenu rm on mi.Id=rm.MenuId and rm.RoleId=@RoleId ");
            sbSql4org.Append(" order by mi.Id ");
            pms.Add(new SqlParameter("@RoleId", roleid));
            return SQLHelp.ExecuteDataTable(sbSql4org.ToString(), CommandType.Text, pms.ToArray());
        }
        #endregion

        #region 根据pid和身份证号查找菜单
        /// <summary>
        /// 根据pid和身份证号查找菜单
        /// </summary>  
        public DataTable GetMenuByPidAndIDCard(string useridcard, string pid)
        {
            StringBuilder sbSql4org;
            List<SqlParameter> pms = new List<SqlParameter>();
            sbSql4org = new StringBuilder();
            sbSql4org.Append(@"select mi.*,ISNULL(um.MenuId,0) as IsOwner from Plat_MenuInfo mi
                     left join
					 ( select distinct rm.MenuId from Plat_RoleOfUser ru
                    inner join Plat_RoleOfMenu rm on ru.RoleId=rm.RoleId 
					inner join Plat_Role pr on pr.Id=rm.RoleId
					where ru.UserIDCard=@UserIDCard  ) um on mi.Id=um.MenuId
                    where 1=1 ");
            if (!string.IsNullOrEmpty(pid))
            {
                sbSql4org.Append(" and mi.Pid=@Pid ");
                pms.Add(new SqlParameter("@Pid", pid));
            }
            sbSql4org.Append(" order by mi.Id ");
            pms.Add(new SqlParameter("@UserIDCard", useridcard));
            return SQLHelp.ExecuteDataTable(sbSql4org.ToString(), CommandType.Text, pms.ToArray());
        }
        #endregion
    }
}

