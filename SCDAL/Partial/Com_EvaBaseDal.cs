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
    public partial class Com_EvaBaseDal : BaseDal<Com_EvaBase>, ICom_EvaBaseDal
    {
        #region 获取考评基础项表的分页数据
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
                sbSql4org.Append(@"select base.* from Com_EvaBase base ");
                sbSql4org.Append(@" where 1=1 and base.IsDelete=0 ");
                if (ht.ContainsKey("Name") && !string.IsNullOrEmpty(ht["Name"].ToString()))
                {
                    sbSql4org.Append(" and base.Name like N'%' + @Name + '%' ");
                    pms.Add(new SqlParameter("@Name", ht["Name"].ToString()));
                }
                if (ht.ContainsKey("Cycle") && !string.IsNullOrEmpty(ht["Cycle"].ToString()))
                {
                    sbSql4org.Append(" and base.Cycle=@Cycle ");
                    pms.Add(new SqlParameter("@Cycle", ht["Cycle"].ToString()));
                }
                if (ht.ContainsKey("Target") && !string.IsNullOrEmpty(ht["Target"].ToString()))
                {
                    sbSql4org.Append(" and base.Target=@Target ");
                    pms.Add(new SqlParameter("@Target", ht["Target"].ToString()));
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

        #region 编辑考评基础项
        public int EditComEvaBase(Com_EvaBase model)
        {
            int result = 0;
            SqlParameter[] param = {
                    new SqlParameter("@Id", model.Id),
                    new SqlParameter("@Type", model.Type),
                    new SqlParameter("@Name", model.Name),
                    new SqlParameter("@Content", model.Content),
                    new SqlParameter("@Cycle", model.Cycle),
                    new SqlParameter("@Target", model.Target),
                    new SqlParameter("@Score", model.Score),
                    new SqlParameter("@CreateUID", model.CreateUID),
                    new SqlParameter("@EditUID", model.EditUID??"")
            };
            object obj = SQLHelp.ExecuteScalar("EditComEvaBase", CommandType.StoredProcedure, param);
            result = Convert.ToInt32(obj);
            return result;
        }
        #endregion
    }
}
