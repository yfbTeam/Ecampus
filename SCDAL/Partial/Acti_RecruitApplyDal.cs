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
    public partial class Acti_RecruitApplyDal : BaseDal<Acti_RecruitApply>, IActi_RecruitApplyDal
    {
        #region 获取招新申请表的分页数据
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
                sbSql4org.Append(@"select app.*,depart.Name as DepartName from Acti_RecruitApply app
                                   left join Acti_Activity act on app.ActivityId=act.Id 
                                   left join Acti_DepartInfo depart on depart.Id=act.DepartId ");
                sbSql4org.Append(@" where 1=1 ");
                //1待审核（默认），2审核通过，3审核拒绝
                if (ht.ContainsKey("ExamStatus") && !string.IsNullOrEmpty(ht["ExamStatus"].ToString()))
                {
                    sbSql4org.Append(" and app.ExamStatus=@ExamStatus ");
                    pms.Add(new SqlParameter("@ExamStatus", ht["ExamStatus"].ToString()));
                }
                //1入部申请，2退部申请
                if (ht.ContainsKey("Type") && !string.IsNullOrEmpty(ht["Type"].ToString()))
                {
                    sbSql4org.Append(" and app.Type=@Type ");
                    pms.Add(new SqlParameter("@Type", ht["Type"].ToString()));
                }
                //申请人
                if (ht.ContainsKey("CreateUID") && !string.IsNullOrEmpty(ht["CreateUID"].ToString()))
                {
                    sbSql4org.Append(" and app.CreateUID=@CreateUID ");
                    pms.Add(new SqlParameter("@CreateUID", ht["CreateUID"].ToString()));
                }
                //部长/副部长
                if (ht.ContainsKey("OSLeaderNo") && !string.IsNullOrEmpty(ht["OSLeaderNo"].ToString()))
                {
                    sbSql4org.Append(" and (depart.LeaderNo=@OSLeaderNo or depart.SecondLeaderNo=@OSLeaderNo) ");
                    pms.Add(new SqlParameter("@OSLeaderNo", ht["OSLeaderNo"].ToString()));
                }
                //部门id
                if (ht.ContainsKey("DepartId") && !string.IsNullOrEmpty(ht["DepartId"].ToString()))
                {
                    sbSql4org.Append(" and act.DepartId=@DepartId ");
                    pms.Add(new SqlParameter("@DepartId", ht["DepartId"].ToString()));
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

        #region 添加招新申请
        public int AddActiRecruitApply(Acti_RecruitApply model)
        {
            int result = 0;
            SqlParameter[] param = {
                    new SqlParameter("@ActivityId", model.ActivityId ),
                    new SqlParameter("@Type", model.Type),
                    new SqlParameter("@Content", model.Content),
                    new SqlParameter("@CreateUID", model.CreateUID)
            };
            object obj = SQLHelp.ExecuteScalar("AddActiRecruitApply", CommandType.StoredProcedure, param);
            result = Convert.ToInt32(obj);
            return result;
        }
        #endregion

        #region 招新申请审核
        public int ActiRecruitApply_Audit(Acti_RecruitApply model)
        {
            int result = 0;
            SqlParameter[] param = {
                    new SqlParameter("@Id", model.Id),
                    new SqlParameter("@ExamUserNo", model.ExamUserNo),
                    new SqlParameter("@ExamStatus", model.ExamStatus),
                    new SqlParameter("@ExamSuggest", model.ExamSuggest)
            };
            object obj = SQLHelp.ExecuteScalar("ActiRecruitApply_Audit", CommandType.StoredProcedure, param);
            result = Convert.ToInt32(obj);
            return result;
        }
        #endregion
    }
}
