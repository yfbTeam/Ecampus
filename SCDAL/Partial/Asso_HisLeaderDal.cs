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
    public partial class Asso_HisLeaderDal : BaseDal<Asso_HisLeader>, IAsso_HisLeaderDal
    {
        #region 获取社团历任社团长的分页数据
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
                bool isAssoId = ht.ContainsKey("AssoId") && !string.IsNullOrEmpty(ht["AssoId"].ToString());
                StringBuilder sbSql4org = new StringBuilder();
                sbSql4org.Append(@"select lea.*
,case when lea.OutgoingTime is null then (select STUFF((select ';' + CAST(OSLeaderNo AS NVARCHAR(MAX)) from Asso_HisLeader where Type=1 
       and AssoId=lea.AssoId and (OutgoingTime is null or convert(varchar(10),OfficeTime,21)>=convert(varchar(10),lea.OfficeTime,21) or convert(varchar(10),OutgoingTime,21)>=convert(varchar(10),lea.OfficeTime,21))FOR xml path('')), 1, 1, ''))
else (select STUFF((select ';' + CAST(OSLeaderNo AS NVARCHAR(MAX)) from Asso_HisLeader where Type=1 and AssoId=lea.AssoId 
       and (convert(varchar(10),OfficeTime,21) between convert(varchar(10),lea.OfficeTime,21) and convert(varchar(10),lea.OutgoingTime,21) or convert(varchar(10),OutgoingTime,21) between convert(varchar(10),lea.OfficeTime,21) and convert(varchar(10),lea.OutgoingTime,21))FOR xml path('')), 1, 1, ''))  end as SecondLeaderNo ");
                sbSql4org.Append(@" from Asso_HisLeader lea where lea.Type=0 ");
                if (isAssoId)
                {
                    sbSql4org.Append(" and lea.AssoId=@AssoId ");
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
    }
}
