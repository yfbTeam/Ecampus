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
    public partial class Asso_ApplyDal : BaseDal<Asso_Apply>, IAsso_ApplyDal
    {
        #region 获取社团申请表的分页数据
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
                sbSql4org.Append(@"select app.*,asso.Name as AssoName from Asso_Apply app
                                  left join Asso_Info asso on asso.Id=app.AssoId ");
                sbSql4org.Append(@" where 1=1 ");
                //1待审核，2审核通过，3审核拒绝
                if (ht.ContainsKey("ExamStatus") && !string.IsNullOrEmpty(ht["ExamStatus"].ToString()))
                {
                    sbSql4org.Append(" and app.ExamStatus=@ExamStatus ");
                    pms.Add(new SqlParameter("@ExamStatus", ht["ExamStatus"].ToString()));
                }
                //1入团申请，2退团申请
                if (ht.ContainsKey("ApplyType") && !string.IsNullOrEmpty(ht["ApplyType"].ToString()))
                {
                    sbSql4org.Append(" and app.ApplyType=@ApplyType ");
                    pms.Add(new SqlParameter("@ApplyType", ht["ApplyType"].ToString()));
                }
                //申请人
                if (ht.ContainsKey("ApplyUserNo") && !string.IsNullOrEmpty(ht["ApplyUserNo"].ToString()))
                {
                    sbSql4org.Append(" and app.ApplyUserNo=@ApplyUserNo ");
                    pms.Add(new SqlParameter("@ApplyUserNo", ht["ApplyUserNo"].ToString()));
                }
                //社团长/副社团长
                if (ht.ContainsKey("OSLeaderNo") && !string.IsNullOrEmpty(ht["OSLeaderNo"].ToString()))
                {
                    sbSql4org.Append(" and (asso.LeaderNo=@OSLeaderNo or asso.SecondLeaderNo=@OSLeaderNo) ");
                    pms.Add(new SqlParameter("@OSLeaderNo", ht["OSLeaderNo"].ToString()));
                }
                //社团id
                if (ht.ContainsKey("AssoId") && !string.IsNullOrEmpty(ht["AssoId"].ToString()))
                {
                    sbSql4org.Append(" and app.AssoId=@AssoId ");
                    pms.Add(new SqlParameter("@AssoId", ht["AssoId"].ToString()));
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

        #region 添加社团申请
        public int AddAssoApply(Asso_Apply model)
        {
            int result = 0;
            SqlParameter[] param = {
                    new SqlParameter("@AssoId", model.AssoId),
                    new SqlParameter("@ApplyUserNo", model.ApplyUserNo),
                    new SqlParameter("@ApplyType", model.ApplyType),
                    new SqlParameter("@ApplyIntroduce", model.ApplyIntroduce)
            };
            object obj = SQLHelp.ExecuteScalar("AddAssoApply", CommandType.StoredProcedure, param);
            result = Convert.ToInt32(obj);
            return result;
        }
        #endregion

        #region 社团申请审核
        public int AssoApply_Audit(Asso_Apply model)
        {
            int result = 0;
            SqlParameter[] param = {
                    new SqlParameter("@Id", model.Id),
                    new SqlParameter("@ExamUserNo", model.ExamUserNo),
                    new SqlParameter("@ExamStatus", model.ExamStatus),
                    new SqlParameter("@ExamSuggest", model.ExamSuggest)
            };
            object obj = SQLHelp.ExecuteScalar("AssoApply_Audit", CommandType.StoredProcedure, param);
            result = Convert.ToInt32(obj);
            return result;
        }
        #endregion
    }
}
