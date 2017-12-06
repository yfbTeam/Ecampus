using SCIDAL;
using SCModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SCUtility;
using System.Data.SqlClient;
namespace SCDAL
{
    public partial class ModelManageDal : BaseDal<ModelManage>, IModelManageDal
    {/*
        #region 分页获取模块信息重写
        /// <summary>
        /// 分页获取模块信息重写
        /// </summary>
        /// <param name="ht"></param>
        /// <param name="IsPage"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        public override DataTable GetListByPage(Hashtable ht, out int RowCount, bool IsPage, string where)
        {
            RowCount = 0;
            DataTable dt = new DataTable();
            try
            {
                StringBuilder str = new StringBuilder();
                str.Append(@"select model.*,catory.Name as CatogryName,catory.ID as CatogryID,catory.SortNum from ModelManage model inner join ModelCatogory catory on model.ModelType=catory.ID where catory.Status=1");

                int StartIndex = 0;
                int EndIndex = 0;
                string Where = "";
                if (ht.ContainsKey("ID") && !string.IsNullOrEmpty(ht["ID"].SafeToString()))
                {
                    str.Append(" and model.ID = " + ht["ID"].SafeToString());
                }
                if (ht.ContainsKey("ModelType") && !string.IsNullOrEmpty(ht["ModelType"].SafeToString()))
                {
                    str.Append(" and model.ModelType=" + ht["ModelType"].SafeToString());
                }
                if (ht.ContainsKey("IDCard") && !string.IsNullOrEmpty(ht["IDCard"].SafeToString()))
                {
                    str.Append(" and model.ID " + ht["IsShow"] + " (select ModelID from User_Model_Rel where UserIDCard='" + ht["IDCard"] + "')");
                }
                if (IsPage)
                {
                    StartIndex = Convert.ToInt32(ht["StartIndex"].ToString());
                    EndIndex = Convert.ToInt32(ht["EndIndex"].ToString());
                }
                dt = SQLHelp.GetListByPage("(" + str.ToString() + ")", Where, " SortNum,ID", StartIndex, EndIndex, IsPage, null, out RowCount);
            }
            catch (Exception ex)
            {
                LogService.WriteErrorLog(ex.Message);
            }
            return dt;
        }
        #endregion
      * */
        #region 分页获取模块信息重写
        /// <summary>
        /// 分页获取模块信息重写
        /// </summary>
        /// <param name="ht"></param>
        /// <param name="IsPage"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        public override DataTable GetListByPage(Hashtable ht, out int RowCount, bool IsPage, string where)
        {
            RowCount = 0;
            DataTable dt = new DataTable();
            try
            {
                StringBuilder str = new StringBuilder();
                //str.Append(@"select model.*,catory.Name as CatogryName,catory.ID as CatogryID,catory.SortNum from ModelManage model inner join ModelCatogory catory on model.ModelType=catory.ID where catory.Status=1");
                str.Append(@"select distinct model.*,(select count(1) from ModelManage where Pid=model.Id) as ChildCount,catory.Name as CatogryName,catory.ID as CatogryID,catory.SortNum from Plat_RoleOfUser ru inner join Plat_RoleOfMenu rm on ru.RoleId=rm.RoleId  inner join ModelManage model on model.Id=rm.MenuId  
inner join ModelCatogory catory on model.ModelType=catory.ID where catory.Status=1 and model.Id is not null");// and model.Pid=0");
                int StartIndex = 0;
                int EndIndex = 0;
                string Where = "";
                if (ht.ContainsKey("ID") && !string.IsNullOrEmpty(ht["ID"].SafeToString()))
                {
                    str.Append(" and model.ID = " + ht["ID"].SafeToString());
                }
                if (ht.ContainsKey("Pid") && !string.IsNullOrEmpty(ht["Pid"].SafeToString()))
                {
                    str.Append(" and model.Pid = " + ht["Pid"].SafeToString());
                }
                if (ht.ContainsKey("ModelType") && !string.IsNullOrEmpty(ht["ModelType"].SafeToString()))
                {
                    str.Append(" and model.ModelType=" + ht["ModelType"].SafeToString());
                }
                if (ht.ContainsKey("IDCard") && !string.IsNullOrEmpty(ht["IDCard"].SafeToString()))
                {
                    str.Append("  and ru.UserIDCard='" + ht["IDCard"] + "'");
                    if (ht.ContainsKey("IsShow") && !string.IsNullOrEmpty(ht["IsShow"].SafeToString()))
                    {
                        str.Append(" and model.ID " + ht["IsShow"] + " (select ModelID from User_Model_Rel where UserIDCard='" + ht["IDCard"] + "')");
                    }
                }
                if (IsPage)
                {
                    StartIndex = Convert.ToInt32(ht["StartIndex"].ToString());
                    EndIndex = Convert.ToInt32(ht["EndIndex"].ToString());
                }
                dt = SQLHelp.GetListByPage("(" + str.ToString() + ")", Where, " SortNum,OrderNum,ID", StartIndex, EndIndex, IsPage, null, out RowCount);
            }
            catch (Exception ex)
            {
                LogService.WriteErrorLog(ex.Message);
            }
            return dt;
        }
        #endregion

