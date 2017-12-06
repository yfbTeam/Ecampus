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
    public partial class Acti_ProjectDal : BaseDal<Acti_Project>, IActi_ProjectDal
    {
        #region 获取活动项目成员的分页数据
        public override DataTable GetListByPage(Hashtable ht, out int RowCount, bool IsPage = true, string Where = "")
        {
            RowCount = 0;
            List<SqlParameter> pms = new List<SqlParameter>();
            int StartIndex = 0;
            int EndIndex = 0;
            if (IsPage)
            {
                StartIndex = Convert.ToInt32(ht["StartIndex"].ToString());
                EndIndex = Convert.ToInt32(ht["EndIndex"].ToString());
            }
            try
            {
                bool isMember = ht["IsMember"].ToString() == "1";
                bool isOnlyBase = ht["IsOnlyBase"].ToString() != "0";
                StringBuilder sbSql4org = new StringBuilder();
                sbSql4org.Append(@"SELECT A.Id,A.Name,A.ActivityId ");
                if (isOnlyBase)
                {
                    sbSql4org.Append(@" " + (isMember ? ",B.JoinMember" : "") + ",act.Name as ActivityName,act.DepartId,depart.Name as DepartName,depart.PicURL ");
                    if (ht.ContainsKey("LoginUID") && !string.IsNullOrEmpty(ht["LoginUID"].ToString()))
                    {
                        sbSql4org.Append(@" ,case when depart.LeaderNo = @LoginUID or depart.SecondLeaderNo = @LoginUID then 1 else 0 end as IsLeader
                      ,(select count(1) from func_split(A.JoinMembers,',') where value=@LoginUID) as IsJoin ");
                        pms.Add(new SqlParameter("@LoginUID", ht["LoginUID"].ToString()));
                    }
                }
                if (isMember)
                {
                    sbSql4org.Append(@" FROM (
                                      SELECT StrXml = CONVERT(XML, '<root><v>'+REPLACE(JoinMembers,',','</v><v>')+'</v></root>'),  
                                      Id,Name,ActivityId from Acti_Project where IsDelete=0
                                    ) A 
                                    OUTER APPLY 
                                      (SELECT JoinMember = N.v.value('.','nvarchar(40)')  
                                      FROM A.StrXml.nodes('/root/v') N(v)
                                    ) B ");
                }
                else
                {
                    sbSql4org.Append(@" FROM (SELECT Id,Name,ActivityId from Acti_Project where IsDelete=0) A ");
                }
                if (isOnlyBase)
                {
                    sbSql4org.Append(@" left join Acti_Activity act on A.ActivityId=act.Id 
                                    left join Acti_DepartInfo depart on depart.Id=act.DepartId ");
                }
                sbSql4org.Append(@" where 1=1  ");
                if (ht.ContainsKey("Id") && !string.IsNullOrEmpty(ht["Id"].ToString()))
                {
                    sbSql4org.Append(" and A.Id=@Id ");
                    pms.Add(new SqlParameter("@Id", ht["Id"].ToString()));
                }
                if (ht.ContainsKey("Name") && !string.IsNullOrEmpty(ht["Name"].ToString()))
                {
                    sbSql4org.Append(" and A.Name like N'%' + @Name + '%' ");
                    pms.Add(new SqlParameter("@Name", ht["Name"].ToString()));
                }
                if (ht.ContainsKey("ActivityId") && !string.IsNullOrEmpty(ht["ActivityId"].ToString()))
                {
                    sbSql4org.Append(" and A.ActivityId=@ActivityId ");
                    pms.Add(new SqlParameter("@ActivityId", ht["DepartId"].ToString()));
                }                           
                string orderby = "";
                if (ht.ContainsKey("OrderBy") && !string.IsNullOrEmpty(ht["OrderBy"].ToString()))
                {
                    orderby = ht["OrderBy"].ToString();
                }
                return SQLHelp.GetListByPage("(" + sbSql4org.ToString() + ")", Where, orderby, StartIndex, EndIndex, IsPage, pms.ToArray(), out RowCount);
            }
            catch (Exception ex)
            {
                //写入日志
                //throw;
                return null;
            }
        }
        #endregion

        #region 编辑部门项目
        public int EditActiProject(Acti_Project model)
        {
            int result = 0;
            SqlParameter[] param = {
                    new SqlParameter("@Id", model.Id),
                    new SqlParameter("@ActivityId", model.ActivityId),
                    new SqlParameter("@Name", model.Name),
                    new SqlParameter("@CreateUID", model.CreateUID),
                    new SqlParameter("@EditUID", model.EditUID??""),
            };
            object obj = SQLHelp.ExecuteScalar("EditActiProject", CommandType.StoredProcedure, param);
            result = Convert.ToInt32(obj);
            return result;
        }
        #endregion
    }
}
