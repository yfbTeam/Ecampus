﻿using SCIDAL;
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
    public partial class Asso_ActivityDal : BaseDal<Asso_Activity>, IAsso_ActivityDal
    {
        #region 获取社团活动表的分页数据
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
                sbSql4org.Append(@"select act.*,asso.Name as AssoName,asso.PicURL,DATEDIFF(S,'1970-01-01 00:00:00', act.CreateTime) - 8 * 3600 as CreateTime_Stamp
                    ,(select count(1) from func_split(act.JoinMembers,',')) as MemCount
                ,case when convert(varchar(10),getdate(),21) between convert(varchar(10),act.StartTime,21) and convert(varchar(10),act.EndTime,21) then 1 
                 when convert(varchar(10),getdate(),21)>convert(varchar(10),act.EndTime,21) then 2 else 0 end as ActStatus");
                if (ht.ContainsKey("LoginUID") && !string.IsNullOrEmpty(ht["LoginUID"].ToString()))
                {
                    sbSql4org.Append(@" ,case when asso.LeaderNo = @LoginUID or asso.SecondLeaderNo = @LoginUID then 1 else 0 end as IsLeader
                                      ,(select count(1) from func_split(act.JoinMembers,',') where value=@LoginUID) as IsJoin ");
                    pms.Add(new SqlParameter("@LoginUID", ht["LoginUID"].ToString()));
                }
                sbSql4org.Append(@" from Asso_Activity act
                                   left join Asso_Info asso on asso.Id=act.AssoId where 1=1 and act.IsDelete=0 ");
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
                if (ht.ContainsKey("AssoId") && !string.IsNullOrEmpty(ht["AssoId"].ToString()))
                {
                    sbSql4org.Append(" and act.AssoId=@AssoId ");
                    pms.Add(new SqlParameter("@AssoId", ht["AssoId"].ToString()));
                }
                //活动时间状态
                if (ht.ContainsKey("ActStatus") && !string.IsNullOrEmpty(ht["ActStatus"].ToString()))
                {
                    if (ht["ActStatus"].ToString() == "ing")
                    {
                        sbSql4org.Append(" and convert(varchar(10),getdate(),21) between convert(varchar(10),act.StartTime,21) and convert(varchar(10),act.EndTime,21) ");
                    }else
                    {
                        sbSql4org.Append(" and convert(varchar(10),getdate(),21)>convert(varchar(10),act.EndTime,21) ");
                    }                    
                }
                //我的社团下的活动
                if (ht.ContainsKey("MyUserNo") && !string.IsNullOrEmpty(ht["MyUserNo"].ToString()))
                {
                    sbSql4org.Append(" and (asso.Id in(select distinct AssoId from Asso_Member where MemberNo=@MyUserNo) or (asso.LeaderNo=@MyUserNo or asso.SecondLeaderNo=@MyUserNo)) ");
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

        #region 编辑社团活动
        public int EditAssoActivity(Asso_Activity model)
        {
            int result = 0;
            SqlParameter[] param = {
                    new SqlParameter("@Id", model.Id),
                    new SqlParameter("@AssoId", model.AssoId),
                    new SqlParameter("@Name", model.Name),
                    new SqlParameter("@StartTime", model.StartTime),
                    new SqlParameter("@EndTime", model.EndTime),
                    new SqlParameter("@Address", model.Address),
                    new SqlParameter("@Content", model.Content),
                    new SqlParameter("@AttachUrl", model.AttachUrl??""),
                    new SqlParameter("@ActivityImg", model.ActivityImg),
                    new SqlParameter("@CreateUID", model.CreateUID),
                    new SqlParameter("@EditUID", model.EditUID??"")
            };
            object obj = SQLHelp.ExecuteScalar("EditAssoActivity", CommandType.StoredProcedure, param);
            result = Convert.ToInt32(obj);
            return result;
        }
        #endregion
    }
}
