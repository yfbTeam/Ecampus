using SCIDAL;
using SCModel;
using SCUtility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCDAL
{
    public partial class Plat_RoleOfUserDal : BaseDal<Plat_RoleOfUser>, IPlat_RoleOfUserDal
    {
        public override DataTable GetListByPage(Hashtable ht, out int rowCount, bool IsPage = true, string Where = "")
        {
            List<SqlParameter> pms = new List<SqlParameter>();
            rowCount = 0;
            try
            {
                StringBuilder sbSql4org = new StringBuilder();
                if (ht.ContainsKey("sel_Type") && !string.IsNullOrEmpty(ht["sel_Type"].ToString()))
                {
                    if (ht["sel_Type"].ToString() == "学生")
                    {
                        sbSql4org.Append(@"select distinct  U.Id,U.LoginName,U.Name,U.Sex,U.IDCard,'学生' as UserType from Plat_Student U where U.IDCard != '00000000000000000X' and IsDelete=0");
                    }
                    else
                    {
                        sbSql4org.Append(@"select distinct U.Id,U.LoginName,U.Name,U.Sex,U.IDCard,'普通用户' as UserType from Plat_Teacher U where  U.IDCard != '00000000000000000X' and IsDelete=0");
                    }
                }
                else
                {
                    sbSql4org.Append(@"select distinct U.* from(
                                    select uinfo.Id,uinfo.LoginName,uinfo.Name,uinfo.Sex,uinfo.IDCard,'普通用户' as UserType from Plat_Teacher uinfo	
                                    union
                                    select uinfo.Id,uinfo.LoginName,uinfo.Name,uinfo.Sex,uinfo.IDCard,'学生' as UserType from Plat_Student uinfo
                                     ) U
                                    where U.IDCard != '00000000000000000X' ");
                }
                if (ht.ContainsKey("Name") && !string.IsNullOrEmpty(ht["Name"].ToString()))
                {
                    sbSql4org.Append(" and U.Name like N'%'+@Name+'%' ");
                    pms.Add(new SqlParameter("@Name", ht["Name"].ToString()));
                }
                if (ht.ContainsKey("RoleId") && !string.IsNullOrEmpty(ht["RoleId"].ToString()))
                {
                    sbSql4org.Append(" and U.IDCard ");
                    if (ht.ContainsKey("JoinStr") && !string.IsNullOrEmpty(ht["JoinStr"].ToString()))
                    {
                        sbSql4org.Append(ht["JoinStr"].ToString());
                    }
                    else
                    {
                        sbSql4org.Append(" in ");

                    }
                    sbSql4org.Append(" (select UserIDCard from Plat_RoleOfUser where RoleId=@RoleId) ");
                    pms.Add(new SqlParameter("@RoleId", ht["RoleId"].ToString()));
                }
                DataTable dt = SQLHelp.GetListByPage("(" + sbSql4org.ToString() + ")", "", "", Convert.ToInt32(ht["StartIndex"].ToString()), Convert.ToInt32(ht["EndIndex"].ToString()), IsPage, pms.ToArray(), out rowCount);
                return dt;
            }
            catch (Exception ex)
            {
                //写入日志
                //throw;                
                return null;
            }

        }

        #region 删除关系数据， 删单条
        /// <summary>
        /// 删除关系数据， 删单条
        /// </summary>
        public bool DeleteUserRelation(Plat_RoleOfUser roleu)
        {
            try
            {
                StringBuilder sbSql;
                List<SqlParameter> pms = new List<SqlParameter>();

                sbSql = new StringBuilder();
                sbSql.Append("DELETE FROM Plat_RoleOfUser");
                sbSql.Append(" WHERE RoleId=@RoleId and UserIDCard=@UserIDCard");

                pms.Add(new SqlParameter("@RoleId", roleu.RoleId));
                pms.Add(new SqlParameter("@UserIDCard", roleu.UserIDCard));
                return SQLHelp.ExecuteNonQuery(sbSql.ToString(), CommandType.Text, pms.ToArray()) > 0;
            }
            catch (Exception)
            {
                //写入日志
                //throw;
                return false;
            }
        }
        #endregion

        #region 数据导入
        /// <summary>
        /// 数据导入
        /// </summary>
        /// <returns></returns>
        public string ImportUser(DataTable dt)
        {
            Import(dt);
            string result = "";
            ExcelHelper excelHelp = new ExcelHelper();
            try
            {
                object obj = SQLHelp.ExecuteScalar("AsynUserInfo", CommandType.StoredProcedure, null);
                result = obj.ToString();
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }
        public bool Import(DataTable dt)
        {
            DataView dv = dt.DefaultView;
            DataTable newTable4 = dv.ToTable("NewTableName", true, new string[] { "UserType", "Name", "Sex", "LoginName", "UniqueNo", "AuthenType", "IsDelete" });
            bool Flag = true;
            try
            {
                //用bcp导入数据 
                using (System.Data.SqlClient.SqlBulkCopy bcp = new System.Data.SqlClient.SqlBulkCopy(ConfigHelper.GetConfigString("connStr")))
                {
                    //bcp.SqlRowsCopied += new System.Data.SqlClient.SqlRowsCopiedEventHandler(bcp_SqlRowsCopied);
                    bcp.BatchSize = 100;//每次传输的行数 
                    bcp.NotifyAfter = 100;//进度提示的行数 
                    bcp.DestinationTableName = "Sys_UserInfo";//目标表 
                    bcp.WriteToServer(newTable4);
                    Flag = true;
                }
            }
            catch (Exception ex)
            {
                Flag = false;
            }
            return Flag;
        }
        #endregion

        public string GetUserOfAdmin()
        {
            string RowName = ConfigHelper.GetConfigString("AdminRow");
            string ReturnResult = "";
            string strSql = "select UserIDCard from [dbo].[Plat_RoleOfUser] where RoleId in (select id from Plat_Role where name in (" + RowName + "))";
            DataTable dt = SQLHelp.ExecuteDataTable(strSql, CommandType.Text, null);
            if (dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    ReturnResult += dr["UserIDCard"] + ",";
                }
            }
            return ReturnResult;
        }

    }
}