        #region 根据url查找子级button
        public DataTable GetSubButtonByUrl(string purl, string uniqueNo)
        {
            List<SqlParameter> pms = new List<SqlParameter>();
            string sql = string.Empty;
            if (uniqueNo == "00000000000000000X")//如果是默认最大权限的管理员
            {
                sql = @"select distinct mi.*
                        ,(select STUFF((select ','+CAST(ISNULL(mbtn.ID,0)AS NVARCHAR(MAX))+'|'+ CAST(ModelName AS NVARCHAR(MAX))+'|'+ CAST(iconCss AS NVARCHAR(MAX)) from ModelManage mbtn
                        where Pid = mi.Id and IsMenu = 1 ORDER By ModelName FOR xml path('')), 1, 1, '')) as ButtonField 
                        from ModelManage mi 
                        where 1=1 and mi.Url=@Url ";
            }
            else
            {
                sql = @" select distinct mi.*,(select STUFF((select ','+CAST(ISNULL(mbtn.ID,0)AS NVARCHAR(MAX))+'|'+ CAST(ModelName AS NVARCHAR(MAX))+'|'+ CAST(iconCss AS NVARCHAR(MAX)) from ModelManage mbtn
  where Pid = mi.Id and IsMenu = 1 ORDER By ModelName FOR xml path('')), 1, 1, '')) as ButtonField 
  from Plat_RoleOfUser ru inner join Plat_RoleOfMenu rm on ru.RoleId=rm.RoleId  inner join ModelManage mi on mi.Id=rm.MenuId  
inner join ModelCatogory catory on mi.ModelType=catory.ID where catory.Status=1 and mi.Id is not null
 and ru.UserIDCard=@UniqueNo and mi.Linkurl=@Url ";
            }
            //if (!string.IsNullOrEmpty(menuCode))
            //{
            //    sql += "  and mi.MenuCode=@MenuCode ";
            //}
            pms.Add(new SqlParameter("@UniqueNo", uniqueNo));
            pms.Add(new SqlParameter("@Url", purl));
            //pms.Add(new SqlParameter("@MenuCode", menuCode));
            return SQLHelp.ExecuteDataTable(sql, CommandType.Text, pms.ToArray());
        }
        #endregion
        public string AddModelMenu(ModelManage entity, string UserUniqueNo)
        {
            string result = "";
            SqlParameter[] pa = { 
                                new SqlParameter("@UserUniqueNo",UserUniqueNo),
                                new SqlParameter("@ModelName",entity.ModelName),
                                new SqlParameter("@ModelType",entity.ModelType),
                                new SqlParameter("@OpenType",entity.OpenType),
                                new SqlParameter("@ModelCss",entity.ModelCss),
                                new SqlParameter("@iconCss",entity.iconCss),
                                new SqlParameter("@LinkUrl",entity.LinkUrl),
                                new SqlParameter("@Pid",entity.Pid),
                                new SqlParameter("@OrderNum",entity.OrderNum),
                                new SqlParameter("@IsMenu",entity.IsMenu),
                                new SqlParameter("@MenuType",entity.MenuType)
                                };
            object obj = SQLHelp.ExecuteScalar("AddModelMenu", CommandType.StoredProcedure, pa);
            result = obj.ToString();
            return result;
        }
        public string DeleteModelMenu(int ID)
        {
            string result = "";
            SqlParameter[] pa = {new SqlParameter("@ID",ID)};
            object obj = SQLHelp.ExecuteScalar("DelModelMenu", CommandType.StoredProcedure, pa);
            result = obj.ToString();
            return result;
        }

    }
}
