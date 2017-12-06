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
    public partial class Com_NewInfoDal : BaseDal<Com_NewInfo>, ICom_NewInfoDal
    {
        #region 获取资讯表的分页数据
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
                bool isType = ht.ContainsKey("Type") && !string.IsNullOrEmpty(ht["Type"].ToString());
                bool isOnlyBase=ht["IsOnlyBase"].ToString()!="0";
                sbSql4org.Append(@"select new.* ");
                if (isOnlyBase)
                {
                    sbSql4org.Append(@",DATEDIFF(S,'1970-01-01 00:00:00', new.CreateTime) - 8 * 3600 as CreateTime_Stamp,isnull(Co.CommontCount,0) as CommontCount
               ,isnull((select top 1 CreateUID from Com_NewComment where [NewId]=new.Id order by CreateTime desc),'暂无回复') as LastComUID  
               ,DATEDIFF(S,'1970-01-01 00:00:00', (select top 1 CreateTime from Com_NewComment where [NewId]=new.Id order by CreateTime desc)) - 8 * 3600 as LastComTime ");
                }
                if (isType)
                {
                    if(ht["Type"].ToString() == "2") { sbSql4org.Append(" ,build.Name as BuildName "); }else { sbSql4org.Append(" ,rel.PicURL "); }
                    sbSql4org.Append(" ,rel.Name as RelationName ");
                    if (ht.ContainsKey("LoginUID") && !string.IsNullOrEmpty(ht["LoginUID"].ToString()))
                    {
                        if(ht["Type"].ToString() != "2")
                        {
                            sbSql4org.Append(@",case when rel.LeaderNo = @LoginUID or rel.SecondLeaderNo = @LoginUID then 1 else 0 end as IsLeader");
                        }else
                        {
                            sbSql4org.Append(@",case when rel.ManagerNo = @LoginUID then 1 else 0 end as IsLeader");
                        }
                        pms.Add(new SqlParameter("@LoginUID", ht["LoginUID"].ToString()));
                        if (ht.ContainsKey("Id") && !string.IsNullOrEmpty(ht["Id"].ToString()))
                        {
                            sbSql4org.Append(",(select count(1) from Com_GoodClick where type=0 and RelationId=new.Id and IsDelete=0 and CreateUID=@LoginUID) as IsGoodClick");
                        }
                    }
                }
                sbSql4org.Append(@" from Com_NewInfo new ");
                if (isOnlyBase)
                {
                    sbSql4org.Append(@" left join (select [NewId],count(1) as CommontCount from Com_NewComment group by [NewId])Co on new.Id=Co.[NewId] ");
                }
                if (isType)  //0社团；1部门；2宿舍
                {
                    if (ht["Type"].ToString() == "0") { sbSql4org.Append(" left join Asso_Info rel on rel.Id=new.RelationId "); }
                    else if (ht["Type"].ToString() == "1") { sbSql4org.Append(" left join Acti_DepartInfo rel on rel.Id=new.RelationId ");}
                    else { sbSql4org.Append(" left join Dorm_Room rel on rel.Id=new.RelationId left join Dorm_Building build on rel.BuildId = build.Id ");}
                }
                sbSql4org.Append(@" where 1=1 and new.IsDelete=0 ");
                if (ht.ContainsKey("Id") && !string.IsNullOrEmpty(ht["Id"].ToString()))
                {
                    sbSql4org.Append(" and new.Id=@Id ");
                    pms.Add(new SqlParameter("@Id", ht["Id"].ToString()));
                }
                if (ht.ContainsKey("Name") && !string.IsNullOrEmpty(ht["Name"].ToString()))
                {
                    sbSql4org.Append(" and new.Name like N'%' + @Name + '%' ");
                    pms.Add(new SqlParameter("@Name", ht["Name"].ToString()));
                }
                if (isType)
                {
                    sbSql4org.Append(" and new.Type=@Type ");
                    pms.Add(new SqlParameter("@Type", ht["Type"].ToString()));
                }
                if (ht.ContainsKey("RelationId") && !string.IsNullOrEmpty(ht["RelationId"].ToString()))
                {
                    sbSql4org.Append(" and new.RelationId=@RelationId ");
                    pms.Add(new SqlParameter("@RelationId", ht["RelationId"].ToString()));
                }
                if (ht.ContainsKey("NewType") && !string.IsNullOrEmpty(ht["NewType"].ToString()))
                {
                    sbSql4org.Append(" and new.NewType=@NewType ");
                    pms.Add(new SqlParameter("@NewType", ht["NewType"].ToString()));
                    if (ht["NewType"].ToString()=="1"&& ht.ContainsKey("IsRecruit") && !string.IsNullOrEmpty(ht["IsRecruit"].ToString()))
                    {
                        sbSql4org.Append(" and new.RelationId!=0 ");
                    }
                }
                if (ht.ContainsKey("IsElite") && !string.IsNullOrEmpty(ht["IsElite"].ToString()))
                {
                    sbSql4org.Append(" and new.IsElite=@IsElite ");
                    pms.Add(new SqlParameter("@IsElite", ht["IsElite"].ToString()));
                }
                if (ht.ContainsKey("CreateUID") && !string.IsNullOrEmpty(ht["CreateUID"].ToString()))
                {
                    sbSql4org.Append(" and new.CreateUID=@CreateUID ");
                    pms.Add(new SqlParameter("@CreateUID", ht["CreateUID"].ToString()));
                }
                if (ht.ContainsKey("CommontCount") && !string.IsNullOrEmpty(ht["CommontCount"].ToString()))
                {
                    sbSql4org.Append(" and isnull(Co.CommontCount,0)=@CommontCount ");
                    pms.Add(new SqlParameter("@CommontCount", ht["CommontCount"].ToString()));
                }
                string orderby = "IsTop desc,T.Id desc ";
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

        #region 编辑资讯
        public int EditComNewInfo(Com_NewInfo model)
        {
            int result = 0;
            SqlParameter[] param = {
                    new SqlParameter("@Id", model.Id),
                    new SqlParameter("@Type", model.Type),
                    new SqlParameter("@RelationId", model.RelationId),
                    new SqlParameter("@NewType",model.NewType),
                    new SqlParameter("@Name", model.Name),
                    new SqlParameter("@Content", model.Content),                  
                    new SqlParameter("@CreateUID", model.CreateUID),
                    new SqlParameter("@EditUID", model.EditUID??"")
            };
            object obj = SQLHelp.ExecuteScalar("EditComNewInfo", CommandType.StoredProcedure, param);
            result = Convert.ToInt32(obj);
            return result;
        }
        #endregion

        #region 点赞
        public string GoodClick(Com_GoodClick model)
        {
            SqlParameter[] param = {
                    new SqlParameter("@Type", model.Type),
                    new SqlParameter("@RelationId", model.RelationId),
                    new SqlParameter("@CreateUID", model.CreateUID)
            };
            object obj = SQLHelp.ExecuteScalar("GoodClick", CommandType.StoredProcedure, param);            
            return obj.ToString();
        }
        #endregion
    }
}
