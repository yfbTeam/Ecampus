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
    public partial class Acti_DepartMemberDal : BaseDal<Acti_DepartMember>, IActi_DepartMemberDal
    {
        #region 获取部门成员表的分页数据
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
                sbSql4org.Append(@"select mem.*,depart.Name as DepartName,'成员' as Position from Acti_DepartMember mem
                                     left join Acti_DepartInfo depart on depart.Id=mem.DepartId ");
                sbSql4org.Append(@" where 1=1 ");
                if (ht.ContainsKey("DepartId") && !string.IsNullOrEmpty(ht["DepartId"].ToString()))
                {
                    sbSql4org.Append(" and mem.DepartId=@DepartId ");
                    pms.Add(new SqlParameter("@DepartId", ht["DepartId"].ToString()));
                }
                if (ht.ContainsKey("NewMemerDay") && !string.IsNullOrEmpty(ht["NewMemerDay"].ToString()))
                {
                    sbSql4org.Append(" and DateDiff(dd,mem.CreateTime,getdate())<=@NewMemerDay ");
                    pms.Add(new SqlParameter("@NewMemerDay", ht["NewMemerDay"].ToString()));
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
        #region 添加或删除部门成员
        public int AddOrDelDepartMember(int type, Acti_DepartMember model, string memberNos)
        {
            int result = 0;
            SqlParameter[] param = {
                    new SqlParameter("@Type",type),
                    new SqlParameter("@DepartId", model.DepartId),
                    new SqlParameter("@Introduction", model.Introduction??""),
                    new SqlParameter("@MemberNos", memberNos),
                    new SqlParameter("@CreateUID", model.CreateUID)
            };
            object obj = SQLHelp.ExecuteScalar("AddOrDelDepartMember", CommandType.StoredProcedure, param);
            result = Convert.ToInt32(obj);
            return result; 
        }
        #endregion
    }
}
