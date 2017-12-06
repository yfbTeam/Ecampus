using SCIDAL;
using SCModel;
using SCUtility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCDAL
{
    public partial class Book_CatologDal : BaseDal<Book_Catolog>, IBook_CatologDal
    {
        public override DataTable GetListByPage(Hashtable ht, out int RowCount, bool IsPage, string where)
        {
            RowCount = 0;
            DataTable dt = new DataTable();
            try
            {
                int StartIndex = 0;
                int EndIndex = 0;
                if (ht["IsPage"].SafeToString().Length > 0)
                {
                    IsPage = Convert.ToBoolean(ht["IsPage"]);
                }
                if (IsPage)
                {
                    StartIndex = Convert.ToInt32(ht["StartIndex"].SafeToString());
                    EndIndex = Convert.ToInt32(ht["EndIndex"].SafeToString());
                }

                StringBuilder sb = new StringBuilder();
                sb.Append("select * from Book_Catolog where 1=1 ");

                if (ht.ContainsKey("SubName") && !string.IsNullOrEmpty(ht["SubName"].SafeToString()))
                {
                    sb.Append(" and SubjectID in (select ID from Book_Subject where Name='" + ht["SubName"] + "' and VersionID=" + ht["VersionID"] + " and GradeID=" + ht["GradeID"] + ")");
                }
                if (ht.ContainsKey("SubID") && !string.IsNullOrEmpty(ht["SubID"].SafeToString()))
                {
                    sb.Append(" and SubjectID =" + ht["SubID"]);
                }
                if (ht.ContainsKey("PID") && !string.IsNullOrEmpty(ht["PID"].SafeToString()))
                {
                    sb.Append(" and PID =" + ht["PID"]);
                }
                
                dt = SQLHelp.GetListByPage("(" + sb.ToString() + ")", where, "", StartIndex, EndIndex, IsPage, null, out RowCount);
            }
            catch (Exception ex)
            {
                LogService.WriteErrorLog(ex.Message);
            }
            return dt;
        }
    }
}
