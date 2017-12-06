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
    public partial class Acti_ActivityDal : BaseDal<Acti_Activity>, IActi_ActivityDal
    {
        #region 获取部门活动表的分页数据
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
                sbSql4org.Append(@"select act.*,depart.Name as DepartName,depart.PicURL
                   ,DATEDIFF(S,'1970-01-01 00:00:00', act.CreateTime) - 8 * 3600 as CreateTime_Stamp
                    ,case when convert(varchar(10),getdate(),21) between convert(varchar(10),act.StartTime,21) and convert(varchar(10),act.EndTime,21) then 1 
                     when convert(varchar(10),getdate(),21)>convert(varchar(10),act.EndTime,21) then 2 else 0 end as ActStatus");
                if (ht.ContainsKey("LoginUID") && !string.IsNullOrEmpty(ht["LoginUID"].ToString()))
                {
                    sbSql4org.Append(@" ,case when depart.LeaderNo = @LoginUID or depart.SecondLeaderNo = @LoginUID then 1 else 0 end as IsLeader
                      ,(select count(1) from func_split(act.JoinMembers,',') where value=@LoginUID) as IsJoin ");
                    pms.Add(new SqlParameter("@LoginUID", ht["LoginUID"].ToString()));
                }
                sbSql4org.Append(@" from Acti_Activity act
                                  left join Acti_DepartInfo depart on depart.Id=act.DepartId where 1=1 and act.IsDelete=0 ");
                if (ht.ContainsKey("Id") && !string.IsNullOrEmpty(ht["Id"].ToString()))
                {
                    sbSql4org.Append(" and act.Id=@Id ");
                    pms.Add(new SqlParameter("@Id", ht["Id"].ToString()));
                }
                if (ht.ContainsKey("Name") && !string.IsNullOrEmpty(ht["Name"].ToString()))
                {
                    sbSql4org.Append(" and act.Name like N'%' + @Name + '%' ");
                    pms.Add(new SqlParameter("@Name", ht["Name"].ToString()));
                }
                if (ht.ContainsKey("DepartId") && !string.IsNullOrEmpty(ht["DepartId"].ToString()))
                {
                    sbSql4org.Append(" and act.DepartId=@DepartId ");
                    pms.Add(new SqlParameter("@DepartId", ht["DepartId"].ToString()));
                }
                if (ht.ContainsKey("ActStatus") && !string.IsNullOrEmpty(ht["ActStatus"].ToString()))
                {
                    if (ht["ActStatus"].ToString() == "ing")
                    {
                        sbSql4org.Append(" and convert(varchar(10),getdate(),21) between convert(varchar(10),act.ActStartTime,21) and convert(varchar(10),act.ActEndTime,21) ");
                    }
                    else
                    {
                        sbSql4org.Append(" and convert(varchar(10),getdate(),21)>convert(varchar(10),act.ActEndTime,21) ");
                    }
                }
                //部长/副部长
                if (ht.ContainsKey("OSLeaderNo") && !string.IsNullOrEmpty(ht["OSLeaderNo"].ToString()))
                {
                    sbSql4org.Append(" and (depart.LeaderNo=@OSLeaderNo or depart.SecondLeaderNo=@OSLeaderNo) ");
                    pms.Add(new SqlParameter("@OSLeaderNo", ht["OSLeaderNo"].ToString()));
                }
                //我的部门下的活动
                if (ht.ContainsKey("MyUserNo") && !string.IsNullOrEmpty(ht["MyUserNo"].ToString()))
                {
                    sbSql4org.Append(" and (depart.Id in(select distinct DepartId from Acti_DepartMember where MemberNo=@MyUserNo) or (depart.LeaderNo=@MyUserNo or depart.SecondLeaderNo=@MyUserNo)) ");
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

        #region 编辑部门活动
        public int EditDepartActivity(Acti_Activity model,string projectStr)
        {
            int result = 0;
            SqlParameter[] param = {
                    new SqlParameter("@Id", model.Id),
                    new SqlParameter("@DepartId", model.DepartId),
                    new SqlParameter("@OrganizeUserNO", model.OrganizeUserNO),
                    new SqlParameter("@Name", model.Name),
                    new SqlParameter("@Range", model.Range),
                    new SqlParameter("@Type", model.Type),
                    new SqlParameter("@ActStartTime", model.ActStartTime),
                    new SqlParameter("@ActEndTime", model.ActEndTime),
                    new SqlParameter("@Introduction", model.Introduction),
                    new SqlParameter("@EntStartTime", model.EntStartTime),
                    new SqlParameter("@EntEndTime", model.EntEndTime),
                    new SqlParameter("@Address", model.Address),
                    new SqlParameter("@Awards", model.Awards),
                    new SqlParameter("@AttachUrl", model.AttachUrl),
                    new SqlParameter("@ActivityImg", model.ActivityImg),
                    new SqlParameter("@SendExamStatus",model.SendExamStatus),
                    new SqlParameter("@CreateUID", model.CreateUID),
                    new SqlParameter("@EditUID", model.EditUID??""),
                    new SqlParameter("@ProjectStr", projectStr)
            };
            object obj = SQLHelp.ExecuteScalar("EditDepartActivity", CommandType.StoredProcedure, param);
            result = Convert.ToInt32(obj);
            return result;
        }
        #endregion
    }
}
