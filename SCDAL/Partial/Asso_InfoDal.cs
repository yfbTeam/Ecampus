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
    public partial class Asso_InfoDal : BaseDal<Asso_Info>, IAsso_InfoDal
    {
        #region 获取社团信息表的分页数据
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
                bool isAssoId= ht.ContainsKey("Id") && !string.IsNullOrEmpty(ht["Id"].ToString());
                sbSql4org.Append(@"select distinct asso.*,(select count(1) from Asso_Member where AssoId=asso.Id) as MemCount,atype.Name as AssoTypeName ");
                if (isOnlyBase)
                {
                    sbSql4org.Append(@" ,(select count(1) from Com_NewInfo where type=0 and RelationId=asso.Id) as NewCount
                              ,(select count(1) from Asso_Apply where AssoId=asso.Id and ExamStatus=1 )as ExamApplyCount ");
                }
                if (ht.ContainsKey("LoginUID") && !string.IsNullOrEmpty(ht["LoginUID"].ToString()))
                {
                    sbSql4org.Append(@" ,(select count(1) from Asso_Member where AssoId=asso.Id and MemberNo=@LoginUID) as IsMember
                        ,case when asso.LeaderNo = @LoginUID or asso.SecondLeaderNo = @LoginUID then 1 else 0 end as IsLeader");
                    pms.Add(new SqlParameter("@LoginUID", ht["LoginUID"].ToString()));                    
                    if (ht.ContainsKey("Href") && !string.IsNullOrEmpty(ht["Href"].ToString())&& isAssoId)
                    {
                        sbSql4org.Append(@" ,(select count(1) from Com_Favorites where Href=@Href and CreateUID=@LoginUID and Type=0 and RelationId=@Id and IsDelete=0 ) as IsFavorite ");
                        pms.Add(new SqlParameter("@Href", ht["Href"].ToString()));
                    }
                }                
                sbSql4org.Append(@" from Asso_Info asso
                                    left join Asso_Type atype on atype.Id=asso.AssoType ");
                sbSql4org.Append(@" where 1=1 ");
                if (ht.ContainsKey("Name") && !string.IsNullOrEmpty(ht["Name"].ToString()))
                {
                    sbSql4org.Append(" and asso.Name like N'%' + @Name + '%' ");
                    pms.Add(new SqlParameter("@Name", ht["Name"].ToString()));
                }
                if (ht.ContainsKey("AssoType") && !string.IsNullOrEmpty(ht["AssoType"].ToString()))
                {
                    if (ht["AssoType"].ToString() == "-1")
                    {
                        if (ht.ContainsKey("UnAssoTypes") && !string.IsNullOrEmpty(ht["UnAssoTypes"].ToString()))
                        {
                            StringBuilder strFirst = new StringBuilder();
                            string[] UnAssoType = ht["UnAssoTypes"].SafeToString().Split(',');
                            for (int i = 0; i < UnAssoType.Length; i++)
                            {
                                strFirst.Append("@UnAssoType" + i + ",");
                                pms.Add(new SqlParameter("@UnAssoType" + i, UnAssoType[i]));
                            }
                            sbSql4org.Append(string.Format(" and asso.AssoType not in({0})", strFirst.ToString().TrimEnd(',')));
                        }                        
                    }
                    else
                    {
                        sbSql4org.Append(" and asso.AssoType=@AssoType ");
                        pms.Add(new SqlParameter("@AssoType", ht["AssoType"].ToString()));
                    }                    
                }
                if (ht.ContainsKey("IsDelete") && !string.IsNullOrEmpty(ht["IsDelete"].ToString()))
                {
                    sbSql4org.Append(" and asso.IsDelete=@IsDelete ");
                    pms.Add(new SqlParameter("@IsDelete", ht["IsDelete"].ToString()));
                }
                if (isAssoId)
                {
                    sbSql4org.Append(" and asso.Id=@Id ");
                    pms.Add(new SqlParameter("@Id", ht["Id"].ToString()));
                }
                if (ht.ContainsKey("MyUserNo") && !string.IsNullOrEmpty(ht["MyUserNo"].ToString()))
                {
                    sbSql4org.Append(" and (asso.Id in(select distinct AssoId from Asso_Member where MemberNo=@MyUserNo) or asso.LeaderNo=@MyUserNo or asso.SecondLeaderNo=@MyUserNo) ");
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

        #region 编辑社团信息
        public int EditAssoInfo(Asso_Info model)
        {
            int result = 0;
            SqlParameter[] param = {
                    new SqlParameter("@Id", model.Id),
                    new SqlParameter("@Name", model.Name),
                    new SqlParameter("@Slogan", model.Slogan??""),
                    new SqlParameter("@Introduce", model.Introduce),
                    new SqlParameter("@LeaderNo", model.LeaderNo),
                    new SqlParameter("@SecondLeaderNo", model.SecondLeaderNo??""),
                    new SqlParameter("@AssoType", model.AssoType),
                    new SqlParameter("@ApplyType",model.ApplyType),
                    new SqlParameter("@PicURL", model.PicURL),
                    new SqlParameter("@CreateUID", model.CreateUID),
                    new SqlParameter("@EditUID", model.EditUID??"")
            };
            object obj = SQLHelp.ExecuteScalar("EditAssoInfo", CommandType.StoredProcedure, param);
            result = Convert.ToInt32(obj);
            return result;
        }
        #endregion

        #region 获取社团报名时间        
        public DataTable GetAssoEnteredData()
        {
            StringBuilder sbSql4org;
            List<SqlParameter> pms = new List<SqlParameter>();
            sbSql4org = new StringBuilder();
            sbSql4org.Append(@"select ent.*,case when convert(varchar(10),StartTime,21)>convert(varchar(10),getdate(),21) then -1
                    when convert(varchar(10),getdate(),21) between convert(varchar(10),StartTime,21) and convert(varchar(10),EndTime,21) then 1
                    when convert(varchar(10),getdate(),21)>convert(varchar(10),EndTime,21) then -2 end as EnteredStats
                    from Asso_EnteredSet ent ");//-1 报名未开始；1报名中；-2 报名已结束
            return SQLHelp.ExecuteDataTable(sbSql4org.ToString(), CommandType.Text, pms.ToArray());
        }
        #endregion

        #region 社团报名时间设置
        public int SetAssoEntered(Asso_EnteredSet model)
        {
            int result = 0;
            SqlParameter[] param = {
                    new SqlParameter("@IsOnly", model.IsOnly),
                    new SqlParameter("@StartTime", model.StartTime),
                    new SqlParameter("@EndTime", model.EndTime),
                    new SqlParameter("@CreateUID", model.CreateUID)
            };
            object obj = SQLHelp.ExecuteScalar("SetAssoEntered", CommandType.StoredProcedure, param);
            result = Convert.ToInt32(obj);
            return result;
        }
        #endregion
    }
}
