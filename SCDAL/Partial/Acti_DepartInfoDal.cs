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
    public partial class Acti_DepartInfoDal : BaseDal<Acti_DepartInfo>, IActi_DepartInfoDal
    {
        #region 获取部门信息表的分页数据
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
                StringBuilder sbSql4org = new StringBuilder();
                bool isOnlyBase = ht["IsOnlyBase"].ToString() != "0";
                sbSql4org.Append(@"select distinct depart.* ");
                if (isOnlyBase)
                {
                    sbSql4org.Append(@",(select count(1) from Acti_DepartMember where DepartId=depart.Id) as MemCount
                ,(select count(1) from Com_NewInfo where type=0 and RelationId=depart.Id) as NewCount
                ,(select count(1) from Acti_RecruitApply app 
                    left join Acti_Activity act on app.ActivityId=act.Id where DepartId=depart.Id and app.ExamStatus=1 )as ExamApplyCount ");
                }
                if (ht.ContainsKey("LoginUID") && !string.IsNullOrEmpty(ht["LoginUID"].ToString()))
                {
                    sbSql4org.Append(@" ,(select count(1) from Acti_DepartMember where DepartId=depart.Id and MemberNo=@LoginUID) as IsMember
                        ,case when depart.LeaderNo = @LoginUID or depart.SecondLeaderNo = @LoginUID then 1 else 0 end as IsLeader");
                    pms.Add(new SqlParameter("@LoginUID", ht["LoginUID"].ToString()));
                }
                sbSql4org.Append(@" from Acti_DepartInfo depart ");
                sbSql4org.Append(@" where 1=1 ");
                if (ht.ContainsKey("Name") && !string.IsNullOrEmpty(ht["Name"].ToString()))
                {
                    sbSql4org.Append(" and depart.Name like N'%' + @Name + '%' ");
                    pms.Add(new SqlParameter("@Name", ht["Name"].ToString()));
                }
                if (ht.ContainsKey("ParentId") && !string.IsNullOrEmpty(ht["ParentId"].ToString()))
                {
                    sbSql4org.Append(" and depart.ParentId=@ParentId ");
                    pms.Add(new SqlParameter("@ParentId", ht["ParentId"].ToString()));
                }
                if (ht.ContainsKey("IsDelete") && !string.IsNullOrEmpty(ht["IsDelete"].ToString()))
                {
                    sbSql4org.Append(" and depart.IsDelete=@IsDelete ");
                    pms.Add(new SqlParameter("@IsDelete", ht["IsDelete"].ToString()));
                }
                if (ht.ContainsKey("Id") && !string.IsNullOrEmpty(ht["Id"].ToString()))
                {
                    sbSql4org.Append(" and depart.Id=@Id ");
                    pms.Add(new SqlParameter("@Id", ht["Id"].ToString()));
                }
                if (ht.ContainsKey("MyUserNo") && !string.IsNullOrEmpty(ht["MyUserNo"].ToString()))
                {
                    sbSql4org.Append(" and (depart.Id in(select distinct DepartId from Acti_DepartMember where MemberNo=@MyUserNo) or depart.LeaderNo=@MyUserNo or depart.SecondLeaderNo=@MyUserNo) ");
                    pms.Add(new SqlParameter("@MyUserNo", ht["MyUserNo"].ToString()));
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

        #region 编辑部门信息
        public int EditDepartInfo(Acti_DepartInfo model)
        {
            int result = 0;
            SqlParameter[] param = {
                    new SqlParameter("@Id", model.Id),
                    new SqlParameter("@Name", model.Name),
                    new SqlParameter("@ParentId", model.ParentId),
                    new SqlParameter("@LeaderNo", model.LeaderNo),
                    new SqlParameter("@SecondLeaderNo", model.SecondLeaderNo??""),
                    new SqlParameter("@Introduce", model.Introduce),
                    new SqlParameter("@PicURL", model.PicURL),
                    new SqlParameter("@CreateUID", model.CreateUID),
                    new SqlParameter("@EditUID", model.EditUID??"")
            };
            object obj = SQLHelp.ExecuteScalar("EditDepartInfo", CommandType.StoredProcedure, param);
            result = Convert.ToInt32(obj);
            return result;
        }
        #endregion
    }
}
